'use strict';
app.factory('categoryService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/category';
        var categoryFactory = {};

        categoryFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };
        categoryFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };
        categoryFactory.up = function (model) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri + '/up-or-down', model);
        };
        categoryFactory.remove = function (categoryID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?CategoryID=' + categoryID);
        };

        return categoryFactory;
    }]);