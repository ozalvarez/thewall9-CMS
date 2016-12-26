'use strict';
app.factory('orderService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/order';
        var orderFactory = {};

        orderFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };
        orderFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };
        orderFactory.remove = function (orderID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?OrderID=' + orderID);
        };

        return orderFactory;
    }
]);