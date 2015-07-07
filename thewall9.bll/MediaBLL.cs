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
        public void DeleteFolder(string Container, string ContainerReference)
        {
            new Utils.FileUtil().DeleteFolder(Container, ContainerReference);
        }
        public void DeleteMedia(string Container, string ContainerReference, int MediaID)
        {
            using (var _c = db)
            {
                var _Model = _c.Medias.Where(m => m.MediaID == MediaID).FirstOrDefault();
                if (_Model != null)
                {
                    _c.Medias.Remove(_Model);
                    _c.SaveChanges();
                    new Utils.FileUtil().DeleteFolder(Container, ContainerReference);
                }
            }
        }
        public void DeleteMedia(int MediaID, int SiteID, string UserID)
        {
            using (var _c = db)
            {
                Can(SiteID, UserID, _c);
                var _Model = _c.Medias.Where(m => m.MediaID == MediaID).FirstOrDefault();
                if (_Model != null)
                {
                    _c.Medias.Remove(_Model);
                    _c.SaveChanges();
                    new Utils.FileUtil().DeleteFolder("media", _Model.MediaID+"/");
                }
            }
        }
        public Media SaveImage(string Container, string ContainerReference, FileRead FileReadModel, bool DeleteFolder)
        {
            if (FileReadModel != null)
            {
                var _ContainerReference = ContainerReference + "/" + FileReadModel.FileName;
                if (DeleteFolder)
                    new Utils.FileUtil().DeleteFolder(Container, ContainerReference);
                new Utils.FileUtil().UploadImage(Utils.ImageUtil.StringToStream(FileReadModel.FileContent), Container, _ContainerReference);
                var _FinalURL = StorageUrl + "/" + Container + "/" + _ContainerReference;
                return SaveMedia(_FinalURL);
            }
            return null;
        }
        public MediaBase SaveImage(FileReadSite Model, string UserID)
        {
            if (Model != null)
            {
                using (var _c = db)
                {
                    Can(Model.SiteID, UserID, _c);
                    var _Media = SaveMedia(null);
                    var _ContainerReference = _Media.MediaID + "/" + Model.FileName;
                    if (new Utils.FileUtil().Exist("media", _ContainerReference))
                    {
                        Model.FileName = Utils.Util.RandomString(5) + Model.FileName;
                        _ContainerReference = _Media.MediaID + "/" + Model.FileName;
                    }
                    new Utils.FileUtil().UploadImage(Utils.ImageUtil.StringToStream(Model.FileContent), "media", _ContainerReference);
                    var _FinalURL = StorageUrl + "/media/" + _ContainerReference;
                    _Media = _c.Medias.Where(m => m.MediaID == _Media.MediaID).FirstOrDefault();
                    _Media.MediaUrl = _FinalURL;
                    _c.SaveChanges();
                    return _Media;
                }
            }
            return null;
        }
        private Media SaveMedia(string FinalUrl)
        {
            using (var _c = db)
            {
                var _Media = new Media
                {
                    MediaUrl = FinalUrl
                };
                _c.Medias.Add(_Media);
                _c.SaveChanges();
                return _Media;
            }
        }
    }
}
