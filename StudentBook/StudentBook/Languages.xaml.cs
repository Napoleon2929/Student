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
    public partial class Languages : ContentPage
    {
        public Languages()
        {
            InitializeComponent();
            for (var i = 0; i < 15; i++)
            {
                LanguagesGrid.RowDefinitions.Add(new RowDefinition());
                var radioButton = new Button() { ClassId = $"c{i}",Text = "Language",HeightRequest=50,BackgroundColor = Color.Transparent,FontSize=40,TextColor = Color.White,CornerRadius = 30 };
                LanguagesGrid.Children.Add(radioButton);
                Grid.SetColumn(radioButton, 0);
                Grid.SetRow(radioButton, i);
            }
        }
    }
}