using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RETO_CREDILIKEME.Models
{
    /// <summary>
    /// Clase para almacenar información de
    /// identificación para hacer los llamados
    /// a los procedimientos almacenados.
    /// </summary>
    public class Identificador
    {
        public String token { get; set; }

        public String id_facebook { get; set; }

        public int id_cliente { get; set; }
    }
}