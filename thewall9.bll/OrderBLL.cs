using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class OrderBLL : BaseBLL
    {
        private Order GetByID(int OrderID, ApplicationDbContext _c)
        {
            return _c.Orders.Where(m => m.OrderID == OrderID).SingleOrDefault();
        }
        public List<OrderBinding> Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Orders.Where(m => m.SiteID == SiteID).Select(m => new OrderBinding
                {
                    OrderID=m.OrderID,
                    SiteID = m.SiteID,
                    Address = m.Address,
                    City = m.City,
                    Comment = m.Comment,
                    Email = m.Email,
                    Name = m.Name,
                    Phone = m.Phone,
                    CurrencyID = m.CurrencyID,
                    CurrencyName=m.Currency.CurrencyName,
                    DateCreated=m.DateCreated,
                    Products = m.OrderProducts.Select(op=>new OrderProductBinding
                    {
                        Number=op.Number,
                        OrderID=op.OrderID,
                        ProductID=op.ProductID,
                        ProductAlias=op.Product.ProductAlias
                    }).ToList()
                }).ToList();
            }
        }
        public void Save(OrderBinding Model)
        {
            using (var _c = db)
            {
                var _Order = new Order(Model);
                _c.Orders.Add(_Order);
                _c.SaveChanges();
            }
        }
        public void Delete(int OrderID, string UserID)
        {
            using (var _c = db)
            {
                var _Order = GetByID(OrderID, _c);
                Can(_Order.SiteID, UserID, _c);
                _c.Orders.Remove(_Order);
                _c.SaveChanges();
            }
        }
    }
}
