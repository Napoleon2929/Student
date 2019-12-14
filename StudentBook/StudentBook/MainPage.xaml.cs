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
        private void SelectQuestions()
        {

            Singleton.Quiz = new Quiz(Singleton.Parametrs.Count);
            int count = Singleton.Parametrs.Count;
            var questions = studentDB.GetQuestionsTable();
            if (questions.Count <= count)
            {
                Singleton.Quiz = new Quiz(studentDB.GetQuestionsRange(questions, Singleton.Parametrs.Language));
                return;
            }
            List<QuestionsToView> pairs = new List<QuestionsToView>();
            Random random = new Random();
            for (var i = 0; i < count; i++)
            {
                int index = random.Next() % Singleton.Parametrs.TopicsFilter.Count;
                var topic = Singleton.Parametrs.TopicsFilter[index];
                var list = questions.Where(q => q.TopicID == topic.ID).ToList();
                var toView = studentDB.GetQuestionsRange(list, Singleton.Parametrs.Language);
                var newindex = random.Next() % toView.Count;
                bool isUnique = true;
                foreach (var item in pairs)
                {
                    if (item.ID == toView[newindex].ID)
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
                pairs.Add(toView[newindex]);
            }
            Singleton.Quiz = new Quiz(pairs);
            //return pairs.Count;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
