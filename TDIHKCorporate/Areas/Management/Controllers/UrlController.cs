using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.Helpers.Extensions;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class UrlController : Controller
    {
        [HttpPost]
        public string GenerateFriendlyUrl(string text)
        {
            return UrlExtensions.FriendlyUrl(new UrlHelper(),text);
        }
    }
}