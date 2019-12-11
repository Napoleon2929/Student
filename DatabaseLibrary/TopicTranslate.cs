using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    public class TopicTranslate
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        
        [ForeignKey(typeof(Topics))]
        public int TopicID { get; set; }
        public string Name { get; set; }

        [ForeignKey(typeof(Languages))]
        public string LanguageID { get; set; }
    }
}
