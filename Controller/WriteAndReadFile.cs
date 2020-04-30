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
            var json = File.ReadAllText("Files/products.json");
            DataLists.ProductList = JsonConvert.DeserializeObject<List<Models.IProdukt>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }
        static public void ReadPhysicalProductsJson()
        {
            string json = File.ReadAllText("Files/physicalProducts.json");
            DataLists.PhysicalProductList = JsonConvert.DeserializeObject<List<Models.IpProduct>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }
        static public void ReadElectronicalProductsJson()
        {
            string json = File.ReadAllText("Files/electronicalProducts.json");
            DataLists.ElectronicalProductList = JsonConvert.DeserializeObject<List<Models.IeProduct>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }
        static public void ReadBorrowJson()
        {
            string json = File.ReadAllText("Files/borrows.json");
            DataLists.BorrowProductList = JsonConvert.DeserializeObject<List<Models.IBorrow>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
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
            string json = JsonConvert.SerializeObject(DataLists.ProductList.ToArray(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            System.IO.File.WriteAllText("Files/products.json", json);
        }
        static public void WritePhysicalProductsJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.PhysicalProductList.ToArray(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            System.IO.File.WriteAllText("Files/physicalProducts.json", json);
        }
        static public void WriteElectronicalProductsJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.ElectronicalProductList.ToArray(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            System.IO.File.WriteAllText("Files/electronicalProducts.json", json);
        }
        static public void WriteBorrowJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.BorrowProductList.ToArray(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
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
