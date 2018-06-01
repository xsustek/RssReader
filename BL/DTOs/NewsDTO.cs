using System;
using BL.DTOs;
using DAL;

namespace BL
{
    public class NewsDTO
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public bool Favorite { get; set; }
        public bool Visited { get; set; }
        public ResourceDTO Resource { get; set; }
    }
}