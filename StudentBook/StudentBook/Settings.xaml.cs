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
    public partial class Settings : ContentPage
    {
        private Parametrs parametrs;
        private IParams iParams = DependencyService.Get<IParams>();
        public Settings()
        {
            InitializeComponent();
            parametrs = iParams.GetParametrs();
            Sounds.IsToggled = parametrs.Sounds;
            Notices.IsToggled = parametrs.Notices;
            Disappearing += Settings_Disappearing;
        }

        private void Settings_Disappearing(object sender, EventArgs e)
        {
            parametrs.Sounds = Sounds.IsToggled;
            parametrs.Notices = Notices.IsToggled;
            iParams.SetParametrs(parametrs);
        }
        private async void SelectSubject(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Subjects());
        }
        private async void SelectLanguage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Languages());
        }
    }
}