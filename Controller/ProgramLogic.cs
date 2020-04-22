﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Controller
{
    class ProgramLogic
    {
        #region Fundamental
        #region Book
        public void AddBook()
        {
            Models.Buch b = new Models.Buch()
            {
                ID = ++DataLists.IC.HighestBookID,
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
            var success = false;
            var borrowed = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden Buches an: ");

            for (int i = 0; i < DataLists.Books.Count; i++)
            {
                if (DataLists.Books[i].ID == removeID)
                {
                    foreach (var exemplaryObj in DataLists.BookExemplaries)
                    {
                        if (exemplaryObj.BookBelonging.ID == removeID && exemplaryObj.IsBorrowed == true)
                        {
                            borrowed = true;
                            break;
                        }
                    }
                    if (borrowed)
                        break;
                    for (int x = 0; x < DataLists.BookExemplaries.Count; x++)
                    {
                        if (DataLists.BookExemplaries[x].BookBelonging.ID == removeID)
                        {
                            DataLists.BookExemplaries.RemoveAt(x);
                            x--;
                        }
                    }
                    DataLists.Books.RemoveAt(i);
                    WriteAndReadFile.WriteBookJson();
                    WriteAndReadFile.WriteBookExemplaryJson();
                    success = true;
                    Console.Clear();
                    Console.WriteLine("Erfolgreich gelöscht!");
                    break;
                }
            }
            if (borrowed)
                Console.WriteLine("Eines der Exemplare ist noch ausgeliehen!");
            else if (!success)
                Console.WriteLine("Das Buch mit dieser ID existiert nicht!");
            Program.BorderLine();
        }
        public void EditBook()
        {
            var success = false;
            var bookID = Program.IntInputFunction("Geben Sie die Buch ID ein: ");
            foreach (var obj in DataLists.Books)
            {
                if (obj.ID == bookID)
                {
                    obj.Author_Publisher = Program.StringInputFunction("Geben Sie den Author ein (Leer lassen wenn keine Änerung): ", obj.Author_Publisher);
                    obj.Title = Program.StringInputFunction("Geben Sie den Titel ein (Leer lassen wenn keine Änerung): ", obj.Title);
                    obj.Pages = Program.IntInputFunction("Geben Sie die Seitenanzahl ein (Leer lassen wenn keine Änerung): ", obj.Pages);
                    obj.Country = Program.StringInputFunction("Geben Sie das Land ein (Leer lassen wenn keine Änerung): ", obj.Country);
                    obj.Language = Program.StringInputFunction("Geben Sie die Sprache ein (Leer lassen wenn keine Änerung): ", obj.Language);
                    obj.ImageLink = Program.StringInputFunction("Geben Sie den Bild-Link ein (Leer lassen wenn keine Änerung): ", obj.ImageLink);
                    obj.Link = Program.StringInputFunction("Geben Sie den Link ein (Leer lassen wenn keine Änerung): ", obj.Link);
                    obj.Year = Program.IntInputFunction("Geben Sie das Jahr ein (Leer lassen wenn keine Änerung): ", obj.Year);
                    success = true;
                    WriteAndReadFile.WriteBookJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich geändert!");
                    break;
                }
            }
            if (!success)
                Console.WriteLine("Die Buch ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayBooksSimple()
        {
            Console.WriteLine("ID | Autor | Titel | Sprache | Jahr");
            foreach (var obj in DataLists.Books)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", obj.ID, obj.Author_Publisher, obj.Title, obj.Language, obj.Year);
            Program.BorderLine();
        }
        public void DisplayBooksExtended()
        {
            Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
            foreach (var obj in DataLists.Books)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}", obj.ID, obj.Author_Publisher, obj.Title, obj.Pages, obj.Country, obj.Language, obj.Year, obj.ImageLink, obj.Link);
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
                    searchFor = DataLists.IC.HighestBookID;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var bookObj in DataLists.Books)
                {
                    if (bookObj.ID == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
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
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
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
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
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
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }
        #endregion

        #region Magazine
        public void AddMagazine()
        {
            Models.Magazin m = new Models.Magazin()
            {
                ID = ++DataLists.IC.HighestMagazineID,
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
            var success = false;
            var borrowed = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden Magazins an: ");

            for (int i = 0; i < DataLists.Magazines.Count; i++)
            {
                if (DataLists.Magazines[i].ID == removeID)
                {
                    foreach (var exemplaryObj in DataLists.MagazineExemplaries)
                    {
                        if (exemplaryObj.MagazineBelonging.ID == removeID && exemplaryObj.IsBorrowed == true)
                        {
                            borrowed = true;
                            break;
                        }
                    }
                    if (borrowed)
                        break;
                    for (int x = 0; x < DataLists.MagazineExemplaries.Count; x++)
                    {
                        if (DataLists.MagazineExemplaries[x].MagazineBelonging.ID == removeID)
                        {
                            DataLists.MagazineExemplaries.RemoveAt(x);
                            x--;
                        }
                    }
                    DataLists.Magazines.RemoveAt(i);
                    WriteAndReadFile.WriteMagazineJson();
                    WriteAndReadFile.WriteMagazineExemplaryJson();
                    success = true;
                    Console.Clear();
                    Console.WriteLine("Erfolgreich gelöscht!");
                    break;
                }
            }
            if (borrowed)
                Console.WriteLine("Eines der Magazin-Exemplare ist noch ausgeliehen!");
            else if (!success)
                Console.WriteLine("Das Magazin mit dieser ID existiert nicht!");
            Program.BorderLine();
        }
        public void EditMagazine()
        {
            var success = false;
            var magazineID = Program.IntInputFunction("Geben Sie die Buch ID ein: ");
            foreach (var obj in DataLists.Magazines)
            {
                if (obj.ID == magazineID)
                {
                    obj.Author_Publisher = Program.StringInputFunction("Geben Sie den Author ein (Leer lassen wenn keine Änerung): ", obj.Author_Publisher);
                    obj.Title = Program.StringInputFunction("Geben Sie den Titel ein (Leer lassen wenn keine Änerung): ", obj.Title);
                    obj.Group = Program.StringInputFunction("Geben Sie die Gruppe an (Leer lassen wenn keine Änerung): ", obj.Group);
                    obj.Topic = Program.StringInputFunction("Geben Sie die Sachgruppe an (Leer lassen wenn keine Änerung): ", obj.Topic);
                    success = true;
                    WriteAndReadFile.WriteMagazineJson();
                    Console.Clear();
                    Console.WriteLine("Erfolgreich geändert!");
                    break;
                }
            }
            if (!success)
                Console.WriteLine("Die Magazin ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayMagazine()
        {
            Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe");
            foreach (var obj in DataLists.Magazines)
                Console.WriteLine("{0} | {1} | {2} | {3} | {4}", obj.ID, obj.Author_Publisher, obj.Title, obj.Group, obj.Topic);
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
                    searchFor = DataLists.IC.HighestMagazineID;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var magazineObj in DataLists.Magazines)
                {
                    if (magazineObj.ID == searchFor)
                    {
                        Console.WriteLine("ID | Autor | Titel | Gruppe | Sachgruppe");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4}",
                            magazineObj.ID, magazineObj.Author_Publisher, magazineObj.Title, magazineObj.Group, magazineObj.Topic
                            );
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
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
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
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
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
                        Console.WriteLine("ID | Autor | Titel | Seiten | Land | Sprache | Jahr | Bild-Link | Link");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}",
                            bookObj.ID, bookObj.Author_Publisher, bookObj.Title, bookObj.Pages, bookObj.Country, bookObj.Language, bookObj.Year, bookObj.ImageLink, bookObj.Link
                            );
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
                {
                    if (obj.ID == bookID)
                    {
                        exist = true;
                        e.BookBelonging = obj;
                        DataLists.BookExemplaries.Add(e);
                        WriteAndReadFile.WriteICJson();
                        WriteAndReadFile.WriteBookExemplaryJson();
                        Console.Clear();
                        DisplaySpecificBookExemplary(true);
                        Console.WriteLine("Erfolgreich hinzugefügt!");
                        break;
                    }
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
            {
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
            {
                if (exemplaryObj.ID == exemplaryID)
                {
                    var exist = false;
                    var bookID = Program.IntInputFunction("Geben Sie die Buch ID an (leer lassen für keine Änderung): ", exemplaryObj.BookBelonging.ID);
                    foreach (var bookObj in DataLists.Books)
                    {
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
                    }
                    if (!exist)
                        Console.WriteLine("Buch ID existiert nicht!");
                }
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
            {
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
            {
                if (exemplaryObj.ID == exemplaryID)
                {
                    var exist = false;
                    var magazineID = Program.IntInputFunction("Geben Sie die Buch ID an (leer lassen für keine Änderung): ", exemplaryObj.MagazineBelonging.ID);
                    foreach (var magazineObj in DataLists.Magazines)
                    {
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
                    }
                    if (!exist)
                        Console.WriteLine("Buch ID existiert nicht!");
                }
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
            Models.Ausleihe e = new Models.Ausleihe()
            {
                ID = ++DataLists.IC.HighestBorrowID,
                Customer = Program.StringInputFunction("Geben Sie die Kundendaten an: "),
                StartBorrowDate = DateTime.Now,
                EndBorrowDate = DateTime.Now.AddDays(30),
            };
            var exist = false;
            var exemplaryID = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Exemplares an: ");
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
                    e.ExemplarBorrowed = obj;
                    obj.IsBorrowed = true;
                    DataLists.BookBorrowedList.Add(e);
                    exist = true;
                    WriteAndReadFile.WriteICJson();
                    WriteAndReadFile.WriteBorrowJson();
                    Console.Clear();
                    DisplaySpecificBorrow(true);
                    Console.WriteLine("Erfolgreich hinzugefügt!");
                    break;
                }
            }
            if (!exist)
                Console.WriteLine("Diese ExemplarID existiert nicht!");
            Program.BorderLine();
        }
        public void RemoveBookBorrow()
        {
            bool success = false;
            var removeID = Program.IntInputFunction("Geben Sie die ID des zu löschenden ausgeliehenen Gegenstandes an: ");

            for (int i = 0; i < DataLists.BookBorrowedList.Count; i++)
            {
                if (DataLists.BookBorrowedList[i].ID == removeID)
                {
                    //DataLists.BorrowedList[i].ExemplarBorrowed.IsBorrowed = false;
                    Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)DataLists.BookBorrowedList[i].ExemplarBorrowed;
                    bookBorrowObj.IsBorrowed = false;
                    DataLists.BookBorrowedList[i].ExemplarBorrowed = bookBorrowObj;
                    DataLists.BookBorrowedList.RemoveAt(i);
                    Console.WriteLine("Erfolgreich gelöscht!");
                    success = true;
                    WriteAndReadFile.WriteBookExemplaryJson();
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
            var success = false;
            var borrowID = Program.IntInputFunction("Geben Sie die Ausleih ID ein: ");
            foreach (var borrowObj in DataLists.BookBorrowedList)
            {
                if (borrowObj.ID == borrowID)
                {
                    borrowObj.Customer = Program.StringInputFunction("Kundendaten (Leer lassen für keine Änderung): ", borrowObj.Customer);
                    var borrowTime = Program.IntInputFunction("In wieviel Tagen muss der Kunde das Buch zurückgeben? (Geben Sie -1 ein um keine Änderungen vorzunehmen): ", -1);
                    if (borrowTime != -1)
                        borrowObj.EndBorrowDate = DateTime.Now.AddDays(borrowTime);
                    var exist = false;
                    Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)borrowObj.ExemplarBorrowed;
                    var exemplaryID = Program.IntInputFunction("Geben Sie die Exemplar ID an (Leer lassen für keine Änderung): ", bookBorrowObj.ID);
                    foreach (var exemplaryObj in DataLists.BookExemplaries)
                    {
                        if (exemplaryObj.ID == exemplaryID)
                        {
                            borrowObj.ExemplarBorrowed = exemplaryObj;
                            Console.WriteLine("Leihvorgang wurde erfolgreich Bearbeitet!");
                            success = true;
                            exist = true;
                            Console.Clear();
                            Console.WriteLine("Erfolgreich geändert!");
                            break;
                        }
                    }
                    if (!exist)
                        Console.WriteLine("Exemplar ID existiert nicht!");
                }
            }
            if (!success)
                Console.WriteLine("Die Ausleihe ID existiert nicht!");
            Program.BorderLine();
        }
        public void DisplayBookBorrowed()
        {
            Console.WriteLine("Ausleih ID | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID | Autor | Titel");
            foreach (var obj in DataLists.BookBorrowedList)
            {
                Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)obj.ExemplarBorrowed;
                Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                    obj.ID, obj.Customer, obj.StartBorrowDate, obj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.BookBelonging.ID, bookBorrowObj.BookBelonging.Author_Publisher, bookBorrowObj.BookBelonging.Title);
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
                    searchFor = DataLists.IC.HighestBorrowID;
                else
                    searchFor = Program.IntInputFunction("Nach welcher ID suchen Sie?: ");
                foreach (var borrowObj in DataLists.BookBorrowedList)
                {
                    if (borrowObj.ID == searchFor)
                    {
                        Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)borrowObj.ExemplarBorrowed;
                        Console.WriteLine("Ausleih ID | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Autor | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                            borrowObj.ID, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.BookBelonging.ID, bookBorrowObj.BookBelonging.Author_Publisher, bookBorrowObj.BookBelonging.Title
                            );
                    }
                }
            }
            else if (searchColumn == "kunde")
            {
                var searchFor = Program.StringInputFunction("Nach welchen Kunden suchen Sie?: ");
                foreach (var borrowObj in DataLists.BookBorrowedList)
                {
                    if (borrowObj.Customer == searchFor)
                    {
                        Models.BuchExemplar bookBorrowObj = (Models.BuchExemplar)borrowObj.ExemplarBorrowed;
                        Console.WriteLine("Ausleih ID | Kunde | Ausgeliehen | Abgabe | Exemplar ID | Buch ID |Author | Titel");
                        Console.WriteLine(
                            "{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}",
                            borrowObj.ID, borrowObj.Customer, borrowObj.StartBorrowDate, borrowObj.EndBorrowDate, bookBorrowObj.ID, bookBorrowObj.BookBelonging.ID, bookBorrowObj.BookBelonging.Author_Publisher, bookBorrowObj.BookBelonging.Title
                            );
                    }
                }
            }
            else
                Console.WriteLine("Solch eine Spalte existiert nicht!");
            Program.BorderLine();
        }

        public void AddMagazineBorrow()
        {
            Models.Ausleihe e = new Models.Ausleihe()
            {
                ID = ++DataLists.IC.HighestBorrowID,
                Customer = Program.StringInputFunction("Geben Sie die Kundendaten an: "),
                StartBorrowDate = DateTime.Now,
                EndBorrowDate = DateTime.Now.AddDays(2),
            };
            var exist = false;
            var exemplaryID = Program.IntInputFunction("Geben Sie die ID des dazugehörigen Exemplares an: ");
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
                    e.ExemplarBorrowed = obj;
                    obj.IsBorrowed = true;
                    DataLists.BookBorrowedList.Add(e);
                    exist = true;
                    WriteAndReadFile.WriteICJson();
                    WriteAndReadFile.WriteBorrowJson();
                    Console.Clear();
                    DisplaySpecificBorrow(true);
                    Console.WriteLine("Erfolgreich hinzugefügt!");
                    break;
                }
            }
            if (!exist)
                Console.WriteLine("Diese ExemplarID existiert nicht!");
            Program.BorderLine();
        }
        #endregion
        #endregion

        #region Start
        public void ProofExistingFile()
        {
            if (!File.Exists("Files/IC.json"))
            {
                DataLists.IC = new Models.IDBookmark()
                {
                    HighestBookID = 0,
                    HighestBookExemplaryID = 0,
                    HighestBorrowID = 0,
                };
                WriteAndReadFile.WriteICJson();
                WriteAndReadFile.ReadBookJson();
                AddBookID();
                CreateFirstExemplaries();
                CreateBorrowJson();
                WriteAndReadFile.WriteICJson();
            }
            else
            {
                WriteAndReadFile.ReadICJson();
                WriteAndReadFile.ReadBookJson();
                WriteAndReadFile.ReadBookExemplaryJson();
                WriteAndReadFile.ReadBorrowJson();
            }
        }
        public void AddBookID()
        {
            foreach (var bookObj in DataLists.Magazines)
            {
                DataLists.IC.HighestBookID++;
                bookObj.ID = DataLists.IC.HighestBookID;
            }
            WriteAndReadFile.WriteBookJson();
        }
        public void CreateFirstExemplaries()
        {
            DataLists.BookExemplaries = new List<Models.BuchExemplar>();
            foreach (var bookObj in DataLists.Books)
            {
                DataLists.IC.HighestBookExemplaryID++;
                Models.BuchExemplar e1 = new Models.BuchExemplar()
                {
                    ID = DataLists.IC.HighestBookExemplaryID,
                    IsBorrowed = false,
                    BookBelonging = bookObj,
                };
                DataLists.BookExemplaries.Add(e1);
                DataLists.IC.HighestBookExemplaryID++;
                DataLists.BookExemplaries.Add(
                    new Models.BuchExemplar
                    {
                        ID = DataLists.IC.HighestBookExemplaryID,
                        IsBorrowed = false,
                        BookBelonging = bookObj,
                    });
            }
            WriteAndReadFile.WriteBookExemplaryJson();
        }
        public void CreateBorrowJson()
        {
            DataLists.BookBorrowedList = new List<Models.Ausleihe>();
            WriteAndReadFile.WriteBorrowJson();
        }
        #endregion
    }
}
