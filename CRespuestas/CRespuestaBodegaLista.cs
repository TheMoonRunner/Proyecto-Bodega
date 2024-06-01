using API_REST.CClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuestaBodegaLista : CRespuestaBase
    {
        public List<CBodega> datos { get; set; }

        public CRespuestaBodegaLista()
        {

        }


    }
}
