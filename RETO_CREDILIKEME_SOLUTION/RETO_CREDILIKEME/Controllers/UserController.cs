using RETO_CREDILIKEME.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RETO_CREDILIKEME.Core;

namespace RETO_CREDILIKEME.Controllers
{
    public class UserController : ApiController
    {
        Fbc varx;
        
        public UserController(){
            varx = new Fbc();
        }

        /// <summary>
        /// Solamente para pruebas, ingresas a url:port/auth/id_facebook/accesstoken
        /// y hace la llamada a facebook, valida la informacion y nos regresa un JSON
        /// con la informacion del usuario, si el token no es valido regresa codigo de
        /// estado unauthorized
        /// </summary>
        [Route("auth")]
        [HttpPost]
        public dynamic Authenticate(Identificador id)
        {
            dynamic x = varx.getUserInfo(id);
            if (x == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return x;
        }
    }
}
