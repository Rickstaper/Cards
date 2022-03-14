using Cards.Contracts;
using Cards.Entity;

namespace Cards.Repository
{
    /// <summary>
    /// Class for getting access to repositories.
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        private ICardRepository _cardRepository;
        private Serializer _serializer;



        public ICardRepository CardRepository
        {
            get
            {
                if(_cardRepository == null)
                {
                    _cardRepository = new CardRepository(_serializer);
                }

                return _cardRepository;
            }
        }
    }
}
