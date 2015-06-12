using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.data.binding
{
    public enum ContentPropertyType
    {
        IMG = 1,
        TXT = 2,
        LIST = 3,
        HTML = 4
    }
    public class ContentBase
    {
        public int ContentPropertyID { get; set; }
        public int ContentPropertyParentID { get; set; }
        public int SiteID { get; set; }
        public ContentPropertyType ContentPropertyType { get; set; }
        public string ContentPropertyAlias { get; set; }
        public int Priority { get; set; }
        public bool Lock { get; set; }
        public bool ShowInContent { get; set; }
        public bool Enabled { get; set; }
    }
    public class ContentBinding : ContentBase
    {

    }
    public class ContentCultureBase
    {
        public string ContentPropertyValue { get; set; }
        public string Hint { get; set; }

    }
    public class ContentCultureBinding : ContentCultureBase
    {
        public int ContentPropertyID { get; set; }
        public int CultureID { get; set; }
        public string ContentPropertyBinary { get; set; }
        public string CultureName { get; set; }
    }
    public class ContentCultureBindingFile : ContentCultureBinding
    {
        public string FileContent { get; set; }
        public string FileName { get; set; }
    }
    public class ContentBindingList : ContentBinding
    {
        public ICollection<ContentBindingList> Items { get; set; }
        public ICollection<ContentCultureBinding> ContentCultures { get; set; }
    }

    public class ContentMoveBinding
    {
        public int Index { get; set; }
        public int ContentPropertyParentID { get; set; }
        public int ContentPropertyID { get; set; }
    }
    public class ContentFileUpload
    {
        public ContentCultureBindingFile File { get; set; }
        public int SiteID { get; set; }
        public int ContentPropertyID { get; set; }
    }
    public class ContentBoolean
    {
        public int ContentPropertyID { get; set; }
        public bool Boolean { get; set; }
    }
    public class ContentEditBinding
    {
        public int SiteID { get; set; }
        public int CultureID { get; set; }
        public List<ContentEdit> Items { get; set; }
        
    }
    public class ContentEdit : ContentCultureBase
    {
        public int ContentPropertyParentID { get; set; }
        public int ContentPropertyID { get; set; }
        public ContentPropertyType ContentPropertyType { get; set; }
        public bool Edit { get; set; }
        public string FileContent { get; set; }
        public string FileName { get; set; }
        public string ParentHint { get; set; }
        public bool ShowInContent { get; set; }
        public bool IsEditable { get; set; }
        public string Priority { get; set; }
        public List<ContentEdit> Items { get; set; }

    }
    public class ContentTreeBinding
    {
        public int SiteID { get; set; }
        public int CultureID { get; set; }
        public List<ContentTree> Items { get; set; }

    }
    public class ContentTree : ContentCultureBase
    {
        public int ContentPropertyID { get; set; }
        public int ContentPropertyParentID { get; set; }
        public ContentPropertyType ContentPropertyType { get; set; }
        public bool Edit { get; set; }
        public string FileContent { get; set; }
        public string FileName { get; set; }
        public bool ShowInContent { get; set; }
        public bool IsEditable { get; set; }
        public bool Enabled { get; set; }
        public List<ContentTree> Items { get; set; }
    }
    public class ImportBinding
    {
        public int SiteID { get; set; }
        public int ContentPropertyID { get; set; }
        public FileRead FileRead { get; set; }
    }
}
