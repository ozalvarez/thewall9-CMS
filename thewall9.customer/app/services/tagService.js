'use strict';
app.factory('tagService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/tag';
        var tagFactory = {};

        tagFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };
        tagFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };
        tagFactory.remove = function (tagID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?tagID=' + tagID);
        };

        return tagFactory;
    }
]);