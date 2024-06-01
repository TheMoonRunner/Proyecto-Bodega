using API_REST.CClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuestaMovimientos : CRespuestaBase
    {


        public List<Cmovimientos> datos { get; set; }

        public List<Int32> datos_num { get; set; }

        public CRespuestaMovimientos()
        {

        }


    }
}
