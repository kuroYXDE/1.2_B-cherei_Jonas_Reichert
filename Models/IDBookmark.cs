using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class IDBookmark
    {
        public int HighestBookID { get; set; }
        public int HighestMagazineID { get; set; }
        public int HighestBookExemplaryID { get; set; }
        public int HighestMagazineExemplaryID { get; set; }
        public int HighestBorrowID { get; set; }
    }
}
