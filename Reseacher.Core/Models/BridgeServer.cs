using Renci.SshNet;
using System;

namespace Reseacher
{
    public class BridgeServer
    {
        public BridgeServer() { }

        public BridgeServer(string connectionString)
        {
            var constr = connectionString.Split(new[] { ";" }, StringSplitOptions.None);
            Host = constr[0].Replace("host=", "");
            Port = Convert.ToInt32(constr[1].Replace("port=", ""));
            UserName = constr[2].Replace("user=", "");
            Password = constr[3].Replace("password=", "");
            Timeout = Convert.ToInt32(constr[4].Replace("timeout=", ""));
            KeyFilePath = constr[5].Replace("keyfile=", "");
        }

        public string Host { get; set; }

        public int Port { get; set; } = 22;

        public string UserName { get; set; }

        public string Password { get; set; }

        private string _keyFilePath = "";

        public string KeyFilePath
        {
            get => _keyFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _keyFilePath = value;
                    HasKeyFile = false;
                    return;
                }
                _keyFilePath = value;
                HasKeyFile = true;
            }
        }

        private TimeSpan _timeout = new TimeSpan(0, 0, 5);

        public int Timeout
        {
            get => _timeout.Seconds;
            set => _timeout = new TimeSpan(0, 0, value);
        }

        public bool HasKeyFile { get; private set; } = false;

        public SshClient CreateSshClient()
        {
            SshClient _client;
            if (HasKeyFile)
            {
                var pkfile = new PrivateKeyFile(KeyFilePath, Password);
                _client = new SshClient(Host, Port, UserName, pkfile);
            }
            else
            {
                _client = new SshClient(Host, Port, UserName, Password);
            }
            _client.ConnectionInfo.Timeout = new TimeSpan(0, 0, 5);
            return _client;
        }

        public override string ToString() => $"host={Host};port={Port};user={UserName};password={Password};timeout={Timeout};keyfile={KeyFilePath}";
    }
}
