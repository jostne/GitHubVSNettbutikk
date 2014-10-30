using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.IO;
using BLL;

namespace Nettbutikkprosjekt.Controllers
{
    public class KunderController : Controller
    {
        //
        // GET: /Kunder/
        
        public ActionResult administrator()
        {
            if (Session["Admin"] != null)
            {
                if ((bool)Session["Admin"] == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult adminlogg()
        {
            if (Session["Admin"] == null)
            {
                Session["Admin"] = false;
                ViewBag.admin = false;
            }
            else
            {
                ViewBag.admin = (bool)Session["Admin"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult adminlogg(sikkerAdmin innadmin)
        {
            if (Admin_i_DB(innadmin))
            {
                Session["Admin"] = true;
                ViewBag.admin = true;
                Session["Administrator"] = innadmin.brukernavn;
                return RedirectToAction("adminlogg");
            }
            else
            {
                Session["Admin"] = false;
                ViewBag.admin = false;
                return View();
            }
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
        public ActionResult Index(Kunde innkunde)
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
        private static bool Bruker_i_DB(Kunde innkunde)
        {
                
                var Kundebll = new KundeBLL();
                if (Kundebll.Bruker_I_DB(innkunde))
                {
                    return true;
                }
                    
                else
                {
                    return false;
                }
                    
            
        }

        private static bool Admin_i_DB(sikkerAdmin innadmin)
        {
            
                var Kundebll = new KundeBLL();
                if (Kundebll.Admin_I_DB(innadmin))
                {
                    return true;
                }

                else
                {
                    return false;
                }

            
        }
        public ActionResult innlogget()
        {


           if (Session["LoggetInn"] != null)
            {
                bool loggetinn = (bool)Session["LoggetInn"];
                var Kundebll = new KundeBLL();
                if (loggetinn)
                {
                    
                    string navn = (string)Session["Bruker"];
                    
                      //ViewData.Model = Kundebll.finnordre(navn);
                    //return View();
                    

                    
                    ViewData.Model = Kundebll.finnordre(navn);
                    return View();
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
            var Kundebll = new KundeBLL();
            ViewData.Model = Kundebll.hentalle();
            //return View(Kundebll.hentalle());
            return View();
        }
        public ActionResult ListAlleOrdre()
        {
            var Kundebll = new KundeBLL();
            return View(Kundebll.hentordre());
        }
        
        public ActionResult endreBruker()
        {
            var Kundebll = new KundeBLL();
            bool loggetinn = (bool)Session["LoggetInn"];
            if (loggetinn)
            {
                string navnet = (string)Session["Bruker"];
                //var person = db.kundene.Find(Session["Bruker"]);
                return View(Kundebll.finnkundemednavn(navnet));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult endreBruker(Kunde innbruker)
        {
            var Kundebll = new KundeBLL();
            if(Kundebll.endrekunde(innbruker))
            {
                return RedirectToAction("Innlogget");
            } 
            else
            {
                return View();
            }
        }
        public ActionResult endreAdmin(int id)
        {
            bool loggetinn = (bool)Session["Admin"];

            if (loggetinn)
            {
                var Kundebll = new KundeBLL();
                string navnet = (string)Session["Bruker"];
                
                if (Kundebll.finnbruker1(id))
                {
                    return View(Kundebll.finnkunde(id));

                }
                else
                {
                    return RedirectToAction("ListAlleKunder");
                }
                
                
                
            }
            else
            {
                return RedirectToAction("ListAlleKunder");
            }
        }
        [HttpPost]
        public ActionResult endreAdmin(Kunde innbruker)
        {
            try
            {
                

                    string navnet = (string)Session["Bruker"];
                    var Kundebll = new KundeBLL();
                    var funnetperson = Kundebll.finnkunde(Kundebll.finnid(navnet));
                    
                   
                    if (Kundebll.endreadmin(funnetperson))
                    {
                        return RedirectToAction("ListAlleKunder");
                        
                    }
                    else
                    {
                        return RedirectToAction("Index");

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
                var Kundebll = new KundeBLL();
                Kundebll.opprettprodukt(innProdukt);
                return RedirectToAction("ListAlleKunder");
                
            }
            catch (Exception feil)
            {
                return View();
            }
        }

        public ActionResult lastopp(HttpPostedFileBase fil) 
        {
            if (fil != null)
            {
                string bilde = System.IO.Path.GetFileName(fil.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Bilder"), bilde);
                fil.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    fil.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            return RedirectToAction("Opprettprodukt");
        }
        public ActionResult sykkel()
        {
            var Kundebll = new KundeBLL();
            ViewData.Model = Kundebll.sykkel();
            return View();
        }
        public ActionResult del()
        {
            var Kundebll = new KundeBLL();
            ViewData.Model = Kundebll.del();
            return View();
        }

        public ActionResult info(int id){
            var Kundebll = new KundeBLL();
            var infoen = Kundebll.info(id);
            if (infoen == null)
            {
                return HttpNotFound();
            }
            return View(infoen);
        }
        public ActionResult slett(int id)
        {
            var Kundebll = new KundeBLL();
            if (Kundebll.slett(id))
            {
                return RedirectToAction("ListAlleKunder");
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        public ActionResult utstyr()
        {
            var Kundebll = new KundeBLL();
            ViewData.Model = Kundebll.utstyr();
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
        public ActionResult opprettAdmin()
        {
            return View();
        } 
        [HttpPost] 
        
        public ActionResult opprettAdmin(sikkerAdmin innadmin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {

                var Kundebll = new KundeBLL();
                Kundebll.opprettAdmin(innadmin);
                return RedirectToAction("adminlogg");
                
            }
            catch (Exception feil)
            {
                return View();
            }
        }
        public ActionResult OprettBruker()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OprettBruker(Kunde innkunde)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var Kundebll = new KundeBLL();
                Kundebll.opprettBruker(innkunde);
                return RedirectToAction("Index"); 
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
            Session["Admin"] = false;
            return RedirectToAction("Index");
        }
        public ActionResult Home()
        {
            return View();
        }

    }
}
