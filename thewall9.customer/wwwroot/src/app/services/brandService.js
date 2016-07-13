'use strict';
app.factory('brandService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/brand';
        var brandFactory = {};

        brandFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };
        brandFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };
        brandFactory.remove = function (brandID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?BrandID=' + brandID);
        };

        return brandFactory;
    }
]);