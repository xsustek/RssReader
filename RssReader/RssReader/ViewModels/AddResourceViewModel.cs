using System.Threading.Tasks;
using BL;
using BL.DTOs;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    public class AddResourceViewModel : ViewModelBase
    {
        private readonly ResourceService resourceService;
        private readonly NewsService newsService;
        private ResourceDTO resource = new ResourceDTO();

        public ResourceDTO Resource
        {
            get => resource;
            set
            {
                resource = value;
                OnPropertyChanged();
            }
        }

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