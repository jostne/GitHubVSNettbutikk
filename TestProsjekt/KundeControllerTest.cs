using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL;
using Model;
using Nettbutikkprosjekt.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;


using System.Web;

namespace TestProsjekt
{
    [TestClass]
    public class KundeControllerTest
    {

        [TestMethod]
        public void listorde()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));
            var forventetResultat = new List<Produkten>();

            var prod = new Produkten()
            {
                produktid = 1,
                beskrivelse = "løk",
                kategori = "Sykkel",
                navn = "",
                path = "",
                pris = 200

            };
            forventetResultat.Add(prod);
            forventetResultat.Add(prod);
            forventetResultat.Add(prod);

            var actionResult = (ViewResult)controller.ListAlleProdukter();
            var resultat = (List<Produkten>)actionResult.Model;
            Assert.AreEqual(actionResult.ViewName, "");


        }

        [TestMethod]
        public void Liste()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));
            var forventetResultat = new List<Kunde>();

            var kunde = new Kunde()
            {
                  kundeid = 1,
                fornavn = "thanh",
                etternavn = "test",
                telefonnr = 12345678,
                postnr = "1234",
                poststed = "agder",
                adresse = "oslo 11",
                epost = "dad@hotmail.no",
                passord = "test"

            };
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);

            var actionResult = (ViewResult)controller.ListAlleKunder();
            var resultat = (List<Kunde>)actionResult.Model;
            Assert.AreEqual(actionResult.ViewName, "");

        

        }

        [TestMethod]
        public void Sjekk_om_slett()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));

            var actionResult = (RedirectToRouteResult)controller.slett(1);
            

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");

        }


        
        [TestMethod]
        public void endre_produkt()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));

          /* var produkt = new Produkt()
            {
                produktid = 1,
                beskrivelse = "heihei",
                navn = "bilsykkel",
                kategori = "Utstyr",
                pris = 399,
                path = "",
            };
            */

            

            var result = (ViewResult)controller.endreOrdre(1);
            Assert.AreEqual(result.ViewName, "");



            

        }
         [TestMethod]
        public void opprettprodukt()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));
            

            var result = (ViewResult)controller.Opprettprodukt();
            Assert.AreEqual(result.ViewName, "");
        }

       
       [TestMethod]
        public void endrebruker()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));


            var kunde = new Kunde()
            {
                fornavn = "thanh",
                etternavn = "vu",
                adresse = "oslo 1",
                epost = "test@hotmail.no",
                passord = "heihei",
                telefonnr = 1233435,
                postnr = "0456",
                poststed = "agder"
            };
           // var result = (RedirectToRouteResult)controller.endreBruker(innbruker);
            var result = (ViewResult)controller.endreBruker(kunde);
            Assert.AreEqual(result.ViewName, "");

            // Assert
           // Assert.AreEqual(result.RouteName, "");

        }
        [TestMethod]
        public void endreadmin()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));

        
         var kunde = new Kunde()
            {
                kundeid = 0,
                fornavn = "thanh",
                etternavn = "vu",
                adresse = "oslo 1",
                epost = "test@hotmail.no",
                passord = "heihei",
                telefonnr = 1233435,
                postnr = "0456",
                poststed = "agder"
            };
           // var result = (RedirectToRouteResult)controller.endreBruker(innbruker);
            var result = (ViewResult)controller.endreAdmin(kunde);
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void opprett_admin()
        {
            var controller = new KunderController(new KundeBLL(new KundeRepositoryStub()));

            var sikkert = new sikkerAdmin()
            {
                adminid = 1,
                brukernavn = "test",
                passord = "test"
            };
            var result = (RedirectToRouteResult)controller.opprettAdmin(sikkert);
            //var result = (ViewResult)controller.opprettAdmin(sikkert);
           // Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(result.RouteName, "");
        }

    }
}
