using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
   public  class Estructura_Promocion_Viaje_Lista
    {
        public string codigo { get; set; }
        public List<Promocion_viaje> data { get; set; }
        public string mensaje { get; set; }

    }
}
