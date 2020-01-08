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
        private int position;
        private AnswerMode answerMode;

        public AnswersPage(int pos)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            nextButton.Text = Resx.AppResources.NextButton;
            previousButton.Text = Resx.AppResources.PreviousButtton;
            position = pos;
            Update();
        }

        private void Update()
        {
            if (position == 0)
                previousButton.IsEnabled = false;
            else
                previousButton.IsEnabled = true;
            if (position == Singleton.Quiz.UncorrectQuestions.Count - 1)
                nextButton.IsEnabled = false;
            else
                nextButton.IsEnabled = true;

            answerMode = AnswerMode.Correct;
            questionsToView = Singleton.Quiz.UncorrectQuestions[position];
            checks = new List<CheckBox>();
            var numberText = position + 1;
            Correct.Text = $"{numberText.ToString()}/{Singleton.Quiz.UncorrectQuestions.Count}";
            TaskText.Text = questionsToView.Task;
            QuestionsGrid.Children.Clear();
            QuestionsGrid.RowDefinitions = new RowDefinitionCollection();
            for (var i = 0; i < questionsToView.Answers.Length; i++)
            {
                QuestionsGrid.RowDefinitions.Add(new RowDefinition());
                var checkbox = new CheckBox() { ClassId = $"{i}", IsEnabled = false };
                checkbox.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                checks.Add(checkbox);

                var text = new Label() { Text = questionsToView.Answers[i] };
                TapGestureRecognizer touch = new TapGestureRecognizer();
                text.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
                text.GestureRecognizers.Add(touch);

                QuestionsGrid.Children.Add(checkbox);
                QuestionsGrid.Children.Add(text);

                Grid.SetColumn(checkbox, 0);
                Grid.SetColumn(text, 1);
                Grid.SetRow(checkbox, i);
                Grid.SetRow(text, i);
            }
            ShowAnswers(answerMode);
        }

        private void ShowAnswers(AnswerMode mode)
        {
            foreach (var check in checks)
                check.IsChecked = false;
            switch (mode)
            {
                case AnswerMode.Correct:
                    ChangeAnswerButton.Text = Resx.AppResources.Showmyanswers;
                    foreach (var index in questionsToView.CorrectAnswer)
                        checks[index].IsChecked = true;
                    break;
                case AnswerMode.Given:
                    ChangeAnswerButton.Text = Resx.AppResources.Showcorrectanswers;
                    foreach (var index in questionsToView.GivenAnswers)
                        checks[index].IsChecked = true;
                    break;
            }
        }

        private void PreviousButton(object sender, EventArgs e)
        {
            position--;
            Update();
        }

        private void NextButton(object sender, EventArgs e)
        {
            position++;
            Update();
        }
        private async void BackButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ChangeAnswerButton_Clicked(object sender, EventArgs e)
        {
            if (answerMode == AnswerMode.Correct)
                answerMode = AnswerMode.Given;
            else
                answerMode = AnswerMode.Correct;
            ShowAnswers(answerMode);
        }
    }
}