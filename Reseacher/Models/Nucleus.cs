using Reseacher.Core;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Reseacher
{
    /* reseacher core class */
    internal static class Nucleus
    {
        internal static readonly ServerRack ServerRack = new ServerRack();

        internal static void ReadConfig()
        {
            if (File.Exists(Application.StartupPath + "/Settings.json") == false) return;

            var settings = new List<SettingJson>();
            using (var fs = new FileStream(Application.StartupPath + "/Settings.json", FileMode.Open, FileAccess.Read))
            {
                settings = (List<SettingJson>)JsonSerializer.SerializerList<SettingJson>().ReadObject(fs);
            }
            foreach (var setting in settings)
            {
                ServerRack.Add(new Server(setting.Name, setting.Engine.ToEngine())
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
            foreach (var server in ServerRack)
            {
                settings.Add(new SettingJson
                {
                    Name = server.Name,
                    Engine = server.Engine.ToString(),
                    ConnectionString = server.ConnectionString,
                    SshConnectionString = server.BridgeServer.ToString(),
                    UseBridgeServer = server.UseBridgeServer
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
