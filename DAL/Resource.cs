using System.Linq;
using Realms;

namespace DAL
{
    public class Resource : RealmObject
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Display { get; set; }
        [Backlink(nameof(DAL.News.Resource))]
        public IQueryable<News> News { get; }
    }
}