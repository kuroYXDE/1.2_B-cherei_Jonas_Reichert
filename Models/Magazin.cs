using System;
using System.Collections.Generic;
using System.Text;

namespace _1._2_Bücherei_Jonas_Reichert.Models
{
    class Magazin : IProdukt
    {
        public int ID { get; set; }
        public string Author_Publisher { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }
        public string Topic { get; set; }
        public Magazin()
        {
            AddId();
            /*AddAuthor();
            AddTitle();
            AddGroup();
            AddTopic();*/
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
        public void AddGroup()
        {
            Group = Program.StringInputFunction("Gruppe:");
        }
        public void AddTopic()
        {
            Topic = Program.StringInputFunction("Thema:");
        }
    }
}
