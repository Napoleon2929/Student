﻿using System;
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
            checkAns.Text = Resx.AppResources.CheckAnswerButton;
            ToMenuButton.Text = Resx.AppResources.ToMenuButton;
            stateLabel.Text = Resx.AppResources.CorrectAnswers + Singleton.Quiz.CorrectQuestions.Count + Resx.AppResources.From + Singleton.Quiz.Questions.Count;
            checkAns.IsEnabled = Singleton.Quiz.UncorrectQuestions.Count != 0;
        }

        private async void CheckAnswers_Clicked(object sender, EventArgs e)
        {
            checkAns.IsEnabled = false;
            await Navigation.PushAsync(new AnswersPage(0));
            checkAns.IsEnabled = true;
        }

        private async void ToMainMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}