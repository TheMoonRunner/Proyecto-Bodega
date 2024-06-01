using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_REST.CRespuestas;
using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_REST.CClases
{
    public class CContenedor
    {


        //Atributos de la clase CContenedor
        public string id_bodega { get; set; }
        public Nullable<int> id { get; set; }
        public int id_padre { get; set; }
        public int tipo_contenedor { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
        public int ancho { get; set; }
        public int alto { get; set; }
        public int largo { get; set; }
        public int maximo_peso { get; set; }

        public string tipo_contenedor_desc { get; set; }

        public string codigo { get; set; }
        public int unidad { get; set; }
        public int peso { get; set; }
        public string estado { get; set; }
        public string unidades { get; set; }
        public string empresa { get; set; }

        public int id_contenedor { get; set; }


        // Trae todos los materiales de la bodega por contenedor

        public List<CContenedor> get_materiales_contenedor_bodega(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, string id_bodega, ref CError o_error)
        {
            o_error = new CError();


            string sql = "select  " +
                       "  MAT.ID " +
                       "  , MAT.CODIGO " +
                       "  , MAT.NOMBRE " +
                       "  , MAT.DESCRIPCION " +
                       "  , MAT.ESTADO " +
                       "  , MAT.EMPRESA " +
                       "  , MAT.UNIDADES " +
                       "  , CONT.TIPO_CONTENEDOR " +
                       "   FROM INV_MATERIAL MAT " +
                       "   JOIN INV_MATERIAL_CONTENEDOR MC ON MAT.ID = MC.ID_MATERIAL " +
                       "   JOIN INV_CONTENEDOR CONT ON MC.ID_CONTENEDOR = CONT.ID " +
                       "   JOIN INV_BODEGAS BOD ON CONT.ID_BODEGA = BOD.ID " +
                       "   WHERE ID_BODEGA = :ID_BODEGA ";


            OracleCommand comando = new OracleCommand(sql, x_con);
            OracleDataReader leer;

            comando.Parameters.Add("ID_BODEGA", OracleDbType.NVarchar2).Value = id_bodega.ToUpper();

            List<CContenedor> x_respuesta = new List<CContenedor>();

            try
            {

                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CContenedor cont = new CContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        cont.id = leer.GetInt32(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        cont.codigo = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        cont.nombre = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        cont.descripcion = leer.GetString(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        cont.estado = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        cont.empresa = leer.GetString(5);
                    }
                    if (!leer.IsDBNull(6))
                    {
                        cont.unidades = leer.GetString(6);
                    }
                    if (!leer.IsDBNull(7))
                    {
                        cont.tipo_contenedor = leer.GetInt32(7);
                    }

                    x_respuesta.Add(cont);
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



        // Trae todos los materiales independiente del contenedor de una bodega seleccionada
        public List<CContenedor> get_materiales_bodega(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, string id_bodega, ref CError o_error) // si aparece MATCH
        {
            o_error = new CError();


            string sql = "  select  " +
                            "  MAT.ID " +
                            "  , MAT.CODIGO " +
                            "  , MAT.NOMBRE " +
                            "  , MAT.DESCRIPCION " +
                            "  , MAT.ESTADO " +
                            "  , MAT.EMPRESA " +
                            "  , MAT.UNIDADES " +
                            "   FROM INV_MATERIAL MAT " +
                            "   JOIN INV_MATERIAL_CONTENEDOR MC ON MAT.ID = MC.ID_MATERIAL " +
                            "   JOIN INV_CONTENEDOR CONT ON MC.ID_CONTENEDOR = CONT.ID " +
                            "   JOIN INV_BODEGAS BOD ON CONT.ID_BODEGA = BOD.ID " +
                            "   WHERE ID_BODEGA = :ID_BODEGA";


            OracleCommand comando = new OracleCommand(sql, x_con);
            OracleDataReader leer;

            comando.Parameters.Add("ID_BODEGA", OracleDbType.NVarchar2).Value = id_bodega.ToUpper();

            List<CContenedor> x_respuesta = new List<CContenedor>();

            try
            {

                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CContenedor cont = new CContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        cont.id = leer.GetInt32(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        cont.codigo = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        cont.nombre = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        cont.descripcion = leer.GetString(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        cont.estado = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        cont.empresa = leer.GetString(5);
                    }
                    if (!leer.IsDBNull(6))
                    {
                        cont.unidades = leer.GetString(6);
                    }
                    //if (!leer.IsDBNull(7))
                    //{
                    //    cont.tipo_contenedor = leer.GetInt32(7);
                    //}

                    x_respuesta.Add(cont);
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

        public List<CContenedor> listar_materiales_bodega(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, Nullable<int> id, string nombre, Nullable<int> id_contenedor, ref CError o_error) //si hay match
        {
            o_error = new CError();



            string sql = "SELECT  " +
                        "  MAT.ID ID_MATERIAL " +
                        "  , MAT.CODIGO CODIGO_MATERIAL " +
                        "  , CONT.ID ID_CONTENEDOR " +
                        "  , MAT.NOMBRE  " +
                        "  , MAT.DESCRIPCION  " +
                        "  , BOD.ID BODEGA " +
                        "  , MAT.ESTADO  " +
                        "  , MAT.EMPRESA  " +
                        "  , MAT.UNIDADES  " +
                        "   FROM INV_MATERIAL MAT  " +
                        "   JOIN INV_MATERIAL_CONTENEDOR MC ON MAT.ID = MC.ID_MATERIAL     " +
                        "   JOIN INV_CONTENEDOR CONT ON MC.ID_CONTENEDOR = CONT.ID  " +
                        "   JOIN INV_BODEGAS BOD ON CONT.ID_BODEGA = BOD.ID " +
                        "   WHERE 1=1  ";


            if (id != null)
            {
                sql = sql + " and mat.id = :mat_id ";
            }
            if (nombre != null)
            {
                sql = sql + " and mat.nombre like :mat_nombre ";
            }
            if (id_contenedor != null)
            {
                sql = sql + " and cont.id = :cont_id";
            }


            OracleCommand comando = new OracleCommand(sql, x_con);
            OracleDataReader leer;
            List<CContenedor> x_respuesta = new List<CContenedor>();

            try
            {
                if (id != null)
                {
                    comando.Parameters.Add("mat_id", OracleDbType.Int32).Value = id;
                }
                if (nombre != null)
                {
                    comando.Parameters.Add("mat_nombre", OracleDbType.NVarchar2, 200).Value = "%" + nombre.ToUpper() + "%";
                }
                if (id_contenedor != null)
                {
                    comando.Parameters.Add("cont_id", OracleDbType.Int32).Value = id_contenedor;
                }


                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CContenedor cont = new CContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        cont.id = leer.GetInt32(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        cont.codigo = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        cont.id_contenedor = leer.GetInt32(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        cont.nombre = leer.GetString(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        cont.descripcion = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        cont.id_bodega = leer.GetString(5);
                    }
                    if (!leer.IsDBNull(6))
                    {
                        cont.estado = leer.GetString(6);
                    }
                    if (!leer.IsDBNull(7))
                    {
                        cont.empresa = leer.GetString(7);
                    }
                    if (!leer.IsDBNull(8))
                    {
                        cont.unidades = leer.GetString(8);
                    }


                    x_respuesta.Add(cont);
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

        //Nuevo metodo similar al utilizado en CBodegas para obtener datos de una bodega (get_bodega)
        public CContenedor get_contenedor(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, int id, ref CError o_error)
        {
            o_error = new CError();

            string sql = "select  " +
                            " c.id_bodega " +
                            ",  c.id  " +
                            ", c.id_padre " +
                            ", c.tipo_contenedor  " +
                            ", c.nombre " +
                            ", tc.descripcion tipo_contenedor_desc" +
                            ", c.descripcion " +
                            ", c.ubicacion " +
                            ", c.ancho " +
                            ", c.alto " +
                            ", c.largo  " +
                            ", c.maximo_peso  " +
                            " from inv_contenedor  c " +
                            " , inv_tipo_contenedor tc " +
                            " where c.tipo_contenedor = tc.id " +
                            " and c.id = :id ";



            OracleCommand comando = new OracleCommand(sql, x_con);
            OracleDataReader leer;

            comando.Parameters.Add("id", OracleDbType.Int32).Value = id;

            CContenedor x_respuesta = new CContenedor();

            try
            {

                leer = comando.ExecuteReader();

                if (leer.Read())
                {
                    CContenedor cont = new CContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        cont.id_bodega = leer.GetString(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        cont.id = leer.GetInt32(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        cont.id_padre = leer.GetInt32(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        cont.tipo_contenedor = leer.GetInt32(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        cont.nombre = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        cont.tipo_contenedor_desc = leer.GetString(5); //Descripcion de la nueva tabla agregada
                    }
                    if (!leer.IsDBNull(6))
                    {
                        cont.descripcion = leer.GetString(6);
                    }
                    if (!leer.IsDBNull(7))
                    {
                        cont.ubicacion = leer.GetString(7);
                    }
                    if (!leer.IsDBNull(8))
                    {
                        cont.ancho = leer.GetInt32(8);
                    }
                    if (!leer.IsDBNull(9))
                    {
                        cont.alto = leer.GetInt32(9);
                    }
                    if (!leer.IsDBNull(10))
                    {
                        cont.largo = leer.GetInt32(10);
                    }
                    if (!leer.IsDBNull(11))
                    {
                        cont.maximo_peso = leer.GetInt32(11);
                    }
                    x_respuesta = cont;
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






        // Fin nuevo metodo



        //Método para leer los registros de la tabla inv_contenedor
        public List<CContenedor> leer_registros(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, string id_bodega, Nullable<int> id_padre, Nullable<int> tipo_contenedor, string nombre, ref CError o_error)
        {
            o_error = new CError();

            string sql = "select  " +
                            " c.id_bodega " +
                            ",  c.id  " +
                            ", c.id_padre " +
                            ", c.tipo_contenedor  " +
                            ", c.nombre " +
                            ", tc.descripcion tipo_contenedor_desc" +
                            ", c.descripcion " +
                            ", c.ubicacion " +
                            ", c.ancho " +
                            ", c.alto " +
                            ", c.largo  " +
                            ", c.maximo_peso  " +
                            " from inv_contenedor  c " +
                            " , inv_tipo_contenedor tc " +
                            " where c.tipo_contenedor = tc.id ";

            if (id_bodega != null)
            {
                sql = sql + " and c.id_bodega = :id_bodega ";
            }
            if (id_padre != null)
            {
                sql = sql + " and c.id_padre = :id_padre ";
            }
            if (tipo_contenedor != null)
            {
                sql = sql + " and c.tipo_contenedor = :tipo_contenedor ";
            }
            if (nombre != null)
            {
                sql = sql + " and lower (c.nombre) like :nombre ";
            }
            sql = sql + " order by c.id_bodega asc ";
            OracleCommand comando = new OracleCommand(sql, x_con);
            OracleDataReader leer;
            List<CContenedor> x_lista = new List<CContenedor>();

            try
            {
                if (id_bodega != null)
                {
                    comando.Parameters.Add("c.id_bodega", OracleDbType.NVarchar2).Value = id_bodega.ToUpper();
                }

                if (id_padre != null)
                {
                    comando.Parameters.Add("c.id_padre", OracleDbType.Int32).Value = id_padre;
                }

                if (tipo_contenedor != null)
                {
                    comando.Parameters.Add("c.tipo_contenedor", OracleDbType.Int32).Value = tipo_contenedor;
                }

                if (nombre != null)
                {
                    comando.Parameters.Add("c.nombre", OracleDbType.NVarchar2).Value = "%" + nombre.ToUpper() + "%";
                }

                leer = comando.ExecuteReader();

                if (leer.Read())
                {
                    CContenedor cont = new CContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        cont.id_bodega = leer.GetString(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        cont.id = leer.GetInt32(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        cont.id_padre = leer.GetInt32(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        cont.tipo_contenedor = leer.GetInt32(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        cont.nombre = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        cont.tipo_contenedor_desc = leer.GetString(5); //Descripcion de la nueva tabla agregada
                    }
                    if (!leer.IsDBNull(6))
                    {
                        cont.descripcion = leer.GetString(6);
                    }
                    if (!leer.IsDBNull(7))
                    {
                        cont.ubicacion = leer.GetString(7);
                    }
                    if (!leer.IsDBNull(8))
                    {
                        cont.ancho = leer.GetInt32(8);
                    }
                    if (!leer.IsDBNull(9))
                    {
                        cont.alto = leer.GetInt32(9);
                    }
                    if (!leer.IsDBNull(10))
                    {
                        cont.largo = leer.GetInt32(10);
                    }
                    if (!leer.IsDBNull(11))
                    {
                        cont.maximo_peso = leer.GetInt32(11);
                    }


                    x_lista.Add(cont);

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
        public int contenedor_ingresar(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();
            int x_f = 0;
            int b_id = -1;


            string sql2 = "select seq_inv_contenedor.nextval from dual";
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

                string sql = "insert into inv_contenedor" +
                    "(,id_bodega" +
                    ",id" +
                    ",id_padre" +
                    ",tipo_contenedor" +
                    ",nombre" +
                    ",descripcion" +
                    ",ubicacion," +
                    "ancho" +
                    ",alto" +
                    ",largo" +
                    ",maximo_peso) " +
                    "values " +
                    "(:id_bodega" +
                    ",:id" +
                    ",:id_padre" +
                    ",:tipo_contenedor" +
                    ",:nombre" +
                    ",:descripcion" +
                    ",:ubicacion" +
                    ",:ancho" +
                    ",:alto" +
                    ",:largo" +
                    ",:maximo_peso)";

                OracleCommand comando = new OracleCommand(sql, x_con);


                if (id_bodega == null)
                {
                    comando.Parameters.Add("id_bodega", OracleDbType.NVarchar2, 20).Value = DBNull.Value;
                }
                else
                {
                    if (id_bodega != null)
                    {
                        comando.Parameters.Add("id_bodega", OracleDbType.NVarchar2, 20).Value = this.id_bodega.ToUpper();
                    }
                }

                comando.Parameters.Add("id", OracleDbType.Int32).Value = this.id;
                comando.Parameters.Add("id_padre", OracleDbType.Int32).Value = this.id_padre;
                comando.Parameters.Add("tipo_contenedor", OracleDbType.Int32).Value = this.tipo_contenedor;
                comando.Parameters.Add("nombre", OracleDbType.NVarchar2, 100).Value = this.nombre.ToUpper();

                if (descripcion == null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {
                    if (descripcion != null)
                    {
                        comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = this.descripcion.ToUpper();
                    }
                }

                if (ubicacion == null)
                {
                    comando.Parameters.Add("ubicacion", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {
                    if (ubicacion != null)
                    {
                        comando.Parameters.Add("ubicacion", OracleDbType.NVarchar2, 200).Value = this.ubicacion.ToUpper();
                    }
                }

                comando.Parameters.Add("ancho", OracleDbType.Int32).Value = this.ancho;
                comando.Parameters.Add("alto", OracleDbType.Int32).Value = this.alto;
                comando.Parameters.Add("largo", OracleDbType.Int32).Value = this.largo;
                comando.Parameters.Add("maximo_peso", OracleDbType.Int32).Value = this.maximo_peso;


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
                    o_error.mensaje = "Error, revisar datos ingresados";
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



        //Método para modificar los registros de la tabla inv_contenedor
        public int modificar_registros(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {
            o_error = new CError();

            int x_f = 0;
            string b_id = "";

            try
            {
                string sql = " update inv_contenedor" +
                   " set tipo_contenedor=:tipo_contenedor " +
                   " ,descripcion=:descripcion " +
                   " ,ubicacion=:ubicacion " +
                   " ,ancho=:ancho " +
                   " ,alto=:alto " +
                   " ,largo=:largo " +
                   " ,maximo_peso=:maximo_peso " +
                   "  ,id_padre=:id_padre " +
                   " where " +
                   " id=:id ";

                OracleCommand comando = new OracleCommand(sql, x_con);

                comando.Parameters.Add("tipo_contenedor", OracleDbType.Int32).Value = this.tipo_contenedor;

                if (descripcion == null)
                {
                    comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {
                    if (descripcion != null)
                    {
                        comando.Parameters.Add("descripcion", OracleDbType.NVarchar2, 200).Value = this.descripcion.ToUpper();
                    }
                }

                if (ubicacion == null)
                {
                    comando.Parameters.Add("ubicacion", OracleDbType.NVarchar2, 200).Value = DBNull.Value;
                }
                else
                {
                    if (ubicacion != null)
                    {
                        comando.Parameters.Add("ubicacion", OracleDbType.NVarchar2, 200).Value = this.ubicacion;
                    }
                }

                comando.Parameters.Add("ancho", OracleDbType.Int32).Value = this.ancho;
                comando.Parameters.Add("alto", OracleDbType.Int32).Value = this.alto;
                comando.Parameters.Add("largo", OracleDbType.Int32).Value = this.largo;
                comando.Parameters.Add("maximo_peso", OracleDbType.Int32).Value = this.maximo_peso;


                if (id_padre == null)
                {
                    comando.Parameters.Add("id_padre", OracleDbType.Int32).Value = DBNull.Value;
                }
                else
                {
                    if (id_padre != null)
                    {
                        comando.Parameters.Add("id_padre", OracleDbType.Int32).Value = this.id_padre;
                    }
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
                    o_error.mensaje = "Error, revisar dato ingresado";
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

        //Método para eliminar los registros de la tabla inv_contenedor
        public int eliminar_registros(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, int id, ref CError o_error)
        {

            int x_f = 0;
            o_error = new CError();

            try
            {

                string sql = "delete from inv_contenedor where id=:id";

                OracleCommand comando = new OracleCommand(sql, x_con);

                comando.Parameters.Add("id", OracleDbType.Int32).Value = id;

                CError error_x = new CError();
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
                    o_error.mensaje = "Error, verifique la id a eliminar";
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
    }
}

