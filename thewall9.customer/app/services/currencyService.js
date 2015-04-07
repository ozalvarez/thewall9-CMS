'use strict';
app.factory('currencyService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/currency';
        var currencyFactory = {};

        currencyFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };
        currencyFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };
        currencyFactory.default = function (currencyID) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + uri + '/default?CurrencyID='+currencyID);
        };
        currencyFactory.remove = function (currencyID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?CurrencyID=' + currencyID);
        };

        return currencyFactory;
    }
]);