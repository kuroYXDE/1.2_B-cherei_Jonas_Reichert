using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class Borrow : IBorrow
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public DateTime StartBorrowDate { get; set; }
        public DateTime EndBorrowDate { get; set; }
        public object CopyBorrowed { get; set; }
        public void AddId()
        {
            ID = ++Controller.DataLists.IC.HighestBorrowId;
        }
        public void SetCustomer()
        {
            Customer = Program.StringInputFunction("Kundenname: ");
        }
        public void SetBorrowDate(int days)
        {
            StartBorrowDate = DateTime.Now;
            EndBorrowDate = StartBorrowDate.AddDays(days);
        }
        public void AddBelonging(int type)
        {
            var exist = false;
            var productId = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Produktes an: ");
            while (!exist)
            {
                if (type == 1)
                {
                    foreach (var Obj in Controller.DataLists.PhysicalProductList)
                        if (Obj.ID == productId)
                        {
                            CopyBorrowed = Obj;
                            exist = true;
                        }
                }
                if (type == 2)
                {
                    foreach (var Obj in Controller.DataLists.ElectronicalProductList)
                        if (Obj.ID == productId)
                        {
                            CopyBorrowed = Obj;
                            exist = true;
                        }
                }
                if (!exist)
                    Console.WriteLine("Buch existiert nicht!");

            }
        }
    }
}
