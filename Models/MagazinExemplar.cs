using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class MagazinExemplar
    {
        public int ID { get; set; }
        public bool IsBorrowed { get; set; }
        public Magazin MagazineBelonging { get; set; }
    }
}
