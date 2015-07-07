'use strict';
app.factory('mediaService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/media';
        var mediaFactory = {};

        mediaFactory.upload = function (model) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };

        return mediaFactory;
    }]);