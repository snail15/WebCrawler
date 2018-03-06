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

        public void Crawl(string url, string selector)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
  
            var nodes = htmlDoc.DocumentNode
                            .SelectNodes(selector);

            foreach(var node in nodes)
            {
                try
                {
                    string value = node.Attributes["class"].Value;

                    if (value.Equals("title"))
                    {
                        Console.WriteLine($"Title: {node.InnerText.Trim()}");
                    }else if (value.Equals("recommend"))
                    {
                        Console.WriteLine($"Liked: {node.InnerText.Trim()}");
                    }
                    else if (value.Equals("author"))
                    {
                        Console.WriteLine($"Author: {node.InnerText.Trim()}");
                    }
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("pass");
                }  
            }
        }
       
    }
}
