using System.Collections.ObjectModel;

namespace Reseacher
{

    public class TreeViewModelView
    {
        public ObservableCollection<Server> TreeViewRoot { get; set; }

        public TreeViewModelView(ServerRack serverRack)
        {
            TreeViewRoot = serverRack;
        }
    }
}
