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
                                  Title = m.Title
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
                              Published = bpc.Published,
                              FriendlyUrl = bpc.FriendlyUrl,
                              BlogPostID = bpc.BlogPostID,
                              CultureID = bpc.CultureID,
                              Tags = bpc.BlogPostTags.Select(m => new BlogTagModelBinding
                              {
                                  BlogTagName = m.BlogTag.BlogTagName,
                                  BlogTagID=m.BlogTagID
                              }).ToList(),
                              Categories = bpc.BlogPost.BlogPostCategories.Select(m => new BlogPostCategorieModelBinding
                              {
                                  BlogCategoryID = m.BlogCategoryID
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
                if (Model.BlogPostID == 0)
                {
                    var _BlogPost = new BlogPost(Model.SiteID);

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

                    //CATEGORIES
                    if (Model.Categories != null)
                    {
                        var _C = Model.Categories
                            .Where(m => m.Adding)
                            .Select(m => new BlogPostCategory
                            {
                                BlogCategoryID = m.BlogCategoryID,
                                BlogPostID=Model.BlogPostID
                            });
                        foreach (var item in _C.ToList())
                        {
                            if(!_c.BlogPostCategories
                                .Where(m=>m.BlogCategoryID==item.BlogCategoryID
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

                              CultureInfo = bp.BlogCategoryCultures
                              .Where(m => m.CultureID == CultureID)
                              .Select(m => new BlogCategoryCultureBase
                              {
                                  BlogCategoryName = m.BlogCategoryName
                              })
                              .FirstOrDefault()
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
                foreach (var item in Model.Categories)
                {
                    if (Model.BlogCategoryID == 0)
                    {
                        _C.BlogCategoryCultures.Add(new BlogCategoryCulture
                        {
                            BlogCategoryName = item.BlogCategoryName,
                            CultureID = item.CultureID
                        });
                        _c.BlogCategories.Add(_C);
                    }
                    else
                    {
                        var _BCC = _c.BlogCategoryCultures
                            .Where(m => m.BlogCategoryID == Model.BlogCategoryID
                                && m.CultureID == item.CultureID).FirstOrDefault();
                        if (_BCC != null)
                            _BCC.BlogCategoryName = item.BlogCategoryName;
                        else
                        {
                            _BCC = new BlogCategoryCulture
                            {
                                BlogCategoryID = Model.BlogCategoryID,
                                BlogCategoryName = item.BlogCategoryName,
                                CultureID = item.CultureID
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
                        Adding=true,
                        Deleting=false
                    })
                    .Take(20)
                    .ToList();
            }
        }
    }
}
