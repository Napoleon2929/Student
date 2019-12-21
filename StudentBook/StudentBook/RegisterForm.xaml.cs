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
            Login.Text = Resx.AppResources.Login;
            Password.Text = Resx.AppResources.Password;
            SignInButton.Text = Resx.AppResources.SignInButton;

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            SignInButton.IsEnabled = false;
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}