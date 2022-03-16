using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Cards.Client.Models
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
    }
}
