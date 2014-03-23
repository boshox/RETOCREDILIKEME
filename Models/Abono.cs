using System;

namespace RETO_CREDILIKEME.Models
{
    public class Abono
    {
        public string token { get; set; }
        public int id_cliente { get; set; }
        public Decimal importe { get; set; }
        public String observacion { get; set; }
        public int id_forma_pago { get; set; }
        public String referencia { get; set; }
        public int id_banco { get; set; }
    }
}