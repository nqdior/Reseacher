using System.ComponentModel;

namespace Reseacher
{
    public class TabContent : INotifyPropertyChanged
    {
        public TabContent(string header, object content)
        {
            Header = header;
            Content = content;
        }

        public string _header;

        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        public object Content { get; }

        public event PropertyChangedEventHandler PropertyChanged = null;

        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }
}