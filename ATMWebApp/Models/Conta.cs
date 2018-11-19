using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMWebApp.Models
{
    public partial class Conta
    {
        public int Numero { get; set; }

        public string PrimeiroNome { get; set; }
        public string Apelido { get; set; }

        public decimal Saldo { get; set; } = 0;
        public DateTime Data { get; set; } = DateTime.Now;

    }

}