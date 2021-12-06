using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
   public  class DNI
    {
        public bool success { get; set; }
        public string message { get; set; }
        public datos_persona data { get; set; } //ok agregado
    }
    public class datos_persona
    {
        public string numero { get; set; }
        public string nombre_completo { get; set; }
    }
}
