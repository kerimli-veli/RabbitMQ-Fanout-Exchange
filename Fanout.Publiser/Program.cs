// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Publiser");



ConnectionFactory factory = new ConnectionFactory();

factory.Uri = new("amqps://fpuoiyor:t8SZYf8YmxPfSnvxwPiWbKYJwjfmNWlW@goose.rmq2.cloudamqp.com/fpuoiyor");

using IConnection connection = await factory.CreateConnectionAsync();

using IChannel channel = await connection.CreateChannelAsync();



await channel.ExchangeDeclareAsync(exchange: "fanaut-example - with - my-group", type: ExchangeType.Fanout);

for(int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"number {i}");
    await channel.BasicPublishAsync(
         exchange: "fanaut-example - with - my-group",
         routingKey: string.Empty,
         body: message

        );

}