using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountOfQuestions : ContentPage
    {
        private int questionCount;
        public CountOfQuestions()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            InitializeComponent();
            questionCount = CheckQuestions();
            while (questionCount < Singleton.Parametrs.Count)
            {
                Singleton.Parametrs.Count -= 5;
            }
               
            nQuestions.Text = Resx.AppResources.InDB + questionCount + Resx.AppResources.NowUsed + Singleton.Parametrs.Count + Resx.AppResources.Questions;
        }
        private void Clicked5(object sender, EventArgs e) => Number(5);
        private void Clicked10(object sender, EventArgs e) => Number(10);
        private void Clicked15(object sender, EventArgs e) => Number(15);
        private void Clicked20(object sender, EventArgs e) => Number(20);
        private void Number(int n)
        {
            if (questionCount >= n)
            {
                Singleton.Parametrs.Count = n;
                nQuestions.Text = Resx.AppResources.InDB + questionCount + Resx.AppResources.NowUsed + Singleton.Parametrs.Count + Resx.AppResources.Questions;
                Parametrs.SetParametrs(Singleton.Parametrs);
            }
            else
                DisplayAlert(Resx.AppResources.Warning, Resx.AppResources.CheckQuestions, "OK!");
        }
        private int CheckQuestions()
        {
            List<QuestionsToView> questionsToViews = Singleton.StudentDB.GetQuestionsRange(Singleton.StudentDB.GetQuestionsTable(), Singleton.Parametrs.Language);
            List<QuestionsToView> questions = new List<QuestionsToView>();
            for (var i = 0; i < Singleton.Parametrs.TopicsFilter.Count; i++)
            {
                questions.AddRange(questionsToViews.Where(q => q.TopicID == Singleton.Parametrs.TopicsFilter[i].ID));
            }
            return questions.Count;
        }
    }
}