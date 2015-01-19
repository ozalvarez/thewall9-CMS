'use strict';
app.factory('contentService', ["$myhttp", "$q", "ngAuthSettings", "siteService", "cultureService",
    function ($http, $q, ngAuthSettings, siteService, cultureService) {
        return {
            get: function () {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content?SiteID=' + siteService.site.SiteID);
            },
            getTree: function () {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content?SiteID=' + siteService.site.SiteID + "&CultureID=" + cultureService.currentCulture.CultureID);
            },
            save: function (object) {
                object.SiteID = siteService.site.SiteID;
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content', object);
            },
            saveTree: function (items) {
                var obj = {
                    SiteID: siteService.site.SiteID,
                    CultureID: cultureService.currentCulture.CultureID,
                    Items: items
                }
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/save-tree', obj);
            },
            remove: function (object) {
                return $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/content?ContentPropertyID=' + object.ContentPropertyID);
            },
            move: function (Index, ContentPropertyParentID, ContentPropertyID) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/move', {
                    Index: Index,
                    ContentPropertyParentID: ContentPropertyParentID,
                    ContentPropertyID: ContentPropertyID
                });
            },
            duplicate: function (object) {
                object.SiteID = siteService.site.SiteID;
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/duplicate', object);
            },
            exportContent: function (ContentPropertyID) {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content/export?SiteID=' + siteService.site.SiteID + '&ContentPropertyID=' + ContentPropertyID);
            },
            importContent: function (ContentPropertyID, FileRead) {
                var obj = {
                    SiteID : siteService.site.SiteID,
                    ContentPropertyID: ContentPropertyID,
                    FileRead: FileRead
                }
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/import', obj);
            },
            lock: function (contentPropertyID, enabled) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/lock', {
                    ContentPropertyID: contentPropertyID,
                    Boolean: enabled
                })
            },
            lockAll: function () {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/lock-all', siteService.site.SiteID);
            },
            showInContent: function (contentPropertyID, enabled) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/show-in-content', {
                    ContentPropertyID: contentPropertyID,
                    Boolean: enabled
                })
            }
        }
    }]);