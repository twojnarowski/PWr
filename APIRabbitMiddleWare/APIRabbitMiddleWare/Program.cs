using System;
using System.IO;
using System.Net;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace APIRabbitMiddleWare
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "scraper",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var temp = message.Split("\t");
                    var intoUrl = temp[0];
                    var json = temp[1];
                    Console.WriteLine("intoURL: " + intoUrl);
                    Console.WriteLine("json: " + json);

                    Console.WriteLine(" [x] Received {0}", message);
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(intoUrl);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Console.WriteLine("Wpisano do bazy: " + result);
                    }
                };
                channel.BasicConsume(queue: "scraper",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}