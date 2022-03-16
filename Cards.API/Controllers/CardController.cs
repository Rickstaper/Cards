using AutoMapper;
using Cards.Contracts;
using Cards.Entity.DataTransferObject;
using Cards.Entity.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cards.API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<CardController> _logger;
        private readonly IMapper _mapper;

        public CardController(IRepositoryManager repositoryManager, ILogger<CardController> logger, 
            IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var cards = await _repository.CardRepository.GetAllCardsAsync();

            if(cards is null)
            {
                _logger.LogError("Json file with cards is empty.");
                return NotFound();
            }

            var cardsDto = _mapper.Map<IEnumerable<CardDto>>(cards);

            return Ok(cardsDto);
        }

        [HttpGet("{id}", Name = "CardById")]
        public async Task<IActionResult> GetCard(Guid id)
        {
            var card = await _repository.CardRepository.GetCardAsync(id);

            if(card is null)
            {
                _logger.LogInformation($"Card with id: {id} doesn't exist in json file.");
                return NotFound();
            }

            var cardDto = _mapper.Map<CardDto>(card);

            return Ok(cardDto);
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody]CardForCreationDto cardFromBody)
        {
            if(cardFromBody == null)
            {
                _logger.LogError("CardForCreationDto object sent from client is null.");

                return BadRequest("CardForCreationDto objext is null.");
            }

            var cardEntity = _mapper.Map<Card>(cardFromBody);

            _repository.CardRepository.CreateCard(cardEntity);

            var cardToReturn = _mapper.Map<CardDto>(cardEntity);

            return CreatedAtRoute("CardById", new { id = cardToReturn.Id }, cardToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var cardFromFile = await _repository.CardRepository.GetCardAsync(id);

            if(cardFromFile is null)
            {
                _logger.LogInformation($"Card with id: {id} doesn't exist in json file.");
                return NotFound();
            }

            _repository.CardRepository.DeleteCard(cardFromFile);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(Guid id, [FromBody]CardForUpdateDto cardFromBody)
        {
            if (cardFromBody == null)
            {
                _logger.LogError("CardForCreationDto object sent from client is null.");

                return BadRequest("CardForCreationDto objext is null.");
            }

            var cardFromFile = await _repository.CardRepository.GetCardAsync(id);

            if (cardFromFile is null)
            {
                _logger.LogInformation($"Card with id: {id} doesn't exist in json file.");
                return NotFound();
            }

            _mapper.Map(cardFromBody, cardFromFile);

            _repository.CardRepository.UpdateCard(cardFromFile);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateCard(Guid id, [FromBody]JsonPatchDocument<CardForUpdateDto> pathDoc)
        {
            if(pathDoc is null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("pathDoc object is null.");
            }

            var cardFromFile = await _repository.CardRepository.GetCardAsync(id);

            if (cardFromFile is null)
            {
                _logger.LogInformation($"Card with id: {id} doesn't exist in json file.");
                return NotFound();
            }

            var cardToPatch = _mapper.Map<CardForUpdateDto>(cardFromFile);

            pathDoc.ApplyTo(cardToPatch);

            _mapper.Map(cardToPatch, cardFromFile);

            _repository.CardRepository.UpdateCard(cardFromFile);

            return NoContent();
        }
    }
}
