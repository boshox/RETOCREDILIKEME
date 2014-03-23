using System;

namespace RETO_CREDILIKEME.Models
{
    public class Saldo
    {
        public Decimal total_pagar { get; set; }
        public int cuotas_pagadas { get; set; }
        public int num_cuotas { get; set; }
        public Decimal monto { get; set; }
        public char proximo_pago { get; set; }
        public int status_cumplimiento { get; set; }
    }
}