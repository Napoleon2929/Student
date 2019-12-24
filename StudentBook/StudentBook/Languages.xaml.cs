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
        //StudentDBEntity studentDB = new StudentDBEntity("task.db");
        public Languages()
        {
            InitializeComponent();
            var table = Singleton.StudentDB.GetLanguagesTable();
            for (var i = 0; i < table.Count; i++)
            {
                var radioButton = new Button() {
                    ClassId = $"{table[i].ID}",
                    Text = $"{table[i].Name}",
                    HeightRequest = 50,
                    BackgroundColor = Color.Transparent,
                    FontSize = Device.GetNamedSize(NamedSize.Medium,typeof(Label)),
                    TextColor = Color.White,
                    BorderColor = Color.Black,
                    Margin = new Thickness(0,5,0,5),
                    BorderWidth = 1};
                if (table[i].ID == Singleton.Parametrs.Language)
                    radioButton.TextColor = Color.Orange;
                radioButton.Clicked += RadioButton_Clicked;
                LanguagesGrid.Children.Add(radioButton);
                //Grid.SetColumn(radioButton, 0);
                //Grid.SetRow(radioButton, i);
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