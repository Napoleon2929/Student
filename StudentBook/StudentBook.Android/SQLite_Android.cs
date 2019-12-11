using System;
using Android.Content;
using System.IO;
using Xamarin.Forms;
using StudentBook.Droid;
using DatabaseLibrary;
using System.Net.Http;
using System.Threading.Tasks;
using Java.Net;
using System.Net;
using Android.App;
using System.Threading;
using Java.Lang;
using Android.Widget;
using Android.OS;

[assembly: Dependency(typeof(SQLite_Android))]
namespace StudentBook.Droid
{
    //load db for android
    class SQLite_Android : ISQLite
    {
        private readonly HttpClient httpClient;
        private readonly string defaultLink = @"http://v33649w1.beget.tech/";
        private readonly string versionName = "version.txt";
        private bool CanWithoutUpdate = true;
        public SQLite_Android()
        {
            //set header User-agent for download
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        }
        public string GetDatabasePath(string sqliteDatabaseName)
        {
            //get personal folder (close folder for documents, which user shouldn't see)
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(documentsPath, sqliteDatabaseName);
            var versionPath = Path.Combine(documentsPath, versionName);
            //if can not find database and version file
            if (!File.Exists(databasePath) || !File.Exists(versionPath))
            {
                CanWithoutUpdate = false;
                DownloadFile(sqliteDatabaseName, databasePath);
                DownloadFile(versionName, versionPath);
                //CanWithoutUpdate = true;
            }
            //in onther case compare exist files with files from internet 
            else
            {
                RewriteFile(versionName, versionPath, sqliteDatabaseName, databasePath);
            }
            return databasePath;
        }
        //checking function
        private async void RewriteFile(string versionName, string versionPath, string databaseName, string databasePath)
        {
            bool? result = await CheckRemoteFileAsync(versionName, versionPath);
            if (result == false)
            {
                DownloadFile(versionName, versionPath);
                DownloadFile(databaseName, databasePath);
            }
        }
        private async Task<bool?> CheckRemoteFileAsync(string fileName, string existFilePath)
        {
            try
            {
                using (var response = await httpClient.GetAsync(defaultLink + fileName))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var existFile = File.Open(existFilePath, FileMode.Open, FileAccess.Read))
                {
                    return existFile.Equals(stream);
                }
            }
            catch (UnknownHostException)
            {
                //if launch without internet
                WarningWindows();
                if (!CanWithoutUpdate)
                    System.Environment.Exit(0);
                return null;
            }
        }
        //get file from internet and rewrite or create file on the device
        private async void DownloadFile(string fileName, string filePath)
        {
            try
            {
                var response = await httpClient.GetAsync(defaultLink + fileName);
                var stream = await response.Content.ReadAsStreamAsync();
                var file = File.Create(filePath);
                stream.CopyTo(file);
                file.Close();
            }
            catch (UnknownHostException)
            {
                //if launch without internet
                WarningWindows();
                if (!CanWithoutUpdate)
                    System.Environment.Exit(0);
            }
        }
        //should show warnings in any case, but don't beat me
        private void WarningWindows()
        {
            Toast.MakeText(MainActivity.Instance, "Please check your internet connection", ToastLength.Long).Show();
            //Task task = new Task(() => Toast.MakeText(MainActivity.Instance, "Please check your internet connection", ToastLength.Long).Show());
            if (!CanWithoutUpdate)
            {
                Task.Delay(2000).Wait();
               // task.Start();
            }
            //MainActivity.Instance.RunOnUiThread(()=> );
        }
    }
}

