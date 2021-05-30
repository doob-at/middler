using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doob.middler.Common.SharedModels.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace doob.middler
{
    public class FileExtensionMap
    {
        private SimpleDictionary<Func<FileInfo, string, MiddlerAction>> _map = new(StringComparer.CurrentCultureIgnoreCase);

        public FileExtensionMap Set(string extension, Func<FileInfo, string, MiddlerAction> handler)
        {
            _map[extension] = handler;
            return this;
        }

        internal List<string> GetRegisteredExtensions()
        {
            return _map.Keys.ToList();
        }

        internal Func<FileInfo, string, MiddlerAction>? GetFunc(string extension)
        {
            return _map.TryGetValue(extension, out var func) ? func : null;
        }
    }
}
