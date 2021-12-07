using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rabbitmq_Listening.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq_Listening
{
    class Program
    {
        //se agrego colas
        static void Main(string[] args)
        {
            Console.Title = "Sistemas Distribuidos UPC";
            Console.WriteLine("Iniciado la recepcion de mensajes ");
            //Recepcion de colas 

            var factory = new ConnectionFactory()
            {
                HostName = Settings.Default.IpRabbitMq,
                Port = 5672,
                VirtualHost = "/",
                UserName = "admin",
                Password = "admin"
            };


           

            using (var conection = factory.CreateConnection())
            {
                using (var channel = conection.CreateModel())
                {
                    channel.QueueDeclare("MensajeDeReserva", false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("MensajeDeReserva", true, consumer);
                    consumer.Received += (sender, e) =>
                    {
                        var body = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(message);
                    };
                    Console.ReadLine();

                }
            }


            Console.ReadLine();
        }
    }
}
