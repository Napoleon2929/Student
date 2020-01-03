using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using System.Net.Http;
using System.Threading;
using Plugin.Connectivity;
using Xamarin.Forms.Xaml;

namespace StudentBook
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false), XamlCompilation(XamlCompilationOptions.Compile)]
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
        public void UpdateText()
        {
            PlayButton.Text = Resx.AppResources.PlayButton;
        }
        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            UpdateText();
            if (Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopToRootAsync();
                //await DisplayAlert("qw", $"Navigation.NavigationStack.Count", "ok");
            }
            if (!Singleton.IsUpdate)
            {
                Singleton.IsUpdate = true;
                Singleton.StudentDB = new StudentDBEntity("task.db");
                if (!Singleton.StudentDB.IsCorrect)
                {
                    while (true)
                    {
                        if (!CrossConnectivity.Current.IsConnected)
                        {
                            if (await DisplayAlert("Warning!", "You don't have Internet connection for get database", "Try again", "Exit   "))
                                continue;
                            else
                                Environment.Exit(1);
                        }
                        else
                            Singleton.StudentDB = new StudentDBEntity("task.db");    
                        break;
                    }
                }
            }
        } 

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            SettingsButton.IsEnabled = false;
            await Navigation.PushAsync(new Settings());

            SettingsButton.IsEnabled = true;
        }
        private async void Play_Clicked(object sender, EventArgs e)
        {
            PlayButton.IsEnabled = false;
            if (Singleton.Parametrs == null)
                Singleton.Parametrs = Parametrs.GetParametrs();
            if (Singleton.Parametrs.TopicsFilter == null)
                Singleton.Parametrs.SetDefaultFilter();
            if (await SelectQuestions())
                await Navigation.PushAsync(new PlayingRoom());
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
    }
}
