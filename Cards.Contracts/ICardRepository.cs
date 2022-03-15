using Cards.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Contracts
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAllCardsAsync();
        Card GetCardAsync(Guid id);
        void CreateCard(Card card);
        void DeleteCard(Card card);
        void UpdateCard(Card card);
    }
}
