using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data.binding;

namespace thewall9.bll.Utils
{
    public class FileReadBLL : BaseBLL
    {
        public enum FileType
        {
            PRODUCT_CATEGORY = 0
        }
        public string AddImage(FileReadBinding Image, FileType FileType, string Prefix, string OldImageUrl)
        {
            if (Image != null && !string.IsNullOrEmpty(Image.FileContent))
            {
                var FileName = Image.FileName.CleanFileName();
                string _Container = String.Empty;
                switch (FileType)
                {
                    case FileType.PRODUCT_CATEGORY:
                        _Container = "tw9-category";
                        break;
                    default:
                        break;
                }

                // var _Prefix = Model.PlayerID.ToString();
                if (Image.FileContent != null)
                {
                    var _ContainerReference = Prefix + "/" + FileName;
                    var _Path = StorageUrl + "/" + _Container + "/" + _ContainerReference;

                    new AzureBlobUtil().DeleteFolder(_Container, Prefix);
                    new AzureBlobUtil().UploadImage(Image.FileContent, _Container, _ContainerReference);

                    Image.FileUrl = _Path;
                    return _Path;
                }
                else if (Image.Deleting)
                    new AzureBlobUtil().DeleteFolder(_Container, Prefix);
                Image.FileContent = null;
            }
            return OldImageUrl;
        }
    }
}
