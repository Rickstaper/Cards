using AutoMapper;
using Cards.Contracts;
using Cards.Entity.DataTransferObject;
using Cards.Entity.Models;
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
        public IActionResult GetCards()
        {
            var cards = _repository.CardRepository.GetAllCardsAsync();

            if(cards is null)
            {
                _logger.LogError("Json file with cards is empty.");
                return NotFound();
            }

            var cardsDto = _mapper.Map<IEnumerable<CardDto>>(cards);

            return Ok(cardsDto);
        }

        [HttpGet("{id}", Name = "CardById")]
        public IActionResult GetCard(Guid id)
        {
            var card = _repository.CardRepository.GetCardAsync(id);

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
        public IActionResult DeleteCard(Guid id)
        {
            var card = _repository.CardRepository.GetCardAsync(id);

            if(card is null)
            {
                _logger.LogInformation($"Card with id: {id} doesn't exist in json file.");
                return NotFound();
            }

            _repository.CardRepository.DeleteCard(card);

            return NoContent();
        }
    }
}
