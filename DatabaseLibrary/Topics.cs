using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    public class Topics
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }

        [ForeignKey(typeof(Subjects))]
        public int SubjectID { get; set; }
        public string Name { get; set; }
        [OneToMany]
        public List<TopicTranslate> TopicsTranslate { get; set; }
        [OneToMany]
        public List<Questions> Quastions { get; set; }
    }
}
