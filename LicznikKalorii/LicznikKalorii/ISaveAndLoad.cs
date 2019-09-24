using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace LicznikKalorii
{
    public interface ISaveAndLoad
    {
        string GetLocalFilePath(string filename);
    }
}
