
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATMWebApp.Models
{
    public class EstatisticaViewModel
    {
        [Display(Name = "Total de contas:")]
        public int TotalDeContas { get; set; }

        [Display(Name = "Total de transações: ")]
        public int TotalDeTransacoes { get; set; }

        [Display(Name = "Conta com maior saldo: ")]
        public Conta ContaComMaiorSaldo { get; set; }

        [Display(Name = "Conta com mais transacções: ")]
        public Conta ContaComMaisTransacoes { get; set; }

        [Display(Name = "Contas com maior saldo: ")]
        public List<Conta> ContasComMaiorSaldo { get; set; }


        [Display(Name = "Total de transações por conta: ")]
        public List<TotalDeTransacoesPorContaViewModel> TotalDeTransacoesPorConta { get; set; }
        }


    public class TotalDeTransacoesPorContaViewModel
    {
        public Conta Conta { get; set; }
        public int Total { get; set; }
        }
}