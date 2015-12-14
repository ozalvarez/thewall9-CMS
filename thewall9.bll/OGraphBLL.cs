using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thewall9.data;
using thewall9.data.binding;

namespace thewall9.bll
{
    public class OGraphBLL : BaseBLL
    {
        public int SaveOGraph(OGraphBinding OGraph, int SiteID, string UserID)
        {
            using (var _c = db)
            {
                if (OGraph != null)
                {
                    var _Model = new OGraph
                    {
                        OGraphDescription = OGraph.OGraphDescription,
                        OGraphTitle = OGraph.OGraphTitle
                    };
                    if (OGraph.OGraphID == 0)
                        _c.OGraphs.Add(_Model);
                    else
                    {
                        _Model = _c.OGraphs.Where(m => m.OGraphID == OGraph.OGraphID).FirstOrDefault();
                        _Model.OGraphDescription = OGraph.OGraphDescription;
                        _Model.OGraphTitle = OGraph.OGraphTitle;
                    }
                    _c.SaveChanges();

                    //ADDING MEDIA
                    if (OGraph.FileRead != null)
                    {
                        if (OGraph.FileRead.Deleting)
                        {
                            if (OGraph.FileRead.MediaID != 0)
                            {
                                new MediaBLL().DeleteMedia(OGraph.FileRead.MediaID, SiteID, UserID);
                            }
                        }
                        else
                        {
                            if (OGraph.FileRead != null && !string.IsNullOrEmpty(OGraph.FileRead.FileContent))
                            {
                                new MediaBLL().DeleteMedia(OGraph.FileRead.MediaID, SiteID, UserID);
                                var _Media = new MediaBLL().SaveImage(OGraph.FileRead, SiteID, UserID);
                                _c.OGraphMedias.Add(new OGraphMedia
                                {
                                    MediaID = _Media.MediaID,
                                    OGraphID = _Model.OGraphID
                                });
                                _c.SaveChanges();
                            }
                        }
                    }
                    return _Model.OGraphID;
                }
                return 0;
            }
        }
    }
}
