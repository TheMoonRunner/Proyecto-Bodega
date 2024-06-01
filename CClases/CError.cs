using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{
    public class CError
    {

        public int id { get; set; }
        public string mensaje { get; set; }

        public CError()
        {
            id = 0;
            mensaje = "";
        }
    }
}
