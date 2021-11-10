using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
    public class Promocion_viaje
    {
        public int Pk_Promocion_viaje { get; set; }
        public string fecha_valida { get; set; }
        public double Precio { get; set; }
        public string rutaImagen { get; set; }
        public string Descripcion { get; set; }
        public string ruta { get; set; }
        public int Cupos { get; set; }
    }
}
