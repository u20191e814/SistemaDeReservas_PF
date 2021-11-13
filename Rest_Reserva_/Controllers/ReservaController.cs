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
            //{usuario}/{password}
            return new Db(Settings.Default.SqlConexion).obtenerOrigen();
        }


        

        [HttpGet]
        [Route("api/GetDestino/")]
        public Estructura_Destino_Lista GetDestino(int pk_origen)
        {
            //{usuario}/{password}
            return new Db(Settings.Default.SqlConexion).obtenerDestino(pk_origen);
        }


    }
}
