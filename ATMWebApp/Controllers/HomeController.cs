using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ATMWebApp.Models;

namespace ATMWebApp.Controllers
{
    public class HomeController : Controller
    {
        List<Conta> contas = Context.Db.Contas;

        public ActionResult Index()
        {
            //Uses the ViewBag to save the selected account:
            if (CheckContaCorrente()) ViewBag.ContaCorrente = GetContaCorrente(contas);
            else ViewBag.ContaCorrente = null;

            return View();
            }

        public ActionResult About()
        {
            ViewBag.Message = "Our application description page.";
            return View();
            }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact page.";
            return View();
            }


        // *******************************************************
        // NOVOS

        public ActionResult SelecionarConta()
        {
            return View(contas.OrderBy(c => c.PrimeiroNome));
        }


        public ActionResult Selecionar(int? id)
        {
            //Selected account:
            var conta = GetConta(contas, id);

            //Start a session with the selected account number:
            Session.Set("ContaCorrente", conta.Numero);
            return RedirectToAction("Index", "Home");
            }

        public ActionResult Logout()
        {
            //Ends the session:
            Session.Set("ContaCorrente", default(int));
            return RedirectToAction("Index", "Home");
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
            return (GetNumeroContaCorrente() == default(int)) ? false : true;
        }


        [NonAction]
        private Conta GetContaCorrente(List<Conta> contas)
        {
            return contas.Where(c => c.Numero == GetNumeroContaCorrente()).FirstOrDefault();
        }


        [NonAction]
        public Conta GetConta(List<Conta> contas, int? id)
        {
            return contas.Where(c => c.Numero == id).FirstOrDefault();
            }

    }

}