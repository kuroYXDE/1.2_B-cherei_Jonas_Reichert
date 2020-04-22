using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Controller
{
    static class DataLists
    {
        static public List<Models.Buch> Books { get; set; }
        static public List<Models.Magazin> Magazines { get; set; }
        static public List<Models.BuchExemplar> BookExemplaries { get; set; }
        static public List<Models.MagazinExemplar> MagazineExemplaries { get; set; }
        static public List<Models.Ausleihe> BookBorrowedList { get; set; }
        static public List<Models.Ausleihe> MagazineBorrowedList { get; set; }
        static public Models.IDBookmark IC { get; set; }
    }
}
