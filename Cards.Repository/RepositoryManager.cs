using Cards.Contracts;
using Cards.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Repository
{
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
