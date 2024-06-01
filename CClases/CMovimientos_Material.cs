using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{
    public class CMovimientos_Material
    {

        // Atributos
        //public int id { get; set; }
        //public int id_contenedor { get; set; }
        //public int id_material { get; set; }
        //public int cantidad { get; set; }
        //public int id_documento_origen { get; set; }  //En tabla BD esta definido como INT, recordar.
        //public int id_tipo_documento { get; set; }
        //public string tipo_operacion { get; set; }

        public int saldo { get; set; }

        //metodo para insertar registros en tabla inv_material 18/08/2023

        //
        //
        //
        public CMovimientos_Material movimientos_material_contar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, int id_material, ref CError o_error)
        {
            o_error = new CError();

            string sql = "select" +
                "" +
                "" +
                " sum ( case when tipo_operacion ='I' then cantidad when tipo_operacion='E' then  -1* cantidad end ) as SALDO_TOTAL" +
                         " FROM INV_MOVIMIENTOS" +
                         " WHERE 1=1" +
                            " and ID_MATERIAL = :ID_MATERIAL ";


            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;

            comando.Parameters.Add("id_material", OracleDbType.Int32).Value = id_material;
            CMovimientos_Material x_respuesta = new CMovimientos_Material();
            try
            {

                leer = comando.ExecuteReader();

                if (leer.Read())
                {
                    CMovimientos_Material a = new CMovimientos_Material();

                    if (!leer.IsDBNull(0))
                    {
                        a.saldo = leer.GetInt32(0);
                    }

                    //if (!leer.IsDBNull(0))
                    //{
                    //    a.id = leer.GetInt32(0);
                    //}

                    //if (!leer.IsDBNull(1))
                    //{
                    //    a.id_contenedor = leer.GetInt32(1);
                    //}1
                    //if (!leer.IsDBNull(2))
                    //{
                    //    a.id_material = leer.GetInt32(2);
                    //}
                    //if (!leer.IsDBNull(3))
                    //{
                    //    a.tipo_operacion = leer.GetString(3);
                    //}
                    //if (!leer.IsDBNull(4))
                    //{
                    //    a.cantidad = leer.GetInt32(4);
                    //}
                    //if (!leer.IsDBNull(5))
                    //{
                    //    a.fecha = leer.GetDateTime(5);
                    //}
                    //if (!leer.IsDBNull(6))
                    //{
                    //    a.usuario = leer.GetString(6);
                    //}
                    //if (!leer.IsDBNull(7))
                    //{
                    //    a.ID_DOCUMENTO_ORIGEN = leer.GetInt32(7);
                    //}
                    //if (!leer.IsDBNull(8))
                    //{
                    //    a.ID_TIPO_DOCUMENTO_ORIGEN = leer.GetInt32(8);
                    //}


                    x_respuesta = a;


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
        //
        //
        //



        //public List<CMovimientos_Material> movimientos_material_leer(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        //{

        //    o_error = new CError();


        //    string sql = " SELECT ID, ID_CONTENEDOR, ID_MATERIAL, TIPO_OPERACION, CANTIDAD " +
        //                    " FROM INV_MOVIMIENTOS ";

        //    OracleCommand comando_leer = new OracleCommand(sql, x_con);
        //    OracleDataReader leer;
        //    List<CMovimientos_Material> x_lista = new List<CMovimientos_Material>();

        //    try
        //    {

        //        leer = comando_leer.ExecuteReader();

        //        while (leer.Read())
        //        {

        //            CMovimientos_Material x_leer = new CMovimientos_Material();

        //            if (!leer.IsDBNull(0))
        //            {
        //                x_leer.id = leer.GetInt32(0);
        //            }

        //            if (!leer.IsDBNull(1))
        //            {
        //                x_leer.id_contenedor = leer.GetInt32(1);
        //            }

        //            if (!leer.IsDBNull(2))
        //            {
        //                x_leer.id_material = leer.GetInt32(2);
        //            }

        //            if (!leer.IsDBNull(3))
        //            {
        //                x_leer.tipo_operacion = leer.GetString(3);
        //            }

        //            if (!leer.IsDBNull(4))
        //            {
        //                x_leer.cantidad = leer.GetInt32(4);
        //            }

        //            //if (!leer.IsDBNull(5))
        //            //{
        //            //    x_leer.documento_origen = leer.GetInt32(5);
        //            //}
        //            //if (!leer.IsDBNull(6))
        //            //{
        //            //    x_leer.tipo_documento = leer.GetInt32(6);
        //            //}


        //            x_lista.Add(x_leer);

        //        }
        //        leer.Close();

        //    }
        //    catch (OracleException e)
        //    {
        //        o_error.id = e.Number;
        //        o_error.mensaje = e.Message;
        //        x_lista = null;

        //        if (o_error.id == 1)
        //        {
        //            o_error.mensaje = "Error, revisar Codigo";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        x_lista = null;
        //        o_error.id = 100;
        //        o_error.mensaje = e.Message;
        //    }

        //    return x_lista;

        //}


        //public int movimientos_material_ingresar(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        //{
        //    o_error = new CError();


        //    int b_id = -1;
        //    int f_r = 0;

        //    string sql2 = "select seq_inv_material.nextval from dual";

        //    OracleCommand comando2 = new OracleCommand(sql2, x_con);

        //    OracleDataReader leer;

        //    CRespuestaInt x_r = new CRespuestaInt();

        //    try
        //    {



        //        leer = comando2.ExecuteReader();


        //        if (leer.Read())
        //        {

        //            if (!leer.IsDBNull(0))
        //            {
        //                b_id = leer.GetInt32(0);
        //            }
        //        }
        //        leer.Close();


        //        this.id = b_id;


        //        string sql = "CAMBIAR QUERY";


        //        OracleCommand comando = new OracleCommand(sql, x_con);

        //        comando.Parameters.Add("ID", OracleDbType.Int32).Value = this.id;

        //        if (id_contenedor == null)
        //        {
        //            comando.Parameters.Add("id_contenedor", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            if (id_contenedor != null)
        //            {
        //                comando.Parameters.Add("id_contenedor", OracleDbType.NVarchar2, 100).Value = this.id_contenedor;
        //            }
        //        }

        //        if (id_material == null)
        //        {
        //            comando.Parameters.Add("id_material", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            if (id_material != null)
        //            {
        //                comando.Parameters.Add("id_material", OracleDbType.NVarchar2, 200).Value = this.id_material;
        //            }
        //        }

        //        if (cantidad == null)
        //        {
        //            comando.Parameters.Add("cantidad", OracleDbType.NVarchar2, 400).Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            if (cantidad != null)
        //            {
        //                comando.Parameters.Add("cantidad", OracleDbType.NVarchar2, 400).Value = this.cantidad;
        //            }
        //        }

        //        if (id_documento_origen == null)
        //        {
        //            comando.Parameters.Add("id_documento_origen", OracleDbType.NVarchar2, 1).Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            if (id_documento_origen != null)
        //            {
        //                comando.Parameters.Add("estid_documento_origenado", OracleDbType.NVarchar2, 1).Value = this.id_documento_origen;
        //            }
        //        }


        //        if (id_tipo_documento == null)
        //        {
        //            comando.Parameters.Add("id_tipo_documento", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            if (id_tipo_documento != null)
        //            {
        //                comando.Parameters.Add("id_tipo_documento", OracleDbType.NVarchar2, 100).Value = this.id_tipo_documento;
        //            }
        //        }

        //        f_r = comando.ExecuteNonQuery();

        //    }
        //    catch (OracleException e)
        //    {
        //        f_r = -1;
        //        this.id = -1;
        //        o_error.id = e.Number;
        //        o_error.mensaje = e.Message;

        //        if (o_error.id == 1)
        //        {
        //            o_error.mensaje = "Codigo material ya existe";
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        this.id = -1;
        //        f_r = -1;
        //        o_error.id = 100;
        //        o_error.mensaje = e.Message;
        //    }

        //    return this.id;

        //}

        //public int movimiento_material_eliminar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, string id, ref CError o_error)
        //{
        //    int x_r = 0;
        //    o_error = new CError();

        //    try
        //    {
        //        string sql = "delete from inv_movimientos where id=:id";

        //        OracleCommand comando = new OracleCommand(sql, i_con);

        //        comando.Parameters.Add("id", OracleDbType.Int32).Value = id;

        //        x_r = comando.ExecuteNonQuery();
        //    }

        //    catch (OracleException e)
        //    {
        //        x_r = -1;
        //        this.id = -1;
        //        o_error.id = e.Number;
        //        o_error.mensaje = e.Message;

        //        if (o_error.id == 1)
        //        {
        //            o_error.mensaje = "Error al eliminar, revisar ID";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        this.id = -1;
        //        x_r = -1;
        //        o_error.id = 100;
        //        o_error.mensaje = e.Message;
        //    }
        //    return x_r;
        //}






    }
}

