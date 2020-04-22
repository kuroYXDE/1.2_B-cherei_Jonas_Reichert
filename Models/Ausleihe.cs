using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class Ausleihe
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public DateTime StartBorrowDate { get; set; }
        public DateTime EndBorrowDate { get; set; }
        public object ExemplarBorrowed { get; set; }
        public bool IsElectronic { get; set; }
    }
}
