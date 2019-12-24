﻿using System;
using Android.Content;
using System.IO;
using Xamarin.Forms;
using StudentBook.Droid;
using DatabaseLibrary;
using System.Net.Http;
using Java.Net;
using System.Net;
using Android.App;
using System.Threading;
//using Java.Lang;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SQLite_Android))]
namespace StudentBook.Droid
{
    //load db for android
    class SQLite_Android : ISQLite
    {
        private readonly HttpClient httpClient;
        private readonly string defaultLink = @"http://v33649w1.beget.tech/";
        private readonly string versionName = "version.txt";
        private string versionPath;
        //private string databaseName;
        private string databasePath;
        private bool CanWithoutUpdate = true;

        public bool IsCorrect { get; set; }
        public SQLite_Android()
        {
            //set header User-agent for download
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            versionPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            versionPath = Path.Combine(versionPath, versionName);
            IsCorrect = true;
        }
        public string GetDatabasePath(string sqliteDatabaseName)
        {
            //get personal folder (close folder for documents, which user shouldn't see)
            databasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            databasePath = Path.Combine(databasePath, sqliteDatabaseName);
            //if can not find database and version file
            if (!File.Exists(databasePath) || !File.Exists(versionPath))
            {
                CanWithoutUpdate = false;
                DownloadFile(sqliteDatabaseName, databasePath);
                DownloadFile(versionName, versionPath);
                //CanWithoutUpdate = true;
            }
            //in other case compare exist files with files from internet 
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
                    CreateEmptyDB();
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
                    CreateEmptyDB();
            }
        }
        private void CreateEmptyDB()
        {
            Context context = Android.App.Application.Context;
            var dbAssetStream = context.Assets.Open("empty.db");
            var file = File.Create(databasePath);
            dbAssetStream.CopyTo(file);
            file.Close();
            file = File.Create(versionPath);
            file.Close();
        }
        //should show warnings in any case, but don't beat me
        private void WarningWindows()
        {
            //Toast.MakeText(MainActivity.Instance, "Please check your internet connection", ToastLength.Long).Show();
            //Task task = new Task(() => Toast.MakeText(MainActivity.Instance, "Please check your internet connection", ToastLength.Long).Show());
            if (!CanWithoutUpdate)
            {
                IsCorrect = false;
                Toast.MakeText(MainActivity.Instance, "Please check your internet connection", ToastLength.Long).Show();
                //Thread.Sleep(2000);
                //Task.Delay(5000).Wait();
                // task.Start();
            }
            //MainActivity.Instance.RunOnUiThread(()=> );
        }
    }
}

