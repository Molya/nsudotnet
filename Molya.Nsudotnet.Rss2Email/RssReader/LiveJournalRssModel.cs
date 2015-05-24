using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RssReader
{
    public class LiveJournalRssModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PushDateTime { get; set; }
    }
}
