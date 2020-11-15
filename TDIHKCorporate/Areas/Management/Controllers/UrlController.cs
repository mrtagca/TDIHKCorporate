using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Extensions;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class UrlController : SiteBaseController
    {
        [HttpPost]
        public string GenerateFriendlyUrl(string text)
        {
            return UrlExtensions.FriendlyUrl(new UrlHelper(),text);
        }
    }
}