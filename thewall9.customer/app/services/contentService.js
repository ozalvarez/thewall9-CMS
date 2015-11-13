'use strict';
app.factory('contentService', ["$myhttp", "$q", "ngAuthSettings", "siteService", "cultureService",
    function ($http, $q, ngAuthSettings, siteService, cultureService) {
        return {
            get: function () {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content?SiteID=' + siteService.site.SiteID);
            },
            getMenu: function () {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content/menu?SiteID=' + siteService.site.SiteID + "&CultureID=" + cultureService.currentCulture.CultureID);
            },
            getTree: function () {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content?SiteID=' + siteService.site.SiteID + "&CultureID=" + cultureService.currentCulture.CultureID);
            },
            getTreeByProperty: function (ContentPropertyID) {
                return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/content/property?ContentPropertyID=' + ContentPropertyID + "&CultureID=" + cultureService.currentCulture.CultureID);
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
            duplicateTree: function (object) {
                object.SiteID = siteService.site.SiteID;
                object.CultureID = cultureService.currentCulture.CultureID;
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/duplicate-tree', object);
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
            enable: function (contentPropertyID, enabled) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/enable', {
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
            },
            inMenu: function (contentPropertyID, enabled) {
                return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/content/inmenu', {
                    ContentPropertyID: contentPropertyID,
                    Boolean: enabled
                })
            }
        }
    }]);