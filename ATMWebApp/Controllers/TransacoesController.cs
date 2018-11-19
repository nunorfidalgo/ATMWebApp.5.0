using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ATMWebApp.Models;

namespace ATMWebApp.Controllers
{
    public class TransacoesController : Controller
    {
        List<Conta> contas = Context.Db.Contas;
        List<Transacao> transacoes = Context.Db.Transacoes;

        // GET: Transacoes
        public ActionResult Index()
        {
            return View(transacoes);
        }


        // *******************************************************
        // GET: Transacoes/Levantamento

        public ActionResult Levantamento()
        {
            if (!CheckContaCorrente())
                return View(PaginaContaActiva());

            ViewBag.NumeroDaConta = GetNumeroContaCorrente();
            return View();
        }

        // POST: Transacoes/Levantamento/transacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Levantamento(Transacao transacao)
        {
            try
            {
                var conta = GetContaCorrente(contas);
                var levantamento = Math.Abs(transacao.Montante);
                if (conta.Saldo < levantamento) levantamento = conta.Saldo;

                conta.Saldo -= levantamento;
                transacoes.Add(new Transacao() { Montante = -levantamento, Conta = conta, Tipo = TipoTransacao.Levantamento });

                return RedirectToAction("Details", "Contas");
                // ou
                // return RedirectToAction("ContaComListaDeTransacoes", "Contas");
            }
            catch
            {
                return View(transacao);
            }
        }

        // *******************************************************
        // GET: Transacoes/Depósito

        public ActionResult Deposito()
        {
            if (!CheckContaCorrente())
                return View(PaginaContaActiva());

            ViewBag.NumeroDaConta = GetNumeroContaCorrente();
            return View();
        }


        // POST: Transacoes/Deposito/transacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposito(Transacao transacao)
        {
            try
            {
                var conta = GetContaCorrente(contas);
                var deposito = Math.Abs(transacao.Montante);

                conta.Saldo += deposito;
                transacoes.Add(new Transacao() { Montante = deposito, Conta = conta, Tipo = TipoTransacao.Deposito });

                //return RedirectToAction("Details", "Contas");
                // ou
                return RedirectToAction("ContaComListaDeTransacoes", "Contas");
            }
            catch
            {
                return View(transacao);
            }
        }

        // *******************************************************
        // GET: Transacoes/Transferência

        public ActionResult Transferencia()
        {
            if (!CheckContaCorrente())
                return View(PaginaContaActiva());

            ViewBag.Conta = GetContaCorrente(contas);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transferencia(TransferenciaViewModel transferencia)
        {
            if (ModelState.IsValid)
            {
                var contaDestino = GetConta(contas, transferencia.ContaDestino);
                var contaOrigem = GetConta(contas, transferencia.ContaOrigem);

                var valor_transferir = Math.Abs(transferencia.Montante);
                if (contaOrigem.Saldo < valor_transferir) valor_transferir = contaOrigem.Saldo;

                contaOrigem.Saldo -= valor_transferir;
                contaDestino.Saldo += valor_transferir;

                transacoes.Add(new Transacao() { Montante = -valor_transferir, Conta = contaOrigem, Tipo = TipoTransacao.Transferencia });
                transacoes.Add(new Transacao() { Montante = valor_transferir, Conta = contaDestino, Tipo = TipoTransacao.Transferencia });

                return RedirectToAction("SelecionarConta", "Home");
            }

            return View();
        }


        // *******************************************************
        // AUXILIARES:

        [NonAction]
        private int GetNumeroContaCorrente()
        {
            return (Session.Get<int>("ContaCorrente"));
        }

        [NonAction]
        private bool CheckContaCorrente()
        {
            ViewBag.Mensagem = "Selecione uma conta!";
            return (GetNumeroContaCorrente() == default(int)) ? false : true;
        }


        [NonAction]
        private Conta GetContaCorrente(List<Conta> contas)
        {
            return contas.Where(c => c.Numero == GetNumeroContaCorrente()).FirstOrDefault();
        }

        [NonAction]
        public Conta GetConta(List<Conta> contas, int? n)
        {
            return contas.Where(c => c.Numero == n).FirstOrDefault();
        }

        [NonAction]
        private string PaginaContaActiva()
        {
            return "~/Views/Shared/Mensagem_Conta_Activa.cshtml";
        }

        [NonAction]
        private bool Existe(List<Conta> contas, int? id)
        {
            return contas.Any(c => c.Numero == id);
        }


        // *******************************************************
        //REMOTE VALIDATION:

        public JsonResult Positivo(decimal Montante)
        {
            if (Montante > 0.0M) return Json(true, JsonRequestBehavior.AllowGet);
            else return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Diferente(int ContaDestino)
        {
            if (!Existe(contas, ContaDestino))
                return Json(false, JsonRequestBehavior.AllowGet);

            if (ContaDestino == GetNumeroContaCorrente())
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}