using API_REST.CClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CRespuestas
{
    public class CRespuesta_Movimientos_material : CRespuestaBase
    {

        public List<CMovimientos_Material> datos { get; set; }


        public CRespuesta_Movimientos_material()
        {
            datos = new List<CMovimientos_Material>();
        }



    }
}