using Realms;

namespace DAL
{
    public class RssItem : RealmObject
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}