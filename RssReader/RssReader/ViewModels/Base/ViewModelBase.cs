using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace RssReader
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigation Navigation => Application.Current.MainPage?.Navigation;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}