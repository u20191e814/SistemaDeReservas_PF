using ReservaEntidades;
using ReservasDb;
using Servicio_SOAP.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Servicio_SOAP
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceSoap_FullDay" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceSoap_FullDay.svc o ServiceSoap_FullDay.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceSoap_FullDay : IServiceSoap_FullDay
    {

        public Estructura_Post_int RegistrarReservaFullDay(ReservaFullDay reserva)
        {
            return new Db(Settings.Default.SqlConexion).RegistrarReservaFullDay(reserva);
        }
        public Estructura_Post_Bool ModificarReservaFullDay(ReservaFullDay reserva)
        {
            return new Db(Settings.Default.SqlConexion).ModificarReservaFullDay(reserva);
        }
        public Estructura_Post_Bool EliminarReservaFullDay(int Pk_Reserva_FullDay)
        {
            return new Db(Settings.Default.SqlConexion).EliminarReservaFullDay(Pk_Reserva_FullDay);
        }
        public Estructura_ReservaFullDay ObtenerReservaFullDay(int Pk_Reserva_FullDay)
        {
            return new Db(Settings.Default.SqlConexion).ObtenerReservaFullDay(Pk_Reserva_FullDay);
        }


        public Estructura_Promocion_FullDay_Lista obtenerPromocionFullDay(int pk_origen, int pk_destino, DateTime fecha)
        {
            return new Db(Settings.Default.SqlConexion).obtenerPromocionDeFullDay(pk_origen, pk_destino, fecha);
        }

        public Estructura_Promocion_FullDay obtenerPromocionFullDayPorId(int Pk_promocionFullDay)
        {
            return new Db(Settings.Default.SqlConexion).obtenerPromocionFullDayPorId(Pk_promocionFullDay);
        }

        public Estructura_ReservaFullDay_Lista ObtenerReservasFullDay()
        {
            return new Db(Settings.Default.SqlConexion).ObtenerReservasFullDay();
        }

        public Estructura_ViajesProgramados_FullDay_Lista obtenerViajesProgramadosFullDay(int pk_origen, int pk_destino, DateTime fecha)
        {
            return new Db(Settings.Default.SqlConexion).obtenerViajesProgramadosFullDay(pk_origen, pk_destino,fecha);
        }

        public Estructura_ViajesProgramados_FullDay obtenerViajeProgramadosFullDay(int Pk_ViajeProgramadoFullDay)
        {
            return new Db(Settings.Default.SqlConexion).obtenerViajeProgramadosFullDay(Pk_ViajeProgramadoFullDay);
        }
    }
}
