using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Entity.DataTransferObject
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
