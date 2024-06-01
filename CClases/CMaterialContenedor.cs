using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_REST.CClases
{
    public class CMaterialContenedor
    {
        public Nullable<int> id { get; set; }
        public int id_material { get; set; }
        public int id_contenedor { get; set; }
        public int cantidad_maxima { get; set; }

        public string nombre_material { get; set; }
        public string nombre_contenedor { get; set; }



        public int saldo { get; set; } //SALDOS - se agrego esta linea
        public string descripcion_material { get; set; }
        public string descripcion { get; set; }

        //Método para listar los registros de la tabla 
        public List<CMaterialContenedor> Material_contenedor_lista(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {

            o_error = new CError();



            string sql = " select " +
                          " mc.id " +
                          " , mc.id_material " +
                          " , m.nombre nombre_material " +
                          " , mc.id_contenedor " +
                          " ,  c.nombre nombre_contenedor" +
                          " , mc.cantidad_maxima " +
                          " from inv_material_contenedor mc " +
                          " inner join inv_material m " +
                          " on MC.ID_MATERIAL = M.id " +
                          " inner join inv_contenedor c " +
                          " on MC.ID_CONTENEDOR = C.id ";



            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;
            List<CMaterialContenedor> x_lista = new List<CMaterialContenedor>();

            try
            {

                leer = comando.ExecuteReader();

                while (leer.Read())
                {
                    CMaterialContenedor a = new CMaterialContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        a.id = leer.GetInt32(0);
                    }


                    if (!leer.IsDBNull(1))
                    {
                        a.id_material = leer.GetInt32(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        a.nombre_material = leer.GetString(2); //se agrego atributo arriba
                    }
                    if (!leer.IsDBNull(3))
                    {
                        a.id_contenedor = leer.GetInt32(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        a.nombre_contenedor = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        a.cantidad_maxima = leer.GetInt32(5);
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
                    o_error.mensaje = "Error, revisar Codigo fuente o no hay ID's por leer";
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
        //

        //Método para insertar registros de la tabla inv_tipo_contenedor con su secuencia
        public int material_contenedor_insertar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, ref CError o_error)
        {
            o_error = new CError();

            int x_id = -1;
            int f_r = 0;

            string sql2 = "select seq_material_contenedor.nextval from dual";
            OracleCommand comando2 = new OracleCommand(sql2, i_con);

            OracleDataReader leer;

            CRespuestaInt x_r = new CRespuestaInt();

            try
            {

                leer = comando2.ExecuteReader();


                if (leer.Read())
                {

                    if (!leer.IsDBNull(0))
                    {
                        x_id = leer.GetInt32(0);
                    }
                }
                leer.Close();


                this.id = x_id;


                string sql = "insert into inv_material_contenedor" +
                            "(id,id_material" +
                            ", id_contenedor" +
                            ", cantidad_maxima) " +
                            "values" +
                            " (:id" +
                            ", :id_material" +
                            ", :id_contenedor" +
                            ", :cantidad_maxima)";

                OracleCommand comando = new OracleCommand(sql, i_con);


                comando.Parameters.Add("id", OracleDbType.Int32).Value = this.id;
                comando.Parameters.Add("id_material", OracleDbType.Int32).Value = this.id_material;
                comando.Parameters.Add("id_contenedor", OracleDbType.Int32).Value = this.id_contenedor;
                comando.Parameters.Add("cantidad_maxima", OracleDbType.Int32).Value = this.cantidad_maxima;


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
                    o_error.mensaje = "Error, Se ingresado un dato ya ingresado previamente";
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
        public int material_contenedor_modificar(OracleConnection x_con, OracleTransaction x_tran, string x_usuario, ref CError o_error)
        {

            o_error = new CError();
            int x_f = 0;
            string b_id = "";

            try
            {
                string sql = "update inv_material_contenedor" +
                    " set cantidad_maxima=:cantidad_maxima where id=:id ";

                OracleCommand comando = new OracleCommand(sql, x_con);

                comando.Parameters.Add("cantidad_maxima", OracleDbType.Int32).Value = cantidad_maxima;
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
                    o_error.mensaje = "Error, revise que no este ingresando una ID_bodega ya existente";
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
        public int material_contenedor_eliminar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, int id, ref CError o_error)
        {
            o_error = new CError();
            int x_f = 0;
            try
            {

                string sql = "delete from inv_material_contenedor where id=:id";

                OracleCommand comando = new OracleCommand(sql, i_con);

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
                    o_error.mensaje = "Error, ID no existe";
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

        public int movimientos_material_contar(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, int id_material, ref CError o_error)
        {
            o_error = new CError();


            string sql = "select" +
                " sum ( case when tipo_operacion ='I' then cantidad when tipo_operacion='E' then  -1* cantidad end ) as SALDO_TOTAL" +
                         " FROM INV_MOVIMIENTOS" +
                         " WHERE  ID_MATERIAL = :ID_MATERIAL ";



            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;

            comando.Parameters.Add("id_material", OracleDbType.Int32).Value = id_material;
            int x_saldo = 0;
            try
            {

                leer = comando.ExecuteReader();

                if (leer.Read())
                {


                    if (!leer.IsDBNull(0))
                    {
                        x_saldo = leer.GetInt32(0);
                    }


                }
                leer.Close();
            }
            catch (OracleException e)
            {

                o_error.id = e.Number;
                o_error.mensaje = e.Message;
                x_saldo = 0;

                if (o_error.id == 1)
                {
                    o_error.mensaje = "Error, revisar Codigo";
                }

            }
            catch (Exception e)
            {
                x_saldo = 0;
                o_error.id = 100;
                o_error.mensaje = e.Message;
            }
            return x_saldo;
        }

        // MATERIAL - CONTENEDOR - CANTIDAD (SALDO)

        public List<CMaterialContenedor> movimientos_material_cantidades(OracleConnection i_con, OracleTransaction i_tran, string i_usuario, int id_material, ref CError o_error)
        {
            o_error = new CError();




            string sql = "select   " +
                       "  mov.ID_CONTENEDOR " +
                       "  , cont.nombre NOMBRE_CONTENEDOR " +
                       "  , CONT.DESCRIPCION " +
                       "  , mov.ID_MATERIAL " +
                       "  , mat.nombre NOMBRE_MATERIAL " +
                       "  , mat.descripcion DESCRIPCION_MATERIAL " +
                       "  ,sum( case when tipo_operacion ='I' then cantidad when tipo_operacion='E' then  -1* cantidad END) as Saldo " +
                       "  from inv_movimientos mov " +
                       "  join inv_contenedor cont on MOV.ID_CONTENEDOR = CONT.ID " +
                       "  join inv_material mat on MOV.ID_MATERIAL = MAT.ID " +
                       "  WHERE ID_MATERIAL = :ID_MATERIAL " +
                       "  group by mov.ID_CONTENEDOR, cont.nombre, cont.descripcion, id_material, mat.nombre, mat.descripcion ";

            OracleCommand comando = new OracleCommand(sql, i_con);
            OracleDataReader leer;


            List<CMaterialContenedor> x_lista = new List<CMaterialContenedor>();
            comando.Parameters.Add("id_material", OracleDbType.Int32).Value = id_material;
            try
            {
                leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    CMaterialContenedor x_saldo = new CMaterialContenedor();

                    if (!leer.IsDBNull(0))
                    {
                        x_saldo.id_contenedor = leer.GetInt32(0);
                    }
                    if (!leer.IsDBNull(1))
                    {
                        x_saldo.nombre_contenedor = leer.GetString(1);
                    }
                    if (!leer.IsDBNull(2))
                    {
                        x_saldo.descripcion = leer.GetString(2);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        x_saldo.id_material = leer.GetInt32(3);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        x_saldo.id_material = leer.GetInt32(3);
                    }
                    if (!leer.IsDBNull(3))
                    {
                        x_saldo.id_material = leer.GetInt32(3);
                    }
                    if (!leer.IsDBNull(4))
                    {
                        x_saldo.nombre_material = leer.GetString(4);
                    }
                    if (!leer.IsDBNull(5))
                    {
                        x_saldo.descripcion_material = leer.GetString(5);
                    }
                    if (!leer.IsDBNull(6))
                    {
                        x_saldo.saldo = leer.GetInt32(6);
                    }

                    x_lista.Add(x_saldo);
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
