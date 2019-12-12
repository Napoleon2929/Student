using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class SubjectsToView
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SubjectsToView() { }
        public SubjectsToView(Subjects subjects)
        {
            ID = subjects.ID;
            Name = subjects.Name;
        } 
        public SubjectsToView(SubjectTranslate translate)
        {
            ID = translate.ID;
            Name = translate.Name;
        }
    }
}
