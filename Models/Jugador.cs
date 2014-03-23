using System;

namespace RETO_CREDILIKEME.Models
{
    public class Jugador
    {
        public String nombre { get; set; }

        public String apellido_paterno { get; set; }

        public String apellido_materno { get; set; }

        public DateTime fecha_nacimiento { get; set; }

        public String sexo { get; set; }

        public String tel_celular { get; set; }

        public String ocupacion { get; set; }

        public String email { get; set; }

        public String twitter { get; set; }

        public String email_adicional { get; set; }

        public String cuenta_stp { get; set; }
    }
}