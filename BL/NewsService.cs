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
    public class NewsService
    {
        private readonly RssParser rssParser;
        private readonly IMapper mapper = MapperFactory.Mapper;
        private readonly ResourceService resourceService;
        private readonly RealmFactory realm;

        public NewsService()
        {
            rssParser = new RssParser();
            resourceService = new ResourceService();
            realm = new RealmFactory();
        }

        public void Create(NewsDTO news)
            => realm.Realm.Write(() => realm.Realm.Add(mapper.Map<News>(news)));

        public async Task CreateAsync(IEnumerable<NewsDTO> news)
            => await realm.Realm
                          .WriteAsync(r => news.Select(mapper.Map<News>).ForEach(n => r.Add(n)));

        public async Task Update(IEnumerable<ResourceDTO> resources = null)
        {
            foreach (var resourceDto in resources ?? resourceService.All(r => r.Display))
            {
                var existing = resourceService.GetNews(resourceDto);

                var news = (await rssParser.Parse(resourceDto))
                           .Where(n => n != null)
                           .OrderByDescending(c => c.PublishDate)
                           .DistinctBy(c => c.Title)
                           .ExceptBy(existing, n => n.Title)
                           .Take(10);

                await realm.Realm.WriteAsync(q =>
                {
                    foreach (var newsDto in news)
                    {
                        var newq = mapper.Map<News>(newsDto);
                        newq.Resource =
                            q.Find<Resource>(resourceDto.Name);
                        q.Add(newq);
                    }
                });
            }
        }

        public async Task Update(IEnumerable<NewsDTO> news)
            => await realm.Realm.WriteAsync(q =>
            {
                news.ForEach(n => q.Add(mapper.Map<News>(n), true));
            });

        public void Update(NewsDTO news)
            => realm.Realm.Write(() => realm.Realm.Add(mapper.Map<News>(news), true));

        public async Task RemoveOld(int shouldRemain = 100)
            => await realm.Realm.WriteAsync(q =>
            {
                q.All<News>()
                 .Where(r => r.Visited)
                 .OrderByDescending(n => n.PublishDate)
                 .AsEnumerable()
                 .Skip(shouldRemain)
                 .ForEach(n => realm.Realm.Remove(n));

                q.All<News>()
                 .Where(r => !r.Visited)
                 .OrderByDescending(n => n.PublishDate)
                 .AsEnumerable()
                 .Skip(shouldRemain)
                 .ForEach(n => realm.Realm.Remove(n));
            });

        public List<NewsDTO> All(Expression<Func<News, bool>> where = null)
            => realm.Realm
                    .All<News>()
                    .Where(where ?? (_ => true))
                    .AsEnumerable()
                    .Select(mapper.Map<NewsDTO>)
                    .ToList();
    }
}