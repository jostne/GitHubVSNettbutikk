using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
namespace Nettbutikkprosjekt.Models
{
    public partial class ShoppingCart
    {
        Kundecontext storeDB = new Kundecontext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Produkt produt)
        {
            // Get the matching cart and produt instances
            var cartItem = storeDB.vogner.SingleOrDefault(
                c => c.vognid == ShoppingCartId
                && c.produktid == produt.produktid);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new vogn
                {
                    produktid = produt.produktid,
                    vognid = ShoppingCartId,
                    antall = 1,
                    datolaget = DateTime.Now
                };
                storeDB.vogner.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.antall++;
            }
            // Save changes
            storeDB.SaveChanges();
        }
       
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = storeDB.vogner.Single(
                cart => cart.vognid == ShoppingCartId
                && cart.viktigid == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.antall > 1)
                {
                    cartItem.antall--;
                    itemCount = cartItem.antall;
                }
                else
                {
                    storeDB.vogner.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.vogner.Where(
                cart => cart.vognid == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                storeDB.vogner.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public List<vogn> GetCartItems()
        {
            return storeDB.vogner.Where(
                cart => cart.vognid == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.vogner
                          where cartItems.vognid == ShoppingCartId
                          select (int?)cartItems.antall).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply produt price by count of that produt to get 
            // the current price for each of those produts in the cart
            // sum all produt price totals to get the cart total
            decimal? total = (from cartItems in storeDB.vogner
                              where cartItems.vognid == ShoppingCartId
                              select (int?)cartItems.antall *
                              cartItems.produkt.pris).Sum();
            return total ?? decimal.Zero;
        }
        public int CreateOrder(Bestilling order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new Bestiltprodukt
                {
                    produktid = item.produktid,
                   bestillingid  = order.bestillingsid,
                    pris = item.produkt.pris,
                    antall = item.antall
                };
                // Set the order total of the shopping cart
                orderTotal += (item.antall * item.produkt.pris);
                storeDB.bestilteprodukter.Add(orderDetail);
            }
            // Set the order's total to the orderTotal count
            order.total = orderTotal;
            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.bestillingsid;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.vogner.Where(
                c => c.vognid == ShoppingCartId);
            foreach (vogn item in shoppingCart)
            {
                item.vognid = userName;
            }
            storeDB.SaveChanges();
        }
    }
}