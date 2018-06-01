using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BL.Automapper;
using BL.DTOs;
using DAL;
using MoreLinq;

namespace BL
{
    public class ResourceService
    {
        private readonly RealmFactory realm;
        private readonly IMapper mapper = MapperFactory.Mapper;

        public ResourceService()
        {
            realm = new RealmFactory();
        }

        public void Create(ResourceDTO resource)
            => realm.Realm.Write(() => realm.Realm.Add(mapper.Map<Resource>(resource)));

        public async Task Update(IEnumerable<ResourceDTO> resources)
            => await realm.Realm.WriteAsync(q =>
            {
                resources.ForEach(r => q.Add(mapper.Map<Resource>(r), true));
            });

        public void Update(ResourceDTO resource)
            => realm.Realm.Write(() => realm.Realm.Add(mapper.Map<Resource>(resource), true));

        public List<NewsDTO> GetNews(ResourceDTO resource,
                                     Expression<Func<News, bool>> newsWhere = null)
            => realm.Realm
                    .Find<Resource>(resource.Name)
                    .News
                    .Where(newsWhere ?? (_ => true))
                    .AsEnumerable()
                    .Select(mapper.Map<NewsDTO>)
                    .ToList();

        public List<ResourceDTO> All(Expression<Func<Resource, bool>> where = null)
        {
            return realm.Realm
                        .All<Resource>()
                        .Where(@where ?? (_ => true))
                        .AsEnumerable()
                        .Select(mapper.Map<ResourceDTO>)
                        .ToList();
        }

        public List<NewsDTO> AllDisplayed(bool visited)
            => realm.Realm.All<Resource>()
                    .Where(r => r.Display)
                    .AsEnumerable()
                    .SelectMany(r => r.News.Where(n => n.Visited == visited).AsEnumerable())
                    .AsEnumerable()
                    .Select(mapper.Map<NewsDTO>)
                    .OrderByDescending(n => n.PublishDate)
                    .ToList();
    }
}