using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Reseacher
{
    public class ComboBoxViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }

        public ComboBoxViewModel()
        {
            Items = new ObservableCollection<Item>();
        }

        public void DrawComboBox(ServerRack serverRack)
        {
            foreach (var server in serverRack)
            {
                Items.Add(new Item() { Name = server.Key });
            }           
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }

    public class Item
    {
        public string Name { get; set; }
    }
}
