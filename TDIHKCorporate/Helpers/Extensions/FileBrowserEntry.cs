using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDIHKCorporate.Enums;

namespace TDIHKCorporate.Helpers.Extensions
{
    public class FileBrowserEntry
    {
        public string Name { get; set; }
        public EntryType Type { get; set; }
        public long Size { get; set; }
        public DateTime CreationTime { get; set; }
        public string Extension { get; set; }
        public DateTime LastAccessTime { get; set; }

    }
}