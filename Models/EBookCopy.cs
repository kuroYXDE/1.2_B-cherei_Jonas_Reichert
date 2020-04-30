using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class EBookCopy : IeProduct
    {
        public int ID { get; set; }
        public string Link { get; set; }
        public IProdukt Belonging { get; set; }

        public void AddId()
        {
            ID = ++Controller.DataLists.IC.HighestElectronicalProductId;
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
        public void CreateLink()
        {
            Link = "Link";
        }
    }
}
