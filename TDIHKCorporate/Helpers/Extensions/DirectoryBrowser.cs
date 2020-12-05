using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TDIHKCorporate.Enums;

namespace TDIHKCorporate.Helpers.Extensions
{
    public class DirectoryBrowser
    {
        public IEnumerable<FileBrowserEntry> GetContent(string path, string filter)
        {
            return GetFiles(path, filter).Concat(GetDirectories(path));
        }

        private IEnumerable<FileBrowserEntry> GetFiles(string path, string filter)
        {
            var directory = new DirectoryInfo(Server.MapPath(path));

            var extensions = (filter ?? "*").Split(",|;".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

            var asd = extensions.SelectMany(directory.GetFiles);


            return extensions.SelectMany(directory.GetFiles)
                .Select(file => new FileBrowserEntry
                {
                    Name = file.Name,
                    Size = file.Length,
                    Type = EntryType.File,
                    CreationTime = file.CreationTime,
                    Extension = file.Extension,
                    LastAccessTime = file.LastAccessTime
                });
        }

        private IEnumerable<FileBrowserEntry> GetDirectories(string path)
        {
            var directory = new DirectoryInfo(Server.MapPath(path));

            

            return directory.GetDirectories()
                .Select(subDirectory => new FileBrowserEntry
                {
                    Name = subDirectory.Name,
                    Type = EntryType.Directory,
                    //Size = GetDirectorySize(directory.FullName),
                    CreationTime = subDirectory.CreationTime,
                    LastAccessTime=subDirectory.LastAccessTime
                });
        }

        public static long GetDirectorySize(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] subdirectories = Directory.GetDirectories(path);

            long size = files.Sum(x => new FileInfo(x).Length);
            foreach (string s in subdirectories)
                size += GetDirectorySize(s);

            return size;
        }

        public System.Web.HttpServerUtilityBase Server { get; set; }
    }
}