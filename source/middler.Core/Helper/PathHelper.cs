using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doob.middler.Helper
{
    public class PathHelper
    {
        public static string GetFullPath(string path, string contentPath)
        {
            var p = Path.GetFullPath(Path.Combine(contentPath, path));
            return p.Replace("\\", "/");
        }
    }
}
