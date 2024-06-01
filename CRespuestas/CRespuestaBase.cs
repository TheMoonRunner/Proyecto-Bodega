using API_REST.CClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuestaBase
    {
        public CError o_error { get; set; }


        public CRespuestaBase()
        {
            o_error = new CError();


        }
    }
}
