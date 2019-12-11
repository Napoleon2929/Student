using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary
{
    public class Subjects
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int ID { get; set; }
        public string Name { get; set; }
        [OneToMany]
        public virtual ICollection<SubjectTranslate> SubjectsTranslates { get; set; }
        [OneToMany]
        public virtual ICollection<Topics> Topics { get; set; }
    }
}
