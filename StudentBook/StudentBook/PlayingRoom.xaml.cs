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
        private bool isSingle;
        public PlayingRoom()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {
            questionsToView = Singleton.Quiz.Questions[Singleton.Quiz.CurrentPosition];
            checks = new List<CheckBox>();
            Number.Text = (Singleton.Quiz.CurrentPosition + 1).ToString();
            Correct.Text = $"{Singleton.Quiz.CorrectQuestions.Count}/{Singleton.Quiz.Questions.Count}";
            TaskText.Text = questionsToView.Task;
            QuestionsGrid.Children.Clear();
            QuestionsGrid.RowDefinitions = new RowDefinitionCollection();
            isSingle = questionsToView.CorrectAnswer.Length == 1;
            for (var i = 0; i < questionsToView.Answers.Length; i++)
            {
                QuestionsGrid.RowDefinitions.Add(new RowDefinition());
                var checkbox = new CheckBox() { ClassId = $"{i}" };
                checkbox.CheckedChanged += Checkbox_CheckedChanged;
                checkbox.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                checks.Add(checkbox);

                var text = new Label() { Text = questionsToView.Answers[i] };
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

        private void Checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (isSingle)
            {
                isSingle = false;
                var checkbox = sender as CheckBox;
                int position = Grid.GetRow(checkbox);
                for(var i = 0; i < checks.Count; i++)
                {
                    if (checks[i].IsChecked && checks[i] != checkbox)
                        checks[i].IsChecked = false;
                }
                isSingle = true;
                //checkbox.
            }
        }

        private async void Answer_Clicked(object sender, EventArgs e)
        {
            AnswerButton.IsEnabled = false;
            var check = new List<int>();
            foreach(var item in checks)
            {
                if (item.IsChecked)
                    check.Add(int.Parse(item.ClassId));
            }
            if (check.Count == 0)
            {
                AnswerButton.IsEnabled = true;
                return;
            }
            Singleton.Quiz.Answer(questionsToView.CheckAnswers(check.ToArray()));
            if (Singleton.Quiz.CurrentPosition == -1)
            {
                await DisplayAlert("Message", "You have ended for all questions", "OK");
                await Navigation.PushAsync(new ResultOfPlayingRoom());
                //await Navigation.PopToRootAsync();
            }
            else
                UpdateData();
            AnswerButton.IsEnabled = true;
                //await Navigation.PushModalAsync(new PlayingRoom());
                //await DisplayAlert("result", $"{questionsToView.CheckAnswers(answers.ToArray())}\nYou have answered {answers.ToArray()}", "ok");
        }
    }
}