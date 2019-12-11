using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DatabaseLibrary;
using System.Net.Http;
using Java.Net;
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
            //Navigation.PopModalAsync();
        }
        public async void GetLanguages()
        {
            try
            {
                var table = await Singleton.studentDB.database.Table<Languages>().ToListAsync();
                var result = "";
                foreach (var str in table)
                    result += $"{str.ID}\n";
                await DisplayAlert("db", result, "ok");
            }
            catch (SQLite.SQLiteException)
            {
                //await DisplayAlert("error", "Can not find datebase", "ok");
                Singleton.studentDB = new StudentDBEntity("task.db");
                GetLanguages();
            }
        }
        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Settings());
        }
    }
}
