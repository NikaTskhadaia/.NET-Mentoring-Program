using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

const string CONNECTION_STRING = "amqp://guest:guest@localhost:5672";
const string EXCHANGE_NAME = "Data.Capture.Exchange";
const string ROUTING_KEY = "Data.Capture.To.Main.Processing";
const string DIRECTORY_PATH = @"C:\BeingWritten";
const string QUEUE_NAME = "Main.Processing";

var path = Directory.CreateDirectory(DIRECTORY_PATH).FullName;

IConnection connection = SetUpRabbitMQConnection();
IModel channel = SetUpRabbitMQChannel(connection);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += OnMessageReceived;

channel.BasicConsume(QUEUE_NAME, false, consumer);

do
{
    Console.WriteLine("Press Esc to exit...");

} while(Console.ReadKey().Key != ConsoleKey.Escape);

channel.Close();
connection.Close();

IConnection SetUpRabbitMQConnection()
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri(CONNECTION_STRING)
    };
    return factory.CreateConnection();
}

IModel SetUpRabbitMQChannel(IConnection connection)
{
    var channel = connection.CreateModel();
    channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, true);
    channel.QueueDeclare(QUEUE_NAME, true, false, false);
    channel.QueueBind(QUEUE_NAME, EXCHANGE_NAME, ROUTING_KEY);
    return channel;
}

void OnMessageReceived(object? sender, BasicDeliverEventArgs eventArgs)
{
    Console.WriteLine("Received a chunk!");
    string fileName = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["fileName"] as byte[]);
    bool isLastChunk = Convert.ToBoolean(eventArgs.BasicProperties.Headers["finished"]);

    using (FileStream fileStream = new($"{path}\\{fileName}", FileMode.Append, FileAccess.Write))
    {
        fileStream.Write(eventArgs.Body.ToArray(), 0, eventArgs.Body.Length);
        fileStream.Flush();
    }
    Console.WriteLine("Chunk saved. Finished? {0}", isLastChunk);
    channel.BasicAck(eventArgs.DeliveryTag, false);
}