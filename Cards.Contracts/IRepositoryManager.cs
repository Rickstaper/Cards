using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Contracts
{
    public interface IRepositoryManager
    {
        ICardRepository CardRepository { get; }
    }
}
