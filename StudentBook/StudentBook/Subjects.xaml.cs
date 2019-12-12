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
    public partial class Subjects : ContentPage
    {
        public Subjects()
        {
            InitializeComponent();

            for (var i = 0; i < 15; i++)
            {
                SubjectsGrid.RowDefinitions.Add(new RowDefinition());
                var switchButton = new Switch() { ClassId = $"c{i}" };
                var text = new Button() { Text = "Subject" + i.ToString(),BackgroundColor = Color.Transparent, FontSize = 40, TextColor = Color.White };
                SubjectsGrid.Children.Add(text);
                SubjectsGrid.Children.Add(switchButton);
                Grid.SetColumn(text, 0);
                Grid.SetColumn(switchButton, 1);
                Grid.SetRow(text, i);
                Grid.SetRow(switchButton, i);
            }
        }
    }
}