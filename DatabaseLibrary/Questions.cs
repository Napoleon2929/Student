using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    public class Questions
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }

        [ForeignKey(typeof(Topics))]
        public int TopicID { get; set; }
        public string Task { get; set; }
        public string CorrectAnswer { get; set; }
        public string Answers { get; set; }

        [OneToMany]
        public virtual List<QuestionTranslate> QuestionTranslates { get; set; }
    }
}
