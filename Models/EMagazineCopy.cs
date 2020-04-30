using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class EMagazineCopy : IeProduct
    {
        public int ID { get; set; }
        public string Link { get; set; }
        public IProdukt Belonging { get; set; }

        public void AddId()
        {
            ID = ++Controller.DataLists.IC.HighestElectronicalProductId;
        }
        public void CreateLink()
        {
            Link = "Link";
        }
        public void AddBelonging()
        {
            var exist = false;
            while (!exist)
            {
                var magazineId = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Magazines an: ");
                foreach (var Obj in Controller.DataLists.ProductList)
                    if (Obj.ID == magazineId)
                    {
                        Belonging = Obj;
                        exist = true;
                    }
                if (!exist)
                    Console.WriteLine("Magazin existiert nicht!");
            }
        }
    }
}
