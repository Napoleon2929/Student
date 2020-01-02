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
    public partial class AnswersPage : ContentPage
    {
        private QuestionsToView questionsToView;
        private List<CheckBox> checks;
        private bool isSingle;
        public AnswersPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Update();
        }

        private void Update()
        {
           
        }

    }
}