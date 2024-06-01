using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Npgsql;

namespace API_REST.CClases
{
    public class CBodega
    {
        //Atributos de la clase CBodega
        public string id { get; set; }
        public string descripcion { get; set; }
        public string abreviatura { get; set; }
        public string estado { get; set; }


        //----listar aplicando filtros
        //

        public List<CBodega> listar_bodegas_filtro(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, string id, string descripcion, string abreviatura, string estado, ref CError o_error)
        {

            o_error = new CError();

            string sql = "select " +
                        "id" +
                        ",descripcion" +
                        ",abreviatura" +
                        ",estado" +
                        " from inv_bodegas" +
                        " where 1 = 1";
            if (id != null)
            {
                sql = sql + " and id = :id";
            }
            if (descripcion != null)
            {
                sql = sql + " and lower (descripcion) like :descripcion";
            }
            if (abreviatura != null)
            {
                sql = sql + " and abreviatura = :abreviatura";
            }
            if (estado != null)
            {
                sql = sql + " and estado = :estado";
            }

            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;
            List<CBodega> x_lista = new List<CBodega>();


            try
            {

                if (id != null)
                {
                    comando.Parameters.Add("id", OracleDbType.NVarchar2).Value = id.ToUpper();
                }
                if (descripcion != null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2).Value = "%" + descripcion.ToUpper() + "%";
                }
                if (abreviatura != null)
                {
                    comando.Parameters.Add("abreviatura", OracleDbType.NVarchar2).Value = "%" + abreviatura.ToUpper() + "%";
                }
                if (estado != null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2).Value = estado;
                }


                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CBodega a = new CBodega();

                    if (!leer.IsDBNull(0))
                    {
                        a.id = leer.GetString(0);
                    }

                    if (!leer.IsDBNull(1))
                    {
                        a.descripcion = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        a.abreviatura = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        a.estado = leer.GetString(3);
                    }

                    x_lista.Add(a);

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


        //----fin de listar aplicando filtros
        //

        //Nuevo Get Bodega VTR
        public CBodega get_bodega(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, string id, ref CError o_error)
        {
            o_error = new CError();


            //CRespuestaBodegaLista x_lista = new CRespuestaBodegaLista(); //prueba pero no se usa
            string sql = " select " +
                        " id" +
                        ", descripcion" +
                        ", abreviatura" +
                        ", estado" +
                        " from inv_bodegas " +
                        " where ";

            sql = sql + "  id = :id ";

            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;

            comando.Parameters.Add("id", OracleDbType.NVarchar2, 20).Value = id.ToUpper();

            CBodega x_respuesta = new CBodega();
            try
            {

                leer = comando.ExecuteReader();

                while (leer.Read())
                {


                    if (!leer.IsDBNull(0))
                    {
                        x_respuesta.id = leer.GetString(0);
                    }

                    if (!leer.IsDBNull(1))
                    {
                        x_respuesta.descripcion = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        x_respuesta.abreviatura = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        x_respuesta.estado = leer.GetString(3);
                    }

                }
                leer.Close();
            }
            catch (OracleException e)
            {

                o_error.id = e.Number;
                o_error.mensaje = e.Message;
                x_respuesta = null;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Error, revisar Codigo";
                }

            }
            catch (Exception e)
            {
                x_respuesta = null;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }

            return x_respuesta;
        }




        //Método para listar los registros de la tabla inv_bodegas
        public List<CBodega> listar_bodegas(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {

            o_error = new CError();

            string sql = "select " +
                        "id" +
                        ",descripcion" +
                        ",abreviatura" +
                        ",estado" +
                        " from inv_bodegas";

            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;
            List<CBodega> x_lista = new List<CBodega>();


            try
            {

                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CBodega a = new CBodega();

                    if (!leer.IsDBNull(0))
                    {
                        a.id = leer.GetString(0);
                    }

                    if (!leer.IsDBNull(1))
                    {
                        a.descripcion = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        a.abreviatura = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        a.estado = leer.GetString(3);
                    }

                    x_lista.Add(a);

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

        //Método para ingresar los registros de la tabla inv_bodegas
        public int bodega_ingresar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {

            o_error = new CError();

            int x_f = 0;
            int b_id = -1;

            try
            {

                string sql = "insert into inv_bodegas " +
                    "(id " +
                    ", descripcion " +
                    ", abreviatura " +
                    ", estado)" +
                    " values " +
                    "( :id " +
                    ", :descripcion " +
                    ", :abreviatura " +
                    ", :estado) ";


                OracleCommand comando = new OracleCommand(sql, i_con);

                comando.Parameters.Add("id", OracleDbType.NVarchar2, 20).Value = this.id.ToUpper();


                if (descripcion == null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = this.descripcion.ToUpper();

                }

                if (abreviatura == null)
                {
                    comando.Parameters.Add("abreviatura", OracleDbType.NVarchar2, 10).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("abreviatura", OracleDbType.NVarchar2, 10).Value = this.abreviatura.ToUpper();

                }

                if (estado == null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = this.estado.ToUpper();

                }


                x_f = comando.ExecuteNonQuery();

            }
            catch (OracleException e)
            {
                x_f = -1;

                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Error, la ID ya ha sido ingresada previamente";
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

        //Método para modificar los registros de la tabla inv_bodegas
        public int bodega_modificar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {
            o_error = new CError();

            int x_f = 0;
            string b_id = "";



            try
            {

                string sql = "update inv_bodegas set descripcion=:descripcion, abreviatura=:abreviatura, estado=:estado where id=:id";

                OracleCommand comando = new OracleCommand(sql, i_con);

                if (descripcion == null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = this.descripcion.ToUpper();
                }

                if (abreviatura == null)
                {
                    comando.Parameters.Add("abreviatura", OracleDbType.NVarchar2, 10).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("abreviatura", OracleDbType.NVarchar2, 10).Value = this.abreviatura.ToUpper();

                }

                if (estado == null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = this.estado.ToUpper();

                }
                if (id == null)
                {
                    comando.Parameters.Add("id", OracleDbType.NVarchar2, 20).Value = DBNull.Value;
                }
                else
                {

                    comando.Parameters.Add("id", OracleDbType.NVarchar2, 20).Value = this.id.ToUpper();

                }
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

        //Método para eliminar los registros de la tabla inv_bodegas
        public int bodega_eliminar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, string id, ref CError o_error)
        {
            int x_f = 0;
            o_error = new CError();

            try
            {
                string sql = "delete from inv_bodegas where id=:id";
                OracleCommand comando = new OracleCommand(sql, i_con);
                comando.Parameters.Add("id", OracleDbType.NVarchar2, 20).Value = id;
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
        ///////////////////////////////



    }


}
