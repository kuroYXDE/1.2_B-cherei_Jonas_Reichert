using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace _1._2_Bücherei_Jonas_Reichert.Controller
{
    class ProgramLogic
    {
        #region newProgram
        public void DisplayProducts()
        {
            foreach (var pObj in DataLists.ProductList)
                Console.WriteLine(
                    "{0}|{1}|{2}",
                    pObj.ID, pObj.Author_Publisher, pObj.Title);
        }
        public void DisplayPhysicalProducts()
        {
            foreach (var pObj in DataLists.PhysicalProductList)
                Console.WriteLine(
                    "{0}|{1}|{2}|{3}|{4}",
                    pObj.ID, pObj.IsBorrowed, pObj.Belonging.ID, pObj.Belonging.Author_Publisher, pObj.Belonging.Title);
        }
        public void DisplayElectronicalProducts()
        {
            foreach (var pObj in DataLists.ElectronicalProductList)
                Console.WriteLine(
                    "{0}|{1}|{2}|{3}|{4}",
                    pObj.ID, pObj.Belonging.ID, pObj.Belonging.Author_Publisher, pObj.Belonging.Title);
        }
        public void DisplayBorrowedProducts()
        {
            foreach (var pObj in DataLists.BorrowProductList)
            {
                if (pObj.CopyBorrowed.GetType() == typeof(Models.IpProduct))
                {
                    var pProduct = (Models.IpProduct)pObj.CopyBorrowed;
                    Console.WriteLine(
                    "{0}|{1}|{2}|{3}|{4}",
                    pObj.ID, pObj.Customer, pObj.StartBorrowDate, pObj.EndBorrowDate, pProduct.ID, pProduct.Belonging.ID, pProduct.Belonging.Author_Publisher, pProduct.Belonging.Title);
                }
                if (pObj.CopyBorrowed.GetType() == typeof(Models.IeProduct))
                {
                    var eProduct = (Models.IpProduct)pObj.CopyBorrowed;
                    Console.WriteLine(
                    "{0}|{1}|{2}|{3}|{4}",
                    pObj.ID, pObj.Customer, pObj.StartBorrowDate, pObj.EndBorrowDate, eProduct.ID, eProduct.Belonging.ID, eProduct.Belonging.Author_Publisher, eProduct.Belonging.Title);
                }
            }
        }
        public void AddProduct()
        {
            int productType = Program.IntInputFunction("(1): Buch, (2): Magazin");
            if (productType == 1)
            {
                Models.Buch b = new Models.Buch();
                b.AddId(); b.AddAuthor(); b.AddTitle(); b.AddPages(); b.AddCountry(); b.AddLanguage(); b.AddLink(); b.AddImageLink(); b.AddYear();
                DataLists.ProductList.Add(b);
            }
            else if (productType == 2)
            {
                Models.Magazin m = new Models.Magazin();
                m.AddId();  m.AddAuthor(); m.AddTitle(); m.AddGroup(); m.AddTopic();
                DataLists.ProductList.Add(m);
            }
            else
                Console.WriteLine("Ungültige eingabe!");
            WriteAndReadFile.WriteProductJson();
        }
        public void AddPhysicalProduct()
        {
            int productType = Program.IntInputFunction("(1): Buch, (2): Magazin");
            if (productType == 1)
            {
                Models.PBookCopy b = new Models.PBookCopy();
                b.AddId(); b.IsBorrowed = false; b.AddBelonging();
                DataLists.PhysicalProductList.Add(b);
            }
            else if (productType == 2)
            {
                Models.PMagazineCopy m = new Models.PMagazineCopy();
                m.AddId(); m.IsBorrowed = false; m.AddBelonging();
                DataLists.PhysicalProductList.Add(m);
            }
            else
                Console.WriteLine("Ungültige eingabe!");
            WriteAndReadFile.WritePhysicalProductsJson();
        }
        public void AddBorrowProduct()
        {
            var productType = Program.IntInputFunction("(1): Physisch, (2): Elektronisch");
            var pId = Program.IntInputFunction("Geben Sie die ID des Prduktes an: ");
            var exist = false;
            if (productType == 1)
            {
                foreach (var obj in DataLists.PhysicalProductList)
                    if (obj.ID == pId)
                    {
                        exist = true;
                        obj.IsBorrowed = true;
                        Models.Borrow newObj = new Models.Borrow();
                        newObj.AddId();  newObj.SetCustomer(); newObj.CopyBorrowed = obj;
                        if (obj.GetType() == typeof(Models.PBookCopy))
                            newObj.SetBorrowDate(30);
                        if (obj.GetType() == typeof(Models.PMagazineCopy))
                            newObj.SetBorrowDate(2);
                        DataLists.BorrowProductList.Add(newObj);
                        break;
                    }
            }
            if (productType == 2)
            {
                var type = Program.IntInputFunction("(1): Buch, (2): Magazin");
                if (type == 1)
                {
                    Models.EBookCopy copyOpj = new Models.EBookCopy();
                    copyOpj.AddId();
                    copyOpj.CreateLink();
                    copyOpj.AddBelonging();
                    DataLists.ElectronicalProductList.Add(copyOpj);
                    Models.Borrow newObj = new Models.Borrow();
                    newObj.AddId(); newObj.SetCustomer(); newObj.CopyBorrowed = copyOpj;
                    newObj.SetBorrowDate(30);
                    DataLists.BorrowProductList.Add(newObj);
                }
                else if (type == 2)
                {
                    Models.EMagazineCopy copyOpj = new Models.EMagazineCopy();
                    copyOpj.AddId();
                    copyOpj.CreateLink();
                    copyOpj.AddBelonging();
                    DataLists.ElectronicalProductList.Add(copyOpj);
                    Models.Borrow newObj = new Models.Borrow();
                    newObj.AddId(); newObj.SetCustomer(); newObj.CopyBorrowed = copyOpj;
                    newObj.SetBorrowDate(2);
                    DataLists.BorrowProductList.Add(newObj);
                }
                else
                    Console.WriteLine("Kein gültiger Wert!");
            }
            else
                Console.WriteLine("Eingabe ungültig!");
            if (!exist)
                Console.WriteLine("Produkt existiert nicht!");
        }
        public void RemoveProduct()
        {
            var exist = false;
            var isBorrowed = false;
            var productId = Program.IntInputFunction("ID des zu entfernenden Produktes: ");
            foreach (var obj in DataLists.ProductList)
            {
                if (obj.ID == productId)
                {
                    exist = true;
                    foreach (var copyProduct in DataLists.PhysicalProductList)
                    {
                        if (copyProduct.Belonging == obj)
                        {
                            if (copyProduct.IsBorrowed == true)
                            {
                                isBorrowed = true;
                                Console.WriteLine("Ist ausgeliehen");
                                break;
                            }
                        }
                    }
                    if (!isBorrowed)
                    {
                        foreach (var copyProduct in DataLists.ElectronicalProductList)
                        {
                            if (copyProduct.Belonging == obj)
                            {
                                if (copyProduct.IsBorrowed == true)
                                {
                                    isBorrowed = true;
                                    Console.WriteLine("Ist ausgeliehen");
                                    break;
                                }
                            }
                        }
                    }
                    if (!isBorrowed)
                    {
                        DataLists.ProductList.Remove(obj);
                        break;
                    }
                }
            }
            if (!exist)
                Console.WriteLine("Produkt mit dieser ID existiert nicht!");
        }
        public void RemovePhysicalProduct()
        {
            var exist = false;
            var productId = Program.IntInputFunction("ID des zu entfernenden Produktes: ");
            foreach (var obj in DataLists.PhysicalProductList)
            {
                if (obj.ID == productId)
                {
                    exist = true;
                    if (obj.IsBorrowed == true)
                    {
                        Console.WriteLine("Is ausgeliehen");
                        break;
                    }
                    DataLists.PhysicalProductList.Remove(obj);
                    break;
                }
            }
            if (!exist)
                Console.WriteLine("Produkt mit dieser ID existiert nicht!");
        }
        public void RemoveElectronicalProduct()
        {
            var exist = false;
            var productId = Program.IntInputFunction("ID des zu entfernenden Produktes: ");
            foreach (var obj in DataLists.ElectronicalProductList)
            {
                if (obj.ID == productId)
                {
                    exist = true;
                    DataLists.ElectronicalProductList.Remove(obj);
                }
            }
            if (!exist)
                Console.WriteLine("Produkt mit dieser ID existiert nicht!");
        }
        public void RemoveBorrowProduct()
        {
            var exist = false;
            var productId = Program.IntInputFunction("ID des zu entfernenden Produktes: ");
            foreach (var obj in DataLists.BorrowProductList)
            {
                if (obj.ID == productId)
                {
                    exist = true;
                    if (obj.CopyBorrowed.GetType() == typeof(Models.IpProduct))
                    {
                        Models.IpProduct copyObj = (Models.IpProduct)obj.CopyBorrowed;
                        copyObj.IsBorrowed = false;
                    }
                    if (obj.CopyBorrowed.GetType() == typeof(Models.IeProduct))
                    {
                        Models.IeProduct copyObj = (Models.IeProduct)obj.CopyBorrowed;
                        DataLists.ElectronicalProductList.Remove(copyObj);
                    }
                    DataLists.BorrowProductList.Remove(obj);
                }
            }
            if (!exist)
                Console.WriteLine("Produkt mit dieser ID existiert nicht!");
        }

        #endregion

        #region Start
        public void ProofExistingFile()
        {
            if (!File.Exists("Files/IC.json"))
            {
                DataLists.IC = new Models.IDBookmark()
                {
                    HighestProductId = 0,
                    HighestPhysicalProductId = 0,
                    HighestBorrowId = 0
                };
                WriteAndReadFile.WriteICJson();
                WriteAndReadFile.ReadProductsJson();
                //AddBookID();
                CreateFirstExemplaries();
                CreateJsons();
                WriteAndReadFile.WriteICJson();
            }
            else
            {
                WriteAndReadFile.ReadICJson();
                WriteAndReadFile.ReadProductsJson();
                WriteAndReadFile.ReadPhysicalProductsJson();
                WriteAndReadFile.ReadElectronicalProductsJson();
                WriteAndReadFile.ReadBorrowJson();
                //ProofBorrowLists();
            }
        }
        /*public void AddBookID()
        {
            foreach (var bookObj in DataLists.Books)
            {
                //DataLists.IC.HighestBookId++;
                //bookObj.ID = DataLists.IC.HighestBookId;
            }
            //WriteAndReadFile.WriteBookJson();
        }*/
        public void CreateFirstExemplaries()
        {
            DataLists.PhysicalProductList = new List<Models.IpProduct>();
            foreach (var pObj in DataLists.ProductList)
            {
                Models.Buch b = new Models.Buch();
                if (b.GetType() == typeof(Models.Buch))
                {
                    DataLists.PhysicalProductList.Add(new Models.PBookCopy() { Belonging = pObj });
                    DataLists.PhysicalProductList.Add(new Models.PBookCopy() { Belonging = pObj });
                }
                if (b.GetType() == typeof(Models.Magazin))
                {
                    DataLists.PhysicalProductList.Add(new Models.PMagazineCopy() { Belonging = pObj });
                    DataLists.PhysicalProductList.Add(new Models.PMagazineCopy() { Belonging = pObj });
                }
            }
            WriteAndReadFile.WritePhysicalProductsJson();

            /*DataLists.BookExemplaries = new List<Models.PBookCopy>();
            foreach (var bookObj in DataLists.Books)
            {
                //DataLists.IC.HighestBookExemplaryID++;
                Models.PBookCopy e1 = new Models.PBookCopy()
                {
                    //ID = DataLists.IC.HighestBookExemplaryID,
                    IsBorrowed = false,
                    //BookBelonging = bookObj,
                };
                DataLists.BookExemplaries.Add(e1);
                //DataLists.IC.HighestBookExemplaryID++;
                DataLists.BookExemplaries.Add(
                    new Models.PBookCopy
                    {
                        //ID = DataLists.IC.HighestBookExemplaryID,
                        IsBorrowed = false,
                        //BookBelonging = bookObj,
                    });
            }
            WriteAndReadFile.WriteBookExemplaryJson();
            
            DataLists.MagazineExemplaries = new List<Models.PMagazineCopy>();
            foreach (var magazineObj in DataLists.Magazines)
            {
                //DataLists.IC.HighestMagazineExemplaryID++;
                Models.PMagazineCopy e1 = new Models.PMagazineCopy()
                {
                    //ID = DataLists.IC.HighestMagazineExemplaryID,
                    IsBorrowed = false,
                    //MagazineBelonging = magazineObj,
                };
                DataLists.MagazineExemplaries.Add(e1);
                //DataLists.IC.HighestMagazineExemplaryID++;
                DataLists.MagazineExemplaries.Add(
                    new Models.PMagazineCopy
                    {
                        //ID = DataLists.IC.HighestMagazineExemplaryID,
                        IsBorrowed = false,
                        //MagazineBelonging = magazineObj,
                    });
            }
            WriteAndReadFile.WriteMagazineExemplaryJson();*/
        }
        public void CreateJsons()
        {
            DataLists.BorrowProductList = new List<Models.IBorrow>();
            WriteAndReadFile.WriteBorrowJson();
            DataLists.ElectronicalProductList = new List<Models.IeProduct>();
            WriteAndReadFile.WriteElectronicalProductsJson();
        }
        
        /*private void ProofBorrowLists()
        {
            foreach (var obj in DataLists.BooksBorrowedList)
            {
                if (obj.IsElectronic == true)
                {
                    JObject o = JObject.Parse(obj.ExemplarBorrowed.ToString());
                    Models.Buch b = o.ToObject<Models.Buch>();
                    foreach (var dataObj in DataLists.Books)
                    {
                        if (b.ID == dataObj.ID)
                        {
                            obj.ExemplarBorrowed = dataObj;
                            break;
                        }
                    }
                }
                if (obj.IsElectronic == false)
                {
                    JObject o = JObject.Parse(obj.ExemplarBorrowed.ToString());
                    Models.PBookCopy b = o.ToObject<Models.PBookCopy>();
                    foreach (var dataObj in DataLists.BookExemplaries)
                    {
                        if (b.ID == dataObj.ID)
                        {
                            obj.ExemplarBorrowed = dataObj;
                            break;
                        }
                    }
                }
            }
            foreach (var obj in DataLists.MagazineBorrowedList)
            {
                if (obj.IsElectronic == true)
                {
                    JObject o = JObject.Parse(obj.ExemplarBorrowed.ToString());
                    Models.Magazin m = o.ToObject<Models.Magazin>();
                    foreach (var dataObj in DataLists.Magazines)
                    {
                        if (m.ID == dataObj.ID)
                        {
                            obj.ExemplarBorrowed = dataObj;
                            break;
                        }
                    }
                }
                if (obj.IsElectronic == false)
                {
                    JObject o = JObject.Parse(obj.ExemplarBorrowed.ToString());
                    Models.PMagazineCopy m = o.ToObject<Models.PMagazineCopy>();
                    foreach (var dataObj in DataLists.MagazineExemplaries)
                    {
                        if (m.ID == dataObj.ID)
                        {
                            obj.ExemplarBorrowed = dataObj;
                            break;
                        }
                    }
                }
            }
        }*/
        #endregion
    }
}
// Bug: Magazin kann nicht hinzugefügt werden -> ID wird nicht gefunden
// Noch nicht programmiert dass produkte die ausgeborgt sind nicht entfernt werden können