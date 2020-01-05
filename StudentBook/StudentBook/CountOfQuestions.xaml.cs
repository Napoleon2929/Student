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
        
        public CountOfQuestions()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            InitializeComponent();
            CheckQuestions();
            while(Singleton.Quiz.Questions.Count < Singleton.Parametrs.Count)
            {
                Singleton.Parametrs.Count -= 5;
            }
               
            nQuestions.Text = "In database exists " + Singleton.Quiz.Questions.Count + " questions. Now used " + Singleton.Parametrs.Count + " questions";
        }
        private void Clicked5(object sender, EventArgs e) => Number(5);
        private void Clicked10(object sender, EventArgs e) => Number(10);
        private void Clicked15(object sender, EventArgs e) => Number(15);
        private void Clicked20(object sender, EventArgs e) => Number(20);
        private void Number(int n)
        {
            CheckQuestions();
            if (Singleton.Quiz.Questions.Count >= n)
            {
                Singleton.Parametrs.Count = n;
                nQuestions.Text = "In database exists " + Singleton.Quiz.Questions.Count + " questions. Now used " + Singleton.Parametrs.Count + " questions";
                Parametrs.SetParametrs(Singleton.Parametrs);
            }
            else
                DisplayAlert("Warning!", "Don't touch this button", "idk sry(((9!");
        }
        private void CheckQuestions()
        {
            List<QuestionsToView> questionsToViews = Singleton.StudentDB.GetQuestionsRange(Singleton.StudentDB.GetQuestionsTable(), Singleton.Parametrs.Language);
            List<QuestionsToView> questions = new List<QuestionsToView>();
            for (var i = 0; i < Singleton.Parametrs.TopicsFilter.Count; i++)
            {
                questions.AddRange(questionsToViews.Where(q => q.TopicID == Singleton.Parametrs.TopicsFilter[i].ID));
            }
            Singleton.Quiz = new Quiz(questions);
        }
    }
}