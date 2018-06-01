using System;
using Realms;

namespace DAL
{
    public class News : RealmObject
    {
        [PrimaryKey]
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public bool Favorite { get; set; }
        public bool Visited { get; set; }
        public Resource Resource { get; set; }
    }
}