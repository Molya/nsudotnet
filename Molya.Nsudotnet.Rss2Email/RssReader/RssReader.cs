using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RssReader
{
    public interface IRssReader
    {
        string RssPath { get; set; }

    }

    public class LiveJournalReader : IRssReader
    {
        public string RssPath { get; set; }

        public LiveJournalReader(string user)
        {
            RssPath = string.Format("http://{0}.livejournal.com/data/rss", user);
        }

        public List<LiveJournalRssModel> ReadPosts()
        {
            var posts = new List<LiveJournalRssModel>();

            try
            {
                XDocument doc = XDocument.Load(RssPath);
                var elements = doc.Elements().First().Elements().First().Elements("item");
                foreach (var element in elements)
                {
                    var post = new LiveJournalRssModel
                    {
                        Title = element.Elements("title").First().Value,
                        Description = element.Elements("description").First().Value,
                        PushDateTime = DateTime.Parse(element.Elements("pubDate").First().Value)
                    };
                    posts.Add(post);
                }

                return posts;
            }
            catch
            {
                return null;
            }
        }
    }
}
