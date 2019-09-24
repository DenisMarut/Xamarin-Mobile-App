using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using LicznikKalorii;
using LicznikKalorii.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLImplementsIOS))]
namespace LicznikKalorii.iOS
{
    class SQLImplementsIOS : ISaveAndLoad
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}