﻿using Cards.Contracts;
using Cards.Entity;
using Cards.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.Repository
{
    /// <summary>
    /// Repository class for card entity.
    /// </summary>
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(Serializer serializer)
            : base(serializer)
        {

        }

        public void CreateCard(Card card)
        {
            var cardsFromFile = FindAll(DataInitializer.CardsDataPath);
            IList<Card> cardsToFile;

            if (cardsFromFile is null)
            {
                cardsToFile = new List<Card>();
            }
            else
            {
                cardsToFile = cardsFromFile.ToList();
            }

            cardsToFile.Add(card);

            Write(DataInitializer.CardsDataPath, cardsToFile);
        }

        public IEnumerable<Card> GetAllCardsAsync()
        {
            var cards = FindAll(DataInitializer.CardsDataPath);

            if(cards is null)
            {
                return null;
            }

            return cards.OrderBy(card => card.Name).ToList();
        }

        public Card GetCardAsync(Guid id)
        {
            var card = FindByCondition(card => card.Id.Equals(id), DataInitializer.CardsDataPath);

            if (card is null)
            {
                return null;
            }

            return card.SingleOrDefault();
        }
    }
}
