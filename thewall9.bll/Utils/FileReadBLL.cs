using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.bll.Exceptions;
using thewall9.data.binding;

namespace thewall9.bll.Utils
{
    public class FileReadBLL : BaseBLL
    {
        public enum FileType
        {
            PRODUCT_CATEGORY = 0,
            BRAND = 1
        }
        private string GetContainer(FileType FileType)
        {
            switch (FileType)
            {
                case FileType.PRODUCT_CATEGORY:
                    return "tw9-category";
                case FileType.BRAND:
                    return "tw9-brand";
                default:
                    throw new RuleException("this should neve happen");
            }
        }
        public string AddImage(FileReadBinding Image, FileType FileType, string Prefix, string OldImageUrl, int SiteID)
        {
            if (Image != null)
            {
                string _Container = GetContainer(FileType);
                var _ContainerReference = SiteID.ToString() + "/" + Prefix + "/";
                if (Image.Deleting)
                {
                    new AzureBlobUtil().DeleteFolder(_Container, _ContainerReference);
                    return null;
                }
                else if (!string.IsNullOrEmpty(Image.FileContent))
                {
                    var FileName = Image.FileName.CleanFileName();
                    var _Path = StorageUrl + "/" + _Container + "/" + _ContainerReference + FileName;

                    new AzureBlobUtil().DeleteFolder(_Container, _ContainerReference);
                    new AzureBlobUtil().UploadImage(Image.FileContent, _Container, _ContainerReference + FileName);

                    Image.FileUrl = _Path;
                    return _Path;
                }
                Image.FileContent = null;
            }
            return OldImageUrl;
        }
        public void Remove(FileType FileType, string Prefix, int SiteID)
        {
            string _Container = GetContainer(FileType);
            var _ContainerReference = SiteID.ToString() + "/" + Prefix + "/";
            new AzureBlobUtil().DeleteFolder(_Container, _ContainerReference);
        }
        public void RemoveAll(FileType FileType, int SiteID)
        {
            string _Container = GetContainer(FileType);
            var _ContainerReference = SiteID.ToString() + "/";
            new AzureBlobUtil().DeleteFolder(_Container, _ContainerReference);
        }
    }
}
