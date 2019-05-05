using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Rentals
{
    public class PriceAdjusment
    {
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Reason { get; set; }

        public PriceAdjusment(AdjustPrice adjustPrice, decimal oldPrice)
        {
            this.OldPrice = oldPrice;
            this.NewPrice = adjustPrice.NewPrice;
            this.Reason = adjustPrice.Reason;
        }

        public string Describe()
        {
            return string.Format($"{OldPrice} -> {NewPrice}: {Reason}");
        }
    }
}