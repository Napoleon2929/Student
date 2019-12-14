using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using DatabaseLibrary.Models;

namespace StudentBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayingRoom : ContentPage
    {
        private QuestionsToView questionsToView;
        private List<CheckBox> checks;

        public PlayingRoom()
        {
            InitializeComponent();
            checks = new List<CheckBox>();
            questionsToView = Singleton.Quiz.Questions[Singleton.Quiz.CurrentPosition];
            Number.Text = (Singleton.Quiz.CurrentPosition + 1).ToString();
            Correct.Text = $"{Singleton.Quiz.CorrectQuestions.Count}/{Singleton.Quiz.Questions.Count}";
            TaskText.Text = questionsToView.Task;
            for (var i = 0; i < questionsToView.Answers.Length; i++)
            {
                QuestionsGrid.RowDefinitions.Add(new RowDefinition());
                var checkbox = new CheckBox() { ClassId = $"{i}" };
                checkbox.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                checks.Add(checkbox);

                var text = new Label () { Text = questionsToView.Answers[i] };
                TapGestureRecognizer touch = new TapGestureRecognizer();
                text.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                touch.Tapped += Touch_Tapped;
                text.GestureRecognizers.Add(touch);

                QuestionsGrid.Children.Add(checkbox);
                QuestionsGrid.Children.Add(text);

                Grid.SetColumn(checkbox, 0);
                Grid.SetColumn(text, 1);
                Grid.SetRow(checkbox, i);
                Grid.SetRow(text, i);
            }
        }

        private void Touch_Tapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            int position = Grid.GetRow(label);
            checks[position].IsChecked = !checks[position].IsChecked;
        }

        private async void Answer_Clicked(object sender, EventArgs e)
        {
            var check = new List<int>();
            foreach(var item in checks)
            {
                if (item.IsChecked)
                    check.Add(int.Parse(item.ClassId));
            }
            if (check.Count == 0)
                return;
            Singleton.Quiz.Answer(questionsToView.CheckAnswers(check.ToArray()));
            if (Singleton.Quiz.CurrentPosition == -1)
            {
                await DisplayAlert("Message", "You have ended for all questions", "OK");
                await Navigation.PushModalAsync(new MainPage());
            }
            else
                await Navigation.PushModalAsync(new PlayingRoom());
            //await DisplayAlert("result", $"{questionsToView.CheckAnswers(answers.ToArray())}\nYou have answered {answers.ToArray()}", "ok");
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}