using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMWebApp.Models
{
    public partial class TransferenciaViewModel
    {
        [Required]
        public int ContaOrigem { get; set; }

        [Required]
        public int ContaDestino { get; set; }

        [Required]
        public decimal Montante { get; set; }
        }
}