using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ComponentTypes
{
    public class FileBrowserResponse
    {
        public string name { get; set; }
        public string type { get; set; } = "f";
        public int size { get; set; }
        public DateTime creationTime { get; set; }
        public string extension { get; set; }
        public DateTime lastAccessTime { get; set; }
    }

}