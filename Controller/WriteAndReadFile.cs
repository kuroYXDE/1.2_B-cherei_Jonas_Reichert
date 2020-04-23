using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        static public void ReadBookJson()
        {
            using (StreamReader r = new StreamReader("Files/books.json"))
            {
                string json = r.ReadToEnd();
                DataLists.Books = JsonConvert.DeserializeObject<List<Models.Buch>>(json);
            }
        }
        static public void ReadMagazineJson()
        {
            using (StreamReader r = new StreamReader("Files/magazine.json"))
            {
                string json = r.ReadToEnd();
                DataLists.Magazines = JsonConvert.DeserializeObject<List<Models.Magazin>>(json);
            }
        }
        static public void ReadBookExemplaryJson()
        {
            using (StreamReader r = new StreamReader("Files/bookExemplaries.json"))
            {
                string json = r.ReadToEnd();
                DataLists.BookExemplaries = JsonConvert.DeserializeObject<List<Models.BuchExemplar>>(json);
            }
        }
        static public void ReadMagazineExemplaryJson()
        {
            using (StreamReader r = new StreamReader("Files/magazineExemplaries.json"))
            {
                string json = r.ReadToEnd();
                DataLists.MagazineExemplaries = JsonConvert.DeserializeObject<List<Models.MagazinExemplar>>(json);
            }
        }
        static public void ReadBookBorrowJson()
        {
            using (StreamReader r = new StreamReader("Files/bookBorrows.json"))
            {
                string json = r.ReadToEnd();
                DataLists.BooksBorrowedList = JsonConvert.DeserializeObject<List<Models.Ausleihe>>(json);
            }
        }
        static public void ReadMagazineBorrowJson()
        {
            using (StreamReader r = new StreamReader("Files/magazineBorrows.json"))
            {
                string json = r.ReadToEnd();
                DataLists.MagazineBorrowedList = JsonConvert.DeserializeObject<List<Models.Ausleihe>>(json);
            }
        }
        #endregion

        #region Write
        static public void WriteICJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.IC);
            System.IO.File.WriteAllText("Files/IC.json", json);
        }
        static public void WriteBookJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.Books.ToArray());
            System.IO.File.WriteAllText("Files/books.json", json);
        }
        static public void WriteMagazineJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.Magazines.ToArray());
            System.IO.File.WriteAllText("Files/magazin.json", json);
        }
        static public void WriteBookExemplaryJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.BookExemplaries.ToArray());
            System.IO.File.WriteAllText("Files/bookExemplaries.json", json);
        }
        static public void WriteMagazineExemplaryJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.MagazineExemplaries.ToArray());
            System.IO.File.WriteAllText("Files/magazineExemplaries.json", json);
        }
        static public void WriteBookBorrowJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.BooksBorrowedList.ToArray());
            System.IO.File.WriteAllText("Files/bookBorrows.json", json);
        }
        static public void WriteMagazineBorrowJson()
        {
            string json = JsonConvert.SerializeObject(DataLists.MagazineBorrowedList.ToArray());
            System.IO.File.WriteAllText("Files/magazineBorrows.json", json);
        }
        #endregion
    }
}
