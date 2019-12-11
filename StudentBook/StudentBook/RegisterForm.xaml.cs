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
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            //Navigation.InsertPageBefore(new MainPage(), this); 
            //await Navigation.PopAsync().ConfigureAwait(false);
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}