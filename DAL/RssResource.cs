using System;
using Realms;

namespace DAL
{
    public class RssResource : RealmObject
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
