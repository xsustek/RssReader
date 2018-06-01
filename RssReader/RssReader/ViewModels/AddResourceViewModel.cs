using System;
using System.Threading.Tasks;
using BL;
using BL.DTOs;
using DAL;
using Realms;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    public class AddResourceViewModel : ViewModelBase
    {
        private readonly ResourceService resourceService;
        private readonly NewsService newsService;
        private ResourceDTO resource = new ResourceDTO();

        private readonly Func<string, string, string, Task> Alert;

        public ResourceDTO Resource
        {
            get => resource;
            set
            {
                resource = value;
                OnPropertyChanged();
            }
        }

        private Command add;
        public AddResourceViewModel()
        {
            this.resourceService = new ResourceService();
            newsService = new NewsService();
        }


        public async Task Add()
        {
            Resource.Display = true;
            resourceService.Update(Resource);
            await newsService.Update();
        }
    }
}