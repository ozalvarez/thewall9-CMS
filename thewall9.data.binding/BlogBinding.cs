﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    #region Base
    //POST
    public class BlogPostBase
    {
        public int BlogPostID { get; set; }
        public int SiteID { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class BlogPostCultureBase
    {
        public int BlogPostID { get; set; }
        public int CultureID { get; set; }
        public bool Published { get; set; }
        public string Content { get; set; }
        public string FriendlyUrl { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
    }
    //TAG
    public class BlogTagBase
    {
        public int BlogTagID { get; set; }
        public string BlogTagName { get; set; }
    }
    public class BlogPostTagBase
    {
        public int BlogTagID { get; set; }
        public int CultureID { get; set; }
        public int BlogPostID { get; set; }
    }
    //CATEGORY
    public class BlogCategoryBase
    {
        public int BlogCategoryID { get; set; }
        public int SiteID { get; set; }
    }
    public class BlogCategoryCultureBase
    {
        public int BlogCategoryID { get; set; }
        public int CultureID { get; set; }
        public string BlogCategoryName { get; set; }
    }

    public class BlogPostCategoryBase
    {
        public int BlogPostID { get; set; }
        public int BlogCategoryID { get; set; }
    }
    //FEATURE IMAGE
    //MEDIA
    #endregion

    #region Binding
    //POST
    /// <summary>
    /// Used for listing post in Customer
    /// </summary>
    public class BlogPostListBinding : BlogPostBase
    {
        public BlogPostListCultureBinding CultureInfo { get; set; }
    }
    public class BlogPostListCultureBinding
    {
        public string Title { get; set; }
    }
    /// <summary>
    /// Is the Model to Save the Post (Create/Update)
    /// </summary>
    public class BlogPostModelBinding : BlogPostCultureBase
    {
        public int SiteID { get; set; }
        public List<BlogPostCategorieModelBinding> Categories { get; set; }
        public List<BlogTagModelBinding> Tags { get; set; }
    }
    /// <summary>
    /// Used to Set categories on creating BlogPost
    /// </summary>
    public class BlogPostCategorieModelBinding
    {
        public int BlogCategoryID { get; set; }
        public bool Adding { get; set; }
        public bool Deleting { get; set; }
    }

    //CATEGORY
    public class BlogCategoryCultureBinding : BlogCategoryCultureBase
    {
        public string CultureName { get; set; }
    }
    public class BlogCategoryListBinding : BlogCategoryBase
    {
        public string BlogCategoryName { get; set; }
        public List<BlogCategoryCultureBinding> CategoryCultures { get; set; }
    }
    public class BlogCategoryModelBinding
    {
        public List<BlogCategoryCultureBase> CategoryCultures { get; set; }
        public int SiteID { get; set; }
        public int BlogCategoryID { get; set; }
    }

    //TAGS
    public class BlogTagModelBinding
    {
        public int BlogTagID { get; set; }
        public string BlogTagName { get; set; }
        public bool Adding { get; set; }
        public bool Deleting { get; set; }
    }
    #endregion

    #region WEB
    public class BlogListWeb: BlogPostCultureBase
    {
        
    }
    #endregion
}