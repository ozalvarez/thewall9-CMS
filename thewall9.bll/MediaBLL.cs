using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data;
using thewall9.data.binding;

namespace thewall9.bll
{
    public class MediaBLL : BaseBLL
    {
        public Media SaveImage(string Container, string ContainerReference, FileRead FileReadModel, bool DeleteFolder)
        {
            if (FileReadModel != null)
            {
                var _ContainerReference = ContainerReference + "/" + FileReadModel.FileName;
                if (DeleteFolder)
                    new Utils.FileUtil().DeleteFolder(Container, ContainerReference);
                new Utils.FileUtil().UploadImage(Utils.ImageUtil.StringToStream(FileReadModel.FileContent), Container, _ContainerReference);
                var _FinalURL = StorageUrl + "/" + Container + "/" + _ContainerReference;

                using (var _c = db)
                {
                    var _Media = new Media
                    {
                        MediaUrl = _FinalURL
                    };
                    _c.Medias.Add(_Media);
                    _c.SaveChanges();
                    return _Media;
                }
            }
            return null;
        }
    }
}
