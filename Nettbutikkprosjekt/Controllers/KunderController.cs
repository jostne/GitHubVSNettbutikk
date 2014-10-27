using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikkprosjekt.Models;

namespace Nettbutikkprosjekt.Controllers
{
    public class KunderController : Controller
    {
        //
        // GET: /Kunder/
        public ActionResult send()
        {
            Session["Navn"] = "Hei din dritt";
            return RedirectToAction("info");
        }
        public ActionResult Index()
        {
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Sikkerkunde innkunde)
        {
            if (Bruker_i_DB(innkunde))
            {
                Session["LoggetInn"] = true;
                ViewBag.Innlogget = true;
                Session["Bruker"] = innkunde.epost;
                return RedirectToAction("Innlogget");
            }
            else
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }
        private static bool Bruker_i_DB(Models.Sikkerkunde innkunde)
        {
            using (var db = new Models.Kundecontext())
            {   
                byte[] passordDb = lagHash(innkunde.passord);
                var funnetkunde = db.kundene.FirstOrDefault
                    (b => b.passord == passordDb && b.epost == innkunde.epost);
                if (funnetkunde == null)
                {
                    return false;
                }
                    
                else
                {
                    return true;
                }
                    
            }
        }
        public ActionResult innlogget()
        {


           if (Session["LoggetInn"] != null)
            {
                bool loggetinn = (bool)Session["LoggetInn"];
                if (loggetinn)
                {
                    var db = new Models.Kundecontext();
                    string navn = (string)Session["Bruker"];
                    var kunden = db.kundene.FirstOrDefault(p => p.epost == navn);
                    List<Models.Bestilling> Listeavprodukter = db.bestillingene.Where(p => p.kundeid.kundeid == kunden.kundeid).ToList();
                    ViewData.Model = Listeavprodukter;
                    //return View();
                    return View("innlogget");
                }
            }
            return RedirectToAction("Index");
        }
       /* [HttpPost]
        public ActionResult innlogget(Bestilling inn)
        {
            var db = new Models.Kundecontext();
            string navn = (string)Session["Bruker"];
            var kunden = db.kundene.FirstOrDefault(p => p.epost == navn);
            List<Models.Bestilling> Listeavprodukter = db.bestillingene.Where(p => p.kundeid.kundeid == kunden.kundeid).ToList();
            ViewData.Model = Listeavprodukter;
            return View();
        }*/
        public ActionResult ListAlleKunder()
        {

            return RedirectToAction("Home");
        }
        public ActionResult endreBruker()
        {
            bool loggetinn = (bool)Session["LoggetInn"];
            if (loggetinn)
            {
                var db = new Models.Kundecontext();
                string navnet = (string)Session["Bruker"];
                Models.Kunder person = db.kundene.FirstOrDefault(p => p.epost == navnet);
                //var person = db.kundene.Find(Session["Bruker"]);
                return View(person);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult endreBruker(Models.Sikkerkunde innbruker)
        {
            try
            {
                using (var db = new Models.Kundecontext())
                {
                    
                    string navnet = (string)Session["Bruker"];
                    Models.Kunder funnetperson = db.kundene.FirstOrDefault(p => p.epost == navnet);
                    //var funnetperson = db.kundene.FirstOrDefault(p => p.epost == navnet);
                    if (funnetperson == null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                            funnetperson.adresse = innbruker.adresse;
                            funnetperson.epost = innbruker.epost;
                            funnetperson.etternavn = innbruker.etternavn;
                            funnetperson.fornavn = innbruker.fornavn;
                            byte[] passordDb = lagHash(innbruker.passord);
                            funnetperson.passord = passordDb;
                            funnetperson.postnr = innbruker.postnr;
                            funnetperson.poststed = innbruker.poststed;
                            funnetperson.telefonnr = innbruker.telefonnr;
                            db.SaveChanges();
                            return RedirectToAction("Innlogget");

                    }
                }
            }
            catch (Exception feil)
            {
                return View();
            }
        }
        public ActionResult Opprettprodukt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Opprettprodukt(FormCollection innProdukt)
        {
            try
            {
                using (var db = new Models.Kundecontext())
                {
                    var nyttProduk = new Models.Produkt();
                    nyttProduk.navn = innProdukt["produktnavn"];
                    decimal prisen = decimal.Parse(innProdukt["pris"]);
                    nyttProduk.beskrivelse = innProdukt["beskrivelse"];
                    nyttProduk.kategori = innProdukt["kategori"];
                    nyttProduk.path = innProdukt["path"];
                    nyttProduk.pris = prisen;
                    db.produkter.Add(nyttProduk);
                    db.SaveChanges();
                    return RedirectToAction("ListAlleKunder");
                }
            }
            catch (Exception feil)
            {
                return View();
            }
        }
        public ActionResult sykkel()
        {
            var db = new Models.Kundecontext();
            List<Models.Produkt> Listeavprodukter = db.produkter.ToList();
            ViewData.Model = Listeavprodukter;
            return View();
        }
        public ActionResult del()
        {
            var db = new Models.Kundecontext();
            List<Models.Produkt> Listeavdeler = db.produkter.ToList();
            ViewData.Model = Listeavdeler;
            return View();
        }

        public ActionResult info(int id){
            var db = new Models.Kundecontext();
            Models.Produkt produkt = db.produkter.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }
        public ActionResult utstyr()
        {
            var db = new Models.Kundecontext();
            List<Models.Produkt> Listeavdeler = db.produkter.ToList();
            ViewData.Model = Listeavdeler;
            return View();
        }
        private static byte[] lagHash(string innpassord)
        {
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            innData = System.Text.Encoding.ASCII.GetBytes(innpassord);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }
        public ActionResult OprettBruker()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OprettBruker(Models.Sikkerkunde innkunde)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new Models.Kundecontext())
                {

                    var nyKunde = new Models.Kunder();
                    byte[] passordDb = lagHash(innkunde.passord);
                    nyKunde.passord = passordDb;
                    nyKunde.fornavn = innkunde.fornavn;
                    nyKunde.etternavn = innkunde.etternavn;
                    nyKunde.epost = innkunde.epost;
                    nyKunde.adresse = innkunde.adresse;
                    nyKunde.telefonnr = innkunde.telefonnr;

                    string innpostnr = innkunde.postnr;
                    /*nyKunde.fornavn = innListe["fornavn"];
                    nyKunde.etternavn = innListe["etternavn"];
                    nyKunde.adresse = innListe["adresse"];
                    nyKunde.epost = innListe["epost"];
                    nyKunde.passord = innListe["passord"];
                    string innpostnr = innListe["postnr"];
                    string inntlf = innListe["telefonnr"];*/

                    var funnetPoststed = db.poststedene.FirstOrDefault(p => p.postnr == innpostnr.ToString());
                    if (funnetPoststed == null)
                    {
                        var nyttPoststed = new Models.Poststed();
                        nyttPoststed.postnr = innkunde.postnr.ToString();
                        nyttPoststed.poststed = innkunde.poststed.ToString();
                        //nyttPoststed.postnr = innkunde["postnr"];
                        //nyttPoststed.poststed = innListe["poststed"];
                        db.poststedene.Add(nyttPoststed);
                        nyKunde.poststed = nyttPoststed;
                        nyKunde.postnr = innkunde.postnr;
                        //nyKunde.postnr = int.Parse(innListe["postnr"]);
                    }
                    else
                    {
                        nyKunde.poststed = funnetPoststed;
                        nyKunde.postnr = innkunde.postnr;
                        //nyKunde.postnr = int.Parse(innListe["postnr"]);
                    }
                    db.kundene.Add(nyKunde);
                    db.SaveChanges();
                    return RedirectToAction("Index");



                }
            }
            catch (Exception feil)
            {
                return View();
            }
        }
        public ActionResult logut()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
        public ActionResult Home()
        {
            return View();
        }

    }
}
