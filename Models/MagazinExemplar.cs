using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class MagazinExemplar : IpProduct
    {
        public int ID { get; set; }
        public bool IsBorrowed { get; set; }
        public IProdukt Belonging { get; set; }
        public MagazinExemplar()
        {
            AddId();
            IsBorrowed = false;
        }

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
                var magazineId = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Magazines an: ");
                foreach (var mObj in Controller.DataLists.Magazines)
                    if (mObj.ID == magazineId)
                    {
                        Belonging = mObj;
                        exist = true;
                    }
                if (!exist)
                    Console.WriteLine("Magazin existiert nicht!");
            }
        }
    }
}
