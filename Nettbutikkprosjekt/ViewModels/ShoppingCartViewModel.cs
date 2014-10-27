using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nettbutikkprosjekt.Models;

namespace Nettbutikkprosjekt.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<vogn> vognitem { get; set; }
        public decimal vogntotal { get; set; }
    }
}