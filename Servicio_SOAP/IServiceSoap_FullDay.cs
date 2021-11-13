using ReservaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Servicio_SOAP
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServiceSoap_FullDay" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceSoap_FullDay
    {
        [OperationContract]
        Estructura_Promocion_FullDay_Lista obtenerPromocionFullDay(int pk_origen, int pk_destino, DateTime fecha);

        [OperationContract]
        Estructura_Promocion_FullDay obtenerPromocionFullDayPorId(int Pk_promocionFullDay);

        [OperationContract]
        Estructura_Post_int RegistrarReservaFullDay(ReservaFullDay reserva );//reservafullday


        [OperationContract]
        Estructura_Post_Bool ModificarReservaFullDay(ReservaFullDay reserva);

        [OperationContract]
        Estructura_ReservaFullDay ObtenerReservaFullDay(int Pk_Reserva_FullDay);

        [OperationContract]
        Estructura_Post_Bool EliminarReservaFullDay(int Pk_Reserva_FullDay);

        [OperationContract]
        Estructura_ReservaFullDay_Lista ObtenerReservasFullDay();//ReservaFullda
    }
}
