using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

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
                var text = new Label() { Text = "Record" + i.ToString() };
                text.Parent = checkbox;
                QuestionsGrid.Children.Add(checkbox);
                Grid.SetRow(text, i);
            }
                
        }
    }
}