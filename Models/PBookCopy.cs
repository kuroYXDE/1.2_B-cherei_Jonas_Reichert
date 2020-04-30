using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class PBookCopy : IpProduct
    {
        public int ID { get; set; }
        public bool IsBorrowed { get; set; }
        public IProdukt Belonging { get; set; }
        public void AddId()
        {
            ID = ++Controller.DataLists.IC.HighestPhysicalProductId;
        }
        public void ChangeBorrowed()
        {
            IsBorrowed = !IsBorrowed;
        }
        public void AddBelonging()
        {
            var exist = false;
            while (!exist)
            {
                var bookId = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Buches an: ");
                foreach (var Obj in Controller.DataLists.ProductList)
                    if (Obj.ID == bookId)
                    {
                        Belonging = Obj;
                        exist = true;
                    }
                if (!exist)
                    Console.WriteLine("Buch existiert nicht!");
            }
        }
    }
}
