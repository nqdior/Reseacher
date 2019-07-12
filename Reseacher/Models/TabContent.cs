namespace Reseacher
{
    public class TabContent
    {
        public TabContent(string header, object content)
        {
            Header = header;
            Content = content;
        }

        public string Header { get; }

        public object Content { get; }
    }
}