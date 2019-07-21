using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Reseacher
{
    public class ComboBoxViewModel
    { 
        public ObservableCollection<Server> ServerItems { get; set; }

        public ComboBoxViewModel(ServerRack serverRack)
        {
            ServerItems = serverRack;
        }
    }
}
