'use strict';
app.factory('pageService', ["$myhttp", "ngAuthSettings", "siteService",
    function ($http, ngAuthSettings, siteService) {
        return {
            get: function () {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/page?SiteID=' + siteService.site.SiteID);
            },
            getDetail: function (PageID, CultureID) {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/page?PageID=' + PageID + '&CultureID=' + CultureID);
            },
            save: function (object) {
                object.SiteID = siteService.site.SiteID;
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/page', object);
            },
            saveCulture: function (object) {
                object.SiteID = siteService.site.SiteID;
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/page/save-culture', object);
            },
            remove: function (object) {
                return $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/page?PageID=' + object.PageID);
            },
            move: function (Index, PageParentID, PageID) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/page/move', {
                    Index: Index,
                    PageParentID: PageParentID,
                    PageID: PageID
                });
            },
            inMenu: function (PageID, Published) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/page/in-menu', {
                    PageID: PageID,
                    Published: Published
                });
            }
        }
    }]);