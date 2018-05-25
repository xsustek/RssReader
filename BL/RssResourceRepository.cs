using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Realms;

namespace BL
{
    public class RssResourceRepository<T> where T : RealmObject
    {
        public Task Create(T resource)
        {
            return RealmProvider.Do(r => r.WriteAsync(q => q.Add(resource)));
        }

        public List<T> All()
        {
            return RealmProvider.Do(r => r.All<T>().ToList());
        }
    }
}