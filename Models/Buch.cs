using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class Buch : IProdukt
    {
        public int ID { get; set; }
        public string Author_Publisher { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string ImageLink { get; set; }
        public string Link { get; set; }
        public int Year { get; set; }
        public int borrowed { get; set; }
    }
}
