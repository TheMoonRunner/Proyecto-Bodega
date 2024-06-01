using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuestaInt : CRespuestaBase
    {
        public int valor { get; set; }


        public CRespuestaInt()
        {
            valor = 0;

        }
    }
}