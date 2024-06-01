using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_REST.CClases;
using Oracle.ManagedDataAccess.Client;

namespace API_REST.CRespuestas
{
    public class CrespuestaListaMaterial : CRespuestaBase
    {
        public List<CMaterial> datos { get; set; }


        public CRespuestaListaMaterial()
        {
            datos = new List<CMaterial>();

        }
    }
}

