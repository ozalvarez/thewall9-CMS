using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public class OrderBase
    {
        public int OrderID { get; set; }
        public int SiteID { get; set; }
        public int CurrencyID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Comment { get; set; }

    }
    public class OrderBinding : OrderBase
    {
        public List<OrderProductBinding> Products { get; set; }
    }
    public class OrderProductBase
    {
        public int Number { get; set; }
    }
    public class OrderProductBinding : OrderProductBase
    {
        public int ProductID { get; set; }
        public int OrderID { get; set; }
    }
}
