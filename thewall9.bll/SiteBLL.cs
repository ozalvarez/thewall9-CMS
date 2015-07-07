using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data;
using thewall9.data.binding;
using thewall9.data.Models;

namespace thewall9.bll
{
    public class SiteExceptionMessages
    {
        public static string SITE_NOT_FOUNT = "0sx0";
    }
    public class SiteBLL : BaseBLL
    {
        public SiteBinding Get(int SiteID)
        {
            using (var _c = db)
            {
                return _c.Sites.Where(m => m.SiteID == SiteID).Select(m => new SiteBinding
                {
                    DefaultLang = m.DefaultLang,
                    GAID = m.GAID,
                    SiteID = m.SiteID,
                    SiteName = m.SiteName,
                    ECommerce = m.ECommerce,
                    Blog=m.Blog
                }).SingleOrDefault();
            }
        }

        #region WEB
        public Site Get(string Url, ApplicationDbContext _c)
        {
            return (from m in _c.Sites
                    join u in _c.SiteUrls on m.SiteID equals u.SiteID
                    where u.Url.Equals(Url)
                    select m).FirstOrDefault();
        }
        public SiteFullBinding Get(int SiteID, string Url, string Lang, int CurrencyID)
        {
            using (var _c = db)
            {
                if (SiteID == 0)
                    SiteID = Get(Url, _c).SiteID;
                var _Q = from m in _c.Sites
                         where m.SiteID == SiteID
                         select m;
                var _Site = _Q.Select(m => new SiteFullBinding
                         {
                             Site = new SiteBinding
                             {
                                 DefaultLang = m.DefaultLang,
                                 GAID = m.GAID,
                                 SiteID = m.SiteID,
                                 SiteName = m.SiteName,
                                 ECommerce = m.ECommerce,
                                 Blog=m.Blog
                             }
                         }).SingleOrDefault();
                if (_Site != null)
                {
                    if (string.IsNullOrEmpty(Lang))
                        Lang = _Site.Site.DefaultLang;
                    _Site.Menu = new PageBLL().GetMenu(SiteID, Url, Lang);
                    _Site.ContentLayout = new ContentBLL().GetContent(SiteID, Url, "layout", Lang);
                    _Site.OtherPages = new PageBLL().GetOtherPages(SiteID, Url, Lang);
                    if (_Site.Site.ECommerce)
                    {
                        _Site.Currencies = new CurrencyBLL().Get(SiteID, Url);
                        _Site.EcommercePages = new PageBLL().GetEcommercePages(SiteID, Url, Lang);
                    }
                    return _Site;
                }
                throw new RuleException("Site not found", SiteExceptionMessages.SITE_NOT_FOUNT);
            }
        }
        #endregion

        #region CUSTOMERS
        public List<SiteBinding> Get(string UserOwnerID)
        {
            using (var _c = db)
            {
                return _c.SiteUsers.Where(m => m.UserID.Equals(UserOwnerID)).Select(m => new SiteBinding
                {
                    DefaultLang = m.Site.DefaultLang,
                    GAID = m.Site.GAID,
                    SiteID = m.Site.SiteID,
                    SiteName = m.Site.SiteName,
                    ECommerce = m.Site.ECommerce,
                    Blog = m.Site.Blog
                }).ToList();
            }
        }
        public void Save(SiteBinding Model, string UserOwnerID)
        {
            using (var _c = db)
            {
                var _Site = _c.SiteUsers.Where(m => m.UserID.Equals(UserOwnerID) && m.SiteID == Model.SiteID).Select(m => m.Site).SingleOrDefault();
                _Site.GAID = Model.GAID;
                _Site.DefaultLang = Model.DefaultLang;
                _c.SaveChanges();
            }
        }
        #endregion

        #region ADMIN
        public List<SiteAllBinding> GetAll()
        {
            using (var _c = db)
            {
                return _c.Sites.ToList().Select(m => new SiteAllBinding
                {
                    DefaultLang = m.DefaultLang,
                    GAID = m.GAID,
                    SiteID = m.SiteID,
                    SiteName = m.SiteName,
                    Enabled = m.Enabled,
                    DateCreated = m.DateCreated,
                    Url = string.Join(",", m.SiteUrls.Select(m2 => m2.Url)),
                    ECommerce = m.ECommerce,
                    Cultures = m.Cultures.Select(m2 => new CultureBinding
                    {
                        CultureID = m2.CultureID,
                        Name = m2.Name,
                        SiteID = m2.SiteID
                    }),
                    Blog = m.Blog
                }).ToList();
            }
        }
        public int Save(SiteAllBinding Model)
        {
            using (var _c = db)
            {
                Site _Model;
                var _SiteUrls = string.IsNullOrEmpty(Model.Url) ? new string[0] : Model.Url.Split(',');
                if (Model.SiteID == 0)
                {
                    _Model = new Site
                    {
                        SiteName = Model.SiteName,
                        DateCreated = DateTime.Now,
                        Enabled = false,
                        DefaultLang = Model.DefaultLang,
                        GAID = Model.GAID
                    };
                    if (Model.Cultures == null || Model.Cultures.Count() == 0)
                        throw new RuleException("Cultures is Empty");

                    //ADD CULTURES
                    foreach (var item in Model.Cultures)
                    {
                        _Model.Cultures.Add(new Culture
                        {
                            Name = item.Name,

                            Facebook = item.Facebook,
                            GPlus = item.GPlus,
                            Instagram = item.Instagram,
                            Tumblr = item.Tumblr,
                            Twitter = item.Twitter
                        });
                    }
                    //ADD SITES
                    foreach (var item in _SiteUrls)
                    {
                        _Model.SiteUrls.Add(new SiteUrl
                        {
                            Url = item
                        });
                    }
                    _c.Sites.Add(_Model);
                }
                else
                {
                    _Model = _c.Sites.Where(m => m.SiteID == Model.SiteID).SingleOrDefault();
                    _Model.SiteName = Model.SiteName;
                    //EDIT CULTURES
                    foreach (var item in Model.Cultures)
                    {
                        if (item.CultureID != 0)
                        {
                            var _CultureModel = _Model.Cultures.Where(m => m.CultureID == item.CultureID).SingleOrDefault();
                            _CultureModel.Name = item.Name;
                        }
                        else
                        {
                            var _CultureModel = _Model.Cultures.Where(m => m.Name.ToLower() == item.Name.ToLower() && m.SiteID == Model.SiteID).SingleOrDefault();
                            if (_CultureModel == null)
                            {
                                _Model.Cultures.Add(new Culture
                                {
                                    Name = item.Name
                                });
                            }
                        }
                    }
                    var _CulturesToDelete = _Model.Cultures.Where(m => !Model.Cultures.Select(m2 => m2.CultureID).Contains(m.CultureID)).ToList();
                    foreach (var item in _CulturesToDelete)
                    {
                        if (!_c.PageCultures.Where(m => m.CultureID == item.CultureID).Any() && !_c.ContentPropertyCultures.Where(m => m.CultureID == item.CultureID).Any())
                            _c.Cultures.Remove(item);
                        else
                            throw new RuleException("Culture: '" + item.Name + "' cannot be deleted because there is content or pages with this culture. You have to delete it first");
                    }
                    //EDIT URLs
                    var _UrlToDelete = _Model.SiteUrls.Where(m => !_SiteUrls.Contains(m.Url)).ToList();
                    _c.SiteUrls.RemoveRange(_UrlToDelete);

                    foreach (var item in _SiteUrls)
                    {
                        if (!_c.SiteUrls.Where(m => m.Url.Equals(item)).Any())
                            _Model.SiteUrls.Add(new SiteUrl
                            {
                                Url = item
                            });
                    }
                }
                _c.SaveChanges();
                return _Model.SiteID;
            }
        }
        public void Delete(int SiteID)
        {
            using (var _c = db)
            {
                var _Model = _c.Sites.Where(m => m.SiteID == SiteID).SingleOrDefault();
                _c.Sites.Remove(_Model);
                _c.SaveChanges();
            }
        }
        public void Enable(SiteEnabledBinding Model)
        {
            using (var _c = db)
            {
                var _Model = _c.Sites.Where(m => m.SiteID == Model.SiteID).SingleOrDefault();
                _Model.Enabled = Model.Enabled;
                _c.SaveChanges();
            }
        }
        public void EnableEcommerce(SiteEnabledBinding Model)
        {
            using (var _c = db)
            {
                var _Model = _c.Sites.Where(m => m.SiteID == Model.SiteID).SingleOrDefault();
                _Model.ECommerce = Model.Enabled;
                _c.SaveChanges();
            }
        }
        public void AddUserToAllRoles(AddUserInSiteBinding Model)
        {
            using (var _c = db)
            {
                var _AU = new UserBLL().Find(Model.Email);
                if (_AU == null)
                {
                    if (string.IsNullOrEmpty(Model.Password))
                        throw new RuleException("User not exist you have to add a Password");
                    if (string.IsNullOrEmpty(Model.Name))
                        throw new RuleException("User not exist you have to add a Name");
                    _AU = new UserBLL().Create(Model.Name, Model.Email, Model.Password);
                }
                var _CU = _c.SiteUsers.Where(m => m.UserID == _AU.Id && m.SiteID == Model.SiteID).SingleOrDefault();
                if (_CU == null)
                {
                    _CU = new SiteUser
                    {
                        SiteID = Model.SiteID,
                        UserID = _AU.Id,
                        SiteUserRoles = new List<SiteUserRol>()
                    };
                    _c.SiteUsers.Add(_CU);
                }
                _c.SiteUserRoles.RemoveRange(_c.SiteUserRoles.Where(m => m.UserID == _AU.Id && m.SiteID == Model.SiteID));
                var _CR1 = new SiteUserRol
                {
                    SiteID = Model.SiteID,
                    UserID = _AU.Id,
                    SiteUserType = SiteUserType.CONTENT
                };
                _c.SiteUserRoles.Add(_CR1);
                _c.SaveChanges();
            }
        }
        public void AddUserToRol(SiteAddRol Model)
        {
            using (var _c = db)
            {
                var _Model = _c.SiteUserRoles.Where(m => m.UserID == Model.UserID && m.SiteID == Model.SiteID
                    && m.SiteUserType == Model.SiteUserType).SingleOrDefault();
                if (_Model == null)
                {
                    if (Model.Enabled)
                    {
                        _Model = new SiteUserRol
                        {
                            SiteID = Model.SiteID,
                            SiteUserType = Model.SiteUserType,
                            UserID = Model.UserID
                        };
                        _c.SiteUserRoles.Add(_Model);
                    }
                }
                else
                {
                    if (!Model.Enabled)
                    {
                        _c.SiteUserRoles.Remove(_Model);
                    }
                }
                _c.SaveChanges();
            }
        }
        public List<SiteListUsers> GetUsers(int SiteID)
        {
            using (var _c = db)
            {
                return (from c in _c.SiteUsers
                        where c.SiteID == SiteID
                        select new SiteListUsers
                        {
                            UserID = c.UserID,
                            Email = c.User.Email,
                            Name = c.User.Name,
                            Roles = c.SiteUserRoles.Select(m => m.SiteUserType)
                        }).ToList();
            }
        }
        public void DeleteUser(string UserID, int SiteID)
        {
            using (var _c = db)
            {
                _c.SiteUsers.Remove(_c.SiteUsers.Where(m => m.SiteID == SiteID && m.UserID == UserID).SingleOrDefault());
                _c.SaveChanges();
            }
        }
        public void EnableBlog(SiteEnabledBinding Model)
        {
            using (var _c = db)
            {
                var _Model = _c.Sites.Where(m => m.SiteID == Model.SiteID).SingleOrDefault();
                _Model.Blog = Model.Enabled;
                _c.SaveChanges();
            }
        }

        #region IMPORT / EXPORT / DUPLICATE
        private SiteExport ExportObject(int SiteID)
        {
            var _Model = new SiteExport();
            _Model.Site = Get(SiteID);
            _Model.Cultures = new CultureBLL().Get(SiteID);
            _Model.Pages = new PageBLL().GetWithCultures(SiteID);
            _Model.Content = new ContentBLL().Export(SiteID);
            return _Model;
        }
        public string Export(int SiteID)
        {
            var _URL = SiteID + "/SITE.json";
            var blob = new Utils.FileUtil().GetBlob("export", _URL);
            using (Stream blobStream = blob.OpenWrite())
            {
                using (StreamWriter writer = new StreamWriter(blobStream))
                {
                    string _JSON = JsonConvert.SerializeObject(ExportObject(SiteID));
                    writer.Write(_JSON);
                }
            }
            return StorageUrl + "/export/" + _URL;
        }
        private int ImportObject(SiteExport SiteContent)
        {
            var _Site = new SiteAllBinding(SiteContent.Site, SiteContent.Cultures);
            _Site.SiteID = 0;
            var _SiteID = Save(_Site);
            ImportSavePages(SiteContent.Pages, _SiteID, 0);
            ImportSaveContent(SiteContent.Content, _SiteID, 0);
            return _SiteID;
        }
        public int Import(FileRead _FileRead)
        {
            byte[] binary = Convert.FromBase64String(_FileRead.FileContent.Split(',')[1]);
            return ImportObject(JsonConvert.DeserializeObject<SiteExport>(Encoding.UTF8.GetString(binary)));
        }
        private void ImportSavePages(ICollection<PageBindingListWithCultures> Pages, int SiteID, int PageParentID)
        {
            foreach (var item in Pages)
            {
                item.SiteID = SiteID;
                item.PageParentID = PageParentID;
                int _PageID = new PageBLL().Save(item);
                if (item.Items.Any())
                    ImportSavePages(item.Items, SiteID, _PageID);
            }
        }
        private void ImportSaveContent(ICollection<ContentBindingList> Content, int SiteID, int ContentParentID)
        {
            foreach (var item in Content)
            {
                item.SiteID = SiteID;
                item.ContentPropertyParentID = ContentParentID;
                int _ContentID = new ContentBLL().Save(item);
                if (item.Items.Any())
                    ImportSaveContent(item.Items, SiteID, _ContentID);
            }
        }
        public int Duplicate(int SiteID)
        {
            return Duplicate(SiteID, null);
        }
        public int Duplicate(int SiteID, string PrefixNewSite)
        {
            var _Obj = ExportObject(SiteID);
            if (!string.IsNullOrEmpty(PrefixNewSite))
                _Obj.Site.SiteName = PrefixNewSite + _Obj.Site.SiteName;
            return ImportObject(_Obj);
        }
        #endregion
        #endregion

        #region TEST
        public void RemoveSites(string Prefix)
        {
            using (var _c = db)
            {
               // _c.BlogPostCultures.RemoveRange(_c.BlogPostCultures.Where(m => m.BlogPost.Site.SiteName.Contains(Prefix)).ToList());
                _c.BlogCategories.RemoveRange(_c.BlogCategories.Where(m => m.Site.SiteName.Contains(Prefix)).ToList());
                _c.Products.RemoveRange(_c.Products.Where(m => m.Site.SiteName.Contains(Prefix)).ToList());
                _c.Sites.RemoveRange(_c.Sites.Where(m => m.SiteName.Contains(Prefix)).ToList());
                _c.SaveChanges();
            }
        }

        #endregion
    }
}
