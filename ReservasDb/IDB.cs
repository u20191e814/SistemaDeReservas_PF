using ReservaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasDb
{
    interface IDB
    {
        Estructura_Origen_Lista obtenerOrigen();
        Estructura_Destino_Lista obtenerDestino(int pk_origen);
        Estructura_Promocion_Viaje_Lista obtenerPromocionDeViajes(int pk_origen, int pk_destino, DateTime fecha);
        Estructura_Promocion_FullDay_Lista obtenerPromocionDeFullDay(int pk_origen, int pk_destino, DateTime fecha);
        Estructura_Promocion_Viaje obtenerPromocionDeViaje(int Pk_Promocion_viaje);

        Estructura_Post_int RegistrarReservaFullDay(ReservaFullDay reserva);
        Estructura_Post_Bool ModificarReservaFullDay(ReservaFullDay reserva);
        Estructura_Post_Bool EliminarReservaFullDay(int pk_Reserva_FullDay);
        Estructura_ReservaFullDay ObtenerReservaFullDay(int pk_Reserva_FullDay);
        Estructura_ReservaFullDay_Lista ObtenerReservasFullDay();
       
    }
}
