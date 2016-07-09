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
    public class CurrencyBLL : BaseBLL
    {
        #region Common
        public List<CurrencyBinding> Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Currencies.Where(m => m.SiteID == SiteID).OrderByDescending(m => m.Default).Select(m => new CurrencyBinding
                {
                    CurrencyID = m.CurrencyID,
                    CurrencyName = m.CurrencyName,
                    Default = m.Default,
                    SiteID = m.SiteID,
                    MoneySymbol = m.MoneySymbol,
                    ShippingPrice = m.ShippingPrice
                }).ToList();
            }
        }
        #endregion

        #region Web
        private IQueryable<Currency> Get(int SiteID, string Url, ApplicationDbContext _c)
        {
            return (SiteID != 0
                    ? from c in _c.Currencies
                      where c.SiteID == SiteID
                      orderby c.Default descending
                      select c
                     : from c in _c.Currencies
                       join u in _c.SiteUrls on c.SiteID equals u.SiteID
                       where u.Url.Equals(Url)
                       orderby c.Default descending
                       select c);
        }
        #endregion

        #region Customer
        private Currency GetByID(int CurrencyID, ApplicationDbContext _c)
        {
            return _c.Currencies.Where(m => m.CurrencyID == CurrencyID).SingleOrDefault();
        }
        public int Save(CurrencyBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                Currency _C = new Currency();
                if (Model.CurrencyID == 0)
                {
                    _C.Default = false;
                    _C.SiteID = Model.SiteID;
                    _c.Currencies.Add(_C);
                }
                else
                {
                    _C = GetByID(Model.CurrencyID, _c);
                }
                _C.CurrencyName = Model.CurrencyName;
                _C.MoneySymbol = Model.MoneySymbol;
                _C.ShippingPrice = Model.ShippingPrice;
                _c.SaveChanges();
                return _C.CurrencyID;
            }
        }
        public void SetAsDefault(int CurrencyID, string UserID)
        {
            using (var _c = db)
            {
                var _Currency = GetByID(CurrencyID, _c);
                Can(_Currency.SiteID, UserID, _c);
                _c.Currencies.Where(m => m.SiteID == _Currency.SiteID).ToList().ForEach(m => m.Default = false);
                _Currency.Default = true;
                _c.SaveChanges();
            }
        }
        public void Delete(int CurrencyID, string UserID)
        {
            using (var _c = db)
            {
                var _Currency = GetByID(CurrencyID, _c);
                Can(_Currency.SiteID, UserID, _c);
                _c.Currencies.Remove(_Currency);
                _c.SaveChanges();
            }
        }
        #endregion
    }
}
