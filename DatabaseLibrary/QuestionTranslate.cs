using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    public class QuestionTranslate
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }

        [ForeignKey(typeof(Questions))]
        public int QuestionID { get; set; }
        public string Task { get; set; }
        public string Answers { get; set; }

        [ForeignKey(typeof(Languages))]
        public string LanguageID { get; set; }
    }
}
