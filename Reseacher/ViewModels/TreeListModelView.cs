using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Reseacher
{

    public class TreeListViewModel
    {
        public ObservableCollection<Server> TreeViewRoot { get; set; }

        public TreeListViewModel(ServerRack serverRack)
        {
            TreeViewRoot = serverRack;
        }
    }

    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Kind { get; set; }

        public string Name { get; set; }

        public string Column1 { get; set; }

        public string Column2 { get; set; }

        public List<Category> Children { get; set; }
    }
}
