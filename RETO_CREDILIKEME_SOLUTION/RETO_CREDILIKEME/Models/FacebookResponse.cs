using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RETO_CREDILIKEME.Models
{
    
    /// <summary>
    /// Modelo para asignar la informacion obtenida de un usuario directamente de Facebook
    /// </summary>
    public class FacebookResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string timezone { get; set; }
        public string locale { get; set; }
        public string updated_time { get; set; }
    }
}