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
        #region ADMIN MEDIA
        public List<FileRead> Get(int SiteID, string UserID)
        {
            using (var _c = db)
            {
                Can(SiteID, UserID, _c);
                //var _Media = (from bpf in _c.BlogPostFeatureImages
                //              where bpf.BlogPostCulture.BlogPost.SiteID == SiteID
                //              select new FileRead
                //              {
                //                  MediaID = bpf.MediaID,
                //                  MediaUrl = bpf.Media.MediaUrl
                //              }).Union(from bpi in _c.BlogPostImages
                //                       where bpi.BlogPostCulture.BlogPost.SiteID == SiteID
                //                       select new FileRead
                //                       {
                //                           MediaID = bpi.MediaID,
                //                           MediaUrl = bpi.Media.MediaUrl
                //                       }).Union(from og in _c.PageCulturesOGraphs
                //                                where og.PageCulture.Page.SiteID == SiteID
                //                                select new FileRead
                //                                {
                //                                    MediaID = og.OGraph.OGraphMedia.MediaID,
                //                                    MediaUrl = og.OGraph.OGraphMedia.Media.MediaUrl
                //                                });
                return (from m in _c.Medias
                        where m.SiteID == SiteID
                        select new FileRead
                        {
                            MediaID = m.MediaID,
                            MediaUrl = m.MediaUrl
                        }).ToList();

            }
        }

        #endregion
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
                    new Utils.AzureBlobUtil().DeleteFolder("media", _Model.MediaID + "/");
                }
            }
        }
        public MediaBinding SaveImage(FileRead Model, int SiteID, string UserID)
        {
            if (Model != null)
            {
                using (var _c = db)
                {
                    Can(SiteID, UserID, _c);
                    var _Media = SaveMedia(null, SiteID);
                    var _ContainerReference = _Media.MediaID + "/" + Model.FileName;
                    if (new Utils.AzureBlobUtil().Exist("media", _ContainerReference))
                    {
                        Model.FileName = Utils.Util.RandomString(5) + Model.FileName;
                        _ContainerReference = _Media.MediaID + "/" + Model.FileName;
                    }
                    new Utils.AzureBlobUtil().UploadImage(Utils.ImageUtil.StringToStream(Model.FileContent), "media", _ContainerReference);
                    var _FinalURL = StorageUrl + "/media/" + _ContainerReference;
                    _Media = _c.Medias.Where(m => m.MediaID == _Media.MediaID).FirstOrDefault();
                    _Media.MediaUrl = _FinalURL;
                    _c.SaveChanges();
                    return new MediaBinding
                    {
                        MediaID=_Media.MediaID,
                        SiteID=SiteID,
                        MediaUrl=_Media.MediaUrl
                    };
                }
            }
            return null;
        }
        private Media SaveMedia(string FinalUrl, int SiteID)
        {
            using (var _c = db)
            {
                var _Media = new Media
                {
                    MediaUrl = FinalUrl,
                    SiteID = SiteID
                };
                _c.Medias.Add(_Media);
                _c.SaveChanges();
                return _Media;
            }
        }
    }
}
