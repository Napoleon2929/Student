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
    public partial class ResultOfPlayingRoom : ContentPage
    {
        public ResultOfPlayingRoom()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void CheckAnswers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnswersPage());
        }

        private async void ToMainMenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}