using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
   public  class Estructura_ConsultaReservas
    {
        public string codigo { get; set; }
        public List<ConsultaReservas> data { get; set; }
        public string mensaje { get; set; }
    }
}
