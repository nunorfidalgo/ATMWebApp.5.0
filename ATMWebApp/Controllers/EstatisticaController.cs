using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ATMWebApp.Models;

namespace ATMWebApp.Controllers
{
    public class EstatisticaController : Controller
    {

        List<Conta> contas = Context.Db.Contas;
        List<Transacao> transacoes = Context.Db.Transacoes;

        // *******************************************************
        // GET: Estatistica sumária das contas e das transações

        public ActionResult EstatisticaSumaria()
        {
            //Cálculos primários:

            var total_contas = contas.Count();
            var total_transacoes = transacoes.Count();

            var saldo_maior = contas.Max(c => c.Saldo);
            var conta_com_maior_saldo = contas.Where(c => c.Saldo == saldo_maior).First();

            var contas_transacoes = contas.Join(transacoes, ct => ct, tc => tc.Conta, (ct, tc) => new { Conta = ct, Transacao = tc }).ToList();

            var total_transacoes_por_conta = contas_transacoes.GroupBy(c => c.Conta).
                Select(p => new TotalDeTransacoesPorContaViewModel { Conta = p.Key, Total = p.Count() }).ToList();

            var contas_com_maior_saldo = contas.OrderByDescending(c => c.Saldo).Take(5).ToList();

            var maximo = total_transacoes_por_conta.Max(c => c.Total);
            var conta_com_mais_transacoes = total_transacoes_por_conta.Where(c => c.Total == maximo).First();

            //O resultado, numa instância de EstatisticaViewModel:

            EstatisticaViewModel estatistica = new EstatisticaViewModel()
            {
                ContaComMaiorSaldo = conta_com_maior_saldo,
                TotalDeContas = total_contas,
                TotalDeTransacoes = total_transacoes,
                ContasComMaiorSaldo = contas_com_maior_saldo,
                TotalDeTransacoesPorConta = total_transacoes_por_conta,
                ContaComMaisTransacoes = conta_com_mais_transacoes.Conta
            };


            return View(estatistica);
        }
    }
}