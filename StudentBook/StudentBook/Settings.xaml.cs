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
        public Settings()
        {
            InitializeComponent();
            SoundEffects.Text = Resx.AppResources.SoundEffects;
            Notice.Text = Resx.AppResources.Notice;
            Subjects.Text = Resx.AppResources.Subjects;
            Languages.Text = Resx.AppResources.Languages;
            CountOfQuestions.Text = Resx.AppResources.CountOfQuestions;
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