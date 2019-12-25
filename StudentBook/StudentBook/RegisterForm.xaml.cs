using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms.Xaml;


namespace StudentBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterForm
    {
        public RegisterForm()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Login.Text = Resx.AppResources.Login;
            Password.Text = Resx.AppResources.Password;
            SignInButton.Text = Resx.AppResources.SignInButton;

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            //while (true)
            //{
            //    if (!CrossConnectivity.Current.IsConnected)
            //    {
            //        if (await DisplayAlert("Warning!", "You don't have Internet connection", "Try again", "Continue"))
            //            continue;
            //    }
            //    break;
            //}
            SignInButton.IsEnabled = false;
            await Navigation.PushAsync(new MainPage());
            //await Navigation.PushModalAsync(new MainPage());
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }
    }
}