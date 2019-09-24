using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using LicznikKalorii;
using LicznikKalorii.UWP;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLImplementsUWP))]
namespace LicznikKalorii.UWP
{
    class SQLImplementsUWP : ISaveAndLoad
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
