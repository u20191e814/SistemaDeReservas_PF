using ReservaEntidades;
using ReservasDb;
using Rest_Reserva_.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rest_Reserva_.Controllers
{
    public class ReservaController : ApiController
    {
        [HttpGet]
        [Route("api/GetOrigen/")]
        public Estructura_Origen_Lista GetOrigen()
        {
           
            return new Db(Settings.Default.SqlConexion).obtenerOrigen();
        }
                      

        [HttpGet]
        [Route("api/GetDestino/")]
        public Estructura_Destino_Lista GetDestino(int pk_origen)
        {           
            return new Db(Settings.Default.SqlConexion).obtenerDestino(pk_origen);
        }

        [HttpGet]
        [Route("api/GetReservasPorDni/")]
        public Estructura_ConsultaReservas GetReservasPorDni(string Dni)
        {
            return new Db(Settings.Default.SqlConexion).GetReservasPorDni(Dni);
        }

        [HttpGet]
        [Route("api/GetPromocionViajes/")]
        public Estructura_Promocion_Viaje_Lista GetPromocionViajes(int pk_origen, int pk_destino, DateTime fecha)
        {
           
            return new Db(Settings.Default.SqlConexion).obtenerPromocionDeViajes(pk_origen,pk_destino,fecha);
        }
    }
}
