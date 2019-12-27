using DatabaseLibrary;
using System;
using System.Globalization;
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
            Resx.AppResources.Culture = new CultureInfo(Singleton.Parametrs.Language);
            Login.Text = Resx.AppResources.Login;
            Password.Text = Resx.AppResources.Password;
            SignInButton.Text = Resx.AppResources.SignInButton;

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("wa", Resx.AppResources.Culture.Name, "ok");
            SignInButton.IsEnabled = false;
            await Navigation.PushAsync(new MainPage());
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }
    }
}