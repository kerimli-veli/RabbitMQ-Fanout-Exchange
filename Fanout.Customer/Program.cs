using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Consumer");

ConnectionFactory factory = new ConnectionFactory
{
    Uri = new Uri("amqps://fpuoiyor:t8SZYf8YmxPfSnvxwPiWbKYJwjfmNWlW@goose.rmq2.cloudamqp.com/fpuoiyor")
};

using IConnection connection = await factory.CreateConnectionAsync();
using IChannel channel = await connection.CreateChannelAsync();


string exchangeName = "fanaut-example - with - my-group";
await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout);


QueueDeclareOk queue = await channel.QueueDeclareAsync(queue: "", exclusive: true);


await channel.QueueBindAsync(queue: queue.QueueName, exchange: exchangeName, routingKey: "");


var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Gelen mesaj: " + message);
    await Task.Yield(); 
};

await channel.BasicConsumeAsync(queue: queue.QueueName, autoAck: true, consumer: consumer);


Console.WriteLine("Mesajlar gözlənilir...");
Console.ReadLine();
