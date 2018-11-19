using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ATMWebApp.Models;

namespace ATMWebApp.Controllers
{
    public class ContasController : Controller
    {
        //Db simulation:
        List<Conta> contas = Context.Db.Contas;

        // GET: Contas
        public ActionResult Index()
        {
            return RedirectToAction("SelecionarConta", "Home");
        }

        // GET: Contas/Details/
        public ActionResult Details()
        {
            if (CheckContaCorrente())
                return View(GetContaCorrente(contas));

            return View(PaginaContaActiva());
        }

        // GET: Contas/Create
        public ActionResult Create()
        {
            //Creates an account
            return View(new Conta());
        }

        // POST: Contas/Create
        [HttpPost]
        public ActionResult Create(Conta conta)
        {
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        if (!Existe(contas, conta.Numero))
            //        {
            //            contas.Add(conta);
            //            return RedirectToAction("Index");
            //        }
            //        else ModelState.AddModelError("Numero", "Este número já foi atribuído");
            //    }
            //    return View(conta);
            //}
            //catch
            //{
            //    return View(conta);
            //}
            try
            {
                if (ModelState.IsValid)
                {
                    contas.Add(conta);
                    return RedirectToAction("Index");
                }
                return View(conta);
            }
            catch
            {
                return View(conta);
            }
        }

        // *******************************************************
        // GET: Contas/Edit/
        public ActionResult Edit()
        {
            if (CheckContaCorrente())
                return View(GetContaCorrente(contas));

            return View(PaginaContaActiva());
        }

        // POST: Contas/Edit/conta
        [HttpPost]
        public ActionResult Edit(Conta conta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (conta == null) return RedirectToAction("Index");
                    var conta_to_update = GetConta(contas, conta.Numero);

                    if (TryUpdateModel(conta_to_update, "", new string[] { "Apelido", "PrimeiroNome" }))
                        return RedirectToAction("Details");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(conta);
            }
        }

        // *******************************************************
        // GET: Contas/Delete/

        public ActionResult Delete()
        {
            if (CheckContaCorrente())
                return View(GetContaCorrente(contas));

            return View(PaginaContaActiva());
        }

        // POST: Contas/Delete/conta

        [HttpPost]
        public ActionResult Delete(Conta conta)
        {
            try
            {
                var conta_remover = GetConta(contas, conta.Numero);
                if (conta_remover == null) return RedirectToAction("Index");

                //transacoes.RemoveAll(t => t.Conta.Numero == conta.Numero);
                contas.Remove(conta_remover);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(conta);
            }
        }

        // *******************************************************
        // NOVO: Contas/ContaComListaDeTransacoes

        public ActionResult ContaComListaDeTransacoes()
        {
            if (!CheckContaCorrente())
                return View(PaginaContaActiva());

            return View(new ContaComListaDeTransacoesViewModel(GetContaCorrente(contas)));
        }

        // *******************************************************
        // AUXILIARES:

        [NonAction]
        public Conta GetConta(List<Conta> contas, int? id)
        {
            return contas.Where(c => c.Numero == id).FirstOrDefault();
        }


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

        public JsonResult NumeroUnico(int Numero)
        {
            //Modo edit
            if (Numero == GetNumeroContaCorrente())
                return Json(true, JsonRequestBehavior.AllowGet);

            // Modo create
            if (Existe(contas, Numero))
                return Json(false, JsonRequestBehavior.AllowGet);
            else return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
