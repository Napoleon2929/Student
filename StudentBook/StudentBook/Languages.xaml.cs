using DatabaseLibrary;
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
        StudentDBEntity studentDB = new StudentDBEntity("task.db");
        public Languages()
        {
            InitializeComponent();
            var table = studentDB.GetLanguagesTable();
            for (var i = 0; i < table.Count; i++)
            {
                LanguagesGrid.RowDefinitions.Add(new RowDefinition());
                var radioButton = new Button() {
                    ClassId = $"{table[i].ID}",
                    Text = $"{table[i].Name}",
                    HeightRequest = 50,
                    BackgroundColor = Color.Transparent,
                    FontSize = 40,
                    TextColor = Color.White,
                    CornerRadius = 30 };
                if (table[i].ID == Singleton.Parametrs.Language)
                    radioButton.BackgroundColor = Color.Red;
                radioButton.Clicked += RadioButton_Clicked;
                LanguagesGrid.Children.Add(radioButton);
                Grid.SetColumn(radioButton, 0);
                Grid.SetRow(radioButton, i);
            }
        }

        private async void RadioButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Singleton.Parametrs.Language = button.ClassId;
            Parametrs.SetParametrs(Singleton.Parametrs);
            await Navigation.PopModalAsync();
        }
    }
}