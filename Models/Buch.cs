using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class Buch : IProdukt
    {
        public int ID { get; set; }
        public string Author_Publisher { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }
        public int Year { get; set; }
        public Buch()
        {
            AddId();
            /*AddAuthor();
            AddTitle();
            AddPages();
            AddCountry();
            AddLanguage();
            AddLink();
            AddImageLink();
            AddYear();*/
        }
        public void AddId()
        {
            ID = ++Controller.DataLists.IC.HighestProductId;
        }
        public void AddAuthor()
        {
            Author_Publisher = Program.StringInputFunction("Autor:");
        }
        public void AddTitle()
        {
            Title = Program.StringInputFunction("Titel:");
        }
        public void AddPages()
        {
            Pages = Program.IntInputFunction("Seiten:");
        }
        public void AddCountry()
        {
            Country = Program.StringInputFunction("Land:");
        }
        public void AddLanguage()
        {
            Language = Program.StringInputFunction("Sprache:");
        }
        public void AddLink()
        {
            Link = Program.StringInputFunction("Link:");
        }
        public void AddImageLink()
        {
            ImageLink = Program.StringInputFunction("Bildlink:");
        }
        public void AddYear()
        {
            Year = Program.IntInputFunction("Jahr:");
        }
    }
}
