using Campus.Eventos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Campus.ComunicacionAsync
{
    public class BusDeMensajesSuscriptor : BackgroundService
    {
        private readonly IConfiguration configuracion;
        private readonly IProcesadorDeEventos procesador;
        private  IConnection conexion;
        private  IModel canal;
        private string cola;
        public BusDeMensajesSuscriptor(IConfiguration configuracion, IProcesadorDeEventos procesador)
        {
            this.configuracion = configuracion;
            this.procesador = procesador;
            IniciarRabbitMQ();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        { //Este método, parte de la superclase, contiene la conexi{on principal
            stoppingToken.ThrowIfCancellationRequested();//detener si se lo solicita
            var consumidor = new EventingBasicConsumer(canal);//establecer nuevo consumidor RabbitMQ
            consumidor.Received += (modulo, eveArgs) =>
            {
                Console.WriteLine("Un evento sucedió.");
                var cuerpo = eveArgs.Body;
                var mensaje = Encoding.UTF8.GetString(cuerpo.ToArray());
                procesador.ProcesarEvento(mensaje);
            };
            canal.BasicConsume(
                queue: cola,
                autoAck: true,
                consumer: consumidor
            );
            return Task.CompletedTask;

        }

        //este bloque nos puede servir más adelante ... es completamente opcional
        public void RabbitMQ_evento_shutdown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("Se desconecta de RabbitMQ y algo podría ejecutarse ahora");
            //Código de interés
        }
        private void IniciarRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuracion["Host_RabbitMQ"],
                Port = int.Parse(configuracion["Puerto_RabbitMQ"])
            };
            conexion = factory.CreateConnection();
            canal = conexion.CreateModel();
            canal.ExchangeDeclare(
                exchange: "mi_exchange",
                type: ExchangeType.Fanout
            );
            cola = canal.QueueDeclare().QueueName;
            canal.QueueBind(
                queue: cola,
                exchange: "mi_exchange",
                routingKey: ""
            );
            conexion.ConnectionShutdown += RabbitMQ_evento_shutdown;
        }
        public override void Dispose()
        {
            if (canal.IsOpen)
            {
                canal.Close();
                conexion.Close();
            }
            base.Dispose();

        }
    }
}
