using System;

namespace RETO_CREDILIKEME.Models
{
    public class GarantiaLiquida
    {
        public string token { get; set; }
        public int id_cliente { get; set; }
        public int tipo_movimiento { get; set; }
        public Decimal monto { get; set; }
        public int status { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_modifica { get; set; }
    }
}