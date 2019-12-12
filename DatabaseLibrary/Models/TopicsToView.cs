using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLibrary.Models
{
    public class TopicsToView
    {
        public int ID { get; set; }
        public int SubjectID { get; set; }
        public string Name { get; set; }

        public TopicsToView() { }

        public TopicsToView(Topics topics)
        {
            ID = topics.ID;
            SubjectID = topics.SubjectID;
            Name = topics.Name;
        }
        public TopicsToView(Topics topics, TopicTranslate translate)
            :this(topics)
        {
            Name = translate.Name;
        }
    }
}
