'use strict';
app.factory('mediaService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/media';
        var mediaFactory = {};

        mediaFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };

        mediaFactory.upload = function (model) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };

        mediaFactory.remove = function (model) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?MediaID=' + model.MediaID + '&SiteID=' + siteService.site.SiteID);
        };

        return mediaFactory;
    }]);