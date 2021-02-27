using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Helpers.Rss
{
    public class RssFeeder
    {
        public List<News> GetRobertKochRss()
        {

            WebClient webclient = new WebClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webclient.Encoding = Encoding.UTF8; 
            string CekilenVeriler = webclient.DownloadString("https://www.rki.de/SiteGlobals/Functions/RSSFeed/RSSGenerator_nCoV.xml"); 

            XDocument xmlhali = XDocument.Parse(CekilenVeriler);


            IEnumerable<XElement> xElements = xmlhali.Descendants("item");

            List<News> rssNews = new List<News>();

            foreach (var item in xElements)
            {
                News news = new News();
                news.NewsCategoryID = 12;
                news.NewsTitle = ((string)item.Element("title")) + " - [Robert Koch Institut]";
                news.NewsDescription = ((string)item.Element("description"));
                news.NewsSeoLink = ((string)item.Element("link"));
                news.CreatedDate = Convert.ToDateTime(((string)item.Element("pubDate")));
                rssNews.Add(news);
            }

            return rssNews.OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public List<News> GetWhoRss()
        {

            WebClient webclient = new WebClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webclient.Encoding = Encoding.UTF8;  
            string CekilenVeriler = webclient.DownloadString("https://www.who.int/feeds/entity/csr/don/en/rss.xml"); 

            XDocument xmlhali = XDocument.Parse(CekilenVeriler);


            IEnumerable<XElement> xElements = xmlhali.Descendants("item");

            List<News> rssNews = new List<News>();

            foreach (var item in xElements)
            {
                News news = new News();
                news.NewsCategoryID = 12;
                news.NewsTitle = ((string)item.Element("title")) + " - [WHO]";
                news.NewsDescription = ((string)item.Element("description"));
                news.NewsSeoLink = ((string)item.Element("link"));
                news.CreatedDate = Convert.ToDateTime(((string)item.Element("pubDate")));
                rssNews.Add(news);
            }

            return rssNews.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<News> GetSaglikGovTrRss()
        {

            WebClient webclient = new WebClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webclient.Encoding = Encoding.UTF8;
            string CekilenVeriler = webclient.DownloadString("https://www.saglik.gov.tr/rss?tip=2&Anah=96");

            XDocument xmlhali = XDocument.Parse(CekilenVeriler);


            IEnumerable<XElement> xElements = xmlhali.Descendants("item");

            List<News> rssNews = new List<News>();

            foreach (var item in xElements)
            {
                News news = new News();
                news.NewsCategoryID = 12;
                news.NewsTitle = ((string)item.Element("title")) + " - [T.C. Sağlık Bakanlığı]";
                news.NewsDescription = ((string)item.Element("description"));
                news.NewsSeoLink = ((string)item.Element("link"));
                news.CreatedDate = Convert.ToDateTime(((string)item.Element("pubDate")));
                rssNews.Add(news);
            }

            return rssNews.OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}