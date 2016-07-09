using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public interface IEditableBinding
    {
        bool Adding { get; set; }
        bool Deleting { get; set; }
    }
    public interface IFileReadBinding : IEditableBinding
    {
        string FileContent { get; set; }
        string FileUrl { get; set; }
        string FileName { get; set; }
    }
    public class FileReadBinding : IFileReadBinding
    {
        public string FileContent { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public bool Adding { get; set; }
        public bool Deleting { get; set; }
    }
    //TO-DO CHANGE AL TO FILEREADBINDING
    public class FileRead : MediaBase
    {
        public string FileContent { get; set; }
        public string FileName { get; set; }
        public bool Deleting { get; set; }
        public bool Adding { get; set; }
    }
    public class SiteMapModel
    {
        public bool Ecommerce { get; set; }
        
        public List<PageCultureBinding> Pages { get; set; }
        public List<ProductWeb> Products { get; set; }
        public List<CategoryWeb> Categories { get; set; }

        public bool Blog { get; set; }
        public List<BlogPostWeb> Posts { get; set; }
        public List<BlogCategoryCultureBase> BlogCategories { get; set; }
        public List<BlogTagBase> BlogTags{ get; set; }
    }
}
