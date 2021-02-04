using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Compress;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class NewsController : SiteBaseController
    {
        [OutputCache(Duration = 3600, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        [Compress]
        public ActionResult Show(string seolink)
        {

            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            News newsItem = news.Get(@"SELECT * FROM News (NOLOCK)
            where NewsSeoLink = @newsSeoLink", new { newsSeoLink = seolink });

            return View(newsItem);
        }

        public ActionResult SiteMap()
        {
            DapperRepository<News> news = new DapperRepository<News>();

            List<News> newsList = news.GetList(@"select * from [News] (nolock) where IsActive = 1 order by CreatedDate desc", null);

            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter xr = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xr.WriteStartDocument();
            xr.WriteStartElement("urlset");
            xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");
         

            xr.WriteStartElement("url");
            xr.WriteElementString("loc", Request.Url.Authority);
            xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            xr.WriteElementString("changefreq", "daily");
            xr.WriteElementString("priority", "1");
            xr.WriteEndElement();


            foreach (var n in newsList)
            {
                xr.WriteStartElement("url");
                if (n.Language == "tr")
                {
                    xr.WriteElementString("loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/tr/etkinlikler/" + n.NewsSeoLink);
                }
                else
                {
                    xr.WriteElementString("loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/de/nachrichten/" + n.NewsSeoLink);
                }
                xr.WriteElementString("lastmod", n.CreatedDate.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "daily");
                xr.WriteEndElement();
            }

            xr.WriteEndDocument();
            //urlset etiketini kapattık
            xr.Flush();
            xr.Close();
            Response.End();
            return View();
        }
    }
}