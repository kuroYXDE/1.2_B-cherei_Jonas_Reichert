﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class PMagazineCopy : IpProduct
    {
        public int ID { get; set; }
        public bool IsBorrowed { get; set; }
        public IProdukt Belonging { get; set; }
        public PMagazineCopy()
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