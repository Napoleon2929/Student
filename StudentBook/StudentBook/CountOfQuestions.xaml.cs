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
            nQuestions.Text = "In database exists " + Singleton.Quiz.Questions.Count + " questions. Now used " + Singleton.Parametrs.Count + " questions";
        }
        private void Clicked5(object sender, EventArgs e) => Number(5);
        private void Clicked10(object sender, EventArgs e) => Number(10);
        private void Clicked15(object sender, EventArgs e) => Number(15);
        private void Clicked20(object sender, EventArgs e) => Number(20);
        private void Number(int n)
        {
            Singleton.Parametrs.Count = n;
            Parametrs.SetParametrs(Singleton.Parametrs);
            CheckQuestions();
            nQuestions.Text = "In database exists " + Singleton.Quiz.Questions.Count + " questions. Now used " + Singleton.Parametrs.Count + " questions";
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