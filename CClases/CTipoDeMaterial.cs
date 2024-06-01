using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{
    public class CTipoDeMaterial
    {


        //ATRIBUTOS DE LA CLASE

        public int id { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }


        public List<CTipoDeMaterial> tipo_material_leer(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();

            string sql = "SELECT " +
                        " ID " +
                        ", DESCRIPCION" +
                        ", ESTADO" +
                        " FROM INV_TIPO_MATERIAL ";

            OracleCommand comando_leer = new OracleCommand(sql, x_con);
            OracleDataReader leer;
            List<CTipoDeMaterial> x_lista = new List<CTipoDeMaterial>(); //Tiene lo mismo que me sirve para usar en inv_tipo material

            CTipoDeMaterial seleccione = new CTipoDeMaterial();
            seleccione.id = -1;
            seleccione.descripcion = "(SELECCIONE)";
            x_lista.Add(seleccione);


            try
            {
                leer = comando_leer.ExecuteReader();

                while (leer.Read())
                {
                    CTipoDeMaterial x_leer = new CTipoDeMaterial();

                    if (!leer.IsDBNull(0))
                    {
                        x_leer.id = leer.GetInt32(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        x_leer.descripcion = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        x_leer.estado = leer.GetString(2);
                    }

                    x_lista.Add(x_leer);

                }
                leer.Close();

            }
            catch (OracleException e)
            {
                o_error.id = e.Number;
                o_error.mensaje = e.Message;
                x_lista = null;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Error, revisar Codigo";
                }
            }
            catch (Exception e)
            {
                x_lista = null;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }
            return x_lista;
        }

    }
}
