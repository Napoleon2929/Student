using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using Xamarin.Forms;

namespace DatabaseLibrary
{
    public class StudentDBEntity
    {
        public SQLiteConnection database;
        public StudentDBEntity(string connection)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(connection);
            database = new SQLiteConnection(databasePath);
        }
        //get tables
        public List<Languages> GetLanguagesTable()
        {
            return database.Table<Languages>().ToList();
        }
        public List<Subjects> GetSubjectsTable()
        {
            return database.Table<Subjects>().ToList();
        }
        public List<Topics> GetTopicsTable()
        {
            return database.Table<Topics>().ToList();
        }
        public List<Questions> GetQuestionsTable()
        {
            return database.Table<Questions>().ToList();
        }
        public List<QuestionTranslate> GetQuestionsTranslateTable()
        {
            return database.Table<QuestionTranslate>().ToList();
        }
        // get translate
        public SubjectsToView GetSubjects(int id, string language)
        {
            var data = database.Table<SubjectTranslate>().FirstOrDefault(s => s.LanguageID.Equals(language) && s.SubjectID == id);
            if (data == null)
            {
                var subject = database.Table<Subjects>().FirstOrDefault(s => s.ID == id);
                return new SubjectsToView(subject);
            }
            return new SubjectsToView(data);
        }
        public List<SubjectsToView> GetSubjectsRange(List<Subjects> subjects, string language)
        {
            List<SubjectsToView> toViews = new List<SubjectsToView>();
            foreach (var item in subjects)
                toViews.Add(GetSubjects(item.ID, language));
            return toViews;
        }

        public TopicsToView GetTopics(int id, string language)
        {
            var data = database.Table<TopicTranslate>().FirstOrDefault(s => s.LanguageID.Equals(language) && s.TopicID == id);
            var topic = database.Table<Topics>().FirstOrDefault(s => s.ID == id);
            if (data == null)
                return new TopicsToView(topic);
            return new TopicsToView(topic, data);
        }
        public List<TopicsToView> GetTopicsRange(List<Topics> topics, string language)
        {
            List<TopicsToView> toViews = new List<TopicsToView>();
            foreach (var item in topics)
                toViews.Add(GetTopics(item.ID, language));
            return toViews;
        }

        public QuestionsToView GetQuestions(int id, string language)
        {
            var data = database.Table<QuestionTranslate>().FirstOrDefault(s => s.LanguageID.Equals(language) && s.QuestionID == id);
            var question = database.Table<Questions>().FirstOrDefault(s => s.ID == id);
            if (data == null)
                return new QuestionsToView(question);
            return new QuestionsToView(question, data);
        }
        public List<QuestionsToView> GetQuestionsRange(List<Questions> questions, string language)
        {
            List<QuestionsToView> toViews = new List<QuestionsToView>();
            foreach (var item in questions)
                toViews.Add(GetQuestions(item.ID, language));
            return toViews;
        }
    }
}

