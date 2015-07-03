﻿using System;
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
            var _Categories=new List<BlogCategoryCultureBase>();
            _Categories.Add(new BlogCategoryCultureBase{
                BlogCategoryName="c1-es",
                CultureID=_Cultures[0].CultureID,
            });
            _Categories.Add(new BlogCategoryCultureBase{
                BlogCategoryName="c1-en",
                CultureID=_Cultures[1].CultureID
            });
            _Category=new BlogCategoryModelBinding
            {
                SiteID = _SiteID,
                Categories=_Categories
            };
            _Category.BlogCategoryID=new BlogBLL().SaveCategory(_Category, _CustomerUser.Id);

            //CREATING TAGS
            var _Tags = new List<BlogTagModelBinding>();
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName="t1"
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
                Title="b1",
                Tags=_Tags
            };
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            Assert.IsTrue(_BlogPost.BlogPostID != 0);

            _BlogPost2 = new BlogPostModelBinding
            {
                SiteID = _SiteID,
                CultureID = _Cultures[0].CultureID,
                Title="b2"
            };
            _BlogPost2.BlogPostID = new BlogBLL().Save(_BlogPost2, _CustomerUser.Id);
            Assert.IsTrue(_BlogPost2.BlogPostID != 0);

            _BlogPost3 = new BlogPostModelBinding
            {
                SiteID = _SiteID,
                CultureID = _Cultures[1].CultureID,
                Title = "b3"
            };
            _BlogPost3.BlogPostID = new BlogBLL().Save(_BlogPost3, _CustomerUser.Id);
            Assert.IsTrue(_BlogPost3.BlogPostID != 0);
        }

        #region POST
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
            new BlogBLL().Delete(_BlogPost3.BlogPostID,_CustomerUser.Id);
            var _Post = new BlogBLL().GetDetail(_BlogPost3.BlogPostID, _BlogPost3.CultureID);
            Assert.IsNull(_Post);
        }
        #endregion

        #region CATEGORIES
        [TestMethod]
        public void BlogCategoryCreateTest()
        {
            SettingUp();
            var _C=new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_C.Count == 1);
        }
        [TestMethod]
        public void BlogCategoryUpdatingTest()
        {
            SettingUp();
            _Category.Categories[0].BlogCategoryName = "newCAT";
            new BlogBLL().SaveCategory(_Category, _CustomerUser.Id);
            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.AreEqual(_Category.Categories[0].BlogCategoryName ,_C[0].CultureInfo.BlogCategoryName);
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
                Categories = _Categories
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
                Categories = _Categories2,
                BlogCategoryID = _Category1.BlogCategoryID
            };
            _Category2.BlogCategoryID = new BlogBLL().SaveCategory(_Category2, _CustomerUser.Id);

            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.AreEqual(_Category1.Categories[0].BlogCategoryName, _C[1].CultureInfo.BlogCategoryName);
            var _C2 = new BlogBLL().GetCategories(_SiteID, _Cultures[1].CultureID);
            Assert.AreEqual(_Category2.Categories[0].BlogCategoryName, _C2[1].CultureInfo.BlogCategoryName);
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
                Categories = _Categories
            };
            _Category1.BlogCategoryID = new BlogBLL().SaveCategory(_Category1, _CustomerUser.Id);

            var _C = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_C.Count == 2);
            Assert.IsNotNull(_C[0].CultureInfo);
            Assert.IsNotNull(_C[1].CultureInfo);
            _C = new BlogBLL().GetCategories(_SiteID, _Cultures[1].CultureID);
            Assert.IsNotNull(_C[0].CultureInfo);
            Assert.IsNull(_C[1].CultureInfo);
            Assert.IsTrue(_C.Count == 2);
        }
        [TestMethod]
        public void BlogCategoryRemoveTest()
        {
            SettingUp();
            new BlogBLL().DeleteCategory(_Category.BlogCategoryID, _CustomerUser.Id);
            var _Cs = new BlogBLL().GetCategories(_SiteID, _Cultures[0].CultureID);
            Assert.IsTrue(_Cs.Count==0);
        }
        #endregion

        #region TAGS
        [TestMethod]
        public void BlogTagCreateTest()
        {
            SettingUp();
            var _B = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _Cultures[0].CultureID);
            Assert.AreEqual(2, _B.Tags.Count);
            Assert.AreEqual(_BlogPost.Tags[0].BlogTagName,_B.Tags[0].BlogTagName);
        }
        [TestMethod]
        public void BlogTagUpdateTest()
        {
            SettingUp();
            //CREATING TAGS
            var _Tags = new List<BlogTagModelBinding>();
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName = "t3",
                Adding=true
            });
            _Tags.Add(new BlogTagModelBinding
            {
                BlogTagName = "t2",
                Deleting=true
            });
            _BlogPost.Tags = _Tags;
            _BlogPost.BlogPostID = new BlogBLL().Save(_BlogPost, _CustomerUser.Id);
            var _B = new BlogBLL().GetDetail(_BlogPost.BlogPostID, _Cultures[0].CultureID);
            Assert.AreEqual(2, _B.Tags.Count);
            Assert.AreEqual("t1", _B.Tags[0].BlogTagName);
            Assert.AreEqual(_Tags[0].BlogTagName, _B.Tags[1].BlogTagName);
        }
        #endregion
    }
}
