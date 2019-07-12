namespace Reseacher
{
    public class BridgeServer
    {
        public string Host { get; set; }

        public int Port { get; set; }

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

        public bool HasKeyFile { get; private set; } = false;
    }
}
