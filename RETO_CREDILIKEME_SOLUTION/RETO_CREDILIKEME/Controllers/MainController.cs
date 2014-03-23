using RETO_CREDILIKEME.Core;
using RETO_CREDILIKEME.Models;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RETO_CREDILIKEME.Controllers
{
    /// <summary>
    /// Controlador con las acciones
    /// principales de la api.
    /// </summary>
    public class MainController : ApiController
    {
        private SqlConnection connection;
        Fbc fb;

        public MainController()
        {
            this.connection = new SqlConnection("Data Source=XXX.XXX.XXX.XXX;Initial Catalog=XXX;Persist Security Info=True;User ID=XXX;Password=XXX");
            this.fb = new Fbc();
        }

        #region consultas
        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_ObtenerJugador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("jugador")]
        [HttpPost]
        [RequireHttps]
        public HttpResponseMessage ObtenerJugador(Identificador id)
        {
            if (id != null)
            {
                //ELIMINTAR COMENTARIO PARA VALIDAR CON FACEBOOK
                //Jugador jugador = fb.validate(id);
                Jugador jugador = fb.verifyUser(id);
                if (jugador != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, jugador, "application/json");
                }
            }
            
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Llama al procedimiento almacenado proc_reto_ObtenerClienteFacebookId
        /// para obtener info de cliente dado un id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("cliente")]
        [HttpPost]
        [RequireHttps]
        public HttpResponseMessage ObtenerCliente(Identificador id)
        {

            if (id != null) 
            {
                //DESCOMENTAR PARA VALIDAR CON FACEBOOK
                //if(fb.validate(id)!= null)
                //{ 
                    ClienteFacebook cliente = fb.getFbClientId(id);
                    if (cliente.id_cliente != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, cliente, "application/json");
                    }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
            
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_ObtenerBancoDisponible.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("banco")]
        [HttpPost]
        [RequireHttps]
        public HttpResponseMessage ObtenerBancoDispnible(Identificador id)
        {
            if (id != null)
            {
                //DESCOMENTAR PARA VALIDAR CON FACEBOOK
                //if(fb.validate(id)!= null)
                //{ 
                Banco banco = fb.getBank(id);
                if (banco.id != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, banco, "application/json");
                }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// PROC_RETO_CONSULTA_SALDO_CAJERO.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("cliente/saldo")]
        [HttpPost]
        [RequireHttps]
        public HttpResponseMessage ConsultaSaldo(Identificador id)
        {
            if(id!=null)
            {
                //DESCOMENTAR PARA VALIDAR CON FACEBOOK
                //if(fb.validate(id)!= null)
                //{ 
                Saldo saldo = fb.getBalance(id);
                if (saldo.total_pagar != 0)
                { 
                    return Request.CreateResponse(HttpStatusCode.OK, saldo, "application/json");
                }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_cunsultagarantiasliquidas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("cliente/garantia")]
        [HttpPost]
        [RequireHttps]
        public HttpResponseMessage CosultarGarantias(Identificador id)
        {
            if(id!=null)
            {
                //DESCOMENTAR PARA VALIDAR CON FACEBOOK
                //if(fb.validate(id)!= null)
                //{ 
                GarantiaLiquida garantia = fb.getWarranty(id);
                if (garantia.id_cliente != 0)
                { 
                    return Request.CreateResponse(HttpStatusCode.OK, garantia, "application/json");
                }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        #endregion consultas


        #region guardado

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_guardargarantialiquida.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("cliente/garantia")]
        [HttpPut]
        [RequireHttps]
        public HttpResponseMessage GuardarGarantia(GarantiaLiquida garantia)
        {
            // curl http://localhost:16453/cliente/garantia/169 -XPUT -d"id_cliente=169&tipo_
            // movimiento=1&monto=1.23"

            if (garantia != null)
            {
                //DESCOMENTAR PARA VALIDAR CON FACEBOOK
                //if(fb.validate(id)!= null)
                //{ 
                int result = fb.saveWarranty(garantia);
                if (result == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_GuardarAbono.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("cliente/abono")]
        [HttpPut]
        [RequireHttps]
        public HttpResponseMessage GuardarAbono(Abono abono)
        {
            if (abono != null)
            {
                //DESCOMENTAR PARA VALIDAR CON FACEBOOK
                //if(fb.validate(id)!= null)
                //{ 
                int result = fb.savePayment(abono);
                if (result != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
                }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }

        #endregion guardado
    }
}
