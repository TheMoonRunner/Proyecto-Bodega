using API_REST.CClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuestaMaterialContenedor : CRespuestaBase
    {

        public List<CMaterialContenedor> datos { get; set; }


        public CRespuestaMaterialContenedor()
        {
            datos = new List<CMaterialContenedor>();

        }
    }
}
