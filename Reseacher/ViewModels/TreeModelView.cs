using System.Collections.ObjectModel;

namespace Reseacher
{

    public class TreeModelView
    {
        public ObservableCollection<Server> TreeViewRoot { get; set; }

        public TreeModelView(ServerRack serverRack)
        {
            TreeViewRoot = serverRack;
        }
    }
}
