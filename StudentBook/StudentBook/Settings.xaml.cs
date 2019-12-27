using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        //private Parametrs parametrs;
        public Settings()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            UpdateText();
            Appearing += Settings_Appearing;
            Disappearing += Settings_Disappearing;
        }
        private void UpdateText()
        {
            try
            {
                SoundEffects.Text = Resx.AppResources.SoundEffects;
                Notice.Text = Resx.AppResources.Notice;
                Subjects.Text = Resx.AppResources.Subjects;
                Languages.Text = Resx.AppResources.Languages;
                CountOfQuestions.Text = Resx.AppResources.CountOfQuestions;
            }
            catch (CultureNotFoundException)
            {
                //Resx.AppResources.Culture = new CultureInfo();
            }
        }
        private void Settings_Appearing(object sender, EventArgs e)
        {
            UpdateText();
            Singleton.Parametrs = Parametrs.GetParametrs();
            if (Singleton.Parametrs.TopicsFilter == null)
                Singleton.Parametrs.SetDefaultFilter();
            Sounds.IsToggled = Singleton.Parametrs.Sounds;
            Notices.IsToggled = Singleton.Parametrs.Notices;
        }
        private void Settings_Disappearing(object sender, EventArgs e)
        {
            Singleton.Parametrs.Sounds = Sounds.IsToggled;
            Singleton.Parametrs.Notices = Notices.IsToggled;
            Parametrs.SetParametrs(Singleton.Parametrs);
        }

        private async void SelectSubject(object sender, EventArgs e)
        {
            Subjects.IsEnabled = false;
            await Navigation.PushAsync(new Subjects());

            Subjects.IsEnabled = true;
        }

        private async void SelectLanguage(object sender, EventArgs e)
        {
            Languages.IsEnabled = false;
            await Navigation.PushAsync(new Languages());

            Languages.IsEnabled = true;
        }
    }
}