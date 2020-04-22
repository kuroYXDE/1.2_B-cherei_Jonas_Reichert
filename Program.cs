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
                "10: Bücher anzeigen (Wenig Informationen)\n" +
                "11: Bücher anzeigen (Alle Informationen)\n" +
                "12: Spezielles Buch anzeigen\n" +
                "14: Buch hinzufügen\n" +
                "15: Buch bearbeiten\n" +
                "16: Buch entfernen\n" +
                "21: Exemplare anzeigen\n" +
                "22: Spezielles Exemplar anzeigen\n" +
                "24: Exemplar hinzufügen\n" +
                "25: Exemplar bearbeiten\n" +
                "26: Exemplar entfernen\n" +
                "31: Ausgeliehene Exemplare anzeigen\n" +
                "32: Spezielles Ausgeliehenes Exemplar anzeigen\n" +
                "34: Exemplar ausleihen\n" +
                "35: Ausleihe bearbeiten\n" +
                "36: Ausleihe aufheben\n" +
                "666: Exit Program");
                switch (input)
                {
                    case 10:
                        pL.DisplayBooksSimple();
                        break;
                    case 11:
                        pL.DisplayBooksExtended();
                        break;
                    case 12:
                        pL.DisplaySpecificBook(false);
                        break;
                    case 14:
                        pL.AddBook();
                        break;
                    case 15:
                        pL.EditBook();
                        break;
                    case 16:
                        pL.RemoveBook();
                        break;
                    case 21:
                        pL.DisplayExemplaries();
                        break;
                    case 22:
                        pL.DisplaySpecificExemplary(false);
                        break;
                    case 24:
                        pL.AddExemplary();
                        break;
                    case 25:
                        pL.EditExemplary();
                        break;
                    case 26:
                        pL.RemoveExemplary();
                        break;
                    case 31:
                        pL.DisplayBorrowed();
                        break;
                    case 32:
                        pL.DisplaySpecificBorrow(false);
                        break;
                    case 34:
                        pL.AddBorrow();
                        break;
                    case 35:
                        pL.EditBorrow();
                        break;
                    case 36:
                        pL.RemoveBorrow();
                        break;
                    case 666:
                        System.Environment.Exit(0);
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
        static public void BorderLine()
        {
            Console.WriteLine("==============================");
        }
    }
}
