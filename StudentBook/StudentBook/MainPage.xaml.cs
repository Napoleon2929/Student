using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using System.Net.Http;
using System.Threading;

namespace StudentBook
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        StudentDBEntity studentDB = new StudentDBEntity("task.db");
        public MainPage()
        {
            InitializeComponent();
            //GetLanguages();
            PlayButton.Text = Resx.AppResources.PlayButton;
        }
        public async void GetLanguages()
        {
            StudentDBEntity studentDB = new StudentDBEntity("task.db");
            try
            {

                var table = studentDB.GetLanguagesTable();
                var result = "";
                foreach (var str in table)
                    result += $"{str.ID}\n";
                await DisplayAlert("db", result, "ok");
            }
            catch (SQLite.SQLiteException)
            {
                await DisplayAlert("error", "Can not find datebase", "ok");
                //GetLanguages();
            }
        }
        private async void Settings_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new Settings());
        }
        private async void Play_Clicked(object sender, EventArgs e)
        {
            SelectQuestions();
            await Navigation.PushModalAsync(new PlayingRoom());
        }
        private async void SelectQuestions()
        {
            int count = Singleton.Parametrs.Count;
            Singleton.Quiz = new Quiz(count);
            List<QuestionsToView> questionsToViews = studentDB.GetQuestionsRange(studentDB.GetQuestionsTable(), Singleton.Parametrs.Language);
            List<QuestionsToView> questions = new List<QuestionsToView>();
            for (var i = 0; i < count; i++)
            {
                questions.AddRange(questionsToViews.Where(q => q.TopicID == Singleton.Parametrs.TopicsFilter[i].ID));
            }
            questionsToViews = null;
            if (questions.Count == 0)
            {
                await DisplayAlert("Warning!", "Don't have questions for your filters", "OK!");
                return;
            }
            if (questions.Count <= count)
            {
                Singleton.Quiz = new Quiz(questions);
            }
            List<QuestionsToView> pairs = new List<QuestionsToView>();
            Random random = new Random();
            for (var i = 0; i < count; i++)
            {
                var index = random.Next() % questions.Count;
                bool isUnique = true;
                foreach (var item in pairs)
                {
                    if (item.ID == questions[index].ID)
                    {
                        isUnique = false;
                        break;
                    }
                }
                if (!isUnique)
                {
                    i--;
                    continue;
                }
                pairs.Add(questions[index]);
            }
            Singleton.Quiz = new Quiz(pairs);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
