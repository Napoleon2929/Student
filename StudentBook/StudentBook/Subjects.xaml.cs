using DatabaseLibrary;
using DatabaseLibrary.Models;
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
        //private StudentDBEntity studentDB = new StudentDBEntity("task.db");
        private List<TopicsToView> items;
        private List<Switch> switches;
        public Subjects()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Disappearing += Subjects_Disappearing;
            switches = new List<Switch>();
            items = Singleton.StudentDB.GetTopicsRange(Singleton.StudentDB.GetTopicsTable(), Singleton.Parametrs.Language);
            var chosen = Singleton.Parametrs.TopicsFilter;
            for (var i = 0; i < items.Count; i++)
            {
                SubjectsGrid.RowDefinitions.Add(new RowDefinition());
                var switchButton = new Switch() { ClassId = $"{i}", ThumbColor= Color.White, OnColor = Color.Orange };
                foreach(var choice in chosen)
                {
                    if (choice.ID == items[i].ID)
                        switchButton.IsToggled = true;
                }
                switches.Add(switchButton);
                var text = new Button() { Text = $"{items[i].Name}",BackgroundColor = Color.Transparent, FontSize = 20, HorizontalOptions = LayoutOptions.Start,  TextColor = Color.White };
                text.Clicked += Text_Clicked;
                SubjectsGrid.Children.Add(text);
                SubjectsGrid.Children.Add(switchButton);
                Grid.SetColumn(text, 0);
                Grid.SetColumn(switchButton, 1);
                Grid.SetRow(text, i);
                Grid.SetRow(switchButton, i);
                SubjectStack.Children.Add(SubjectsGrid);
            }

        }

        private async void Subjects_Disappearing(object sender, EventArgs e)
        {
            List<TopicsToView> views = new List<TopicsToView>();
            foreach(var check in switches)
            {
                if (check.IsToggled)
                    views.Add(items[int.Parse(check.ClassId)]);
            }
            Singleton.Parametrs.TopicsFilter = views;
            Parametrs.SetParametrs(Singleton.Parametrs);
            //await Navigation.PopAsync();
        }

        private void Text_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int position = Grid.GetRow(button);
            switches[position].IsToggled = !switches[position].IsToggled;
        }

    }
}