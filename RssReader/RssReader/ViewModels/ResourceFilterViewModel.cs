using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BL;
using BL.DTOs;
using DAL;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    public class ResourceFilterViewModel : ViewModelBase
    {
        private readonly ResourceService resourceService;
        private ObservableCollection<ResourceDTO> resources;

        public ObservableCollection<ResourceDTO> Resources
        {
            get => resources;
            set
            {
                resources = value;
                OnPropertyChanged();
            }
        }

        private Command dismiss;
        public ResourceFilterViewModel()
        {
            this.resourceService = new ResourceService();
        }
        public Command Dismiss => dismiss ?? (dismiss = new Command(DismissCommand));

        public void Load()
        {
            Resources = new ObservableCollection<ResourceDTO>(resourceService.All());
        }

        public async Task Save()
        {
            await resourceService.Update(Resources);
        }

        public async void DismissCommand()
        {
            await Save();
            await Navigation.PopModalAsync();
        }
    }
}