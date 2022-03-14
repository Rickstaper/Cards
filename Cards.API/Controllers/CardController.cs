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

        [HttpGet("{id}")]
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
    }
}
