using Cards.Contracts;
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

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            string cardContents = DataInitializer.GetContents(DataInitializer.CardsDataPath);

            var cards = FindAll(cardContents);

            if(cards is null)
            {
                return null;
            }

            return await cards
                        .OrderBy(card => card.Name)
                        .ToListAsync();
        }

        public async Task<Card> GetCardAsync(Guid id)
        {
            string cardContents = DataInitializer.GetContents(DataInitializer.CardsDataPath);

            var card = FindByCondition(card => card.Id.Equals(id), cardContents);

            if (card is null)
            {
                return null;
            }

            return await card.SingleOrDefaultAsync();
        }
    }
}
