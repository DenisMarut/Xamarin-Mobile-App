﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using LicznikKalorii;
using LicznikKalorii.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLImplementAndroid))]
namespace LicznikKalorii.Droid
{
    class SQLImplementAndroid : ISaveAndLoad
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);

        }
    }
}