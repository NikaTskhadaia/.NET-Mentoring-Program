using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

const string QueueName = "Main.Processing";
const string ExchangeName = "Data.Capture.Exchange";
const string RoutingKey = "Data.Capture.To.Main.Processing";

var path = Directory.CreateDirectory(@"C:\BeingWritten").FullName;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(QueueName, true, false, false);
channel.QueueBind(QueueName, ExchangeName, RoutingKey);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, eventArgs) =>
{
    var fileName = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["fileName"] as byte[]);
    File.WriteAllBytes($"{path}\\{fileName}", eventArgs.Body.ToArray());

    Console.WriteLine($"Writing to {fileName}");
};

channel.BasicConsume(QueueName, false, consumer);

Console.ReadLine();

channel.Close();
connection.Close();