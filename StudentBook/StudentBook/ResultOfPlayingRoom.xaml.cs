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
    public partial class ResultOfPlayingRoom : ContentPage
    {
        public ResultOfPlayingRoom()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ToMenuButton.Text = Resx.AppResources.ToMenuButton;
            checkAns.Text = Resx.AppResources.CheckAnswerButton;
            stateLabel.Text = "Correct answers " + Singleton.Quiz.CorrectQuestions.Count + " from " + Singleton.Quiz.Questions.Count;
        }

        private async void CheckAnswers_Clicked(object sender, EventArgs e)
        {
            checkAns.IsEnabled = false;
            await Navigation.PushAsync(new AnswersPage());
            checkAns.IsEnabled = true;
        }

        private async void ToMainMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}