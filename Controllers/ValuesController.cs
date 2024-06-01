using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using API_REST.Controllers;
using API_REST.CRespuestas;
using API_REST.CClases;

namespace API_REST.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        #region BODEGAS

        //  //  //
        [HttpGet]
        [Route("bodegas/listar_filtro")]
        public ActionResult<CRespuestaBodegaLista> listar_bodegas_filtro([FromQuery] string id, [FromQuery] string descripcion, [FromQuery] string abreviatura, [FromQuery] string estado)
        {
            CRespuestaBodegaLista x_respuesta = new CRespuestaBodegaLista();
            CError x_error = new CError();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {

                CBodega x_b = new CBodega();
                List<CBodega> x_lista = null;



                x_lista = x_b.listar_bodegas_filtro(con, null, x_usuario, id, descripcion, abreviatura, estado, ref x_error);

                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;


            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }
        }
        //  //  //

        [HttpGet]
        [Route("bodega/{id}")]
        public ActionResult<CRespuestaBodega> get_bodega(string id)
        {
            CRespuestaBodega x_respuesta = new CRespuestaBodega();
            CError x_error = new CError();

            //Pendiente a modificar/entender correctamente el funcionamiento (este comentario solo estara en esta seccion del codigo, en los otro no estara)
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            CBodega x_b2 = new CBodega();
            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {
                CBodega x_b = new CBodega();
                x_b2 = x_b.get_bodega(con, null, x_usuario, id, ref x_error);
                x_respuesta.o_error = x_error; //Ambos son del tipo error
                x_respuesta.datos = x_b2; //se asigna debido a que deben ser de la misma clase
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }
            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }
        }
        // FIN DE INTENTO DE CAMBIO DE RUTA PARA EL NUEVO BUSCAR  - VTR - 



        //Métodos para la tabla inv_bodegas GET, POST y DELETE //



        [HttpGet]
        [Route("bodegas/listar")]
        public ActionResult<CRespuestaBodegaLista> listar_bodegas([FromQuery] string id, [FromQuery] string descripcion, [FromQuery] string abreviatura, [FromQuery] string estado)
        {
            CRespuestaBodegaLista x_respuesta = new CRespuestaBodegaLista();
            CError x_error = new CError();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {

                CBodega x_b = new CBodega();
                List<CBodega> x_lista = null;



                x_lista = x_b.listar_bodegas(con, null, x_usuario, ref x_error);

                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;


            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }
        }
        //

        [HttpPost]
        [Route("bodegas/ingresar")]
        public ActionResult<CRespuestaInt> bodega_ingresar([FromBody] CBodega b)
        {
            CRespuestaInt r = new CRespuestaInt();
            CError x_error = new CError();
            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\



            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));

                con.Open();
                int h = -1;


                h = b.bodega_ingresar(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = h;
                    return Ok(r);
                }

                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }

            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }


        }


        [HttpPost]
        [Route("bodegas/modificar")]
        public ActionResult<CRespuestaInt> bodega_modificar([FromBody] CBodega c)
        {

            CRespuestaInt r = new CRespuestaInt();



            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\


            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                int h = -1;
                CError x_error = new CError();


                h = c.bodega_modificar(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = h;
                    return Ok(r);
                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }

            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }



        }

        [HttpDelete]
        [Route("bodegas/eliminar/{id}")]
        public ActionResult<CRespuestaInt> bodega_eliminar(string id)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\



            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));


            con.Open();
            int x_f = -1;
            CError x_error = new CError();

            CBodega d = new CBodega();



            x_f = d.bodega_eliminar(con, null, x_usuario, id, ref x_error);
            r.valor = x_f; //De prueba, no recuerdo como hacer esta linea segun explicaciones (interrupcion)
            r.o_error = x_error; //De prueba, no recuerdo como hacer esta linea segun explicaciones (interrupcion)
            con.Close();

            return Ok(r);
        }

        #endregion




        #region TIPO_CONTENEDOR
        //-------------------------------------------------------------------------------------------------------------------------------//

        // Metodos para la tabla inv_tipo_contenedor GET, POST y DELETE//
        [HttpGet]
        [Route("inv_tipo_contenedor/listar")]
        public ActionResult<List<CRespuestaTipoContenedor>> leer_datos()
        {
            CRespuestaTipoContenedor x_respuesta = new CRespuestaTipoContenedor();
            CError x_error = new CError();


            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\




            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {

                CTipoContenedor x_b = new CTipoContenedor();
                List<CTipoContenedor> x_lista = null;

                x_lista = x_b.leer_datos(con, null, x_usuario, ref x_error);

                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;

            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }
        }

        [HttpPost]
        [Route("inv_tipo_contenedor/ingresar")]
        public ActionResult<CRespuestaInt> insertar_datos([FromBody] CTipoContenedor c)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\


            if (c.nombre == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "No se puede ingresar un registro con el mismo nombre, por favor ingrese otro con un nombre distinto";
                return BadRequest(r);
            }

            if (c.nombre == null)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "No se puede ingresar el nombre en nulo";
                return BadRequest(r);
            }

            if (c.nombre.Trim() == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "No se puede ingresar el nombre vacio";
                return BadRequest(r);
            }

            if (c.descripcion == null)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "No se puede ingresar la descripcion nula";
                return BadRequest(r);
            }

            if (c.descripcion == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "No se puede ingresar la descripcion vacia";
                return BadRequest(r);
            }

            if (c.estado == null)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "No se puede ingresar el estado nulo";
                return BadRequest(r);
            }

            if (c.estado == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "No se puede ingresar el estado vacio";
                return BadRequest(r);
            }
            try
            {

                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                int b = -1;

                CError x_error = new CError();

                b = c.insertar_datos(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = b;
                    return Ok(r);
                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }

            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }




        }



        [HttpPost]
        [Route("inv_tipo_contenedor/modificar")]
        public ActionResult<CRespuestaInt> modificar_contenedor([FromBody] CTipoContenedor d_tpc)
        {

            CRespuestaInt c = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\


            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                int x = -1;
                CError x_error = new CError();

                x = d_tpc.modificar_datos(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    c.valor = x;


                    return Ok(c);
                }
                else
                {
                    c.o_error = x_error;
                    c.valor = -1;
                    return BadRequest(c);

                }
            }

            catch (OracleException e)
            {
                c.o_error = new CError();
                c.o_error.id = e.Number;
                c.o_error.mensaje = e.Message;
                c.valor = -1;
                return BadRequest(c);
            }
            catch (Exception e)
            {
                c.o_error = new CError();
                c.o_error.id = 100;
                c.o_error.mensaje = e.Message;
                c.valor = -1;
                return BadRequest(c);
            }


        }

        [HttpDelete]
        [Route("inv_tipo_contenedor/eliminar/{id}")]
        public ActionResult<CRespuestaInt> eliminar_contenedor(int id)
        {

            CRespuestaInt r = new CRespuestaInt();


            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            int x_f = -1;
            CError x_error = new CError();

            CTipoContenedor d = new CTipoContenedor();

            x_f = d.eliminar_datos(con, null, x_usuario, id, ref x_error);
            r.valor = x_f;
            r.o_error = x_error;

            con.Close();

            return Ok(r);

            //

            //

        }

        //-------------------------------------------------------------------------------------------------------------------//

        #endregion


        #region CONTENEDOR

        // TRAE LOS MATERIALES DE LA BODEGA INDICANDO SU CONTENEDOR

        [HttpGet]
        [Route("CContenedor_get_materiales_contenedor/{id_bodega}")]
        public ActionResult<CRespuestaContenedor> get_materiales_bodega_contenedor(string id_bodega) //no veo en CCONTENEDOR
        {
            CRespuestaContenedor x_respuesta = new CRespuestaContenedor();
            CError x_error = new CError();

            //Pendiente a modificar/entender correctamente el funcionamiento (este comentario solo estara en esta seccion del codigo, en los otro no estara)
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }
            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }
            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }
            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            //CContenedor x_con = new CContenedor();

            //CContenedor x_b2 = new CContenedor();
            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {
                CContenedor x_b = new CContenedor();
                List<CContenedor> x_lista = null;

                x_lista = x_b.get_materiales_bodega(con, null, x_usuario, id_bodega, ref x_error);
                x_respuesta.o_error = x_error; //Ambos son del tipo error
                x_respuesta.datos = x_lista; //son de la misma clase
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }
            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }
        }


        //TRAE TODOS LOS MATERIALES DE LA BODEGA INDEPENDIENTE DEL CONTENEDOR

        [HttpGet]
        [Route("CContenedor_get_materiales/{id_bodega}")]
        public ActionResult<CRespuestaContenedor> get_materiales_bodega(string id_bodega) //si aparece MATCH
        {
            CRespuestaContenedor x_respuesta = new CRespuestaContenedor();
            CError x_error = new CError();

            //Pendiente a modificar/entender correctamente el funcionamiento (este comentario solo estara en esta seccion del codigo, en los otro no estara)
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }
            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }
            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }
            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            //CContenedor x_con = new CContenedor();

            //CContenedor x_b2 = new CContenedor();
            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {
                CContenedor x_b = new CContenedor();
                List<CContenedor> x_lista = null;

                x_lista = x_b.get_materiales_bodega(con, null, x_usuario, id_bodega, ref x_error);
                x_respuesta.o_error = x_error; //Ambos son del tipo error
                x_respuesta.datos = x_lista; //son de la misma clase
            }
            catch (Exception e)
            {
                //tran.rollback();
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }
            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                //tran.Commit();
                return Ok(x_respuesta);
            }
        }

        // -----------
        [HttpGet]
        [Route("CContenedor_get_materiales/listar_material_bodega")]
        public ActionResult<CRespuestaContenedor> listar_materiales_bodega([FromQuery] Nullable<int> id, [FromQuery] string nombre, [FromQuery] Nullable<int> id_contenedor) //SI ENCUENTRO MATCH
        {

            CRespuestaContenedor x_respuesta = new CRespuestaContenedor();
            CError x_error = new CError();

            //Pendiente a modificar/entender correctamente el funcionamiento (este comentario solo estara en esta seccion del codigo, en los otro no estara)
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";



            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }
            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }
            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }
            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }


            //CContenedor x_con = new CContenedor();

            //CContenedor x_b2 = new CContenedor();
            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {
                CContenedor x_b = new CContenedor();
                List<CContenedor> x_lista = null;

                x_lista = x_b.listar_materiales_bodega(con, null, x_usuario, id, nombre, id_contenedor, ref x_error);
                x_respuesta.o_error = x_error; //Ambos son del tipo error

                x_respuesta.datos = x_lista; //son de la misma clase
            }
            catch (Exception e)
            {
                //tran.rollback();
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }
            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                //tran.Commit();
                return Ok(x_respuesta);
            }
        }

        //-----------




        //GET CONTENEDOR

        [HttpGet]
        [Route("contenedor/{id}")]
        public ActionResult<CRespuestaContenedorId> get_contenedor(int id)
        {
            CRespuestaContenedorId x_respuesta = new CRespuestaContenedorId();
            CError x_error = new CError();



            //Pendiente a modificar/entender correctamente el funcionamiento (este comentario solo estara en esta seccion del codigo, en los otro no estara)
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            //Fin de Pendiente



            CContenedor x_con = new CContenedor();
            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {
                CContenedor x_b = new CContenedor();
                x_con = x_b.get_contenedor(con, null, x_usuario, id, ref x_error);
                x_respuesta.o_error = x_error; //Ambos son del tipo error
                x_respuesta.datos = x_con; //son de la misma clase
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }
            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }
        }

        //FIN GET CONTENEDOR

        //Métodos para la tabla inv_contenedor GET, POST y DELETE //
        [HttpGet]
        [Route("listar/inv_contenedor")]
        public ActionResult<List<CContenedor>> leer_registros([FromQuery] string id_bodega, [FromQuery] Nullable<int> id_padre, [FromQuery] Nullable<int> tipo_contenedor, [FromQuery] string nombre)
        {

            CRespuestaContenedor x_respuesta = new CRespuestaContenedor();
            CError x_error = new CError();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\



            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            try
            {
                CContenedor c = new CContenedor();
                List<CContenedor> x_lista = null;

                x_lista = c.leer_registros(con, null, x_usuario, id_bodega, id_padre, tipo_contenedor, nombre, ref x_error);

                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;

            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }


        }




        [HttpPost]
        [Route("ingresar/inv_contenedor")]
        public ActionResult<CRespuestaInt> contenedor_ingresar([FromBody] CContenedor c)
        {
            CRespuestaInt r = new CRespuestaInt();


            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\


            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));

                con.Open();
                int h = -1;
                CError x_error = new CError();

                h = c.contenedor_ingresar(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = h;
                    return Ok(r);
                }

                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }

            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }



        }

        [HttpPost]
        [Route("modificar/inv_contenedor")]
        public ActionResult<CRespuestaInt> modificar_inv_contenedor([FromBody] CContenedor b)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\


            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                //OracleTransaction tran = con.BeginTransaction();
                int y = -1;
                CError x_error = new CError();

                y = b.modificar_registros(con, null, x_usuario, ref x_error);

                // tran.Commit();
                con.Close();
                if (x_error.id == 0)
                {
                    r.valor = y;
                    return Ok(r);
                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }
            }

            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }


        }

        [HttpDelete]
        [Route("eliminar/inv_contenedor/{id}")]
        public ActionResult<CRespuestaInt> eliminar_registros(int id)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));



            con.Open();
            int x_f = -1;
            CError x_error = new CError();

            CContenedor d = new CContenedor();

            x_f = d.eliminar_registros(con, null, x_usuario, id, ref x_error);
            r.valor = x_f;
            r.o_error = x_error;

            con.Close();

            return Ok(r);

        }

        #endregion
        //---------------------------------------- * CRUD * MATERIALES * ------------------------------------------------------------------------------//


        #region MATERIAL

        [HttpPost]
        [Route("material/ingresar")]
        public ActionResult<CRespuestaInt> ingresar_material([FromBody] CMaterial c)
        {
            CRespuestaInt r = new CRespuestaInt();


            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }


            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            if (c.codigo == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR CODIGO NULO";
                return BadRequest(r);
            }

            if (c.nombre == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR NOMBRE NULO O VACIO";
                return BadRequest(r);
            }

            if (c.estado == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ESTADO NULO/VACIO";
                return BadRequest(r);
            }
            if (c.empresa == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR EMPRESA NULO O VACIO";
                return BadRequest(r);
            }
            if (c.unidades == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR UNIDADES NULO O VACIO";
                return BadRequest(r);
            }
            if (c.peso == -1)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR PESO NULO O VACIO";
                return BadRequest(r);
            }
            if (c.ancho == -1)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ANCHO NULO O VACIO";
                return BadRequest(r);
            }
            if (c.alto == -1)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ALTO NULO O VACIO";
                return BadRequest(r);
            }
            if (c.largo == -1)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR LARGO NULO O VACIO";
                return BadRequest(r);
            }
            try
            {

                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));

                con.Open();
                int x_r = -1;
                CError x_error = new CError();

                x_r = c.ingresar_material(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = x_r;
                    return Ok(r);

                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return Ok(r);
                }
            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return Ok(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return Ok(r);
            }

        }

        //Modificar materiales

        [HttpPost]
        [Route("material/modificar")]
        public ActionResult<CRespuestaInt> modificar_material([FromBody] CMaterial c)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            if (c.codigo == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR CODIGO NULO";
                return BadRequest(r);
            }

            if (c.nombre == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR NOMBRE NULO O VACIO";
                return BadRequest(r);
            }

            if (c.descripcion == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR DESCRIPCION NULO";
                return BadRequest(r);
            }

            if (c.estado == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ESTADO NULO/VACIO";
                return BadRequest(r);
            }
            if (c.empresa == "")
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR EMPRESA NULO O VACIO";
                return BadRequest(r);
            }
            if (c.unidades == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR UNIDADES NULO O VACIO";
                return BadRequest(r);
            }

            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                //OracleTransaction tran = con.BeginTransaction();
                int x_r = -1;
                CError x_error = new CError();
                x_r = c.modificar_material(con, null, x_usuario, ref x_error);
                // tran.Commit();
                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = x_r;
                    // material_leer(); SE HABIA PROBADO - RESULTADO: FALLIDO
                    return Ok(r);

                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }




            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }




        }

        [HttpGet]
        [Route("material/listar")]
        public ActionResult<List<CRespuestaListaMaterial>> material_leer([FromQuery] Nullable<int> id, [FromQuery] string codigo, [FromQuery] string nombre, [FromQuery] string empresa)
        {

            CRespuestaListaMaterial x_respuesta = new CRespuestaListaMaterial();
            CError x_error = new CError();

            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            try
            {
                CMaterial x_b = new CMaterial();
                List<CMaterial> x_lista = null;

                x_lista = x_b.material_leer(con, null, x_usuario, id, nombre, codigo, empresa, ref x_error);

                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }

        }

        [HttpDelete]
        [Route("material/eliminar/{id}")]
        public ActionResult<CRespuestaInt> material_eliminar(string id)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            ///
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            int x_f = -1;
            CError x_error = new CError();

            CMaterial d = new CMaterial();

            x_f = d.material_eliminar(con, null, x_usuario, id, ref x_error);
            r.valor = x_f;
            r.o_error = x_error;

            con.Close();

            return Ok(r);
        }
        #endregion
        //  LO SIGUIENTE ES PARA EL CMaterial_Contenedor

        #region MATERIAL_CONTENEDOR

        [HttpGet]
        [Route("material_contenedor/lista")]
        public ActionResult<CRespuestaMaterialContenedor> Material_contenedor_lista()
        {
            CRespuestaMaterialContenedor x_respuesta = new CRespuestaMaterialContenedor();
            CError x_error = new CError();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            try
            {
                CMaterialContenedor x_mc = new CMaterialContenedor();
                List<CMaterialContenedor> x_lista = null;


                x_lista = x_mc.Material_contenedor_lista(con, null, x_usuario, ref x_error);
                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;
            }

            catch (OracleException e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = e.Number;
                x_respuesta.o_error.mensaje = e.Message;


            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
            }
            return Ok(x_respuesta);

        }

        [HttpPost]
        [Route("material_contenedor/ingresar")]
        public ActionResult<CRespuestaInt> material_contenedor_insertar([FromBody] CMaterialContenedor c)
        {
            CRespuestaInt r = new CRespuestaInt();
            // Los valores 

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                int h = -1;
                CError x_error = new CError();

                h = c.material_contenedor_insertar(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = h;
                    return Ok(r);
                }

                else
                {
                    r.o_error = x_error;
                    r.valor = h;
                    return BadRequest(r);
                }

            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = r.valor;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.valor = r.valor;
                r.o_error.mensaje = e.Message;

                return BadRequest(r);
            }


        }

        [HttpPost]
        [Route("material_contenedor/modificar")]
        public ActionResult<CRespuestaInt> material_contenedor_modificar([FromBody] CMaterialContenedor c)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            try
            {

                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                int y;
                CError x_error = new CError();

                y = c.material_contenedor_modificar(con, null, x_usuario, ref x_error);

                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = y;
                    return Ok(r);
                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }
            }

            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }

        }
        [HttpDelete]
        [Route("material_contenedor/eliminar/{id}")]
        public ActionResult<CRespuestaInt> material_contenedor_eliminar(int id)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            int x_f = -1;
            CError x_error = new CError();

            CMaterialContenedor d = new CMaterialContenedor();

            x_f = d.material_contenedor_eliminar(con, null, x_usuario, id, ref x_error);
            r.valor = x_f; //De prueba, no recuerdo como hacer esta linea segun explicaciones (interrupcion)
            r.o_error = x_error; //De prueba, no recuerdo como hacer esta linea segun explicaciones (interrupcion)
            con.Close();

            return Ok(r);


        }

        [HttpGet] // Esto corresponde a lo que esta en CMaterialContenedor
        [Route("CContenedorSaldo/{id_material}")]
        public ActionResult<CRespuestaInt> movimientos_material_contar(int id_material)
        {
            CRespuestaInt x_respuesta = new CRespuestaInt();
            CError x_error = new CError();

            var re = Request;
            var headers = re.Headers;
            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }
            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }
            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }
            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            CMaterialContenedor x_con = new CMaterialContenedor();
            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            try
            {
                CMaterialContenedor x_mc = new CMaterialContenedor();
                int h = -1;

                h = x_mc.movimientos_material_contar(con, null, x_usuario, id_material, ref x_error);
                x_respuesta.valor = h;
                x_respuesta.o_error = x_error;
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.valor = 0;
            }
            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }

        }



        #endregion


        #region TIPO_MATERIAL
        [HttpGet]
        [Route("tipo_material/listar")]
        public ActionResult<List<CRespuesta_CTipoDeMaterial>> tipo_material_leer()
        {
            CRespuesta_CTipoDeMaterial x_respuesta = new CRespuesta_CTipoDeMaterial();
            CError x_error = new CError();

            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            try
            {
                CTipoDeMaterial x_b = new CTipoDeMaterial();
                List<CTipoDeMaterial> x_lista = null;
                x_lista = x_b.tipo_material_leer(con, null, x_usuario, ref x_error);
                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }

        }

        #endregion

        #region CMovimientos

        //  LO SIGUIENTE ES PARA EL CMovimientos



        [HttpGet]
        [Route("Movimientos/listado")]
        public ActionResult<List<CRespuestaMovimientos>> movimientos_listado([FromQuery] Nullable<int> id, [FromQuery] Nullable<int> id_material, [FromQuery] string tipo_operacion, [FromQuery] string usuario)
        {

            CRespuestaMovimientos x_respuesta = new CRespuestaMovimientos();
            CError x_error = new CError();


            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\


            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            try
            {

                CMovimientos x_b = new CMovimientos();
                List<CMovimientos> x_lista = null;

                x_lista = x_b.movimientos_listado(con, null, x_usuario, id, id_material, tipo_operacion, usuario, ref x_error);

                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;
            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
                x_respuesta.datos = null;
            }

            con.Close();

            if (x_error.id != 0)
            {
                return BadRequest(x_respuesta);
            }
            else
            {
                return Ok(x_respuesta);
            }

        }


        [HttpPost]
        [Route("movimientos/ingresar_lista")]
        public ActionResult<CRespuestaInt> movimientos_ingresar_lista([FromBody] List<CMovimientos> i_lista)
        {
            CRespuestaInt respuesta = new CRespuestaInt();
            List<CRespuestaMovimientos> x_respuesta_lista = new List<CRespuestaMovimientos>();

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();
            int filas = -1;
            OracleTransaction transaction = con.BeginTransaction(); // permite el uso del commit y el rollback 

            var re = Request;
            var headers = re.Headers;
            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }
            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }
            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }
            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            int h = -1;

            try
            {
                int x_r = -1;
                CError x_error = new CError();
                CMovimientos c = new CMovimientos();
                CMovimientos x_lista = new CMovimientos();

                for (int i = 0; i < i_lista.Count; i++) //Hace el llamado de la funcion a insertar segun la cantidad de documentos en el json
                {
                    h = i;
                    x_r = i_lista[i].movimientos_ingresar_lista(con, null, x_usuario, ref x_error);

                    if (x_error.id != 0) // Esto realiza el chequeo del codigo y hace un break cuando el bucle detecta un insert 'malo'
                    {
                        x_error.mensaje = "Linea " + (i + 1).ToString() + ": " + x_error.mensaje; //
                        break;
                    }

                    filas = i + 1;

                }

                //x_r = c.movimientos_ingresar_lista(i_lista, con, null, x_usuario, ref x_error);



                if (x_error.id == 0)
                {
                    //respuesta.valor = x_r;+
                    respuesta.valor = filas;
                    transaction.Commit(); //graba el estado actual de cambios en la BD           
                                            //if (filas == x_r) { }
                    con.Close();
                    return Ok(respuesta);

                }
                else
                {

                    respuesta.o_error = x_error;
                    respuesta.valor = -1;
                    transaction.Rollback(); //volver al estado anterior --> BD                    
                    con.Close();
                    return BadRequest(respuesta);

                }

            }
            catch (OracleException e)
            {
                respuesta.o_error = new CError();
                respuesta.o_error.id = e.Number;
                respuesta.o_error.mensaje = "Ha ocurrido un eror en el registro: " + h.ToString() + " - " + e.Message;
                respuesta.valor = -1;
                transaction.Rollback(); //volver al estado anterior --> BD
                con.Close();
                return BadRequest(respuesta);
            }
            catch (Exception e)
            {
                respuesta.o_error = new CError();
                respuesta.o_error.id = 100;
                respuesta.o_error.mensaje = e.Message;
                respuesta.valor = -1;
                transaction.Rollback(); //volver al estado anterior --> BD
                con.Close();
                return BadRequest(respuesta);
            }

        }

        [HttpPost]
        [Route("movimientos/ingresar")]
        public ActionResult<CRespuestaInt> movimientos_ingresar([FromBody] CMovimientos c)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            try
            {

                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                int x_r = -1;
                CError x_error = new CError();

                x_r = c.movimientos_ingresar(con, null, x_usuario, ref x_error);

                con.Close();
                if (x_error.id == 0)
                {
                    r.valor = x_r;
                    return Ok(r);

                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }


            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }



        }

        //
        [HttpPost]
        [Route("movimientos/modificar")]
        public ActionResult<CRespuestaInt> Movimientos_modificar([FromBody] CMovimientos c)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }

            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            if (c.id == null)
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR ID ";
                return BadRequest(r);
            }

            if (c.id_material == null)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ID_MATERIAL NULO O VACIO";
                return BadRequest(r);
            }

            if (c.id_contenedor == null)
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR ID_MATERIAL NULO";
                return BadRequest(r);
            }

            if (c.tipo_operacion == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR TIPO_OPERACION NULO/VACIO";
                return BadRequest(r);
            }
            if (c.cantidad == null)
            {
                r.valor = -1;
                r.o_error.id = 101;
                r.o_error.mensaje = "ERROR CANTIDAD NULO O VACIO";
                return BadRequest(r);
            }
            if (c.usuario == "")
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR USUARIO NULO O VACIO";
                return BadRequest(r);
            }
            if (c.ID_DOCUMENTO_ORIGEN == null)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ID_DOCUMENTO_ORIGEN NULO O VACIO";
                return BadRequest(r);
            }
            if (c.ID_TIPO_DOCUMENTO_ORIGEN == null)
            {
                r.valor = -1;
                r.o_error.id = 100;
                r.o_error.mensaje = "ERROR ID_TIPO_DOCUMENTO_ORIGEN NULO O VACIO";
                return BadRequest(r);
            }


            try
            {
                OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
                con.Open();
                //OracleTransaction tran = con.BeginTransaction();
                int x_r = -1;
                CError x_error = new CError();
                x_r = c.Movimientos_modificar(con, null, x_usuario, ref x_error);
                // tran.Commit();
                con.Close();

                if (x_error.id == 0)
                {
                    r.valor = x_r;
                    // material_leer(); SE HABIA PROBADO - RESULTADO: FALLIDO
                    return Ok(r);

                }
                else
                {
                    r.o_error = x_error;
                    r.valor = -1;
                    return BadRequest(r);
                }




            }
            catch (OracleException e)
            {
                r.o_error = new CError();
                r.o_error.id = e.Number;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }
            catch (Exception e)
            {
                r.o_error = new CError();
                r.o_error.id = 100;
                r.o_error.mensaje = e.Message;
                r.valor = -1;
                return BadRequest(r);
            }




        }

        //


        [HttpDelete]
        [Route("Movimientos/eliminar/{id}")]
        public ActionResult<CRespuestaInt> movimientos_eliminar(int id)
        {
            CRespuestaInt r = new CRespuestaInt();

            ////////////////////\\\\\\\\\\\\\\\\\\
            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }



            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }

            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }

            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }
            /////////////////////\\\\\\\\\\\\\\\\\\

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            CError x_error = new CError();

            CMovimientos d = new CMovimientos();

            int x_f = d.movimientos_eliminar(con, null, x_usuario, id, ref x_error);
            r.valor = x_f;
            r.o_error = x_error;
            con.Close();

            return Ok(r);

        }

        #endregion

        #region MOVIMIENTOS_MATERIAL


        [HttpGet]
        [Route("material_contenedor_cantidades/{id_material}")]
        public ActionResult<List<CMaterialContenedor>> movimientos_material_cantidades(int id_material)
        {
            CRespuestaMaterialContenedor x_respuesta = new CRespuestaMaterialContenedor();
            CError x_error = new CError();

            var re = Request;
            var headers = re.Headers;

            string x_token = "";
            string x_app = "";
            string x_usuario = "";
            string x_app_version = "";

            if (headers.ContainsKey("access_token"))
            {
                x_token = headers["access_token"];
            }
            if (headers.ContainsKey("usuario"))
            {
                x_usuario = headers["usuario"];
            }
            if (headers.ContainsKey("app"))
            {
                x_app = headers["app"];
            }
            if (headers.ContainsKey("app_version"))
            {
                x_app_version = headers["app_version"];
            }

            OracleConnection con = new OracleConnection(_configuration.GetConnectionString("BdSyS"));
            con.Open();

            try
            {
                CMaterialContenedor c = new CMaterialContenedor();
                List<CMaterialContenedor> x_lista = null;

                x_lista = c.movimientos_material_cantidades(con, null, x_usuario, id_material, ref x_error);
                x_respuesta.datos = x_lista;
                x_respuesta.o_error = x_error;
            }

            catch (OracleException e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = e.Number;
                x_respuesta.o_error.mensaje = e.Message;


            }
            catch (Exception e)
            {
                x_respuesta.o_error = new CError();
                x_respuesta.o_error.id = 100;
                x_respuesta.o_error.mensaje = e.Message;
            }
            return Ok(x_respuesta);

        }
        #endregion
    }
}

