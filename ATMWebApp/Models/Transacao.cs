using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMWebApp.Models
{
    public enum TipoTransacao { Levantamento, Transferencia, Deposito }

    public partial class Transacao
    {
        public decimal Montante { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;
        public Conta Conta{ get; set; }
        public TipoTransacao Tipo { get; set; }

        }
}