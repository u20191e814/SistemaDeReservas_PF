using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
    public class Estructura_ReservaFullDay_Lista
    {
        public string codigo { get; set; }
        public List< ReservaFullDay> data { get; set; }
        public string mensaje { get; set; }
    }
}
