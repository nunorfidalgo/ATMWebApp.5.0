using ATMWebApp.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ATMWebApp.Models
{
    public partial class ContaMetaData
    {
        [Display(Name = "Número da conta")]
        //[Required]
        [RegularExpression(@"\d{5,10}", ErrorMessage = "0 {0} dever entre 5 a 7 digitos")]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [Remote("NumeroUnico", "Contas", ErrorMessage = "Este numero ja esta atribuido")]
        public int Numero { get; set; }

        [Display(Name = "Nome")]
        //[Required]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [MaxPalavras(2, ErrorMessage = "So pode colocar dois nomes")]
        public string PrimeiroNome { get; set; }

        [Display(Name = "Apelidos")]
        //[Required]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [MaxPalavras(4, ErrorMessage = "So pode colocar quatro apelidos")]
        public string Apelido { get; set; }

        public decimal Saldo { get; set; } = 0;

        [Display(Name = "Data inicial")]
        public DateTime Data { get; set; } = DateTime.Now;

    }

    [MetadataType(typeof(ContaMetaData))]
    public partial class Conta { }
}