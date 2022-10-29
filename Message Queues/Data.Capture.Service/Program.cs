using RabbitMQ.Client;

const string ExchangeName = "Data.Capture.Exchange";
const string RoutingKey = "Data.Capture.To.Main.Processing";
const string DirectoryPath = @"C:\BeingWatched";

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true);

FileSystemWatcher folderWatcher = StartListeningToFolderChange();

while (true)
{

}

folderWatcher.Dispose();

FileSystemWatcher StartListeningToFolderChange()
{
    FileSystemWatcher watcher = new()
    {
        Path = Directory.CreateDirectory(DirectoryPath).FullName,
        EnableRaisingEvents = true,
        NotifyFilter = NotifyFilters.FileName,
        Filter = "*.pdf"
    };

    watcher.Created += (object sender, FileSystemEventArgs e) =>
    {
        WaitFileToBeReady(e.FullPath);
        var properties = channel.CreateBasicProperties();
        properties.Headers = new Dictionary<string, object> ()
        { 
            { "fileName", e.Name }, 
        };
        var fileBytes = File.ReadAllBytes(e.FullPath);
        channel.BasicPublish(ExchangeName, RoutingKey, properties, fileBytes);
    };

    watcher.Disposed += (object? sender, EventArgs e) =>
    {
        if (connection is not null)
        {
            channel.Close();
            connection.Close();
        }
    };

    return watcher;
}

void WaitFileToBeReady(string filePath)
{
    while (true)
    {
        try
        {
            using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            return;
        }
        catch (IOException)
        {
        }
    }
}