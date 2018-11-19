using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMWebApp.Models
{
    public class ContaComListaDeTransacoesViewModel
    {
        public Conta Conta { get; set; }
        public List<Transacao> Transacoes { get; set; }

        //Constructor
        public ContaComListaDeTransacoesViewModel(Conta conta)
        {
            Conta = conta;
            Transacoes= Context.Db.Transacoes.Where(t => t.Conta == conta).OrderByDescending(t=>t.Data).ToList();
            }
    }
}