using EasyNetQ;
using EasyNetQ.DI;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using PNH_Queue_Processor;

string exchange = args[0];
var queue = args[1];
var connectionString = "amqp://pnh:AppSierra%40324@localhost:5672";

IHost host = Host.CreateDefaultBuilder(args)
    .Build();

using (var bus = RabbitHutch.CreateBus(connectionString, conn =>
{
    conn.EnableConsoleLogger();
    conn.Register<ITypeNameSerializer, TypeSerializer>();
}).Advanced)
{
    await bus.ConnectAsync();
    var e = await bus.ExchangeDeclareAsync(exchange, configure =>
    {
        configure.AsDurable(true);
        configure.WithType("direct");
    });

    var q = await bus.QueueDeclareAsync(queue, configure =>
    {
        configure.AsDurable(true);
        configure.WithQueueType();
    });

    await bus.BindAsync(e, q, "");

    bus.Consume<JObject>(q, (response, metadata) => JObjectMessage.MessageHandler(response.Body, metadata));
    bus.Consume<EmailMessage>(q, (response, metadata) => EmailMessage.MessageHandler(response.Body, metadata));

    await host.RunAsync();
}