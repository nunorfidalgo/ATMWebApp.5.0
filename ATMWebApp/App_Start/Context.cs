using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ATMWebApp.Models; //Fundamental!

namespace ATMWebApp
{
    public sealed class Context
    {

        private static Context db = null;
        static readonly object acesso = new object();

        public List<Conta> Contas { get; set; }
        public List<Transacao> Transacoes { get; set; }

        private Context()
        {

            Contas = new List<Conta>()
            {
                new Conta() {Numero=10001, PrimeiroNome="Joanita",Apelido="Navalhas" , Saldo=140.5M},
                new Conta() {Numero=10002, PrimeiroNome="Zeca",Apelido="Palmeirinha" , Saldo=50},
                new Conta() {Numero=10003, PrimeiroNome="Sapita",Apelido="Sonecas" , Saldo=125},
                  new Conta() {Numero=10004, PrimeiroNome="Rafaelito",Apelido="Anzol" , Saldo=0},
                  new Conta() {Numero=10005, PrimeiroNome="Henriqueta",Apelido="Remos" , Saldo=10000},
                  new Conta() {Numero=10006, PrimeiroNome="Anzolino",Apelido="Cascudo" , Saldo=2500}
            };


            Transacoes= new List<Transacao>()
            {
                new Transacao() { Conta=Contas.ElementAt(0), Montante=100, Tipo=TipoTransacao.Deposito },
                new Transacao() { Conta=Contas.ElementAt(0), Montante=-10, Tipo=TipoTransacao.Levantamento},
                new Transacao() { Conta=Contas.ElementAt(0), Montante=50.5M, Tipo=TipoTransacao.Deposito},
                new Transacao() { Conta=Contas.ElementAt(2), Montante=-25, Tipo=TipoTransacao.Levantamento},
                new Transacao() { Conta=Contas.ElementAt(3), Montante=65, Tipo=TipoTransacao.Levantamento },
                new Transacao() { Conta=Contas.ElementAt(1), Montante=25, Tipo=TipoTransacao.Deposito},
                new Transacao() { Conta=Contas.ElementAt(3), Montante=125, Tipo=TipoTransacao.Deposito},
                new Transacao() { Conta=Contas.ElementAt(5), Montante=125, Tipo=TipoTransacao.Deposito},
                new Transacao() { Conta=Contas.ElementAt(5), Montante=225, Tipo=TipoTransacao.Deposito},
                new Transacao() { Conta=Contas.ElementAt(5), Montante=325, Tipo=TipoTransacao.Deposito},
            };
        }

        public static Context Db
        {
            get
            {
                lock (acesso)
                {
                    if (db == null) db = new Context();

                    return db;
                    }
                }
            }
    }



}