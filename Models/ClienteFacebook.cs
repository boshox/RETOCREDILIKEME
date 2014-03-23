using System;

namespace RETO_CREDILIKEME.Models
{
    public class ClienteFacebook
    {
        public int id_cliente {get; set;}
        public String id_usuario { get; set; }
        public String id_facebook { get; set; }
        public String nb_facebook { get; set; }
        public String link_facebook { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public String link_identificacion { get; set; }
        public String localidad { get; set; }
        public String entidad { get; set; }
        public String tipo_movimiento { get; set; }
        public String usuario_registro { get; set; }
        public DateTime fecha_ultimo_movimiento { get; set; }

    }
}