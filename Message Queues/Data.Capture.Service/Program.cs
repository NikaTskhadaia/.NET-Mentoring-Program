using RabbitMQ.Client;

const string CONNECTION_STRING = "amqp://guest:guest@localhost:5672";
const string EXCHANGE_NAME = "Data.Capture.Exchange";
const string ROUTING_KEY = "Data.Capture.To.Main.Processing";
const string DIRECTORY_PATH = @"C:\BeingWatched";
const int CHUNK_SIZE = 102400;

ConnectionFactory factory = new()
{
    Uri = new Uri(CONNECTION_STRING)
};
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();
channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, true);

FileSystemWatcher folderWatcher = GetFolderChangedWatcher();

Console.WriteLine("Press Esc to exit...");
ConsoleKeyInfo input;
do
{
    input = Console.ReadKey();
} while (input.Key != ConsoleKey.Escape);

folderWatcher.Dispose();

FileSystemWatcher GetFolderChangedWatcher()
{
    FileSystemWatcher watcher = new()
    {
        Path = Directory.CreateDirectory(DIRECTORY_PATH).FullName,
        EnableRaisingEvents = true,
        NotifyFilter = NotifyFilters.FileName,
        Filter = "*.pdf"
    };

    watcher.Created += (object sender, FileSystemEventArgs e) =>
    {
        WaitFileToBeReady(e.FullPath);
        IBasicProperties properties = channel.CreateBasicProperties();
        properties.Headers = new Dictionary<string, object> ()
        { 
            { "fileName", e.Name },
            { "finished", true }
        };
        IEnumerable<(byte[], bool)> fileChunks = GetFileChunks(e.Name, e.FullPath);
        foreach (var (fileChunk, finished) in fileChunks)
        {
            properties.Headers["finished"] = finished;

            channel.BasicPublish(EXCHANGE_NAME, ROUTING_KEY, properties, fileChunk);
        }
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

// Returns file chunk and a flag whether the chunk the last one
IEnumerable<(byte[], bool)> GetFileChunks(string fileName, string filePath)
{
    FileStream fileStream = File.OpenRead(filePath);
    StreamReader streamReader = new(fileStream);
    int remainingFileSize = Convert.ToInt32(fileStream.Length);
    int totalFileSize = Convert.ToInt32(fileStream.Length);
    byte[] buffer;
    while (remainingFileSize > 0)
    {
        int read = 0;
        if (remainingFileSize > CHUNK_SIZE)
        {
            buffer = new byte[CHUNK_SIZE];
            read = fileStream.Read(buffer, 0, CHUNK_SIZE);
            yield return (buffer, false);
        }
        else
        {
            buffer = new byte[remainingFileSize];
            read = fileStream.Read(buffer, 0, remainingFileSize);
            yield return (buffer, true);
        }

        remainingFileSize -= read;
    }
}