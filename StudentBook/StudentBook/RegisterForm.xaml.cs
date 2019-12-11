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
    public partial class RegisterForm
    {
        public RegisterForm()
        {
            InitializeComponent();
            GetLanguages();
        }
        public async void GetLanguages()
        {
            try
            {
                var table = await Singleton.studentDB.GetLanguagesTable();
                var result = "";
                foreach (var str in table)
                    result += $"{str.ID}\n";
                await DisplayAlert("db", result, "ok");
            }
            catch (SQLite.SQLiteException)
            {
                //await DisplayAlert("error", "Can not find datebase", "ok");
                //Singleton.studentDB = new StudentDBEntity("task.db");
                GetLanguages();
            }
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            //Navigation.InsertPageBefore(new MainPage(), this); 
            //await Navigation.PopAsync().ConfigureAwait(false);

            await Navigation.PopAsync().ContinueWith((t) => Navigation.PushModalAsync(new MainPage()));
        }
    }
}