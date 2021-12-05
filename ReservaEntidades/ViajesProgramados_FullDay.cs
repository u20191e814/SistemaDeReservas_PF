using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
   public  class ViajesProgramados_FullDay
    {
        
        public int Pk_ViajesProgramados_FullDay { get; set; }
        public string Ruta { get; set; }
        public double Precio { get; set; }
        public int cupos { get; set; }
        public string rutaImagen { get; set; }
    }
}
