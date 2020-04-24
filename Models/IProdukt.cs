using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    interface IProdukt
    {
        int ID { get; set; }
        string Author_Publisher { get; set; }
        string Title { get; set; }
        int borrowed { get; set; }
    }
}
