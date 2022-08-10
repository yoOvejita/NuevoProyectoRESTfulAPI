using Microsoft.Extensions.Configuration;
using NuevoProyectoRESTfulAPI.DTO;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
namespace NuevoProyectoRESTfulAPI.ComunicacionAsync
{
    public class ImplBusDeMensajesCliente : IBusDeMensajesCliente
    {
		private readonly IConfiguration configuration;
		private readonly IConnection conexion;
		private readonly IModel canal;

		//este bloque nos puede servir más adelante ... es completamente opcional
		public void RabbitMQ_evento_shutdown(object sender, ShutdownEventArgs args)
		{
			Console.WriteLine("Se desconecta de RabbitMQ y algo podría ejecutarse ahora");
			//Código de interés
		}

		public ImplBusDeMensajesCliente(IConfiguration configuration)
		{
			this.configuration = configuration;
			ConnectionFactory factory = new ConnectionFactory()
			{
				HostName = configuration["Host_RabbitMQ"],
				Port = int.Parse(configuration["Puerto_RabbitMQ"])
			};
			try
			{
				conexion = factory.CreateConnection();
				canal = conexion.CreateModel();
				canal.ExchangeDeclare(
					exchange: "mi_exchange",
					type: ExchangeType.Fanout
				);
				//Opcionalmente podemos agregar un trigger de evento shutdown (para que se ejecute el método definido opcionalmente arriba)
				conexion.ConnectionShutdown += RabbitMQ_evento_shutdown;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error al tratar de establecer conexión con RabbitMQ: { e.Message}");
			}
		}

		//Ahora concentrémonos en implementar el método de publicación de un estudiante 
		public void PublicarNuevoEstudiante(EstudiantePublisherDTO estudiantePublisherDTO)
		{
			//primero vamos a crear un objeto serializado del objeto estudiantePublisherDTO
			string mensaje = JsonSerializer.Serialize(estudiantePublisherDTO);
			if (conexion.IsOpen)
				Enviar(mensaje);//definiremos este método  abajo
			else
				Console.WriteLine("No se pudo enviar el mensaje al bus de mensaje RabbitMQ");
		}
		private void Enviar(string msj)
		{
			var cuerpo = Encoding.UTF8.GetBytes(msj);
			canal.BasicPublish(
				exchange: "mi_exchange",
				routingKey: "",
				basicProperties: null,
				body: cuerpo
			);
			Console.WriteLine("Se envió mensaje al bus de mensajes");
		}
		private void Finalizar()
		{
			if (canal.IsOpen)
			{
				canal.Close();
				conexion.Close();
			}
		}
	}
}
