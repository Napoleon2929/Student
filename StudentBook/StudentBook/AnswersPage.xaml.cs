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
        private int position;

        public AnswersPage(int pos)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
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

            questionsToView = Singleton.Quiz.UncorrectQuestions[position];
            checks = new List<CheckBox>();
            var numberText = position + 1;
            Number.Text = numberText.ToString();
            Correct.Text = $"{numberText.ToString()}/{Singleton.Quiz.UncorrectQuestions.Count}";
            TaskText.Text = questionsToView.Task;
            QuestionsGrid.Children.Clear();
            QuestionsGrid.RowDefinitions = new RowDefinitionCollection();
            isSingle = questionsToView.CorrectAnswer.Length == 1;
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
            foreach (var index in questionsToView.CorrectAnswer)
                checks[index].IsChecked = true;
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

    }
}