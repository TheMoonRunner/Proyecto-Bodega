using API_REST.CRespuestas;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{
    public class CTipoContenedor
    {
        //Atributos de la clase CTipoContenedor
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }


        //Método para leer registros de la tabla inv_tipo_contenedor
        public List<CTipoContenedor> leer_datos(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {
            o_error = new CError();

            string sql = "select id" +
                        ", nombre " +
                        ", descripcion" +
                        ", estado " +
                        " from inv_tipo_contenedor ";

            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;

            List<CTipoContenedor> x_lista = new List<CTipoContenedor>();

            try
            {

                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CTipoContenedor tpc = new CTipoContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        tpc.id = leer.GetInt32(0);
                    }

                    if (!leer.IsDBNull(1))
                    {
                        tpc.nombre = leer.GetString(1);
                    }

                    if (!leer.IsDBNull(2))
                    {
                        tpc.descripcion = leer.GetString(2);
                    }

                    if (!leer.IsDBNull(3))
                    {
                        tpc.estado = leer.GetString(3);
                    }

                    x_lista.Add(tpc);
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

        //Método para insertar registros de la tabla inv_tipo_contenedor con su secuencia
        public int insertar_datos(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {
            o_error = new CError();

            int b_id = -1;
            int x_f = 0;

            string sql2 = "select seq_inv_tipo_contenedor.nextval from dual";
            OracleCommand comando2 = new OracleCommand(sql2, i_con);

            OracleDataReader leer;

            CRespuestaInt x_r = new CRespuestaInt();

            try
            {

                leer = comando2.ExecuteReader();


                while (leer.Read())
                {

                    if (!leer.IsDBNull(0))
                    {
                        b_id = leer.GetInt32(0);
                    }
                }
                leer.Close();


                this.id = b_id;


                string sql = "insert into inv_tipo_contenedor(id,nombre,descripcion,estado) values (:id,:nombre,:descripcion,:estado)";

                OracleCommand comando = new OracleCommand(sql, i_con);


                comando.Parameters.Add("id", OracleDbType.Int32).Value = this.id;

                if (nombre != null)
                {
                    comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 100).Value = this.nombre.ToUpper();
                }


                if (descripcion != null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 400).Value = this.descripcion.ToUpper();
                }

                if (estado != null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = this.estado.ToUpper();
                }

                x_f = comando.ExecuteNonQuery();
            }

            catch (OracleException e)
            {
                x_f = -1;
                this.id = -1;
                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Codigo  ya existe";
                }

            }
            catch (Exception e)
            {
                this.id = -1;
                x_f = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }

            return x_f;
        }




        //Método para modificar registros de la tabla inv_tipo_contenedor
        public int modificar_datos(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {

            o_error = new CError();
            int x_f = 0;
            int b_id = -1;

            try
            {

                string sql = "update inv_tipo_contenedor set nombre=:nombre, descripcion=:descripcion, estado=:estado where id=:id";

                OracleCommand comando = new OracleCommand(sql, i_con);


                if (nombre != null)
                {
                    comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 100).Value = this.nombre.ToUpper();
                }

                if (descripcion != null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 400).Value = this.descripcion.ToUpper();
                }


                if (estado != null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = this.estado.ToUpper();
                }

                comando.Parameters.Add("id", OracleDbType.Int32).Value = id;


                x_f = comando.ExecuteNonQuery();

            }
            catch (OracleException e)
            {
                x_f = -1;

                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Codigo material ya existe";
                }

            }
            catch (Exception e)
            {

                x_f = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }

            return x_f;

        }

        //Método para eliminar registros de la tabla inv_tipo_contenedor
        public int eliminar_datos(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, int id, ref CError o_error)
        {
            int x_f = 0;
            o_error = new CError();

            try
            {

                string sql = "delete from inv_tipo_contenedor where id=:id";

                OracleCommand comando = new OracleCommand(sql, i_con);

                comando.Parameters.Add("id", OracleDbType.Int32).Value = this.id;

                x_f = comando.ExecuteNonQuery();

            }
            catch (OracleException e)
            {
                x_f = -1;
                o_error.id = e.Number;
                o_error.mensaje = e.Message;
                if (o_error.id == 1)
                {
                    o_error.mensaje = "Codigo material ya existe";
                }
            }
            catch (Exception e)
            {
                x_f = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }
            return x_f;


        }
    }
}

