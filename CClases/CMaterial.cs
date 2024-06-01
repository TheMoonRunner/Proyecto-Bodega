using API_REST.CRespuestas;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{

    public class CMaterial
    {
        //Atributos de la clase CContenedor

        public Nullable<int> id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int ancho { get; set; }
        public int alto { get; set; }
        public int largo { get; set; }
        public string codigo { get; set; }
        public string estado { get; set; }
        public string empresa { get; set; }
        public string unidades { get; set; }
        public int peso { get; set; }
        public string seriado { get; set; }
        public int tipo_material { get; set; }  // se ocupa en el insert
        public string descripcion_tm { get; set; }  //se ocupa en el leer




        //Método para leer los registros de la tabla inv_material
        public List<CMaterial> material_leer(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, Nullable<int> id, string nombre, string codigo, string empresa, ref CError o_error)
        {

            o_error = new CError();


            string sql = "select " +
                            "  MAT.ID " +
                            "  , MAT.CODIGO " +
                            "  , MAT.NOMBRE " +
                            "  , MAT.DESCRIPCION " +
                            "  , MAT.ESTADO " +
                            "  , MAT.EMPRESA " +
                            "  , MAT.UNIDADES" +
                            "  , MAT.PESO " +
                            "  , MAT.ANCHO " +
                            "  , MAT.ALTO " +
                            "  , MAT.LARGO " +
                            "  , MAT.SERIADO " +
                            "  , MAT.TIPO_MATERIAL " +
                            "  , T.DESCRIPCION TIPO_MATERIAL " +
                            "   FROM INV_MATERIAL MAT " +
                            "   JOIN INV_TIPO_MATERIAL T ON MAT.TIPO_MATERIAL = T.ID " +
                            "   WHERE 1 = 1 ";


            if (id != null)
            {
                sql = sql + " and MAT.ID = :mat_id ";
            }
            if (codigo != null)
            {
                sql = sql + " and MAT.CODIGO = :mat_codigo ";
            }
            if (nombre != null)
            {
                sql = sql + " and MAT.NOMBRE like :mat_nombre ";
            }
            if (empresa != null)
            {
                sql = sql + " and mat.empresa like :mat_empresa ";
            }
            sql = sql + " order by mat.id desc ";


            OracleCommand comando_leer = new OracleCommand(sql, x_con);
            OracleDataReader leer;
            List<CMaterial> x_lista = new List<CMaterial>();
            try
            {

                if (id != null)
                {
                    comando_leer.Parameters.Add("mat_id", OracleDbType.Int32).Value = id;
                }
                if (nombre != null)
                {
                    comando_leer.Parameters.Add("mat_nombre", OracleDbType.NVarchar2, 200).Value = "%" + nombre.ToUpper() + "%";
                }
                if (codigo != null)
                {
                    comando_leer.Parameters.Add("mat_codigo", OracleDbType.NVarchar2, 200).Value = codigo.ToUpper();
                }
                if (empresa != null)
                {
                    comando_leer.Parameters.Add("mat_empresa", OracleDbType.NVarchar2, 100).Value = "%" + empresa.ToUpper() + "%";
                }

                leer = comando_leer.ExecuteReader();
                while (leer.Read())
                {
                    CMaterial x_leer = new CMaterial();
                    if (!leer.IsDBNull(0))
                    {
                        x_leer.id = leer.GetInt32(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        x_leer.codigo = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        x_leer.nombre = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        x_leer.descripcion = leer.GetString(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        x_leer.estado = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        x_leer.empresa = leer.GetString(5);
                    }
                    if (!leer.IsDBNull(6))
                    {
                        x_leer.unidades = leer.GetString(6);
                    }
                    if (!leer.IsDBNull(7))
                    {
                        x_leer.peso = leer.GetInt32(7);
                    }
                    if (!leer.IsDBNull(8))
                    {
                        x_leer.ancho = leer.GetInt32(8);
                    }
                    if (!leer.IsDBNull(9))
                    {
                        x_leer.alto = leer.GetInt32(9);
                    }
                    if (!leer.IsDBNull(10))
                    {
                        x_leer.largo = leer.GetInt32(10);
                    }
                    if (!leer.IsDBNull(11))
                    {
                        x_leer.seriado = leer.GetString(11);
                    }
                    if (!leer.IsDBNull(12))
                    {
                        x_leer.tipo_material = leer.GetInt32(12);
                    }
                    if (!leer.IsDBNull(13))
                    {
                        x_leer.descripcion_tm = leer.GetString(13);
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


        public int ingresar_material(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {

            o_error = new CError();

            int b_id = -1;
            int f_r = 0;

            string sql2 = "select seq_inv_material.nextval from dual";

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


                string sql = "insert into inv_material" +
                        " (ID" +
                        ", CODIGO" +
                        ", NOMBRE" +
                        ", DESCRIPCION" +
                        ", ESTADO" +
                        ", EMPRESA" +
                        ", UNIDADES" +
                        ", PESO" +
                        ", ANCHO" +
                        ", ALTO" +
                        ", LARGO" +
                        ", SERIADO" +
                        ", TIPO_MATERIAL)" +
                        " VALUES" +
                    " (:ID" +
                    ", :CODIGO" +
                    ", :NOMBRE" +
                    ", :DESCRIPCION" +
                    ", :ESTADO" +
                    ", :EMPRESA" +
                    ", :UNIDADES" +
                    ", :PESO" +
                    ", :ANCHO" +
                    ", :ALTO" +
                    ", :LARGO" +
                    ", :SERIADO" +
                    ", :TIPO_MATERIAL)";

                OracleCommand comando = new OracleCommand(sql, x_con);

                comando.Parameters.Add("ID", OracleDbType.Int32).Value = this.id;

                if (codigo == null)
                {
                    comando.Parameters.Add("CODIGO", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
                }
                else
                {
                    if (codigo != null)
                    {
                        comando.Parameters.Add("CODIGO", OracleDbType.NVarchar2, 100).Value = this.codigo.ToUpper();
                    }
                }

                if (nombre == null)
                {
                    comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {
                    if (nombre != null)
                    {
                        comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 200).Value = this.nombre.ToUpper();
                    }
                }

                if (descripcion == null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 400).Value = DBNull.Value;
                }
                else
                {
                    if (descripcion != null)
                    {
                        comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 400).Value = this.descripcion.ToUpper();
                    }
                }

                if (estado == null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = DBNull.Value;
                }
                else
                {
                    if (estado != null)
                    {
                        comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = this.estado.ToUpper();
                    }
                }

                if (empresa == null)
                {
                    comando.Parameters.Add("empresa", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
                }
                else
                {
                    if (empresa != null)
                    {
                        comando.Parameters.Add("empresa", OracleDbType.NVarchar2, 100).Value = this.empresa.ToUpper();
                    }
                }

                //

                if (unidades == null)
                {
                    comando.Parameters.Add("unidades", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
                }
                else
                {
                    if (unidades != null)
                    {
                        comando.Parameters.Add("unidades", OracleDbType.NVarchar2, 100).Value = this.unidades.ToUpper();
                    }
                }

                comando.Parameters.Add("peso", OracleDbType.Int32).Value = this.peso;
                comando.Parameters.Add("ancho", OracleDbType.Int32).Value = this.ancho;
                comando.Parameters.Add("alto", OracleDbType.Int32).Value = this.alto;
                comando.Parameters.Add("largo", OracleDbType.Int32).Value = this.largo;
                comando.Parameters.Add("seriado", OracleDbType.NVarchar2).Value = this.seriado.ToUpper();
                comando.Parameters.Add("tipo_material", OracleDbType.Int32).Value = this.tipo_material;

                f_r = comando.ExecuteNonQuery();

            }
            catch (OracleException e)
            {
                f_r = -1;
                this.id = -1;
                o_error.id = e.Number;
                o_error.mensaje = e.Message;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Codigo material ya existe";
                }

            }
            catch (Exception e)
            {
                this.id = -1;
                f_r = -1;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }

            return f_r;

        }

        //Método para modificar los registros de la tabla inv_contenedor
        public int modificar_material(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();
            int x_f = 0;
            int b_id = 0;

            try
            {
                string sql = " update inv_material set   " +
                                " codigo =:codigo  " +
                                ", nombre =:nombre  " +
                                ", descripcion =:descipcion  " +
                                ", estado=:estado  " +
                                ", empresa =:empresa  " +
                                ", unidades =:unidades  " +
                                ", peso =:peso  " +
                                ", ancho =:ancho  " +
                                ", alto =:alto  " +
                                ", largo =:largo   " +
                                ", seriado = :seriado " +
                                ", tipo_material = :tipo_material " +
                                " where id =:id  ";

                OracleCommand comando = new OracleCommand(sql, x_con);

                comando.Parameters.Add("codigo", OracleDbType.NVarchar2, 100).Value = this.codigo.ToUpper();

                if (nombre == null)
                {
                    comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {
                    if (nombre != null)
                    {
                        comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 200).Value = this.nombre.ToUpper();
                    }
                }

                if (descripcion == null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 400).Value = DBNull.Value;
                }
                else
                {
                    if (descripcion != null)
                    {
                        comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 400).Value = this.descripcion.ToUpper();
                    }
                }

                if (estado == null)
                {
                    comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = DBNull.Value;
                }
                else
                {
                    if (estado != null)
                    {
                        comando.Parameters.Add("estado", OracleDbType.NVarchar2, 1).Value = this.estado.ToUpper();
                    }
                }

                if (empresa == null)
                {
                    comando.Parameters.Add("empresa", OracleDbType.NVarchar2, 100).Value = DBNull.Value;
                }
                else
                {
                    if (empresa != null)
                    {
                        comando.Parameters.Add("empresa", OracleDbType.NVarchar2, 100).Value = this.empresa.ToUpper();
                    }
                }

                if (unidades == null)
                {
                    comando.Parameters.Add("unidades", OracleDbType.NVarchar2, 400).Value = DBNull.Value;
                }
                else
                {
                    if (unidades != null)
                    {
                        comando.Parameters.Add("unidades", OracleDbType.NVarchar2, 400).Value = this.unidades.ToUpper();
                    }
                }

                comando.Parameters.Add("peso", OracleDbType.Int32).Value = this.peso;
                comando.Parameters.Add("ancho", OracleDbType.Int32).Value = this.ancho;
                comando.Parameters.Add("alto", OracleDbType.Int32).Value = this.alto;
                comando.Parameters.Add("largo", OracleDbType.Int32).Value = this.largo;

                comando.Parameters.Add("seriado", OracleDbType.NVarchar2, 200).Value = this.seriado.ToUpper();
                comando.Parameters.Add("tipo_material", OracleDbType.Int32).Value = this.tipo_material;

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
                    o_error.mensaje = "Codigo material ya existe";
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

        //Método para eliminar los registros de la tabla inv_material
        public int material_eliminar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, string id, ref CError o_error)
        {
            int x_r = 0;
            o_error = new CError();
            try
            {
                string sql = "delete from inv_material where id=:id";
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
                    o_error.mensaje = "Error al eliminar, revisar ID";
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

