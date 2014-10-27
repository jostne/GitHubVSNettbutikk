using System;
using System.Linq;
using System.Web.Mvc;
using Nettbutikkprosjekt.Models;
using System.Collections.Generic;

namespace MvcMusicStore.Controllers
{
    public class CheckoutController : Controller
    {
        Kundecontext storeDB = new Kundecontext();
        const string PromoCode = "FREE";
        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            //var order = new Bestilling();
            var db = new Kundecontext();
            
            try
            {
                 List<decimal> ant = Session["ant"] != null ? (List<decimal>)Session["ant"] : null;
                List<int> id = Session["id"] != null ? (List<int>)Session["id"] : null;
                int tell = 0;
                if (id != null)
                {
                    foreach (int i in id)
                    {
                        var order = new Bestilling();
                        Produkt produktet = db.produkter.FirstOrDefault(p => p.produktid == i);
                        order.produkt = produktet;
                        order.dato = DateTime.Now;
                        string navnet = (string)Session["Bruker"];
                        Kunder kunden = db.kundene.FirstOrDefault(p => p.epost == navnet);
                        order.kundeid = kunden;
                        order.total = produktet.pris * ant[tell];
                        tell++;
                        db.bestillingene.Add(order);
                        db.SaveChanges();
                        

                    }
                }
            }
            catch{
                return View();
                }
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmptyCart();
            Session["id"] = null;
            Session["ant"] = null;
            return RedirectToAction("innlogget", "Kunder");
            /*
           try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    var db = new Kundecontext();
                    var kunde = db.kundene.FirstOrDefault(p => p.epost == Session["Bruker"]);
                    order.kundeid = kunde;
                    order.dato = DateTime.Now;

                    //Save Order
                    storeDB.bestillingene.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.bestillingsid });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }*/
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            var db = new Kundecontext();
            string navnet = (string)Session["Bruker"];
            var kunde = db.kundene.FirstOrDefault(p => p.epost == navnet);
            // Validate customer owns this order
            bool isValid = storeDB.bestillingene.Any(
                o => o.bestillingsid== id &&
                o.kundeid == kunde);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}