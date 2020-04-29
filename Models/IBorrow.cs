using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    interface IBorrow
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public DateTime StartBorrowDate { get; set; }
        public DateTime EndBorrowDate { get; set; }
        public object CopyBorrowed { get; set; }
        void AddId();
        void SetBorrowDate(int pType);
        void AddBelonging(int type);
    }
}
