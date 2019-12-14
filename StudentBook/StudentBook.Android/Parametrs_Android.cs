using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StudentBook.Droid;
using Xamarin.Forms;
using StudentBook;
using System.IO;
using Newtonsoft.Json;

[assembly: Dependency(typeof(Parametrs_Android))]
namespace StudentBook.Droid
{
    class Parametrs_Android : IParams
    {
        private string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private string filename;

        public Parametrs_Android()
        {
            filename = Path.Combine(documentsPath, "params.json");
        }

        public Parametrs GetParametrs()
        {
            if (!File.Exists(filename))
            {
                var param = new Parametrs();
                SetParametrs(param);
                return param;
            }
            using (StreamReader reader = new StreamReader(File.Open(filename, FileMode.Open, FileAccess.Read)))
            {
                var check = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Parametrs>(check);
            }
        }

        public void SetParametrs(Parametrs parametrs)
        {
            using (StreamWriter writer = new StreamWriter(File.Create(filename)))
            {
                writer.Write(JsonConvert.SerializeObject(parametrs));
            }
        }
    }
}