using System;
using System.Linq;
using Realms;

namespace DAL
{
    public class RssResource : RealmObject
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public static void Create()
        {
            var realm = Realm.GetInstance();
            realm.Write(() => { realm.Add(new RssResource()); });
        }

        public static int Count()
        {
            return Realm.GetInstance().All<RssResource>().Count();
        }
    }
}