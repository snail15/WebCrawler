using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Linq;

namespace WebCrawler
{
    public class Crawler
    {
        private List<Article> _articles;

        public Crawler()
        {
            _articles = new List<Article>();
        }

        public void Crawl(string url, string selector, int page)
        {
            for(int i = 1; i <= page; i++)
            {
                StringBuilder address = new StringBuilder(url);
                address.Append();
                address.Append(i.ToString());
  
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(address.ToString());

                var nodes = htmlDoc.DocumentNode
                                .SelectNodes(selector);

                foreach (var node in nodes)
                {
                    try
                    {
                        var article = new Article();
                        foreach (var element in node.Descendants("td"))
                        {


                            string value = element.Attributes["class"].Value;
                            string txt = element.InnerText.Trim();
                            switch (value)
                            {
                                case "title":
                                    int idx = txt.IndexOf("\n");
                                    string titleTxt = txt.Substring(0, idx);
                                    string linkUrl = element.Element("a").Attributes["href"].Value;
                                    article.Title = titleTxt;
                                    article.Link = linkUrl;
                                    break;
                                case "recommend":
                                    article.Liked = txt;
                                    break;
                                case "author":
                                    article.Author = txt;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (article.Title != null)
                        {
                            _articles.Add(article);
                        }

                    }
                    catch (NullReferenceException ex)
                    {
                        continue;
                    }
                }
            }
            
        }

        public void PrintArticles()
        {
            foreach (var article in _articles)
            {
                Console.WriteLine($"Title: {article.Title}");
                Console.WriteLine($"URL: {article.Link}");
                Console.WriteLine($"Author: {article.Author}");
                Console.WriteLine($"Liked: {article.Liked}");
                Console.WriteLine("");
            }
        }
    }
}
