using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using Xamarin.Forms;

namespace StudentBook
{
    public class Parametrs
    {
        private StudentDBEntity studentDB = new StudentDBEntity("task.db");
        private static IParams iParams = DependencyService.Get<IParams>();
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
            TopicsFilter = new List<TopicsToView>();//= studentDB.GetTopicsRange(studentDB.GetTopicsTable(), Language);
        }
        public void SetDefaultFilter()
        {
            TopicsFilter = studentDB.GetTopicsRange(studentDB.GetTopicsTable(), Language);
        }
        public Parametrs(bool sounds, bool notices, string language, int count, List<TopicsToView> topics)
        {
            Sounds = sounds;
            Notices = notices;
            Language = language;
            Count = count;
            TopicsFilter = topics;
        }
        public static Parametrs GetParametrs() 
        {
            return iParams.GetParametrs();
        }
        public static void SetParametrs(Parametrs parametrs)
        {
            iParams.SetParametrs(parametrs);
        }
    }
}
