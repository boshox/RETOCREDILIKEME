using System;

namespace RETO_CREDILIKEME.Models
{
    public class Banco
    {
        public int id { get; set; }
        public String descripcion { get; set; }

        public char status { get; set; }

        public Int16 recibe_pagos { get; set; }

        public String num_cuenta { get; set; }

        public String clabe { get; set; }

        public String referencia { get; set; }
    }
}