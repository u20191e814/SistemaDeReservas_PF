using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
   public  class ConsultaReservas
    {
      
        public int Pk_Reserva_FullDay { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string fecha_viaje { get; set; }
        public string Fecha_Generado { get; set; }
        public double precioUnitario { get; set; }
        public int cantidad { get; set; }
        public double precioTotal { get; set; }
        public string Estado { get; set; }
    }
}
