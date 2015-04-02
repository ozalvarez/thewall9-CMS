using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{
    public class Order : OrderBase
    {
        public virtual List<OrderProduct> OrderProducts { get; set; }
        [ForeignKey("CurrencyID")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        public Order() { }
        public Order(OrderBinding Model)
        {
            this.Address = Model.Address;
            this.City = Model.City;
            this.Comment = Model.Comment;
            this.Email = Model.Email;
            this.Name = Model.Name;
            this.Phone = Model.Phone;
            this.SiteID = Model.SiteID;
            this.CurrencyID = Model.CurrencyID;

            this.OrderProducts = new List<OrderProduct>();
            foreach (var item in Model.Products)
            {
                this.OrderProducts.Add(new OrderProduct
                {
                    Number = item.Number,
                    ProductID = item.ProductID
                });
            }
        }
    }
    public class OrderProduct:OrderProductBase
    {
        [Key, Column(Order = 1)]
        public int OrderID { get; set; }
        [Key, Column(Order = 2)]
        public int ProductID { get; set; }
       

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
