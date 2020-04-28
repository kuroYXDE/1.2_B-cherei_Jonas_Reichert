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
        void AddProduct()
        {
            int productType = Program.IntInputFunction("(1): Buch, (2): Magazin");
            if (productType == 1)
            {
                Models.Buch b = new Models.Buch();
                DataLists.ProductList.Add(b);
            }
            if (productType == 2)
            {
                Models.Magazin m = new Models.Magazin();
                DataLists.ProductList.Add(m);
            }
        }
        void AddPhysicalProduct()
        {
            int productType = Program.IntInputFunction("(1): Buch, (2): Magazin");
            if (productType == 1)
            {
                Models.BuchExemplar b = new Models.BuchExemplar();
                DataLists.PhysicalProductList.Add(b);
            }
            if (productType == 2)
            {
                Models.MagazinExemplar m = new Models.MagazinExemplar();
                DataLists.PhysicalProductList.Add(m);
            }
        }

        void DisplayPhysicalProducts()
        {
            foreach (var pObj in DataLists.PhysicalProductList)
                Console.WriteLine(
                    "{0}|{1}|{2}|{3}|{4}",
                    pObj.ID, pObj.IsBorrowed, pObj.Belonging.ID, pObj.Belonging.Author_Publisher, pObj.Belonging.Title);
        }
        #endregion

        /*
        #region Fundamental
        #region Book/Magazine
        public void AddBook()
        {
            Models.Buch b = new Models.Buch()
            {
                ID = ++DataLists.IC.HighestBookId,
                Author_Publisher = Program.StringInputFunction("Autor:"),
                Title = Program.StringInputFunction("Titel: "),
                Pages = Program.IntInputFunction("Seiten: "),
                Country = Program.StringInputFunction("Land: "),
                Language = Program.StringInputFunction("Sprache: "),
                ImageLink = Program.StringInputFunction("Bild-Link: "),
                Link = Program.StringInputFunction("Link: "),
                Year = Program.IntInputFunction("Jahr: "),
            };
            DataLists.Books.Add(b);
            WriteAndReadFile.WriteICJson();
            WriteAndReadFile.WriteBookJson();
            Console.Clear();
            DisplaySpecificBook(true);
            Console.WriteLine("Erfolgreich hinzugefügt!");
            Program.BorderLine();
        }
        public void RemoveBook()
        {
            var borrowed = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden Buches an: ");

            var bookIdExisting = false;
            foreach (var bookObj in DataLists.Books)
                if (bookObj.ID == removeID)
                {
                    if (bookObj.borrowed > 0)
                        break;
                    bookIdExisting = true;
                    var exemplaryRemoveList = new List<int>();
                    var counter = 0;
                    foreach (var exemplaryObj in DataLists.BookExemplaries)
                    {
                        if (exemplaryObj.BookBelonging == bookObj && exemplaryObj.IsBorrowed == true)
                        {
                            Console.WriteLine("Eines der Exemplare ist noch verborgt!");
                            borrowed = true;
                            break;
                        }
                        else if (exemplaryObj.BookBelonging == bookObj)
                            exemplaryRemoveList.Add(counter - exemplaryRemoveList.Count);
                        counter++;
                    }
                    if (borrowed == false)
                    {
                        foreach (var element in exemplaryRemoveList)
                            DataLists.BookExemplaries.RemoveAt(element);
                        DataLists.Books.Remove(bookObj);
                        WriteAndReadFile.WriteBookJson();
                        WriteAndReadFile.WriteBookExemplaryJson();
                        Console.Clear();
                        Console.WriteLine("Buch mit dazugehörigen Exemplaren erfolgreich gelöscht.");
                        break;
                    }
                    if (borrowed == true)
                        break;
                }
            if (bookIdExisting == false)
                Console.WriteLine("Ein Buch mit dieser ID existiert nicht oder ein E-Book ist noch verliehen!");
        }
        public void EditBook()
        {
            var exist = false;
            var bookID = Program.IntInputFunction("Geben Sie die Buch ID ein: ");
            foreach (var obj in DataLists.Books)
            {
                if (obj.ID == bookID)
                {
                    exist = true;
                    obj.Author_Publisher = Program.StringInputFunction("Geben Sie den Author ein (Leer lassen wenn keine Änerung): ", obj.Author_Publisher);
                    obj.Title = Program.StringInputFunction("Geben Sie den Titel ein (Leer lassen wenn keine Änerung): ", obj.Title);
                    obj.Pages = Program.IntInputFunction("Geben Sie die Seitenanzahl ein (Leer lassen wenn keine Änerung): ", obj.Pages);
                    obj.Country = Program.StringInputFunction("Geben Sie das Land ein (Leer lassen wenn keine Änerung): ", obj.Country);
                    obj.Language = Program.StringInputFunction("Geben Sie die Sprache ein (Leer lassen wenn keine Änerung): ", obj.Language);
                    obj.ImageLink = Program.StringInputFunction("Geben Sie den Bild-Link ein (Leer lassen wenn keine Änerung): ", obj.ImageLink);
                    obj.Link = Program.StringInputFunction("Geben Sie den Link ein (Leer lassen wenn keine Änerung): ", obj.Link);
                    obj.Year = Program.IntInputFunction("Geben Sie das Jahr ein (Leer lassen wenn keine Änerung): ", obj.Year);
                    WriteAndReadFile.WriteBookJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich geändert!");
                    break;
                }
            }
            if (!exist)
                Console.WriteLine("Die Buch ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayBooksSimple()
        {
            Console.WriteLine("ID | Autor | Titel | Sprache | Jahr | Ausgeliehen");
            foreach (var obj in DataLists.Books)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", obj.ID, obj.Author_Publisher, obj.Title, obj.Language, obj.Year, obj.borrowed);
            Program.BorderLine();
        }
        public void DisplayBooksExtended()
        {
            Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Ausgeliehen | Bild-Link | Link");
            foreach (var obj in DataLists.Books)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9}", obj.ID, obj.Author_Publisher, obj.Title, obj.Pages, obj.Country, obj.Language, obj.Year, obj.borrowed, obj.ImageLink, obj.Link);
            Program.BorderLine();
        }
        public void DisplaySpecificBook(bool lastElement)
        {
            var searchColumn = "";
            if (lastElement)
                searchColumn = "id";
            else
                searchColumn = Program.StringInputFunction("Welche Spalte soll durchsucht werden?: ").ToLower();
            if (searchColumn == "id")
            {
                var searchFor = 0;
                if (lastElement)
                    searchFor = DataLists.IC.HighestBookId;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.ID == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Ausgeliehen | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.borrowed, bookObj.ImageLink, bookObj.Link);
                    }
                }
            }
            else if (searchColumn == "autor")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Autor suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.Author_Publisher == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Ausgeliehen | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.borrowed, bookObj.ImageLink, bookObj.Link);
                    }
                }
            }
            else if (searchColumn == "titel")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Titel suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.Title == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Ausgeliehen | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.borrowed, bookObj.ImageLink, bookObj.Link);
                    }
                }
            }
            else if (searchColumn == "sprache")
            {
                string searchFor = Program.StringInputFunction("Nach welcher Sprache suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.Language == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Ausgeliehen | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.borrowed, bookObj.ImageLink, bookObj.Link);
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }

        public void AddMagazine()
        {
            Models.Magazin m = new Models.Magazin()
            {
                ID = ++DataLists.IC.HighestMagazineId,
                Author_Publisher = Program.StringInputFunction("Autor:"),
                Title = Program.StringInputFunction("Titel: "),
                Group = Program.StringInputFunction("Gruppe: "),
                Topic = Program.StringInputFunction("Sachgruppe: "),
            };
            DataLists.Magazines.Add(m);
            WriteAndReadFile.WriteICJson();
            WriteAndReadFile.WriteMagazineJson();
            Console.Clear();
            DisplaySpecificMagazine(true);
            Console.WriteLine("Erfolgreich hinzugefügt!");
            Program.BorderLine();
        }
        public void RemoveMagazine()
        {
            var borrowed = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden Buches an: ");

            var bookIdExisting = false;
            foreach (var magazineObj in DataLists.Magazines)
                if (magazineObj.ID == removeID)
                {
                    if (magazineObj.borrowed > 0)
                        break;
                    bookIdExisting = true;
                    var exemplaryRemoveList = new List<int>();
                    var counter = 0;
                    foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                    {
                        if (exemplaryObj.MagazineBelonging == magazineObj && exemplaryObj.IsBorrowed == true)
                        {
                            Console.WriteLine("Eines der Exemplare ist noch verborgt!");
                            borrowed = true;
                            break;
                        }
                        else if (exemplaryObj.MagazineBelonging == magazineObj)
                            exemplaryRemoveList.Add(counter - exemplaryRemoveList.Count);
                        counter++;
                    }
                    if (borrowed == false)
                    {
                        foreach (var element in exemplaryRemoveList)
                            DataLists.MagazineExemplaries.RemoveAt(element);
                        DataLists.Magazines.Remove(magazineObj);
                        WriteAndReadFile.WriteBookJson();
                        WriteAndReadFile.WriteBookExemplaryJson();
                        Console.Clear();
                        Console.WriteLine("Buch mit dazugehörigen Exemplaren erfolgreich gelöscht.");
                        break;
                    }
                    if (borrowed == true)
                        break;
                }
            if (bookIdExisting == false)
                Console.WriteLine("Ein Buch mit dieser ID existiert nicht oder ein E-Paper ist noch verliehen!");
        }
        public void EditMagazine()
        {
            var exist = false;
            var magazineID = Program.IntInputFunction("Geben Sie die Buch ID ein: ");
            foreach (var obj in DataLists.Magazines)
            {
                if (obj.ID == magazineID)
                {
                    exist = true;
                    obj.Author_Publisher = Program.StringInputFunction("Geben Sie den Author ein (Leer lassen wenn keine Änerung): ", obj.Author_Publisher);
                    obj.Title = Program.StringInputFunction("Geben Sie den Titel ein (Leer lassen wenn keine Änerung): ", obj.Title);
                    obj.Group = Program.StringInputFunction("Geben Sie die Gruppe an (Leer lassen wenn keine Änerung): ", obj.Group);
                    obj.Topic = Program.StringInputFunction("Geben Sie die Sachgruppe an (Leer lassen wenn keine Änerung): ", obj.Topic);
                    WriteAndReadFile.WriteMagazineJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich geändert!");
                    break;
                }
            }
            if (!exist)
                Console.WriteLine("Die Magazin ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayMagazine()
        {
            Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe | Ausgeliehen");
            foreach (var obj in DataLists.Magazines)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", obj.ID, obj.Author_Publisher, obj.Title, obj.Group, obj.Topic, obj.borrowed);
            Program.BorderLine();
        }
        public void DisplaySpecificMagazine(bool lastElement)
        {
            var searchColumn = "";
            if (lastElement)
                searchColumn = "id";
            else
                searchColumn = Program.StringInputFunction("Welche Spalte soll durchsucht werden?: ").ToLower();
            if (searchColumn == "id")
            {
                var searchFor = 0;
                if (lastElement)
                    searchFor = DataLists.IC.HighestMagazineId;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var magazineObj in DataLists.Magazines)
                {
                    if (magazineObj.ID == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe | Ausgeliehen");
                        foreach (var obj in DataLists.Magazines)
                            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", obj.ID, obj.Author_Publisher, obj.Title, obj.Group, obj.Topic, obj.borrowed);
                    }
                }
            }
            else if (searchColumn == "autor")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Autor suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.Author_Publisher == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe | Ausgeliehen");
                        foreach (var obj in DataLists.Magazines)
                            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", obj.ID, obj.Author_Publisher, obj.Title, obj.Group, obj.Topic, obj.borrowed);
                    }
                }
            }
            else if (searchColumn == "titel")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Titel suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.Title == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe | Ausgeliehen");
                        foreach (var obj in DataLists.Magazines)
                            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", obj.ID, obj.Author_Publisher, obj.Title, obj.Group, obj.Topic, obj.borrowed);
                    }
                }
            }
            else if (searchColumn == "sprache")
            {
                string searchFor = Program.StringInputFunction("Nach welcher Sprache suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.Language == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe | Ausgeliehen");
                        foreach (var obj in DataLists.Magazines)
                            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", obj.ID, obj.Author_Publisher, obj.Title, obj.Group, obj.Topic, obj.borrowed);
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }
        #endregion

        #region Exemplary
        public void AddBookExemplary()
        {
            Models.BuchExemplar e = new Models.BuchExemplar()
            {
                ID = ++DataLists.IC.HighestBookExemplaryID,
                IsBorrowed = false,
            };
            var exist = false;
            while (!exist)
            {
                var bookID = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Buches an: ");
                foreach (var obj in DataLists.Books)
                    if (obj.ID == bookID)
                    {
                        exist = true;
                        e.BookBelonging = obj;
                        DataLists.BookExemplaries.Add(e);
                        WriteAndReadFile.WriteICJson();
                        WriteAndReadFile.WriteBookExemplaryJson();
                        //Console.Clear();
                        DisplaySpecificBookExemplary(true);
                        Console.WriteLine("Erfolgreich hinzugefügt!");
                        break;
                    }
                if (!exist)
                    Console.WriteLine("Diese BuchID existiert nicht!");
                Program.BorderLine();
            }
        }
        public void RemoveBookExemplary()
        {
            bool success = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden Exemplars an: ");

            for (int i = 0; i < DataLists.BookExemplaries.Count; i++)
                if (DataLists.BookExemplaries[i].ID == removeID)
                {
                    if (DataLists.BookExemplaries[i].IsBorrowed == true)
                    {
                        Console.WriteLine("Sie können das Exemplar nicht entfernen, da es noch verliehen ist!");
                        break;
                    }
                    DataLists.BookExemplaries.RemoveAt(i);
                    success = true;
                    WriteAndReadFile.WriteBookExemplaryJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich gelöscht!");
                    break;
                }
            if (!success)
                Console.WriteLine("Das Buch mit dieser ID existiert nicht!");
            Program.BorderLine();
        }
        public void EditBookExemplary()
        {
            var success = false;
            var exemplaryID = Program.IntInputFunction("Geben Sie die Exemplar ID ein: ");
            foreach (var exemplaryObj in DataLists.BookExemplaries)
                if (exemplaryObj.ID == exemplaryID)
                {
                    var exist = false;
                    var bookID = Program.IntInputFunction("Geben Sie die Buch ID an (leer lassen für keine Änderung): ", exemplaryObj.BookBelonging.ID);
                    foreach (var bookObj in DataLists.Books)
                        if (bookObj.ID == bookID)
                        {
                            exemplaryObj.BookBelonging = bookObj;
                            Console.WriteLine("Exemplar wurde erfolgreich Bearbeitet!");
                            success = true;
                            WriteAndReadFile.WriteBookExemplaryJson();
                            Console.Clear();
                            Console.WriteLine("Erfolgreich geändert!");
                            break;
                        }
                    if (!exist)
                        Console.WriteLine("Buch ID existiert nicht!");
                }
            if (!success)
                Console.WriteLine("Die Exdemplar ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayBookExemplaries()
        {
            Console.WriteLine("Exemplar ID | Ausgeborgt | BuchID | Autor | Titel");
            foreach (var obj in DataLists.BookExemplaries)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", obj.ID, obj.IsBorrowed, obj.BookBelonging.ID, obj.BookBelonging.Author_Publisher, obj.BookBelonging.Title);
            Program.BorderLine();
        }
        public void DisplaySpecificBookExemplary(bool lastExemplar)
        {
            var searchColumn = "";
            if (lastExemplar)
                searchColumn = "id";
            else
                searchColumn = Program.StringInputFunction("Welche Spalte soll durchsucht werden?: ").ToLower();
            if (searchColumn == "id")
            {
                var searchFor = 0;
                if (lastExemplar)
                    searchFor = DataLists.IC.HighestBookExemplaryID;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.BookExemplaries)
                {
                    if (exemplaryObj.ID == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | BuchID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.BookBelonging.ID, exemplaryObj.BookBelonging.Author_Publisher, exemplaryObj.BookBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "ausgeliehen")
            {
                var searchFor = Program.BoolInputFunction("Nach ausgeliehenen (1) oder vorhandenen (2) suchen?: ");
                foreach (var exemplaryObj in DataLists.BookExemplaries)
                {
                    if (exemplaryObj.IsBorrowed == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | BuchID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.BookBelonging.ID, exemplaryObj.BookBelonging.Author_Publisher, exemplaryObj.BookBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "buchid")
            {
                int searchFor = Program.IntInputFunction("Nach welcher BuchID suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.BookExemplaries)
                {
                    if (exemplaryObj.BookBelonging.ID == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | BuchID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.BookBelonging.ID, exemplaryObj.BookBelonging.Author_Publisher, exemplaryObj.BookBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "autor")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Autor suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.BookExemplaries)
                {
                    if (exemplaryObj.BookBelonging.Author_Publisher == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | BuchID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.BookBelonging.ID, exemplaryObj.BookBelonging.Author_Publisher, exemplaryObj.BookBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "titel")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Titel suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.BookExemplaries)
                {
                    if (exemplaryObj.BookBelonging.Title == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | BuchID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.BookBelonging.ID, exemplaryObj.BookBelonging.Author_Publisher, exemplaryObj.BookBelonging.Title
                            );
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }

        public void AddMagazineExemplary()
        {
            Models.MagazinExemplar m = new Models.MagazinExemplar()
            {
                ID = ++DataLists.IC.HighestMagazineExemplaryID,
                IsBorrowed = false,
            };
            var exist = false;
            while (!exist)
            {
                var magazineID = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Magazines an: ");
                foreach (var obj in DataLists.Magazines)
                {
                    if (obj.ID == magazineID)
                    {
                        exist = true;
                        m.MagazineBelonging = obj;
                        DataLists.MagazineExemplaries.Add(m);
                        WriteAndReadFile.WriteICJson();
                        WriteAndReadFile.WriteMagazineExemplaryJson();
                        Console.Clear();
                        DisplaySpecificMagazineExemplary(true);
                        Console.WriteLine("Erfolgreich hinzugefügt!");
                        break;
                    }
                }
                if (!exist)
                    Console.WriteLine("Diese MagazinID existiert nicht!");
                Program.BorderLine();
            }
        }
        public void RemoveMagazineExemplary()
        {
            bool success = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden Exemplars an: ");

            for (int i = 0; i < DataLists.MagazineExemplaries.Count; i++)
                if (DataLists.MagazineExemplaries[i].ID == removeID)
                {
                    if (DataLists.MagazineExemplaries[i].IsBorrowed == true)
                    {
                        Console.WriteLine("Sie können das Exemplar nicht entfernen, da es noch verliehen ist!");
                        break;
                    }
                    DataLists.MagazineExemplaries.RemoveAt(i);
                    success = true;
                    WriteAndReadFile.WriteMagazineExemplaryJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich gelöscht!");
                    break;
                }
            if (!success)
                Console.WriteLine("Das Buch mit dieser ID existiert nicht!");
            Program.BorderLine();
        }
        public void EditMagazineExemplary()
        {
            var success = false;
            var exemplaryID = Program.IntInputFunction("Geben Sie die Exemplar ID ein: ");
            foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                if (exemplaryObj.ID == exemplaryID)
                {
                    var exist = false;
                    var magazineID = Program.IntInputFunction("Geben Sie die Buch ID an (leer lassen für keine Änderung): ", exemplaryObj.MagazineBelonging.ID);
                    foreach (var magazineObj in DataLists.Magazines)
                        if (magazineObj.ID == magazineID)
                        {
                            exemplaryObj.MagazineBelonging = magazineObj;
                            Console.WriteLine("Exemplar wurde erfolgreich Bearbeitet!");
                            success = true;
                            WriteAndReadFile.WriteMagazineExemplaryJson();
                            Console.Clear();
                            Console.WriteLine("Erfolgreich geändert!");
                            break;
                        }
                    if (!exist)
                        Console.WriteLine("Buch ID existiert nicht!");
                }
            if (!success)
                Console.WriteLine("Die Exdemplar ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayMagazineExemplaries()
        {
            Console.WriteLine("Exemplar ID | Ausgeborgt | MagazinID | Autor | Titel");
            foreach (var obj in DataLists.MagazineExemplaries)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", obj.ID, obj.IsBorrowed, obj.MagazineBelonging.ID, obj.MagazineBelonging.Author_Publisher, obj.MagazineBelonging.Title);
            Program.BorderLine();
        }
        public void DisplaySpecificMagazineExemplary(bool lastExemplar)
        {
            var searchColumn = "";
            if (lastExemplar)
                searchColumn = "id";
            else
                searchColumn = Program.StringInputFunction("Welche Spalte soll durchsucht werden?: ").ToLower();
            if (searchColumn == "id")
            {
                var searchFor = 0;
                if (lastExemplar)
                    searchFor = DataLists.IC.HighestMagazineExemplaryID;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                {
                    if (exemplaryObj.ID == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | MagazinID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.MagazineBelonging.ID, exemplaryObj.MagazineBelonging.Author_Publisher, exemplaryObj.MagazineBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "ausgeliehen")
            {
                var searchFor = Program.BoolInputFunction("Nach ausgeliehenen (1) oder vorhandenen (2) suchen?: ");
                foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                {
                    if (exemplaryObj.IsBorrowed == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | MagazinID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.MagazineBelonging.ID, exemplaryObj.MagazineBelonging.Author_Publisher, exemplaryObj.MagazineBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "buchid")
            {
                int searchFor = Program.IntInputFunction("Nach welcher MagazinID suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                {
                    if (exemplaryObj.MagazineBelonging.ID == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | MagazinID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.MagazineBelonging.ID, exemplaryObj.MagazineBelonging.Author_Publisher, exemplaryObj.MagazineBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "autor")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Autor suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                {
                    if (exemplaryObj.MagazineBelonging.Author_Publisher == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | MagazinID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.MagazineBelonging.ID, exemplaryObj.MagazineBelonging.Author_Publisher, exemplaryObj.MagazineBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "titel")
            {
                string searchFor = Program.StringInputFunction("Nach welchem Titel suchen Sie?: ");
                foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                {
                    if (exemplaryObj.MagazineBelonging.Title == searchFor)
                    {
                        Console.WriteLine("Exemplar ID | Ausgeborgt | MagazinID | Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            exemplaryObj.ID, exemplaryObj.IsBorrowed, exemplaryObj.MagazineBelonging.ID, exemplaryObj.MagazineBelonging.Author_Publisher, exemplaryObj.MagazineBelonging.Title
                            );
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }

        #endregion

        #region Borrow
        public void AddBookBorrow()
        {
            var electronic = Program.BoolInputFunction("Wird ein Elektronisches Exemplar verliehen?: ");
            Models.Ausleihe e = new Models.Ausleihe()
            {
                ID = ++DataLists.IC.HighestBookBorrowId,
                Customer = Program.StringInputFunction("Geben Sie die Kundendaten an: "),
                StartBorrowDate = DateTime.Now,
                EndBorrowDate = DateTime.Now.AddDays(30),
            };
            var exist = false;
            var exemplaryID = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Exemplares oder E-Books an: ");
            if (electronic)
            {
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.ID == exemplaryID)
                    {
                        e.ExemplarBorrowed = bookObj;
                        e.IsElectronic = true;
                        bookObj.borrowed++;
                        exist = true;
                        break;
                    }
                }
            }
            if (!electronic)
            {
                foreach (var obj in DataLists.BookExemplaries)
                {
                    if (obj.IsBorrowed)
                    {
                        Console.WriteLine("Dieses Exemplar wurde schon ausgeliehen!");
                        exist = true;
                        break;
                    }
                    if (obj.ID == exemplaryID)
                    {
                        obj.IsBorrowed = true;
                        e.ExemplarBorrowed = obj;
                        e.IsElectronic = false;
                        exist = true;
                        break;
                    }
                }
            }
            if (exist)
            {
                DataLists.BooksBorrowedList.Add(e);
                WriteAndReadFile.WriteICJson();
                WriteAndReadFile.WriteBookBorrowJson();
                WriteAndReadFile.WriteBookExemplaryJson();
                Console.Clear();
                DisplaySpecificBookBorrow(true);
                Console.WriteLine("Erfolgreich ausgeliehen!");
            }
            if (!exist)
                Console.WriteLine("Diese ExemplarID existiert nicht!");
            Program.BorderLine();
        }
        public void RemoveBookBorrow()
        {
            bool success = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden ausgeliehenen Gegenstandes an: ");

            foreach (var borrowObj in DataLists.BooksBorrowedList)
            {
                if (borrowObj.ID == removeID)
                {
                    if (borrowObj.ID == removeID && borrowObj.IsElectronic)
                    {
                        Models.Buch bookBorrowObj = (Models.Buch)borrowObj.ExemplarBorrowed;
                        bookBorrowObj.borrowed--;
                        DataLists.BooksBorrowedList.Remove(borrowObj);
                        WriteAndReadFile.WriteBookJson();
                        success = true;
                    }
                    if (borrowObj.ID == removeID && !borrowObj.IsElectronic)
                    {
                        Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)borrowObj.ExemplarBorrowed;
                        bookBorrowObj.IsBorrowed = false;
                        DataLists.BooksBorrowedList.Remove(borrowObj);
                        WriteAndReadFile.WriteBookExemplaryJson();
                        success = true;
                    }
                    WriteAndReadFile.WriteBookBorrowJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich gelöscht!");
                    break;
                }
                
            }
            if (!success)
                Console.WriteLine("Das Exemplar mit dieser ID existiert nicht!");
            Program.BorderLine();
        }
        public void EditBookBorrow()
        {
            var ausleiheIDExist = false;
            var borrowID = Program.IntInputFunction("Geben Sie die Ausleih ID ein: ");
            foreach (var borrowObj in DataLists.BooksBorrowedList)
            {
                if (borrowObj.ID == borrowID)
                {
                    ausleiheIDExist = true;
                    borrowObj.Customer = Program.StringInputFunction("Kundendaten (Leer lassen für keine Änderung): ", borrowObj.Customer);
                    var borrowTime = Program.IntInputFunction("In wieviel Tagen muss der Kunde das Buch zurückgeben? (Geben Sie -1 ein um keine Änderungen vorzunehmen): ", -1);
                    if (borrowTime != -1)
                        borrowObj.EndBorrowDate = DateTime.Now.AddDays(borrowTime);
                    else
                        borrowObj.EndBorrowDate = DateTime.Now.AddDays(borrowTime);
                    var exemplaryID = Program.IntInputFunction("Geben Sie die Exemplar ID an (-1 für keine Änderung): ");
                    if (exemplaryID != -1)
                    {
                        var exemplaryExist = false;
                        if (borrowObj.IsElectronic == true)
                        {
                            foreach (var bookObj in DataLists.Books)
                            {
                                if (bookObj.ID == exemplaryID)
                                {
                                    borrowObj.ExemplarBorrowed = bookObj;
                                    exemplaryExist = true;
                                    break;
                                }
                            }
                        }
                        if (borrowObj.IsElectronic == false)
                        {
                            foreach (var exemplaryObj in DataLists.BookExemplaries)
                            {
                                if (exemplaryObj.ID == exemplaryID)
                                {
                                    borrowObj.ExemplarBorrowed = exemplaryObj;
                                    exemplaryExist = true;
                                    break;
                                }
                            }
                        }
                        if (!exemplaryExist)
                            Console.WriteLine("Exemplar ID existiert nicht!");
                    }
                    WriteAndReadFile.WriteBookBorrowJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich geändert!");
                }
            }
            if (!ausleiheIDExist)
                Console.WriteLine("Die Ausleihe ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayBooksBorrowed()
        {
            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID | Autor | Titel");
            foreach (var obj in DataLists.BooksBorrowedList)
            {
                if (obj.IsElectronic == true)
                {
                    Models.Buch bookBorrowObj = (Models.Buch)obj.ExemplarBorrowed;
                    Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                        obj.ID, obj.IsElectronic, obj.Customer, obj.StartBorrowDate, obj.EndBorrowDate, "-", bookBorrowObj.ID, bookBorrowObj.Author_Publisher, bookBorrowObj.Title);
                }
                if (obj.IsElectronic == false)
                {
                    Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)obj.ExemplarBorrowed;
                    Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                        obj.ID, obj.IsElectronic, obj.Customer, obj.StartBorrowDate, obj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.BookBelonging.ID, bookBorrowObj.BookBelonging.Author_Publisher, bookBorrowObj.BookBelonging.Title);
                }
            }
            Program.BorderLine();
        }
        public void DisplaySpecificBookBorrow(bool lastBorrow)
        {
            var searchColumn = "";
            if (lastBorrow)
                searchColumn = "id";
            else
                searchColumn = Program.StringInputFunction("Welche Spalte soll durchsucht werden?: ").ToLower();
            if (searchColumn == "id")
            {
                var searchFor = 0;
                if (lastBorrow)
                    searchFor = DataLists.IC.HighestBookBorrowId;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var borrowObj in DataLists.BooksBorrowedList)
                {
                    if (borrowObj.ID == searchFor)
                    {
                        if (borrowObj.IsElectronic == true)
                        {
                            Models.Buch bookBorrowObj = (Models.Buch)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Autor | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, "-", bookBorrowObj.ID, bookBorrowObj.Author_Publisher, bookBorrowObj.Title);
                        }
                        if (borrowObj.IsElectronic == false)
                        {
                            Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Autor | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.BookBelonging.ID, bookBorrowObj.BookBelonging.Author_Publisher, bookBorrowObj.BookBelonging.Title);
                        }
                    }
                }
            }
            else if (searchColumn == "kunde")
            {
                var searchFor = Program.StringInputFunction("Nach welchen Kunden suchen Sie?: ");
                foreach (var borrowObj in DataLists.MagazineBorrowedList)
                {
                    if (borrowObj.Customer == searchFor)
                    {
                        if (borrowObj.IsElectronic == true)
                        {
                            Models.Buch bookBorrowObj = (Models.Buch)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Author | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, "-", bookBorrowObj.ID, bookBorrowObj.Author_Publisher, bookBorrowObj.Title);
                        }
                        if (borrowObj.IsElectronic == false)
                        {
                            Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Author | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.BookBelonging.ID, bookBorrowObj.BookBelonging.Author_Publisher, bookBorrowObj.BookBelonging.Title);
                        }
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }

        public void AddMagazineBorrow()
        {
            var electronic = Program.BoolInputFunction("Wird ein Elektronisches Exemplar verliehen?: ");
            Models.Ausleihe e = new Models.Ausleihe()
            {
                ID = ++DataLists.IC.HighestMagazineBorrowId,
                Customer = Program.StringInputFunction("Geben Sie die Kundendaten an: "),
                StartBorrowDate = DateTime.Now,
                EndBorrowDate = DateTime.Now.AddDays(2),
            };
            var exist = false;
            var exemplaryID = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Exemplares an: ");
            if (electronic)
            {
                foreach (var magazineObj in DataLists.Magazines)
                {
                    if (magazineObj.ID == exemplaryID)
                    {
                        e.ExemplarBorrowed = magazineObj;
                        e.IsElectronic = true;
                        magazineObj.borrowed++;
                        exist = true;
                        break;
                    }
                }
            }
            if (!electronic)
            {
                foreach (var magazineObj in DataLists.MagazineExemplaries)
                {
                    if (magazineObj.IsBorrowed)
                    {
                        Console.WriteLine("Dieses Exemplar wurde schon ausgeliehen!");
                        exist = true;
                        break;
                    }
                    if (magazineObj.ID == exemplaryID)
                    {
                        magazineObj.IsBorrowed = true;
                        e.ExemplarBorrowed = magazineObj;
                        e.IsElectronic = false;
                        exist = true;
                        break;
                    }
                }
            }
            if (exist)
            {
                DataLists.MagazineBorrowedList.Add(e);
                WriteAndReadFile.WriteICJson();
                WriteAndReadFile.WriteMagazineBorrowJson();
                WriteAndReadFile.WriteBookExemplaryJson();
                Console.Clear();
                DisplaySpecificMagazineBorrow(true);
                Console.WriteLine("Erfolgreich hinzugefügt!");
            }
            if (!exist)
                Console.WriteLine("Diese ExemplarID existiert nicht!");
            Program.BorderLine();
        }
        public void RemoveMagazineBorrow()
        {
            bool success = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden ausgeliehenen Gegenstandes an: ");

            foreach (var borrowObj in DataLists.MagazineBorrowedList)
            {
                if (borrowObj.ID == removeID)
                {
                    if (borrowObj.ID == removeID && borrowObj.IsElectronic)
                    {
                        Models.Magazin bookBorrowObj = (Models.Magazin)borrowObj.ExemplarBorrowed;
                        bookBorrowObj.borrowed--;
                        DataLists.MagazineBorrowedList.Remove(borrowObj);
                        WriteAndReadFile.WriteMagazineJson();
                        Console.WriteLine("Erfolgreich gelöscht!");
                        success = true;
                    }
                    if (borrowObj.ID == removeID && !borrowObj.IsElectronic)
                    {
                        Models.MagazinExemplar bookBorrowObj = (Models.MagazinExemplar)borrowObj.ExemplarBorrowed;
                        bookBorrowObj.IsBorrowed = false;
                        DataLists.MagazineBorrowedList.Remove(borrowObj);
                        WriteAndReadFile.WriteMagazineExemplaryJson();
                        Console.WriteLine("Erfolgreich gelöscht!");
                        success = true;
                    }
                    WriteAndReadFile.WriteMagazineBorrowJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich gelöscht!");
                    break;
                }
            }
            if (!success)
                Console.WriteLine("Das Exemplar mit dieser ID existiert nicht!");
            Program.BorderLine();
        }
        public void EditMagazineBorrow()
        {
            var ausleiheIDExist = false;
            var borrowID = Program.IntInputFunction("Geben Sie die Ausleih ID ein: ");
            foreach (var borrowObj in DataLists.MagazineBorrowedList)
            {
                if (borrowObj.ID == borrowID)
                {
                    ausleiheIDExist = true;
                    borrowObj.Customer = Program.StringInputFunction("Kundendaten (Leer lassen für keine Änderung): ", borrowObj.Customer);
                    var borrowTime = Program.IntInputFunction("In wieviel Tagen muss der Kunde das Buch zurückgeben? (Geben Sie -1 ein um keine Änderungen vorzunehmen): ", -1);
                    if (borrowTime != -1)
                        borrowObj.EndBorrowDate = DateTime.Now.AddDays(borrowTime);
                    else
                        borrowObj.EndBorrowDate = DateTime.Now.AddDays(borrowTime);
                    var exemplaryID = Program.IntInputFunction("Geben Sie die Exemplar ID an (-1 für keine Änderung): ");
                    if (exemplaryID != -1)
                    {
                        var exemplaryExist = false;
                        if (borrowObj.IsElectronic == true)
                        {
                            foreach (var bookObj in DataLists.Magazines)
                            {
                                if (bookObj.ID == exemplaryID)
                                {
                                    borrowObj.ExemplarBorrowed = bookObj;
                                    exemplaryExist = true;
                                    break;
                                }
                            }
                        }
                        if (borrowObj.IsElectronic == false)
                        {
                            foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                            {
                                if (exemplaryObj.ID == exemplaryID)
                                {
                                    borrowObj.ExemplarBorrowed = exemplaryObj;
                                    exemplaryExist = true;
                                    break;
                                }
                            }
                        }
                        if (!exemplaryExist)
                            Console.WriteLine("Exemplar ID existiert nicht!");
                    }
                    WriteAndReadFile.WriteMagazineBorrowJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich geändert!");
                }
            }
            if (!ausleiheIDExist)
                Console.WriteLine("Die Ausleihe ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayMagazinesBorrowed()
        {
            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID | Autor | Titel");
            foreach (var obj in DataLists.MagazineBorrowedList)
            {
                if (obj.IsElectronic == true)
                {
                    Models.Magazin bookBorrowObj = (Models.Magazin)obj.ExemplarBorrowed;
                    Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                        obj.ID, obj.IsElectronic, obj.Customer, obj.StartBorrowDate, obj.EndBorrowDate, "-", bookBorrowObj.ID, bookBorrowObj.Author_Publisher, bookBorrowObj.Title);
                }
                if (obj.IsElectronic == false)
                {
                    Models.MagazinExemplar bookBorrowObj = (Models.MagazinExemplar)obj.ExemplarBorrowed;
                    Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                        obj.ID, obj.IsElectronic, obj.Customer, obj.StartBorrowDate, obj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.MagazineBelonging.ID, bookBorrowObj.MagazineBelonging.Author_Publisher, bookBorrowObj.MagazineBelonging.Title);
                }
            }
            Program.BorderLine();
        }

            public void DisplaySpecificMagazineBorrow(bool lastBorrow)
        {
            var searchColumn = "";
            if (lastBorrow)
                searchColumn = "id";
            else
                searchColumn = Program.StringInputFunction("Welche Spalte soll durchsucht werden?: ").ToLower();
            if (searchColumn == "id")
            {
                var searchFor = 0;
                if (lastBorrow)
                    searchFor = DataLists.IC.HighestMagazineBorrowId;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var borrowObj in DataLists.MagazineBorrowedList)
                {
                    if (borrowObj.ID == searchFor)
                    {
                        if (borrowObj.IsElectronic == true)
                        {
                            Models.Magazin magazinBorrowObj = (Models.Magazin)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Autor | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, "-", magazinBorrowObj.ID, magazinBorrowObj.Author_Publisher, magazinBorrowObj.Title);
                        }
                        if (borrowObj.IsElectronic == false)
                        {
                            Models.MagazinExemplar magazineBorrowObj = (Models.MagazinExemplar)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Autor | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, magazineBorrowObj.ID, magazineBorrowObj.MagazineBelonging.ID, magazineBorrowObj.MagazineBelonging.Author_Publisher, magazineBorrowObj.MagazineBelonging.Title);
                        }
                    }
                }
            }
            else if (searchColumn == "kunde")
            {
                var searchFor = Program.StringInputFunction("Nach welchen Kunden suchen Sie?: ");
                foreach (var borrowObj in DataLists.MagazineBorrowedList)
                {
                    if (borrowObj.Customer == searchFor)
                    {
                        if (borrowObj.IsElectronic == true)
                        {
                            Models.Magazin magazinBorrowObj = (Models.Magazin)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Author | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, "-", magazinBorrowObj.ID, magazinBorrowObj.Author_Publisher, magazinBorrowObj.Title);
                        }
                        if (borrowObj.IsElectronic == false)
                        {
                            Models.MagazinExemplar magazinBorrowObj = (Models.MagazinExemplar)borrowObj.ExemplarBorrowed;
                            Console.WriteLine("Ausleih ID | Elektronisch | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Author | Titel");
                            Console.WriteLine(
                                "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                                borrowObj.ID, borrowObj.IsElectronic, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, magazinBorrowObj.ID, magazinBorrowObj.MagazineBelonging.ID, magazinBorrowObj.MagazineBelonging.Author_Publisher, magazinBorrowObj.MagazineBelonging.Title);
                        }
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }
        #endregion
        #endregion*/
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
                AddBookID();
                CreateFirstExemplaries();
                CreateBorrowJson();
                WriteAndReadFile.WriteICJson();
            }
            else
            {
                WriteAndReadFile.ReadICJson();
                //WriteAndReadFile.ReadBookJson();
                WriteAndReadFile.ReadMagazineJson();
                //WriteAndReadFile.ReadBookExemplaryJson();
                //WriteAndReadFile.ReadMagazineExemplaryJson();
                //WriteAndReadFile.ReadBookBorrowJson();
                WriteAndReadFile.ReadMagazineBorrowJson();
                ProofBorrowLists();
            }
        }
        public void AddBookID()
        {
            foreach (var bookObj in DataLists.Books)
            {
                //DataLists.IC.HighestBookId++;
                //bookObj.ID = DataLists.IC.HighestBookId;
            }
            //WriteAndReadFile.WriteBookJson();
        }
        public void CreateFirstExemplaries()
        {
            DataLists.BookExemplaries = new List<Models.BuchExemplar>();
            foreach (var bookObj in DataLists.Books)
            {
                //DataLists.IC.HighestBookExemplaryID++;
                Models.BuchExemplar e1 = new Models.BuchExemplar()
                {
                    //ID = DataLists.IC.HighestBookExemplaryID,
                    IsBorrowed = false,
                    //BookBelonging = bookObj,
                };
                DataLists.BookExemplaries.Add(e1);
                //DataLists.IC.HighestBookExemplaryID++;
                DataLists.BookExemplaries.Add(
                    new Models.BuchExemplar
                    {
                        //ID = DataLists.IC.HighestBookExemplaryID,
                        IsBorrowed = false,
                        //BookBelonging = bookObj,
                    });
            }
            WriteAndReadFile.WriteBookExemplaryJson();
            
            DataLists.MagazineExemplaries = new List<Models.MagazinExemplar>();
            foreach (var magazineObj in DataLists.Magazines)
            {
                //DataLists.IC.HighestMagazineExemplaryID++;
                Models.MagazinExemplar e1 = new Models.MagazinExemplar()
                {
                    //ID = DataLists.IC.HighestMagazineExemplaryID,
                    IsBorrowed = false,
                    //MagazineBelonging = magazineObj,
                };
                DataLists.MagazineExemplaries.Add(e1);
                //DataLists.IC.HighestMagazineExemplaryID++;
                DataLists.MagazineExemplaries.Add(
                    new Models.MagazinExemplar
                    {
                        //ID = DataLists.IC.HighestMagazineExemplaryID,
                        IsBorrowed = false,
                        //MagazineBelonging = magazineObj,
                    });
            }
            WriteAndReadFile.WriteMagazineExemplaryJson();
        }
        public void CreateBorrowJson()
        {
            DataLists.BooksBorrowedList = new List<Models.Ausleihe>();
            DataLists.MagazineBorrowedList = new List<Models.Ausleihe>();
            WriteAndReadFile.WriteBookBorrowJson();
            WriteAndReadFile.WriteMagazineBorrowJson();
        }
        
        private void ProofBorrowLists()
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
                    Models.BuchExemplar b = o.ToObject<Models.BuchExemplar>();
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
                    Models.MagazinExemplar m = o.ToObject<Models.MagazinExemplar>();
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
        }
        #endregion
    }
}
