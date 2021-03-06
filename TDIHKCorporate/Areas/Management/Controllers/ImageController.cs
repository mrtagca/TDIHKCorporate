﻿using DbAccess.Dapper.Repository;
using ImageResizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Enums;
using TDIHKCorporate.Helpers.Extensions;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ComponentTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class ImageController : ManagementBaseController
    {

        private const string contentFolderRoot = "~/Content/MainSite/assets";
        private const string prettyName = "images/";
        private readonly ThumbnailCreator thumbnailCreator;
        private readonly DirectoryBrowser directoryBrowser;

        private const int ThumbnailHeight = 80;
        private const int ThumbnailWidth = 80;

        private const string DefaultFilter = "*.png,*.gif,*.jpg,*.jpeg";

        public ImageController()
        {
            thumbnailCreator = new ThumbnailCreator();
            directoryBrowser = new DirectoryBrowser();
        }

        public string ContentPath
        {
            get
            {
                return Path.Combine(contentFolderRoot, prettyName);
            }
        }

        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult ImageBrowserRead(string path)
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
                        creationTime = f.CreationTime,
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

        public virtual bool AuthorizeRead(string path)
        {
            return CanAccess(path);
        }

        public FileContentResult ThumbnailRead(string path)
        {
            path = NormalizePath(path);

            var physicalPath = Server.MapPath(path);

            if (System.IO.File.Exists(physicalPath))
            {
                Response.AddFileDependency(physicalPath);

                return CreateThumbnail(physicalPath);
            }
            else
            {
                return null;
                //throw new HttpException(404, "File Not Found");
            }

        }

        private FileContentResult CreateThumbnail(string physicalPath)
        {
            using (var fileStream = System.IO.File.OpenRead(physicalPath))
            {
                var desiredSize = new ImageSize
                {
                    Width = ThumbnailWidth,
                    Height = ThumbnailHeight
                };

                const string contentType = "image/png";

                return File(thumbnailCreator.Create(fileStream, desiredSize, contentType), contentType);
            }
        }

        protected virtual bool CanAccess(string path)
        {
            return path.StartsWith(ToAbsolute(ContentPath), StringComparison.OrdinalIgnoreCase);
        }

        public virtual bool AuthorizeCreateDirectory(string path, string name)
        {
            return CanAccess(path);
        }

        [HttpPost]
        public virtual ActionResult ImageBrowserCreate(string path, FileBrowserEntry entry)
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

                return ImageBrowserRead(null);
                return Json(null);
            }

            throw new HttpException(403, "Forbidden");
        }

        [HttpPost]
        public virtual ActionResult ImageBrowserDestroy(string path, string name, string type)
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

                return ImageBrowserRead(null);
                return Json(null);
            }
            throw new HttpException(404, "File Not Found");
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
        public virtual ActionResult ImageBrowserUpload(string path, HttpPostedFileBase file)
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

        public class FileResizeInfos
        {
            public string Width { get; set; }
            public string Height { get; set; }
        }


        [HttpPost]
        public virtual string ImageBrowserUploadForNews(string path, FileResizeInfos formData, HttpPostedFileBase file)
        {

            file = Request.Files[0];
            path = NormalizePath(path + "news/");
            var fileName = Path.GetFileName(file.FileName);

            if (AuthorizeUpload(path, file))
            {

                string filePath = Path.Combine(Server.MapPath(path), fileName);

                file.SaveAs(filePath);

                if (formData.Width != null)
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = int.Parse(formData.Width)
                    };
                    ImageBuilder.Current.Build(filePath, filePath, resizeSetting);
                }

                if (formData.Height != null)
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Height = int.Parse(formData.Height)
                    };
                    ImageBuilder.Current.Build(filePath, filePath, resizeSetting);
                }
                string pathNewsImageFile = Path.Combine(Server.MapPath(path), fileName);

                return path + fileName;
            }

            throw new HttpException(403, "Forbidden");
        }

        [HttpPost]
        public virtual string ImageBrowserUploadForEvents(string path, FileResizeInfos formData, HttpPostedFileBase file)
        {

            file = Request.Files[0];
            path = NormalizePath(path + "events/");
            var fileName = Path.GetFileName(file.FileName);

            if (AuthorizeUpload(path, file))
            {

                string filePath = Path.Combine(Server.MapPath(path), fileName);

                file.SaveAs(filePath);

                if (formData.Width != null && formData.Height != null)
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = int.Parse(formData.Width),
                        Height = int.Parse(formData.Height)
                    };
                    ImageBuilder.Current.Build(filePath, filePath, resizeSetting);
                }
                string pathNewsImageFile = Path.Combine(Server.MapPath(path), fileName);

                return path + fileName;
            }

            throw new HttpException(403, "Forbidden");
        }

        [HttpPost]
        public virtual string ImageBrowserUploadForStandardMemberShip(string path, FileResizeInfos formData, HttpPostedFileBase file)
        {

            file = Request.Files[0];
            path = NormalizePath(path + "StandardMitgliedschaft/");
            var fileName = Path.GetFileName(file.FileName);

            if (AuthorizeUpload(path, file))
            {

                string filePath = Path.Combine(Server.MapPath(path), fileName);

                file.SaveAs(filePath);

                if (formData.Width != null && formData.Height != null)
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = int.Parse(formData.Width),
                        Height = int.Parse(formData.Height)
                    };
                    ImageBuilder.Current.Build(filePath, filePath, resizeSetting);
                }
                string pathNewsImageFile = Path.Combine(Server.MapPath(path), fileName);

                return path + fileName;
            }

            throw new HttpException(403, "Forbidden");
        }

        [HttpPost]
        public virtual string ImageBrowserUploadForNewsThumbnail(string path, FileResizeInfos formData, HttpPostedFileBase file)
        {

            file = Request.Files[0];
            path = NormalizePath(path + "news/thumbnail/");
            var fileName = Path.GetFileName(file.FileName);

            if (AuthorizeUpload(path, file))
            {

                string filePath = filePath = Path.Combine(Server.MapPath(path), fileName);
                 


                file.SaveAs(filePath);

                if (formData.Width != null)
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = int.Parse(formData.Width)
                    };
                    ImageBuilder.Current.Build(filePath, filePath, resizeSetting);
                }

                if (formData.Height != null)
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Height = int.Parse(formData.Height)
                    };
                    ImageBuilder.Current.Build(filePath, filePath, resizeSetting);
                }

                string pathNewsImageFile = Path.Combine(Server.MapPath(path), fileName);

                return path + fileName;
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

        public ActionResult Image(string path)
        {
            path = NormalizePath(path);

            if (AuthorizeImage(path))
            {
                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath))
                {
                    const string contentType = "image/png";
                    return File(System.IO.File.OpenRead(physicalPath), contentType);
                }
            }

            throw new HttpException(403, "Forbidden");
        }

        public virtual bool AuthorizeImage(string path)
        {
            return CanAccess(path) && IsValidFile(Path.GetExtension(path));
        }

        public ActionResult ImageList()
        {
            JsonResult fileJson = ImageBrowserRead("");

            string json = JsonConvert.SerializeObject(fileJson.Data);

            List<ImageBrowserResponse> imageBrowserResponses = JsonConvert.DeserializeObject<List<ImageBrowserResponse>>(json);

            List<ImageBrowserResponse> assds = imageBrowserResponses.OrderBy(x => x.type).ToList();

            return View(imageBrowserResponses.OrderBy(x => x.type).ToList());
        }

        public ActionResult ShowSubImageList(string path)
        {
            string directoryPath = path;
            directoryPath = directoryPath.Replace("/Content/MainSite/assets/images/", "");
            ViewBag.DirectoryPath = directoryPath;

            JsonResult fileJson = ImageBrowserRead(path);

            string json = JsonConvert.SerializeObject(fileJson.Data);

            List<ImageBrowserResponse> imageBrowserResponses = JsonConvert.DeserializeObject<List<ImageBrowserResponse>>(json);

            return View(imageBrowserResponses.OrderBy(x => x.type).ToList());
        }

        public ActionResult ImageCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageCreate(string path, HttpPostedFileBase[] file)
        {
            try
            {
                foreach (var item in file)
                {
                    path = NormalizePath(path);
                    var fileName = Path.GetFileName(item.FileName);

                    if (AuthorizeUpload(path, item))
                    {
                        item.SaveAs(Path.Combine(Server.MapPath(path), fileName));
                    }

                }
                ViewBag.Success = "Success";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Success = "Error:" + ex.Message;
                return View();
            }
        }
    }
}