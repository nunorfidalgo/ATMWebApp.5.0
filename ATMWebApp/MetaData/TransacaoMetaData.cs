using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATMWebApp.Models
{
    public partial class TransacaoMetaData
    {
        [Required(ErrorMessage = "= {0} é obrigatorio")]
        [Remote("Positivo", "Transacoes", ErrorMessage = "O {0} tem de ser um valor > 0")]
        public decimal Montante { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;
        public Conta Conta { get; set; }
        public TipoTransacao Tipo { get; set; }

    }


    [MetadataType(typeof(TransacaoMetaData))]
    public partial class Transacao { }
}