using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BL;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly NewsService newsService;
        private readonly ResourceService resourceService;
        private readonly bool visited;

        public NewsViewModel(bool visited)
        {
            newsService = new NewsService();
            resourceService = new ResourceService();
            this.visited = visited;
        }

        private ObservableCollection<NewsDTO> news = new ObservableCollection<NewsDTO>();

        public ObservableCollection<NewsDTO> News
        {
            get => news;
            set
            {
                news = value;
                OnPropertyChanged();
            }
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        private Command refresh;

        public Command Refresh
            => refresh ??
               (refresh = new Command(async () =>
                   {
                       IsEnabled = false;

                       await Load();

                       IsRefreshing = false;
                       IsEnabled = true;
                   }));

        public async Task Load()
        {
            if(!visited) await newsService.Update();

            await newsService.RemoveOld();

            var collection = resourceService.AllDisplayed(visited);

            News = new ObservableCollection<NewsDTO>(collection);
        }

        public async Task Save()
            => await newsService.Update(News);

        public void Save(NewsDTO newsDto)
            => newsService.Update(newsDto);
    }
}