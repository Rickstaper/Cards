using Cards.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cards.Contracts
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card> GetCardAsync(Guid id);
        void CreateCard(Card card);
        void DeleteCard(Card card);
        void UpdateCard(Card card);
    }
}
