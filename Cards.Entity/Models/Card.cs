using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Entity.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public Card()
        {
            Id = Guid.NewGuid();
        }
    }
}
