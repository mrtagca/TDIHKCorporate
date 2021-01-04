using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Extensions;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class UrlController : ManagementBaseController
    {
        [HttpPost]
        public string GenerateFriendlyUrlTurkish(string text)
        {
            return UrlExtensions.FriendlyUrlTurkish(new UrlHelper(),text);
        }

        [HttpPost]
        public string GenerateFriendlyUrlGerman(string text)
        {
            return UrlExtensions.FriendlyUrlGerman(new UrlHelper(), text);
        }
    }
}