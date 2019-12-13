using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLibrary;
using DatabaseLibrary.Models;

namespace StudentBook
{
    public class Parametrs
    {
        private StudentDBEntity studentDB = new StudentDBEntity("task.db");
        public bool Sounds { get; set; }
        public bool Notices { get; set; }
        public string Language { get; set; }
        public int Count { get; set; }
        public List<TopicsToView> TopicsFilter { get; set; }

        public Parametrs()
        {
            Sounds = false;
            Notices = false;
            Language = "en";
            Count = 5;
            GetTopics();
        }
        private void GetTopics()
        {
            var topics = studentDB.GetTopicsTable();
            TopicsFilter = studentDB.GetTopicsRange(topics, Language);
        }
        public Parametrs(bool sounds, bool notices, string language, int count, List<TopicsToView> topics)
        {
            Sounds = sounds;
            Notices = notices;
            Language = language;
            Count = count;
            TopicsFilter = topics;
        }
    }
}
