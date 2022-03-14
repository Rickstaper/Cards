﻿using Cards.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Contracts
{
    public interface ICardRepository
    {
        public Task<IEnumerable<Card>> GetAllCardsAsync();
    }
}
