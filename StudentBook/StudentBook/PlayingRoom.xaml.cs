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
    public partial class PlayingRoom : ContentPage
    {
        public PlayingRoom()
        {
            InitializeComponent();
            for (var i = 0; i < 100; i++)
                QuestionsGrid.Children.Add(new Label() { Text = "Record" + i.ToString()});
        }
    }
}