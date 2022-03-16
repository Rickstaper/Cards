using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Client.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
