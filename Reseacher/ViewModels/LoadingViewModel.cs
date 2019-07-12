using System.ComponentModel;

namespace Reseacher
{
    class LoadingViewModel : INotifyPropertyChanged
    {
        public LoadingViewModel()
        {
        }

        private string _bindText1 = "loading....";
        public string BindText1
        {
            get => _bindText1;
            set
            {
                _bindText1 = value;
                OnPropertyChanged(nameof(BindText1));
            }
        }

        private string _bindText2 = "loading....";
        public string BindText2
        {
            get => _bindText2;
            set
            {
                _bindText2 = value;
                OnPropertyChanged(nameof(BindText2));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }
}
