using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data;
using thewall9.data.binding;
using thewall9.bll.Utils;
using thewall9.data.Models;
namespace thewall9.bll
{
    public class BlogBLL : BaseBLL
    {
        #region WEB
        private List<BlogCategoryCultureBase> GetCategoriesUsed(int SiteID, string Lang)
        {
            using (var _c = db)
            {
                var _CultureID = new CultureBLL().GetByName(SiteID, Lang).CultureID;
                return GetCategoriesUsed(SiteID, _CultureID);
            }
        }
        private List<BlogCategoryCultureBase> GetCategoriesUsed(int SiteID, int CultureID)
        {
            using (var _c = db)
            {
                return (from bpc in _c.BlogPostCategories
                        join bc in _c.BlogCategories on bpc.BlogCategoryID equals bc.BlogCategoryID
                        join bcc in _c.BlogCategoryCultures on bc.BlogCategoryID equals bcc.BlogCategoryID
                        where bcc.CultureID == CultureID && bc.SiteID == SiteID
                        select new BlogCategoryCultureBase
                        {
                            BlogCategoryName = bcc.BlogCategoryName,
                            FriendlyUrl = bcc.FriendlyUrl,
                            BlogCategoryID = bcc.BlogCategoryID
                        }).Distinct()
                        .ToList();
            }
        }
        public BlogListWeb Get(int SiteID
            , string Url
            , string Lang
            , string FriendlyUrl
            , int Page)
        {
            var _Take = 10;//SHOULD BE CUSTOM BY USER
            using (var _c = db)
            {
                if (SiteID == 0)
                    SiteID = new SiteBLL().Get(Url, _c).SiteID;

                var _Query = from bpc in _c.BlogPostCultures
                             where bpc.BlogPost.SiteID == SiteID
                             && bpc.Culture.Name.ToLower().Equals(Lang.ToLower())
                             && bpc.Published
                             select bpc;
                if (!string.IsNullOrEmpty(FriendlyUrl))
                {
                    _Query = from q in _Query
                             join c in _c.BlogPostCategories on q.BlogPost.BlogPostID equals c.BlogPostID
                             join cc in _c.BlogCategoryCultures on c.BlogCategoryID equals cc.BlogCategoryID
                             where cc.FriendlyUrl.Equals(FriendlyUrl)
                             select q;
                }
                var _Model = new BlogListWeb();
                var _Count = _Query.Count();
                _Model.Pages = _Count % _Take == 0 ? _Count / _Take : (_Count / _Take) + 1;
                _Model.Data = _Query.OrderByDescending(m => m.DateCreated).Select(m => new BlogPostWeb
                {
                    BlogPostID = m.BlogPostID,
                    FriendlyUrl = m.FriendlyUrl,
                    Title = m.Title
                })
                .Skip(_Take * (Page - 1)).Take(_Take).ToList();

                _Model.Categories = GetCategoriesUsed(SiteID, Lang);
                return _Model;
            }
        }

        public BlogPostWeb GetDetail(int BlogPostID, string FriendlyUrl)
        {
            using (var _c = db)
            {
                var _Model = (from bpc in _c.BlogPostCultures
                              where (bpc.BlogPostID == BlogPostID && bpc.FriendlyUrl == FriendlyUrl)
                              select new BlogPostWeb
                              {
                                  Title = bpc.Title,
                                  Content = bpc.Content,
                                  Published = bpc.Published,
                                  FriendlyUrl = bpc.FriendlyUrl,
                                  BlogPostID = bpc.BlogPostID,
                                  CultureID = bpc.CultureID,
                                  SiteID = bpc.BlogPost.SiteID,
                                  FeatureImageUrl = bpc.BlogPostFeatureImage != null ? bpc.BlogPostFeatureImage.Media.MediaUrl : null
                              }).FirstOrDefault();
                _Model.Categories = GetCategoriesUsed(_Model.SiteID, _Model.CultureID);
                return _Model;
            }
        }
        #endregion

        #region CUSTOMER
        //POST
        public List<BlogPostListBinding> Get(int SiteID, int CultureID)
        {
            using (var _c = db)
            {
                var _BP = from bp in _c.BlogPosts
                          where bp.SiteID == SiteID
                          select new BlogPostListBinding
                          {
                              BlogPostID = bp.BlogPostID,
                              DateCreated = bp.DateCreated,
                              SiteID = bp.SiteID,

                              CultureInfo = bp.BlogPostCultures
                              .Where(m => m.CultureID == CultureID)
                              .Select(m => new BlogPostListCultureBinding
                              {
                                  Title = m.Title,
                                  Published = m.Published
                              })
                              .FirstOrDefault()
                          };
                return _BP.ToList();
            }
        }
        public BlogPostModelBinding GetDetail(int BlogPostID, int CultureID)
        {
            using (var _c = db)
            {
                var _BP = from bpc in _c.BlogPostCultures
                          where (bpc.BlogPostID == BlogPostID && bpc.CultureID == CultureID)
                          select new BlogPostModelBinding
                          {
                              Title = bpc.Title,
                              Content = bpc.Content,
                              ContentPreview = bpc.ContentPreview,
                              Published = bpc.Published,
                              FriendlyUrl = bpc.FriendlyUrl,
                              BlogPostID = bpc.BlogPostID,
                              CultureID = bpc.CultureID,

                              Tags = bpc.BlogPostTags.Select(m => new BlogTagModelBinding
                              {
                                  BlogTagName = m.BlogTag.BlogTagName,
                                  BlogTagID = m.BlogTagID
                              }).ToList(),

                              Categories = bpc.BlogPost.BlogPostCategories
                              .Select(m => new BlogPostCategorieModelBinding
                              {
                                  BlogCategoryID = m.BlogCategoryID,
                              }).ToList(),

                              FeatureImageFileRead = bpc.BlogPostFeatureImage != null
                              ? new FileRead
                              {
                                  MediaUrl = bpc.BlogPostFeatureImage.Media.MediaUrl,
                                  MediaID = bpc.BlogPostFeatureImage.MediaID
                              } : null,

                              ImagesFileRead = bpc.BlogPostImages.Select(m => new FileRead
                              {
                                  MediaUrl = m.Media.MediaUrl,
                                  MediaID = m.MediaID
                              }).ToList()
                          };
                return _BP.FirstOrDefault();
            }
        }

        public int Save(BlogPostModelBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                var _BlogPost = new BlogPost(Model.SiteID);
                if (Model.BlogPostID == 0)
                {
                    Model.FriendlyUrl = Model.Title.CleanUrl();
                    var _BlogPostCulture = new BlogPostCulture(Model);
                    //ADD TAGS
                    if (Model.Tags != null)
                    {
                        _BlogPostCulture.BlogPostTags = new List<BlogPostTag>();
                        foreach (var item in Model.Tags)
                        {
                            var _BTID = GetTagID(item.BlogTagName);
                            _BlogPostCulture.BlogPostTags.Add(new BlogPostTag
                            {
                                BlogTagID = GetTagID(item.BlogTagName),
                                CultureID = Model.CultureID
                            });
                        }
                    }

                    _BlogPost.BlogPostCultures = new List<BlogPostCulture>();
                    _BlogPostCulture.ContentPreview = Utils.Util.GetPlainTextFromHtml(Model.Content);
                    _BlogPost.BlogPostCultures.Add(_BlogPostCulture);
                    _c.BlogPosts.Add(_BlogPost);

                    //CATEGORIES
                    if (Model.Categories != null)
                    {
                        _BlogPost.BlogPostCategories = new List<BlogPostCategory>();
                        foreach (var item in Model.Categories)
                        {
                            _BlogPost.BlogPostCategories.Add(new BlogPostCategory
                            {
                                BlogCategoryID = item.BlogCategoryID
                            });
                        }
                    }
                    _c.SaveChanges();
                    return _BlogPost.BlogPostID;
                }
                else
                {
                    var _BP = _c.BlogPostCultures
                        .Where(m => m.CultureID == Model.CultureID && m.BlogPostID == Model.BlogPostID)
                        .FirstOrDefault();
                    _BP.UpdateContent(Model);
                    _BP.ContentPreview = Utils.Util.GetPlainTextFromHtml(Model.Content);

                    //CATEGORIES
                    if (Model.Categories != null)
                    {
                        var _C = Model.Categories
                            .Where(m => m.Adding)
                            .Select(m => new BlogPostCategory
                            {
                                BlogCategoryID = m.BlogCategoryID,
                                BlogPostID = Model.BlogPostID
                            });
                        foreach (var item in _C.ToList())
                        {
                            if (!_c.BlogPostCategories
                                .Where(m => m.BlogCategoryID == item.BlogCategoryID
                                && m.BlogPostID == item.BlogPostID).Any())
                            {
                                _BP.BlogPost.BlogPostCategories.Add(item);
                            }
                        }
                        var _CToDelete = Model.Categories
                            .Where(m => m.Deleting).Select(m => m.BlogCategoryID);
                        _c.BlogPostCategories.RemoveRange(_c.BlogPostCategories
                            .Where(m => _CToDelete.Contains(m.BlogCategoryID))
                            .ToList());
                    }
                    //ADD TAGS
                    if (Model.Tags != null)
                    {
                        foreach (var item in Model.Tags)
                        {
                            var _BTID = GetTagID(item.BlogTagName);

                            var _BPT = _c.BlogPostTags
                                .Where(m => m.BlogTagID == _BTID
                            && m.BlogPostID == Model.BlogPostID
                            && m.CultureID == Model.CultureID)
                            .FirstOrDefault();

                            if (item.Adding && _BPT == null)
                            {
                                _BPT = new BlogPostTag(item, _BTID, Model.CultureID);
                                _BP.BlogPostTags.Add(_BPT);
                            }
                            else if (item.Deleting && _BPT != null)
                            {
                                _c.BlogPostTags.Remove(_BPT);
                            }

                        }
                    }
                    _c.SaveChanges();

                    int _BlogPostID = Model.BlogPostID != 0 ? Model.BlogPostID : _BlogPost.BlogPostID;

                    //ADD FEATURE IMAGE
                    if (Model.FeatureImageFileRead != null)
                    {
                        if (Model.FeatureImageFileRead.Deleting)
                        {
                            if (Model.FeatureImageFileRead.MediaID != 0)
                            {
                                new MediaBLL().DeleteMedia(Model.FeatureImageFileRead.MediaID, Model.SiteID, UserID);
                            }
                        }
                        else
                        {
                            if (Model.FeatureImageFileRead != null && !string.IsNullOrEmpty(Model.FeatureImageFileRead.FileContent))
                            {
                                new MediaBLL().DeleteMedia(Model.FeatureImageFileRead.MediaID, Model.SiteID, UserID);
                                var _Media = new MediaBLL().SaveImage(Model.FeatureImageFileRead, Model.SiteID, UserID);
                                _c.BlogPostFeatureImages.Add(new BlogPostFeatureImage
                                {
                                    BlogPostID = _BlogPostID,
                                    CultureID = Model.CultureID,
                                    MediaID = _Media.MediaID
                                });
                                _c.SaveChanges();
                            }
                        }
                    }
                    //ADD IMAGES
                    if (Model.ImagesFileRead != null)
                    {
                        foreach (var item in Model.ImagesFileRead)
                        {
                            var _BPI = _c.BlogPostImages
                                .Where(m => m.BlogPostID == _BlogPostID
                                && m.CultureID == Model.CultureID
                                && m.MediaID == item.MediaID).FirstOrDefault();
                            if (item.Adding && _BPI == null)
                            {
                                _c.BlogPostImages.Add(new BlogPostImages
                                {
                                    BlogPostID = _BlogPostID,
                                    CultureID = Model.CultureID,
                                    MediaID = item.MediaID
                                });
                            }
                            else if (item.Deleting && _BPI != null)
                            {
                                new MediaBLL().DeleteMedia(item.MediaID, Model.SiteID, UserID);
                            }
                        }
                        _c.SaveChanges();
                    }
                    return _BP.BlogPostID;
                }
            }
        }
        public void Delete(int BlogPostID, string UserID)
        {
            using (var _c = db)
            {
                var _BP = _c.BlogPosts
                    .Where(m => m.BlogPostID == BlogPostID)
                    .FirstOrDefault();
                Can(_BP.SiteID, UserID, _c);
                _c.BlogPosts.Remove(_BP);

                //DELETE ALL MEDIA
                var _M = _c.BlogPostFeatureImages.Where(m => m.BlogPostID == BlogPostID).FirstOrDefault();
                if (_M != null)
                    new MediaBLL().DeleteMedia(_M.MediaID, _BP.SiteID, UserID);

                var _Images = _c.BlogPostImages.Where(m => m.BlogPostID == BlogPostID).ToList();
                foreach (var item in _Images)
                {
                    new MediaBLL().DeleteMedia(item.MediaID, _BP.SiteID, UserID);
                }

                _c.SaveChanges();
            }
        }

        //CATEGORY
        public List<BlogCategoryListBinding> GetCategories(int SiteID, int CultureID)
        {
            using (var _c = db)
            {
                var _BP = from bp in _c.BlogCategories
                          where (bp.SiteID == SiteID)
                          select new BlogCategoryListBinding
                          {
                              BlogCategoryID = bp.BlogCategoryID,

                              BlogCategoryName = bp.BlogCategoryCultures
                              .Where(m => m.CultureID == CultureID)
                              .Select(m => m.BlogCategoryName)
                              .FirstOrDefault(),

                              CategoryCultures = bp.BlogCategoryCultures
                              .Select(m => new BlogCategoryCultureBinding
                              {
                                  BlogCategoryName = m.BlogCategoryName,
                                  BlogCategoryID = m.BlogCategoryID,
                                  CultureName = m.Culture.Name,
                                  CultureID = m.CultureID,
                                  FriendlyUrl = m.FriendlyUrl
                              })
                              .ToList()
                          };
                return _BP.ToList();
            }
        }
        public int SaveCategory(BlogCategoryModelBinding Model, string UserID)
        {
            using (var _c = db)
            {
                Can(Model.SiteID, UserID, _c);
                var _C = new BlogCategory
                {
                    SiteID = Model.SiteID
                };
                if (Model.BlogCategoryID == 0)
                {
                    _C.BlogCategoryCultures = new List<BlogCategoryCulture>();
                }
                else
                {
                    _C = _c.BlogCategories.Where(m => m.BlogCategoryID == Model.BlogCategoryID)
                        .FirstOrDefault();
                }
                foreach (var item in Model.CategoryCultures)
                {
                    if (string.IsNullOrEmpty(item.FriendlyUrl))
                    {
                        item.FriendlyUrl = item.BlogCategoryName.CleanUrl();
                    }
                    if (Model.BlogCategoryID == 0)
                    {
                        _C.BlogCategoryCultures.Add(new BlogCategoryCulture
                        {
                            BlogCategoryName = item.BlogCategoryName,
                            CultureID = item.CultureID,
                            FriendlyUrl = item.FriendlyUrl
                        });
                        _c.BlogCategories.Add(_C);
                    }
                    else
                    {
                        var _BCC = _c.BlogCategoryCultures
                            .Where(m => m.BlogCategoryID == Model.BlogCategoryID
                                && m.CultureID == item.CultureID).FirstOrDefault();
                        if (_BCC != null)
                        {
                            _BCC.BlogCategoryName = item.BlogCategoryName;
                            _BCC.FriendlyUrl = item.FriendlyUrl;
                        }
                        else
                        {
                            _BCC = new BlogCategoryCulture
                            {
                                BlogCategoryID = Model.BlogCategoryID,
                                BlogCategoryName = item.BlogCategoryName,
                                CultureID = item.CultureID,
                                FriendlyUrl = item.FriendlyUrl
                            };
                            _C.BlogCategoryCultures.Add(_BCC);
                        }
                    }
                }
                _c.SaveChanges();
                return _C.BlogCategoryID;
            }
        }
        public void DeleteCategory(int BlogCategoryID, string UserID)
        {
            using (var _c = db)
            {
                var _BP = _c.BlogCategories
                    .Where(m => m.BlogCategoryID == BlogCategoryID)
                    .FirstOrDefault();
                Can(_BP.SiteID, UserID, _c);
                _c.BlogCategories.Remove(_BP);
                _c.SaveChanges();
            }
        }

        //TAGS
        private int GetTagID(string BlogTagName)
        {
            using (var _c = db)
            {
                var _BT = _c.BlogTags
                                .Where(m => m.BlogTagName.ToLower().Equals(BlogTagName.ToLower()))
                                .FirstOrDefault();
                if (_BT == null)
                {
                    _BT = new BlogTag(BlogTagName);
                    _c.BlogTags.Add(_BT);
                    _c.SaveChanges();
                }
                return _BT.BlogTagID;
            }
        }
        public List<BlogTagModelBinding> GetTags(string Query)
        {
            using (var _c = db)
            {
                return _c.BlogTags
                    .Where(m => m.BlogTagName.ToLower().Contains(Query))
                    .Select(m => new BlogTagModelBinding
                    {
                        BlogTagID = m.BlogTagID,
                        BlogTagName = m.BlogTagName,
                        Adding = true,
                        Deleting = false
                    })
                    .Take(20)
                    .ToList();
            }
        }
        #endregion
    }
}
