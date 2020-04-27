using System;
using System.Collections.Generic;

namespace _1._2_Bücherei_Jonas_Reichert
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.ProgramLogic pL = new Controller.ProgramLogic();
            pL.ProofExistingFile();
            ProgramNavigation(pL);
        }

        static private void ProgramNavigation(Controller.ProgramLogic pL)
        {
            while (true)
            {
                var input = IntInputFunction(
                "_______________________________\n" +
                "0: Wenig Informationen         |\n" +
                "1: Alle Informationen          |\n" +
                "2: Spezielles Objekt anzeigen  |\n" +
                "4: Hinzufügen                  |\n" +
                "5: Bearbeiten                  |\n" +
                "6: Entfernen                   |\n" +
                "-------------------------------|\n" +
                "1: Buch                        |\n" +
                "2: Buch Exemplar               |\n" +
                "3: Buch Ausleihe               |\n" +
                "4: Magazin                     |\n" +
                "5: Magazin Exemplar            |\n" +
                "6: Magazin Ausleihe            |\n" +
                "-------------------------------|\n" +
                "000: Help                      |\n" +
                "666: Exit Program              |\n" +
                "_______________________________|");
                switch (input)
                {
                    case 01:
                        pL.DisplayBooksSimple();
                        break;
                    case 11:
                        pL.DisplayBooksExtended();
                        break;
                    case 12:
                        pL.DisplayBookExemplaries();
                        break;
                    case 13:
                        pL.DisplayBooksBorrowed();
                        break;
                    case 14:
                        pL.DisplayMagazine();
                        break;
                    case 15:
                        pL.DisplayMagazineExemplaries();
                        break;
                    case 16:
                        pL.DisplayMagazinesBorrowed();
                        break;
                    case 21:
                        pL.DisplaySpecificBook(false);
                        break;
                    case 22:
                        pL.DisplaySpecificBookExemplary(false);
                        break;
                    case 23:
                        pL.DisplaySpecificBookBorrow(false);
                        break;
                    case 24:
                        pL.DisplaySpecificMagazine(false);
                        break;
                    case 25:
                        pL.DisplaySpecificMagazineExemplary(false);
                        break;
                    case 26:
                        pL.DisplaySpecificMagazineBorrow(false);
                        break;
                    case 41:
                        pL.AddBook();
                        break;
                    case 42:
                        pL.AddBookExemplary();
                        break;
                    case 43:
                        pL.AddBookBorrow();
                        break;
                    case 44:
                        pL.AddMagazine();
                        break;
                    case 45:
                        pL.AddMagazineExemplary();
                        break;
                    case 46:
                        pL.AddMagazineBorrow();
                        break;
                    case 51:
                        pL.EditBook();
                        break;
                    case 52:
                        pL.EditBookExemplary();
                        break;
                    case 53:
                        pL.EditBookBorrow();
                        break;
                    case 54:
                        pL.EditMagazine();
                        break;
                    case 55:
                        pL.EditMagazineExemplary();
                        break;
                    case 56:
                        pL.EditMagazineBorrow();
                        break;
                    case 61:
                        pL.RemoveBook();
                        break;
                    case 62:
                        pL.RemoveBookExemplary();
                        break;
                    case 63:
                        pL.RemoveBookBorrow();
                        break;
                    case 64:
                        pL.RemoveMagazine();
                        break;
                    case 65:
                        pL.RemoveMagazineExemplary();
                        break;
                    case 66:
                        pL.RemoveMagazineBorrow();
                        break;
                    case 000:
                        ShowHelp();
                        break;
                    case 666:
                        Controller.WriteAndReadFile.WriteICJson();
                        Controller.WriteAndReadFile.WriteBookJson();
                        Controller.WriteAndReadFile.WriteBookExemplaryJson();
                        Controller.WriteAndReadFile.WriteBookBorrowJson();
                        Controller.WriteAndReadFile.WriteMagazineJson();
                        Controller.WriteAndReadFile.WriteMagazineExemplaryJson();
                        Controller.WriteAndReadFile.WriteMagazineBorrowJson();
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Geben Sie einen gültigen Wert ein!");
                        break;
                }
            }
        }
        #region Inputs
        static public string StringInputFunction(string message)
        {
            Console.WriteLine(message);
            Console.Write("> ");
            return Console.ReadLine();
        }
        static public string StringInputFunction(string message, string sameValue)
        {
            Console.WriteLine(message);
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input =="")
            {
                return sameValue;
            }
            return input;
        }
        static public int IntInputFunction(string message)
        {
            bool convertSuccess = false;
            var input = 0;
            while (!convertSuccess)
            {
                Console.WriteLine(message);
                Console.Write("> ");
                var x = Console.ReadLine();
                convertSuccess  = Int32.TryParse(x, out input);
            }
            return input;
        }
        static public int IntInputFunction(string message, int sameValue)
        {
            bool convertSuccess = false;
            var y = 0;
            while (!convertSuccess)
            {
                Console.WriteLine(message);
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input == "")
                {
                    return sameValue;
                }
                convertSuccess  = Int32.TryParse(input, out y);
            }
            return y;
        }
        static public bool BoolInputFunction(string message)
        {
            var convertSuccess = false;
            var input = false;
            while (!convertSuccess)
            {
                Console.WriteLine(message);
                Console.Write("> ");
                var x = Console.ReadLine();
                convertSuccess = Boolean.TryParse(x, out input);
            }
            return input;
        }
        #endregion
        static private void ShowHelp()
        {
            Console.WriteLine("Some help!");
        }
        static public void BorderLine()
        {
            Console.WriteLine("==============================");
        }
    }
}
