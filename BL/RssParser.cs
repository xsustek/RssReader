using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BL.DTOs;
using BL.Network;

namespace BL
{
    public class RssParser
    {
        private readonly AppHttpClient saveNetwork = new AppHttpClient();

        public async Task<IEnumerable<NewsDTO>> Parse(ResourceDTO resource)
        {
            var url = resource.Url;
            try
            {

                using (var stream = await saveNetwork.Send(async client => await client.GetStreamAsync(url)))
                {
                    var rss = XDocument.Load(stream);
                    return rss.Descendants("item").Select(i => new NewsDTO
                    {
                        Title = i.Element("title")?.Value.Trim(),
                        Url = i.Element("link")?.Value,
                        Description = i.Element("description")?.Value.Trim(),
                        PublishDate = DateTimeOffset.TryParse(i.Element("pubDate")?.Value, out var res)
                            ? res
                            : DateTimeOffset.Now
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Enumerable.Empty<NewsDTO>();
            }
        }
    }
}