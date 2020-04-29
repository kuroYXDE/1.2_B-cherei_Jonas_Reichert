using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    interface IeProduct
    {
        int ID { get; set; }
        bool IsBorrowed { get; set; }
        string Link { get; set; }
        IProdukt Belonging { get; set; }
        void AddId();
        void ChangeBorrowed();
        void AddBelonging();
    }
}
