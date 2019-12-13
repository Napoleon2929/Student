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
        public PlayingRoom()
        {
            InitializeComponent();
            
            for (var i = 0; i < 100; i++)
            {
                QuestionsGrid.RowDefinitions.Add(new RowDefinition());
                var checkbox = new CheckBox() { ClassId = $"c{i}" };
                var text = new Label() { Text = "RecRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordRecordord" + i.ToString() };
                QuestionsGrid.Children.Add(checkbox);
                QuestionsGrid.Children.Add(text);
                Grid.SetColumn(checkbox, 0);
                Grid.SetColumn(text, 1);
                Grid.SetRow(checkbox, i);
                Grid.SetRow(text, i);
            }    
        }
        public PlayingRoom(QuestionsToView questionsTo)
        {
            TaskText.Text = questionsTo.Task;
            for (var i = 0; i < questionsTo.Answers.Length; i++)
            {
                QuestionsGrid.RowDefinitions.Add(new RowDefinition());
                var checkbox = new CheckBox() { ClassId = $"c{i}" };
                var text = new Label() { Text = questionsTo.Answers[i] };
                QuestionsGrid.Children.Add(checkbox);
                QuestionsGrid.Children.Add(text);
                Grid.SetColumn(checkbox, 0);
                Grid.SetColumn(text, 1);
                Grid.SetRow(checkbox, i);
                Grid.SetRow(text, i);
            }
        }
    }
}