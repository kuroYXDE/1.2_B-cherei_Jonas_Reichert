using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class Magazin : IProdukt
    {
        public int ID { get; set; }
        public string Author_Publisher { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }
        public string Topic { get; set; }
    }
}
