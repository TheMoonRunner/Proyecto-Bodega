using API_REST.CRespuestas;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{
    public class Cmovimientos
    {
        //AQUI VAN LOS ATRIBUTOS DE LA CLASE

        public Nullable<int> id { get; set; }
        public Nullable<int> id_material { get; set; }
        public Nullable<int> id_contenedor { get; set; }
        public string tipo_operacion { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<DateTime> fecha { get; set; }
        public string usuario { get; set; }

        public Nullable<int> ID_DOCUMENTO_ORIGEN { get; set; }
        public Nullable<int> ID_TIPO_DOCUMENTO_ORIGEN { get; set; }
        public string serie { get; set; }



        // FIN DE LOS ATRIBUTOS

        // INGRESAR LISTA METODO ALTERNATIVO

        //Método para insertar los registros de la tabla inv_contenedor con su secuencia
        public int movimientos_ingresar_lista(OracleConnection i_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();

            int f_r = 0;
            int b_id = -1;

            string sql2 = "select SEQ_INV_MOVIMIENTOS.nextval from dual";

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


                string sql = "insert into inv_movimientos " +
                        "(id" +
                        ", id_contenedor" +
                        ", id_material" +
                        ", tipo_operacion" +
                        ", cantidad" +
                        ", fecha" +
                        ", usuario" +
                        ", ID_DOCUMENTO_ORIGEN" +
                        ", ID_TIPO_DOCUMENTO_ORIGEN" +
                        ", SERIE )" +
                        " values" +
                        " ( :id " +
                        ", :id_contenedor" +
                        ", :id_material" +
                        ", :tipo_operacion" +
                        ", :cantidad" +
                        ", sysdate" +
                        ", :usuario " +
                        ", :ID_DOCUMENTO_ORIGEN" +
                        ", :ID_TIPO_DOCUMENTO_ORIGEN" +
                        ", :SERIE)";

                OracleCommand comando = new OracleCommand(sql, i_con);

                comando.Parameters.Add("id", OracleDbType.Int32).Value = this.id;
                comando.Parameters.Add("id_contenedor", OracleDbType.NVarchar2, 50).Value = this.id_contenedor; // da error al poner .ToUpper()
                comando.Parameters.Add("id_material", OracleDbType.Int32).Value = this.id_material;
                comando.Parameters.Add("tipo_operacion", OracleDbType.NVarchar2, 50).Value = this.tipo_operacion.ToUpper();
                comando.Parameters.Add("cantidad", OracleDbType.Int32).Value = this.cantidad;
                comando.Parameters.Add("usuario", OracleDbType.NVarchar2).Value = this.usuario.ToUpper();
                comando.Parameters.Add("ID_DOCUMENTO_ORIGEN", OracleDbType.Int32).Value = this.ID_DOCUMENTO_ORIGEN;
                comando.Parameters.Add("ID_TIPO_DOCUMENTO_ORIGEN", OracleDbType.Int32).Value = this.ID_TIPO_DOCUMENTO_ORIGEN;
                comando.Parameters.Add("SERIE", OracleDbType.NVarchar2).Value = this.serie.ToUpper();




                f_r = comando.ExecuteNonQuery();





            }
            catch (OracleException e)
            {
                f_r = -1;
                //this.id = -1;
                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Codigo material ya existe";
                }

            }
            catch (Exception e)
            {
                //this.id = -1;

                f_r = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }
            // x_r.valor = (int) this.id;

            return f_r;

        }




        public List<Cmovimientos> movimientos_listado(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, Nullable<int> id, Nullable<int> id_material, string tipo_operacion, string usuario, ref CError o_error)
        {
            o_error = new CError();

            string sql = "select id" +
                ", id_contenedor" +
                ", id_material" +
                ", tipo_operacion" +
                ", cantidad" +
                ", fecha" +
                ", usuario" +
                ", ID_DOCUMENTO_ORIGEN" +
                ", ID_TIPO_DOCUMENTO_ORIGEN" +
                " from inv_movimientos " +
                " where 1 = 1 ";

            if (id != null)
            {
                sql = sql + " and id = :id ";
            }
            if (id_material != null)
            {
                sql = sql + " and id_material = :id_material "; //despues del like suele generar problemas al llamarse similar. cambiarlo a id.material en caso de inconvenientes
            }
            if (tipo_operacion != null)
            {
                sql = sql + " and tipo_operacion = :tipo_operacion ";
            }
            if (usuario != null)
            {
                sql = sql + " and usuario like :usuario ";
            }
            sql = sql + " order by id asc ";

            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;


            List<Cmovimientos> x_lista = new List<Cmovimientos>();
            try
            {
                if (id != null)
                {
                    comando.Parameters.Add("id", OracleDbType.Int32).Value = id;
                }
                if (id_material != null)
                {
                    comando.Parameters.Add("id_material", OracleDbType.Int32).Value = id_material;
                }
                if (tipo_operacion != null)
                {
                    comando.Parameters.Add("tipo_operacion", OracleDbType.NVarchar2, 50).Value = tipo_operacion.ToUpper();
                }
                if (usuario != null)
                {
                    comando.Parameters.Add("usuario", OracleDbType.NVarchar2).Value = "%" + usuario.ToUpper() + "%";
                }



                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    Cmovimientos a = new Cmovimientos();

                    if (!leer.IsDBNull(0))
                    {
                        a.id = leer.GetInt32(0);
                    }

                    if (!leer.IsDBNull(1))
                    {
                        a.id_contenedor = leer.GetInt32(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        a.id_material = leer.GetInt32(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        a.tipo_operacion = leer.GetString(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        a.cantidad = leer.GetInt32(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        a.fecha = leer.GetDateTime(5);
                    }
                    if (!leer.IsDBNull(6))
                    {
                        a.usuario = leer.GetString(6);
                    }
                    if (!leer.IsDBNull(7))
                    {
                        a.ID_DOCUMENTO_ORIGEN = leer.GetInt32(7);
                    }
                    if (!leer.IsDBNull(8))
                    {
                        a.ID_TIPO_DOCUMENTO_ORIGEN = leer.GetInt32(8);
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


        //Método para insertar los registros de la tabla inv_contenedor con su secuencia
        public int movimientos_ingresar(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();
            int f_r = 0;
            int b_id = -1;

            string sql2 = "select SEQ_INV_MOVIMIENTOS.nextval from dual";
            OracleCommand comando2 = new OracleCommand(sql2, x_con);

            OracleDataReader leer;

            CRespuestaInt x_r = new CRespuestaInt();
            try
            {
                leer = comando2.ExecuteReader();

                if (leer.Read())
                {
                    if (!leer.IsDBNull(0))
                    {
                        b_id = leer.GetInt32(0);
                    }
                }
                leer.Close();

                this.id = b_id;

                string sql = "insert into inv_movimientos " +
                            "(id" +
                            ", id_contenedor" +
                            ", id_material" +
                            ", tipo_operacion" +
                            ", cantidad" +
                            ", fecha" +
                            ", usuario" +
                            ", ID_DOCUMENTO_ORIGEN" +
                            ", ID_TIPO_DOCUMENTO_ORIGEN" +
                            ", SERIE )" +
                            " values" +
                            " ( :id " +
                            ", :id_contenedor" +
                            ", :id_material" +
                            ", :tipo_operacion" +
                            ", :cantidad" +
                            ", sysdate" +
                            ", :usuario " +
                            ", :ID_DOCUMENTO_ORIGEN" +
                            ", :ID_TIPO_DOCUMENTO_ORIGEN" +
                            ", :SERIE)";


                OracleCommand comando = new OracleCommand(sql, x_con);

                comando.Parameters.Add("id", OracleDbType.Int32).Value = this.id;
                comando.Parameters.Add("id_contenedor", OracleDbType.NVarchar2, 50).Value = this.id_contenedor;
                comando.Parameters.Add("id_material", OracleDbType.Int32).Value = this.id_material;
                comando.Parameters.Add("tipo_operacion", OracleDbType.NVarchar2, 50).Value = this.tipo_operacion.ToUpper();
                comando.Parameters.Add("cantidad", OracleDbType.Int32).Value = this.cantidad;

                comando.Parameters.Add("usuario", OracleDbType.NVarchar2).Value = this.usuario.ToUpper();
                comando.Parameters.Add("ID_DOCUMENTO_ORIGEN", OracleDbType.Int32).Value = this.ID_DOCUMENTO_ORIGEN;
                comando.Parameters.Add("ID_TIPO_DOCUMENTO_ORIGEN", OracleDbType.Int32).Value = this.ID_TIPO_DOCUMENTO_ORIGEN;

                if (serie != null)
                {
                    comando.Parameters.Add("SERIE", OracleDbType.NVarchar2).Value = this.serie.ToUpper();
                }

                f_r = comando.ExecuteNonQuery();

            }

            catch (OracleException e)
            {
                f_r = -1;
                //this.id = -1;
                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Codigo material ya existe";
                }

            }
            catch (Exception e)
            {
                //this.id = -1;
                f_r = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }
            // x_r.valor = (int) this.id;

            return f_r;

        }

        //
        //Método para modificar los registros de la tabla inv_contenedor
        public int Movimientos_modificar(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();
            int x_f = 0;
            int b_id = 0;

            try
            {
                string sql
                            = "update inv_movimientos set " +
                                "   id_contenedor = :id_contenedor " +
                                "   , id_material = :id_material " +
                                "   , tipo_operacion = :tipo_operacion " +
                                "   , cantidad = :cantidad " +
                                "   , usuario = :usuario " +
                                "   , id_documento_origen = :id_documento_origen " +
                                "   , id_tipo_documento_origen = :id_tipo_documento_origen " +
                                "     where id = :id ";


                OracleCommand comando = new OracleCommand(sql, x_con);



                if (id_contenedor == null)
                {
                    comando.Parameters.Add("id_contenedor", OracleDbType.Int32).Value = DBNull.Value;
                }
                else
                {
                    if (id_contenedor != null)
                    {
                        comando.Parameters.Add("id_contenedor", OracleDbType.Int32).Value = this.id_contenedor;
                    }
                }

                if (id_material == null)
                {
                    comando.Parameters.Add("id_material", OracleDbType.Int32).Value = DBNull.Value;
                }
                else
                {
                    if (id_material != null)
                    {
                        comando.Parameters.Add("id_material", OracleDbType.Int32).Value = this.id_material;
                    }
                }

                if (tipo_operacion == null)
                {
                    comando.Parameters.Add("tipo_operacion", OracleDbType.NVarchar2, 50).Value = DBNull.Value;
                }
                else
                {
                    if (tipo_operacion != null)
                    {
                        comando.Parameters.Add("tipo_operacion", OracleDbType.NVarchar2, 50).Value = this.tipo_operacion.ToUpper();
                    }
                }

                if (cantidad == null)
                {
                    comando.Parameters.Add("cantidad", OracleDbType.Int32).Value = DBNull.Value;
                }
                else
                {
                    if (cantidad != null)
                    {
                        comando.Parameters.Add("cantidad", OracleDbType.Int32).Value = this.cantidad;
                    }
                }

                if (usuario == null)
                {
                    comando.Parameters.Add("usuario", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
                }
                else
                {
                    if (usuario != null)
                    {
                        comando.Parameters.Add("usuario", OracleDbType.NVarchar2, 100).Value = this.usuario.ToUpper();
                    }
                }


                if (ID_DOCUMENTO_ORIGEN == null)
                {
                    comando.Parameters.Add("ID_DOCUMENTO_ORIGEN", OracleDbType.Int32).Value = DBNull.Value;
                }
                else
                {
                    if (ID_DOCUMENTO_ORIGEN != null)
                    {
                        comando.Parameters.Add("ID_DOCUMENTO_ORIGEN", OracleDbType.Int32).Value = this.ID_DOCUMENTO_ORIGEN;
                    }
                }

                if (ID_TIPO_DOCUMENTO_ORIGEN == null)
                {
                    comando.Parameters.Add("unidades", OracleDbType.Int32).Value = DBNull.Value;
                }
                else
                {
                    if (ID_TIPO_DOCUMENTO_ORIGEN != null)
                    {
                        comando.Parameters.Add("unidades", OracleDbType.Int32).Value = this.ID_TIPO_DOCUMENTO_ORIGEN;
                    }
                }
                comando.Parameters.Add("id", OracleDbType.Int32).Value = id;
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
                    o_error.mensaje = "Error, verifica el codigo 'Modifcar' ";
                }
            }
            catch (Exception e)
            {
                this.id = -1;
                x_f = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }

            // material_leer(x_con, x_tran, x_usuario, ref o_error); SE HABIA PROBADO - RESULTADO: FALLIDO
            return x_f;
        }

        //

        //Método para eliminar registros de la tabla inv_tipo_contenedor
        public int movimientos_eliminar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, int id, ref CError o_error)
        {
            int x_r = 0;
            o_error = new CError();

            try
            {
                string sql = "delete from inv_movimientos where id=:id";
                OracleCommand comando = new OracleCommand(sql, i_con);
                comando.Parameters.Add("id", OracleDbType.Int32).Value = id;
                x_r = comando.ExecuteNonQuery();
            }
            catch (OracleException e)
            {
                x_r = -1;
                this.id = -1;
                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Error al eliminar, revisr ID";
                }
            }
            catch (Exception e)
            {
                this.id = -1;
                x_r = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }
            return x_r;
        }
    }
}
