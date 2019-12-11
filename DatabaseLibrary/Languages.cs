using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace DatabaseLibrary
{
    public class Languages
    {
        [PrimaryKey, Unique]
        public string ID { get; set; }
        public string Name { get; set; }
        [OneToMany] 
        public virtual ICollection<SubjectTranslate> SubjectsTranslates { get; set; }
        [OneToMany]
        public virtual ICollection<TopicTranslate> TopicsTranslates { get; set; }
        [OneToMany]
        public virtual ICollection<QuestionTranslate> QuastionTranslates { get; set; }
    }
}
