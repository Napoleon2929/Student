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
        public SQLiteAsyncConnection database;
        public StudentDBEntity(string connection)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(connection);
            database = new SQLiteAsyncConnection(databasePath);
        }
        //get tables
        public async Task<List<Languages>> GetLanguagesTable()
        {
            return await database.Table<Languages>().ToListAsync();
        }
        public async Task<List<Subjects>> GetSubjectsTable()
        {
            return await database.Table<Subjects>().ToListAsync();
        }
        public async Task<List<Topics>> GetTopicsTable()
        {
            return await database.Table<Topics>().ToListAsync();
        }
        public async Task<List<Questions>> GetQuestionsTable()
        {
            return await database.Table<Questions>().ToListAsync();
        }
        public async Task<List<QuestionTranslate>> GetQuestionsTranslateTable()
        {
            return await database.Table<QuestionTranslate>().ToListAsync();
        }
        // get translate
        public async Task<SubjectsToView> GetSubjects(int id, string language)
        {
            var data = await database.Table<SubjectTranslate>().FirstOrDefaultAsync(s => s.LanguageID.Equals(language) && s.SubjectID == id);
            if (data == null)
            {
                var subject = await  database.Table<Subjects>().FirstOrDefaultAsync(s => s.ID == id);
                return new SubjectsToView(subject);
            }
            return new SubjectsToView(data);
        }
        public async Task<TopicsToView> GetTopics(int id, string language)
        {
            var data = await database.Table<TopicTranslate>().FirstOrDefaultAsync(s => s.LanguageID.Equals(language) && s.TopicID == id);
            var topic = await database.Table<Topics>().FirstOrDefaultAsync(s => s.ID == id);
            if (data == null)
                return new TopicsToView(topic);
            return new TopicsToView(topic, data);
        }
        public async Task<QuestionsToView> GetQuestions(int id, string language)
        {
            var data = await database.Table<QuestionTranslate>().FirstOrDefaultAsync(s => s.LanguageID.Equals(language) && s.QuestionID == id);
            var question = await database.Table<Questions>().FirstOrDefaultAsync(s => s.ID == id);
            if (data == null)
                return new QuestionsToView(question);
            return new QuestionsToView(question, data);
        }
    }
}

