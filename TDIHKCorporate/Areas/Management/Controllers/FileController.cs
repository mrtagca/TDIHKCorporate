using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Enums;
using TDIHKCorporate.Helpers.Extensions;
using TDIHKCorporate.Types.ComponentTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class FileController : SiteBaseController
    {
        private const string contentFolderRoot = "~/Content/MainSite/assets";
        private const string prettyName = "files/";

        private readonly DirectoryBrowser directoryBrowser;

        private const string DefaultFilter = "*.pdf,*.csv,*.xls,*.xlsx,*.zip,*.rar";
        public FileController()
        {
            directoryBrowser = new DirectoryBrowser();
        }

        public string ContentPath
        {
            get
            {
                return Path.Combine(contentFolderRoot, prettyName);
            }
        }

        public virtual JsonResult FileBrowserRead(string path)
        {
            try
            {
                path = NormalizePath(path);

                directoryBrowser.Server = Server;
                 

                var result = directoryBrowser
                    .GetContent(path, DefaultFilter)
                    .Select(f => new
                    {
                        name = f.Name,
                        type = f.Type == EntryType.File ? "f" : "d",
                        size = f.Size,
                        creationTime=f.CreationTime,
                        extension = f.Extension,
                        lastAccessTime = f.LastAccessTime
                        
                    });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw new HttpException(404, "File Not Found");
            }
        }

        private string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return ToAbsolute(ContentPath);
            }

            return CombinePaths(ToAbsolute(ContentPath), path);
        }

        private string ToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        private string CombinePaths(string basePath, string relativePath)
        {
            return VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(basePath), relativePath);
        }

        [HttpPost]
        public virtual ActionResult FileBrowserDestroy(string path, string name, string type)
        {
            path = NormalizePath(path);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
            {
                path = CombinePaths(path, name);
                if (type.ToLowerInvariant() == "f")
                {
                    DeleteFile(path);
                }
                else
                {
                    DeleteDirectory(path);
                }

                return FileBrowserRead(null);
            }
            throw new HttpException(404, "File Not Found");
        }

        protected virtual bool CanAccess(string path)
        {
            return path.StartsWith(ToAbsolute(ContentPath), StringComparison.OrdinalIgnoreCase);
        }

        public virtual bool AuthorizeDeleteFile(string path)
        {
            return CanAccess(path);
        }

        public virtual bool AuthorizeDeleteDirectory(string path)
        {
            return CanAccess(path);
        }

        public bool DeleteFile(string path)
        {
            try
            {
                if (!AuthorizeDeleteFile(path))
                {
                    throw new HttpException(403, "Forbidden");

                }

                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDirectory(string path)
        {
            try
            {
                if (!AuthorizeDeleteDirectory(path))
                {
                    throw new HttpException(403, "Forbidden");
                }

                var physicalPath = Server.MapPath(path);

                if (Directory.Exists(physicalPath))
                {
                    Directory.Delete(physicalPath, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public virtual ActionResult FileBrowserCreate(string path, FileBrowserEntry entry)
        {
            path = NormalizePath(path);
            var name = entry.Name;

            if (!string.IsNullOrEmpty(name) && AuthorizeCreateDirectory(path, name))
            {
                var physicalPath = Path.Combine(Server.MapPath(path), name);

                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                return FileBrowserRead(null);
            }

            throw new HttpException(403, "Forbidden");
        }

        public virtual bool AuthorizeCreateDirectory(string path, string name)
        {
            return CanAccess(path);
        }

        [HttpPost]
        public virtual ActionResult FileBrowserUpload(string path, HttpPostedFileBase file)
        {
            path = NormalizePath(path);
            var fileName = Path.GetFileName(file.FileName);

            if (AuthorizeUpload(path, file))
            {
                file.SaveAs(Path.Combine(Server.MapPath(path), fileName));

                return Json(new
                {
                    size = file.ContentLength,
                    name = fileName,
                    type = "f"
                }, "text/plain");
            }

            throw new HttpException(403, "Forbidden");
        }

        public virtual bool AuthorizeUpload(string path, HttpPostedFileBase file)
        {
            return CanAccess(path) && IsValidFile(file.FileName);
        }

        private bool IsValidFile(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var allowedExtensions = DefaultFilter.Split(',');

            return allowedExtensions.Any(e => e.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase));
        }

        public ActionResult FileCreate()
        {
            return View();
        }

        public ActionResult FileList()
        {
            JsonResult fileJson = FileBrowserRead("");

            string json = JsonConvert.SerializeObject(fileJson.Data);

            List<FileBrowserResponse> fileBrowserResponses = JsonConvert.DeserializeObject<List<FileBrowserResponse>>(json);

            return View(fileBrowserResponses.OrderBy(x => x.type).ToList());
        }

        public ActionResult ShowSubFileList(string path)
        {
            string directoryPath = path;
            directoryPath = directoryPath.Replace("/Content/MainSite/assets/files/","");
            ViewBag.DirectoryPath = directoryPath;

            JsonResult fileJson = FileBrowserRead(path);

            string json = JsonConvert.SerializeObject(fileJson.Data);

            List<FileBrowserResponse> fileBrowserResponses = JsonConvert.DeserializeObject<List<FileBrowserResponse>>(json);

            return View(fileBrowserResponses.OrderBy(x => x.type).ToList());
        }


        public ActionResult ShowPDFFile(string pdfFilePath,string pdfFileName)
        {
            var path = Server.MapPath(pdfFilePath);
            var file = Server.MapPath(pdfFilePath + pdfFileName);

            if (!file.StartsWith(path))
            {
                // someone tried to be smart and sent 
                // ?filename=..\..\creditcard.pdf as parameter
                throw new HttpException(403, "Forbidden");
            }
            return File(file, "application/pdf");
        }
    }
}