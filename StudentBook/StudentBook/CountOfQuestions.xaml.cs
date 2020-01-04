using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var table = Singleton.StudentDB.GetQuestionsTable();
            nQuestions.Text = "In database exists " + table.Count + " questions. Now used " + Singleton.Parametrs.Count + " questions";
        }
        private void Clicked5(object sender, EventArgs e) => Number(5);
        private void Clicked10(object sender, EventArgs e) => Number(10);
        private void Clicked15(object sender, EventArgs e) => Number(15);
        private void Clicked20(object sender, EventArgs e) => Number(20);
        private void Number(int n)
        {
            Singleton.Parametrs.Count = n;
            Parametrs.SetParametrs(Singleton.Parametrs);
            var table = Singleton.StudentDB.GetQuestionsTable();
            nQuestions.Text = "In database exists " + table.Count + " questions. Now used " + Singleton.Parametrs.Count + " questions";
        }
    }
}