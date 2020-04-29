using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace _1._2_Bücherei_Jonas_Reichert.Controller
{
    static class WriteAndReadFile
    {
        #region Read
        static public void ReadICJson()
        {
            using (StreamReader r = new StreamReader("Files/IC.json"))
            {
                string json = r.ReadToEnd();
                DataLists.IC = JsonConvert.DeserializeObject<Models.IDBookmark>(json);
            }
        }
        static public void ReadProductsJson()
        {
            using (StreamReader r = new StreamReader("Files/products.json"))
            {
                string json = r.ReadToEnd();
                DataLists.ProductList = JsonConvert.DeserializeObject<List<Models.IProdukt>>(json);
            }
        }
        static public void ReadPhysicalProductsJson()
        {
            using (StreamReader r = new StreamReader("Files/physicalProducts.json"))
            {
                string json = r.ReadToEnd();
                DataLists.PhysicalProductList = JsonConvert.DeserializeObject<List<Models.IpProduct>>(json);
            }
        }
        static public void ReadElectronicalProductsJson()
        {
            using (StreamReader r = new StreamReader("Files/electronicalProducts.json"))
            {
                string json = r.ReadToEnd();
                DataLists.ElectronicalProductList = JsonConvert.DeserializeObject<List<Models.IeProduct>>(json);
            }
        }
        static public void ReadBorrowJson()
        {
            using (StreamReader r = new StreamReader("Files/borrows.json"))
            {
                string json = r.ReadToEnd();
                DataLists.BorrowProductList = JsonConvert.DeserializeObject<List<Models.IBorrow>>(json);
            }
        }
        #endregion

        #region Write
        static public void WriteICJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.IC);
            System.IO.File.WriteAllText("Files/IC.json", json);
        }
        static public void WriteProductJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.ProductList.ToArray());
            System.IO.File.WriteAllText("Files/products.json", json);
        }
        static public void WritePhysicalProductsJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.PhysicalProductList.ToArray());
            System.IO.File.WriteAllText("Files/physicalProducts.json", json);
        }
        static public void WriteElectronicalProductsJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.ElectronicalProductList.ToArray());
            System.IO.File.WriteAllText("Files/electronicalProducts.json", json);
        }
        static public void WriteBorrowJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.BorrowProductList.ToArray());
            System.IO.File.WriteAllText("Files/borrows.json", json);
        }

        #endregion

        #region Hopefully Never Use Again
        static public void ReadBooksJson()
        {
            using (StreamReader r = new StreamReader("C:/Users/seongbae/Documents/BBW/Programmierung/Aufgabe(2)/Buecherei/books.json"))
            {
                string json = r.ReadToEnd();
                DataLists.Books = JsonConvert.DeserializeObject<List<Models.Buch>>(json);
            }
        }

        static public void ReadMagazineJson()
        {
            using (StreamReader r = new StreamReader("C:/Users/seongbae/Documents/BBW/Programmierung/Aufgabe(2)/Buecherei/magazine.json"))
            {
                string json = r.ReadToEnd();
                DataLists.Magazines = JsonConvert.DeserializeObject<List<Models.Magazin>>(json);
            }
        }
        #endregion
    }
}
