using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using thewall9.data.binding;
using System.Collections.Generic;
using thewall9.bll.Exceptions;
namespace thewall9.bll.test
{
    [TestClass]
    public class BlogBLLTest : BaseTest
    {
        public BlogPostModelBinding _BlogPost { get; set; }
        public BlogPostModelBinding _BlogPost2 { get; set; }
        public BlogPostModelBinding _BlogPost3 { get; set; }
        public BlogCategoryModelBinding _Category { get; set; }
        private void SettingUp()
        {
            //CREATING CATEGORIES
            var _Categories = new List<BlogCategoryCultureBase>();
            _Categories.Add(new BlogCategoryCultureBase
            {
                BlogCategoryName = "c1-es",
                CultureID = _Cultures[0].CultureID,
            });
            _Categories.Add(new BlogCategoryCultureBase
            {
                BlogCategoryName = "c1-en",
                CultureID = _Cultures[1].CultureID
            });
            _Category = new BlogCategoryModelBinding
            {
                SiteID = _SiteID,
                CategoryCultures = _Categories
            };
            _Category.BlogCategoryID = new BlogBLL().SaveCategory(_Category, _CustomerUser.Id);

            //CREATING TAGS
            var _Tags = new List<BlogTagModelBinding>();
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName = "t1"
            });
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName = "t2"
            });
            //CREATING POSTS
            _BlogPost = new BlogPostModelBinding
            {
                SiteID = _SiteID,
                CultureID = _Cultures[0].CultureID,
                Title = "b1",
                Tags = _Tags,
                Published = true
            };
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            Assert.IsTrue(_BlogPost.BlogPostID != 0);

            _BlogPost2 = new BlogPostModelBinding
            {
                SiteID = _SiteID,
                CultureID = _Cultures[0].CultureID,
                Title = "b2",
                Published = true
            };
            _BlogPost2.BlogPostID = new BlogBLL().Save(_BlogPost2, _CustomerUser.Id);
            Assert.IsTrue(_BlogPost2.BlogPostID != 0);

            _BlogPost3 = new BlogPostModelBinding
            {
                SiteID = _SiteID,
                CultureID = _Cultures[1].CultureID,
                Title = "b3",
                Published = true,
                FriendlyUrl="b3"
            };
            _BlogPost3.BlogPostID = new BlogBLL().Save(_BlogPost3, _CustomerUser.Id);
            Assert.IsTrue(_BlogPost3.BlogPostID != 0);
        }

        #region WEB
        [TestMethod]
        public void BlogWebGetTest()
        {
            SettingUp();
            var _Posts = new BlogBLL().Get(_SiteID, null, _Cultures[0].Name, null,null, 1);
            Assert.IsTrue(_Posts.Pages == 1);
            Assert.IsTrue(_Posts.Data.Count == 2);
        }
        [TestMethod]
        public void BlogWebGetByLangWithNoPostTest()
        {
            SettingUp();
            var _Posts = new BlogBLL().Get(_SiteID, null, _Cultures[1].Name, null,null, 1);
            Assert.IsTrue(_Posts.Pages == 1);
            Assert.IsTrue(_Posts.Data.Count == 3);
        }
        [TestMethod]
        public void BlogWebGetDetailTest()
        {
            SettingUp();
            var _Posts = new BlogBLL().GetDetail(_BlogPost3.BlogPostID, _BlogPost3.FriendlyUrl);
            Assert.IsNotNull(_Posts);
            _Posts = new BlogBLL().GetDetail(_BlogPost3.BlogPostID, "otra-cosa-loca");
            Assert.IsNotNull(_Posts);
        }
        #endregion
        #region POST
       
        [TestMethod]
        public void BlogSaveFeatureImageTest()
        {
            SettingUp();
            _BlogPost.FeatureImageFileRead = GetImgFileRead();
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            var _Post = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _BlogPost.CultureID);
            Assert.IsNotNull(_Post.FeatureImageFileRead.MediaUrl);
        }
        [TestMethod]
        public void BlogDeleteFeatureImageTest()
        {
            SettingUp();
            _BlogPost.FeatureImageFileRead = GetImgFileRead();
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            var _Post = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _BlogPost.CultureID);
            _BlogPost.FeatureImageFileRead.MediaID = _Post.FeatureImageFileRead.MediaID;
            _BlogPost.FeatureImageFileRead.Deleting = true;
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            _Post = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _BlogPost.CultureID);
            Assert.IsNull(_Post.FeatureImageFileRead);
        }
        [TestMethod]
        public void BlogUpdateTest()
        {
            SettingUp();
            _BlogPost3.Title = "newb3";
            _BlogPost3.Content = "<h1>hola mundo</h1>";
            var _PostID = new BlogBLL().Save(_BlogPost3, _CustomerUser.Id);
            Assert.AreEqual(_BlogPost3.BlogPostID, _PostID);
            var _Post = new BlogBLL().GetDetail(_PostID, _BlogPost3.CultureID);
            Assert.AreEqual(_BlogPost3.Title, _Post.Title);
        }
        [TestMethod]
        public void BlogGetTest()
        {
            SettingUp();
            var _Posts = new BlogBLL().Get(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_Posts.Count == 3);
            Assert.AreEqual(_BlogPost.Title, _Posts[0].CultureInfo.Title);
            Assert.AreEqual(_BlogPost2.Title, _Posts[1].CultureInfo.Title);
        }
        [TestMethod]
        public void BlogPostRemoveTest()
        {
            SettingUp();
            new BlogBLL().Delete(_BlogPost3.BlogPostID, _CustomerUser.Id);
            var _Post = new BlogBLL().GetDetail(_BlogPost3.BlogPostID, _BlogPost3.CultureID);
            Assert.IsNull(_Post);
        }
        [TestMethod]
        public void BlogPostSaveContentPreviewTest()
        {
            SettingUp();
            _BlogPost.Content = @"
 <a href='http://meta.stackoverflow.com'
       class='site-link js-gps-track'
       data-id='552'
       data-gps-track='
            site.switch({ target_site:552, item_type:3 }),
        site_switcher.click({ item_type:4 })'>
        <div class='site-icon favicon favicon-stackoverflowmeta' title='Meta Stack Overflow'></div>
        Meta Stack Overflow
    </a>

                </li>
                            <li class='related-site'>
                        <div class='L-shaped-icon-container'>
        <span class='L-shaped-icon'></span>
    </div>

                    <a class='site-link js-gps-track'
                       href='//careers.stackoverflow.com?utm_source=stackoverflow.com&amp;utm_medium=site-ui&amp;utm_campaign=multicollider'
                            data-gps-track='site_switcher.click({ item_type:9 })'
>
                        <div class='site-icon favicon favicon-careers' title='Stack Overflow Careers'></div>
                        Stack Overflow Careers
                    </a>
                </li>
        </ul>
    </div>
    
    <div class='header' id='your-communities-header'>
        <h3>
                <a href='//stackexchange.com/users/192027/?tab=accounts'>your communities</a>
        </h3>
            
            <a href='#' id='edit-pinned-sites'>edit</a>
            <a href='#' id='cancel-pinned-sites' style='display: none;'>cancel</a>
    </div>
";
            var _PostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            var _Post = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _BlogPost.CultureID);
            Assert.IsNotNull( _Post.ContentPreview);
        }
        #endregion

        #region CATEGORIES
        [TestMethod]
        public void BlogCategoryCreateTest()
        {
            SettingUp();
            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_C.Count == 1);
        }
        [TestMethod]
        public void BlogCategoryUpdatingTest()
        {
            SettingUp();
            _Category.CategoryCultures[0].BlogCategoryName = "newCAT";
            new BlogBLL().SaveCategory(_Category, _CustomerUser.Id);
            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.AreEqual(_Category.CategoryCultures[0].BlogCategoryName, _C[0].BlogCategoryName);
        }
        [TestMethod]
        public void BlogCategoryUpdatingNewLangTest()
        {
            SettingUp();
            var _Categories = new List<BlogCategoryCultureBase>();
            _Categories.Add(new BlogCategoryCultureBase
            {
                BlogCategoryName = "c2-es",
                CultureID = _Cultures[0].CultureID,
            });
            var _Category1 = new BlogCategoryModelBinding
            {
                SiteID = _SiteID,
                CategoryCultures = _Categories
            };
            _Category1.BlogCategoryID = new BlogBLL().SaveCategory(_Category1, _CustomerUser.Id);

            var _Categories2 = new List<BlogCategoryCultureBase>();
            _Categories2.Add(new BlogCategoryCultureBase
            {
                BlogCategoryName = "c2-en",
                CultureID = _Cultures[1].CultureID,
            });
            var _Category2 = new BlogCategoryModelBinding
            {
                SiteID = _SiteID,
                CategoryCultures = _Categories2,
                BlogCategoryID = _Category1.BlogCategoryID
            };
            _Category2.BlogCategoryID = new BlogBLL().SaveCategory(_Category2, _CustomerUser.Id);

            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.AreEqual(_Category1.CategoryCultures[0].BlogCategoryName, _C[1].BlogCategoryName);
            var _C2 = new BlogBLL().GetCategories(_SiteID, _Cultures[1].CultureID);
            Assert.AreEqual(_Category2.CategoryCultures[0].BlogCategoryName, _C2[1].BlogCategoryName);
        }
        [TestMethod]
        public void BlogCategoryGetOneLangTest()
        {
            SettingUp();
            var _Categories = new List<BlogCategoryCultureBase>();
            _Categories.Add(new BlogCategoryCultureBase
            {
                BlogCategoryName = "c2-es",
                CultureID = _Cultures[0].CultureID,
            });
            var _Category1 = new BlogCategoryModelBinding
            {
                SiteID = _SiteID,
                CategoryCultures = _Categories
            };
            _Category1.BlogCategoryID = new BlogBLL().SaveCategory(_Category1, _CustomerUser.Id);

            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_C.Count == 2);
            Assert.IsNotNull(_C[0].CategoryCultures);
            Assert.IsNotNull(_C[1].CategoryCultures);
            _C = new BlogBLL().GetCategories(_SiteID, _Cultures[1].CultureID);
            Assert.IsNotNull(_C[0].CategoryCultures);
            Assert.IsTrue(_C.Count == 2);
        }
        [TestMethod]
        public void BlogCategoryRemoveTest()
        {
            SettingUp();
            new BlogBLL().DeleteCategory(_Category.BlogCategoryID, _CustomerUser.Id);
            var _Cs = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_Cs.Count == 0);
        }
        [TestMethod]
        public void BlogCategoryAddingExistingTest()
        {
            SettingUp();
            _BlogPost.Categories = new List<BlogPostCategorieModelBinding>();
            _BlogPost.Categories.Add(new BlogPostCategorieModelBinding
            {
                BlogCategoryID = _Category.BlogCategoryID,
                Adding = true
            });
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            Assert.IsNotNull(_BlogPost.BlogPostID);
        }
        #endregion

        #region TAGS
        [TestMethod]
        public void BlogTagCreateTest()
        {
            SettingUp();
            var _B = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _Cultures[0].CultureID);
            Assert.AreEqual(2, _B.Tags.Count);
            Assert.AreEqual(_BlogPost.Tags[0].BlogTagName, _B.Tags[0].BlogTagName);
        }
        [TestMethod]
        public void BlogTagUpdateTest()
        {
            SettingUp();
            //CREATING TAGS
            var _Tags = new List<BlogTagModelBinding>();
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName = "t3 canción",
                Adding = true
            });
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName = "t2",
                Deleting = true
            });
            _BlogPost.Tags = _Tags;
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            var _B = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _Cultures[0].CultureID);
            Assert.AreEqual(2, _B.Tags.Count);
            Assert.AreEqual("t1", _B.Tags[0].BlogTagName);
            Assert.AreEqual("t3-cancion", _B.Tags[1].BlogTagName);
        }
        #endregion
    }
}
