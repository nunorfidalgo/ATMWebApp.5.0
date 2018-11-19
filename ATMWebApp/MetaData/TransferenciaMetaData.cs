using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATMWebApp.Models
{
    public class TransferenciaMetaData
    {
        [Display(Name = "Conta de origem")]
        public int ContaOrigem { get; set; }

        [Display(Name = "Conta de destino")]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [RegularExpression(@"\d{5,10}", ErrorMessage = "0 {0} dever entre 5 a 7 digitos")]
        [Remote("Diferente", "Transacoes", ErrorMessage = "Selecione outra {0}")]
        public int ContaDestino { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [Remote("Positivo", "Transacoes", ErrorMessage = "Selecione outra {0}")]
        public decimal Montante { get; set; }
    }

    [MetadataType(typeof(TransferenciaMetaData))]
    public partial class TransferenciaViewModel { }
}