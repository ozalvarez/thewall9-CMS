'use strict';
app.factory('productService', ["$myhttp", "$q", "ngAuthSettings", "siteService",
    function ($http, $q, ngAuthSettings, siteService) {
        var uri = 'api/product';
        var productFactory = {};

        productFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '?SiteID=' + siteService.site.SiteID);
        };
        productFactory.getByID = function (productID) {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '/byID?ProductID=' + productID);
        };
        productFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };
        productFactory.remove = function (productID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?ProductID=' + productID);
        };
        productFactory.move = function (productID, index) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri + '/move', {
                ProductID: productID,
                Index:index
            });
        };
        //CATEGORIES
        productFactory.getCategories = function (query) {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '/categories?SiteID=' + siteService.site.SiteID + "&Query=" + query);
        }
        productFactory.getCategoriesUrl = function () {
            return ngAuthSettings.apiServiceBaseUri + uri + '/categories?SiteID=' + siteService.site.SiteID + "&Query=%QUERY";
        }
        //TAGS
        productFactory.getTags = function (query) {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '/tags?SiteID=' + siteService.site.SiteID + "&Query=" + query);
        }
        return productFactory;
    }]);