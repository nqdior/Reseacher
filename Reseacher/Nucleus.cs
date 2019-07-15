using Reseacher.Core;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Reseacher
{
    internal static class Nucleus
    {
        public static MainWindow mainWindow { get; set; }

        internal static readonly ServerRack Servers = new ServerRack();

        internal static void ReadConfig()
        {
            var settings = new List<SettingJson>();
            using (var fs = new FileStream(Application.StartupPath + "/Settings.json", FileMode.Open, FileAccess.Read))
            {
                settings = (List<SettingJson>)JsonSerializer.SerializerList<SettingJson>().ReadObject(fs);
            }
            foreach (var setting in settings)
            {
                Servers.Add(setting.Name, new Server(setting.Name, setting.Engine.ToEngine())
                {
                    ConnectionString = setting.ConnectionString,
                    BridgeServer = new BridgeServer(setting.SshConnectionString),
                    UseBridgeServer = setting.UseBridgeServer
                });
            }
        }

        internal static void WriteConfig()
        {
            var settings = new List<SettingJson>();
            foreach (var server in Servers)
            {
                settings.Add(new SettingJson
                {
                    Name = server.Value.Name,
                    Engine = server.Value.Engine.ToString(),
                    ConnectionString = server.Value.ConnectionString,
                    SshConnectionString = server.Value.BridgeServer.ToString(),
                    UseBridgeServer = server.Value.UseBridgeServer
                });
            }
            using (var fs = new FileStream(Application.StartupPath + "/Settings.json", FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.SerializerList<SettingJson>().WriteObject(fs, settings);
            }
        }
    }

    [DataContract]
    public class SettingJson
    {
        public SettingJson() { }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Engine { get; set; }

        [DataMember]
        public string ConnectionString { get; set; }

        [DataMember]
        public string SshConnectionString { get; set; }

        [DataMember]
        public bool UseBridgeServer { get; set; }
    }

    /* https://qiita.com/Akasaki/items/dee137b24aea4b7e2bcb */
    internal class JsonSerializer
    {
        public static DataContractJsonSerializer Serializer<TYpe>() => new DataContractJsonSerializer(typeof(TYpe));

        public static DataContractJsonSerializer SerializerList<TYpe>() => new DataContractJsonSerializer(typeof(List<TYpe>));

        public static DataContractJsonSerializer SerializerDictionary<TYpe1, TYpe2>() => new DataContractJsonSerializer(typeof(Dictionary<TYpe1, TYpe2>));
    }
}
