'use strict';
app.factory('productService', ["$myhttp", "$q", 'Upload', "ngAuthSettings", "siteService",
    function ($http, $q, Upload, ngAuthSettings, siteService) {
        var uri = 'api/product';
        var uriGallery = 'api/product-gallery';
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
        productFactory.enable= function (ProductID, Enabled) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri + '/enable', {
                ProductID: ProductID,
                Boolean: Enabled
            })
        };
        productFactory.removeGallery = function (galleryID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '/gallery?GalleryID=' + galleryID);
        };
        productFactory.move = function (productID, index) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri + '/move', {
                ProductID: productID,
                Index: index
            });
        };
        productFactory.uploadGallery = function (productID, file) {
            return Upload.upload({
                url: ngAuthSettings.apiServiceBaseUri + uriGallery + "/upload",
                fields: {
                    'ProductID': productID
                },
                file: file
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