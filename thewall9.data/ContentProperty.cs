using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.data
{

    public class ContentPropertyCulture : ContentCultureBase
    {
        [Key, Column(Order = 1)]
        public int ContentPropertyID { get; set; }
        [Key, Column(Order = 2)]
        public int CultureID { get; set; }
        [ForeignKey("CultureID")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ContentPropertyID")]
        public virtual ContentProperty ContentProperty { get; set; }
        public ContentPropertyCulture() { }
        public ContentPropertyCulture(ContentCultureBinding Model)
        {
            SetValues(Model);
        }
        public void SetValues(ContentCultureBinding Model)
        {
            this.ContentPropertyID = Model.ContentPropertyID;
            this.ContentPropertyValue = Model.ContentPropertyValue;
            this.CultureID = Model.CultureID;
            this.Hint = Model.Hint;
        }
    }
    public class ContentProperty : ContentBase
    {
        [ForeignKey("SiteID")]
        public virtual Site Site { get; set; }
        
        public virtual List<ContentPropertyCulture> ContentPropertyCultures { get; set; }
    }
}
