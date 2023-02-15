using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Models
{
    public class OrderModel
    {
        public int Id {get;set;}
        public List<OrderItemModel> Items {get;set;} = new List<OrderItemModel>();
        public int TotalCount {get;set;}
        public decimal TotalPrice {get;set;}
    }
}