using Reseacher.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Reseacher
{
    public class ServerRack : Dictionary<string, Server>, INotifyPropertyChanged
    {
        public List<Server> ToList() => this.Select(s => s.Value).ToList();

        public void Add(Server server)
        {
            Add(server.Name, server);
        }

        public new void Add(string key, Server value)
        {
            base.Add(key, value);
            OnPropertyChanged($"{value.Name} Item Add.");
        }

        public new void Remove(string key)
        {
            base.Remove(key);
            OnPropertyChanged($"{key} Item Removed.");
        }

        /* このプロジェクトのいつどこでどのアイテムを変更されても通知させる */
        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string info) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
    }
}
