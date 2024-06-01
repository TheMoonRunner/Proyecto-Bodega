using API_REST.CClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuesta_CMovimientos : CRespuestaBase
    {

        public CMovimientos_Material datos { get; set; }

        public CRespuesta_CMovimientosInt()
        {

        }


    }
}