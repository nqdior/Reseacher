using System.Collections.ObjectModel;

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
