using NUnit.Framework;
using ServicioProyecto;
using System;
using System.Threading.Tasks;

namespace Test_Proyecto
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRegistarReservaFullDay()
        {
             
                ServicioProyecto.ServiceSoap_FullDayClient client = new ServicioProyecto.ServiceSoap_FullDayClient();
                ReservaFullDay reserva = new ReservaFullDay()
                {
                    Apellido = "apellido ",
                    cantidad = 1,
                    correo = "correo@hotmail.com",
                    Dni_cliente = "00011",
                    Estado = "Activo",
                    Fecha_Viaje = DateTime.Now,
                    Fk_Destino=1,
                    Fk_Origen=1,
                    Nombre="nombre",
                    precioTotal=25.5,
                    precioUnitario=25.5
                    ,
                    Telefono="011918181"
            
                };
            
            var tarea =  client.RegistrarReservaFullDayAsync(reserva);
                tarea.Wait();
               Estructura_Post_int t = tarea.Result;
                 Assert.AreEqual(3, t.data);
             
           
        }

        [Test]
        public void TestModificarReservaFullDay()
        {

            ServicioProyecto.ServiceSoap_FullDayClient client = new ServicioProyecto.ServiceSoap_FullDayClient();
            ReservaFullDay reserva = new ReservaFullDay()
            {
                Apellido = "apellido 1",
                cantidad = 1,
                correo = "correo",
                Dni_cliente = "00011",
                Estado = "Activo",
                Fecha_Viaje = DateTime.Now,
                Fk_Destino = 1,
                Fk_Origen = 1,
                Nombre = "nombre",
                precioTotal = 25.5,
                precioUnitario = 25.5,
                Pk_Reserva_FullDay=1      ,
                Telefono = "011918181"
            };

            var tarea = client.ModificarReservaFullDayAsync(reserva);
            tarea.Wait();
            Estructura_Post_Bool t = tarea.Result;
            Assert.AreEqual(true, t.data);
        }

        [Test]
        public void TestObtenerReservaFullDay()
        {

            ServicioProyecto.ServiceSoap_FullDayClient client = new ServicioProyecto.ServiceSoap_FullDayClient();           

            var tarea = client.ObtenerReservaFullDayAsync(1);
            tarea.Wait();
            Estructura_ReservaFullDay t = tarea.Result;
            Assert.AreEqual("OK", t.codigo);

        }
        [Test]
        public void TestObtenerReservasFullDay()
        {

            ServicioProyecto.ServiceSoap_FullDayClient client = new ServicioProyecto.ServiceSoap_FullDayClient();

            var tarea = client.ObtenerReservasFullDayAsync();
            tarea.Wait();
            Estructura_ReservaFullDay_Lista t = tarea.Result;
            Assert.AreEqual("OK", t.codigo);

        }

        [Test]
        public void TestElimarReservaFullDay()
        {

            ServicioProyecto.ServiceSoap_FullDayClient client = new ServicioProyecto.ServiceSoap_FullDayClient();

            var tarea = client.EliminarReservaFullDayAsync(2);
            tarea.Wait();
            Estructura_Post_Bool t = tarea.Result;
            Assert.AreEqual(true, t.data);

        }
   
    }
}