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
    public partial class Subjects : ContentPage
    {
        public Subjects()
        {
            InitializeComponent();
            Title = "Subject";
        }
    }
}