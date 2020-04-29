using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class IDBookmark
    {
        public int HighestProductId { get; set; }
        public int HighestPhysicalProductId { get; set; }
        public int HighestElectronicalProductId { get; set; }
        public int HighestBorrowId { get; set; }
    }
}
