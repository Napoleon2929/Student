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
        //StudentDBEntity studentDB = new StudentDBEntity("task.db");
        private Random random = new Random();
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            PlayButton.Text = Resx.AppResources.PlayButton;
            Appearing += MainPage_Appearing;
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            if (Navigation.ModalStack.Count > 1)
                await DisplayAlert("qw","Двое рабоают семеро х@@ми пашут тут\nИди работай, сука","ok");

        }

        //for delete
        public async void GetLanguages()
        {
            try
            {

                var table = Singleton.StudentDB.GetLanguagesTable();
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
            settingsButton.IsEnabled = false;
            await Navigation.PushModalAsync(new Settings());
            settingsButton.IsEnabled = true;
        }
        private async void Play_Clicked(object sender, EventArgs e)
        {
            PlayButton.IsEnabled = false;
            if (await SelectQuestions())
                await Navigation.PushModalAsync(new PlayingRoom());
            else
                PlayButton.IsEnabled = true;
        }
        private async Task<bool> SelectQuestions()
        {
            int count = Singleton.Parametrs.Count;
            Singleton.Quiz = new Quiz(count);
            List<QuestionsToView> questionsToViews = Singleton.StudentDB.GetQuestionsRange(Singleton.StudentDB.GetQuestionsTable(), Singleton.Parametrs.Language);
            List<QuestionsToView> questions = new List<QuestionsToView>();
            for (var i = 0; i < Singleton.Parametrs.TopicsFilter.Count; i++)
            {
                questions.AddRange(questionsToViews.Where(q => q.TopicID == Singleton.Parametrs.TopicsFilter[i].ID));
            }
            questionsToViews = null;
            if (questions.Count == 0)
            {
                await DisplayAlert("Warning!", "Don't have questions for your filters", "OK!");
                return false;
            }
            if (questions.Count <= count)
            {
                Singleton.Quiz = new Quiz(questions);
                return true;
            }
            List<QuestionsToView> pairs = new List<QuestionsToView>();
            for (var i = 0; i < count; i++)
            {
                var index = random.Next(questions.Count);
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
            return true;
        }
        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
    }
}
