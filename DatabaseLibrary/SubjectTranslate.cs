using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    public class SubjectTranslate
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        [ForeignKey(typeof(Subjects))]
        public int SubjectID { get; set; }
        public string Name { get; set; }
        [ForeignKey(typeof(Languages))]
        public string LanguageID { get; set; } 
    }
}
