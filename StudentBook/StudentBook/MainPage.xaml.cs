using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DatabaseLibrary;
using System.Net.Http;
using System.Threading;

namespace StudentBook
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetLanguages();
            PlayButton.Text = Resx.AppResources.PlayButton;

            //Navigation.PopModalAsync();
        }
        public async void GetLanguages()
        {
            StudentDBEntity studentDB = new StudentDBEntity("task.db");
            try
            {

                var table = await studentDB.GetLanguagesTable();
                var result = "";
                foreach (var str in table)
                    result += $"{str.ID}\n";
                await DisplayAlert("db", result, "ok");
            }
            catch (SQLite.SQLiteException)
            {
                await DisplayAlert("error", "Can not find datebase", "ok");
                //GetLanguages();
            }
        }
        private async void Settings_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new Settings());
        }
        private async void Play_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PlayingRoom());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
