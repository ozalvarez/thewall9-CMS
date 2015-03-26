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
        private Currency GetByID(int CurrencyID, ApplicationDbContext _c)
        {
            return _c.Currencies.Where(m => m.CurrencyID == CurrencyID).SingleOrDefault();
        }
        public List<CurrencyBinding> Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Currencies.Where(m => m.SiteID == SiteID).OrderByDescending(m => m.Default).Select(m => new CurrencyBinding
                {
                    CurrencyID = m.CurrencyID,
                    CurrencyName = m.CurrencyName,
                    Default = m.Default,
                    SiteID = m.SiteID
                }).ToList();
            }
        }
        public int Save(CurrencyBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                Currency _C = null;
                if (Model.CurrencyID == 0)
                {
                    _C = new Currency
                    {
                        CurrencyName = Model.CurrencyName,
                        Default = false,
                        SiteID = Model.SiteID
                    };
                    _c.Currencies.Add(_C);
                }
                else
                {
                    _C = GetByID(Model.CurrencyID, _c);
                    _C.CurrencyName = Model.CurrencyName;
                }
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
    }
}
