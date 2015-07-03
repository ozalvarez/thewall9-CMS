'use strict';
app.factory('blogService', ["$myhttp", "$q", "ngAuthSettings", "siteService", 'cultureService',
    function ($http, $q, ngAuthSettings, siteService, cultureService) {
        var uri = 'api/blog';
        //var uriGallery = 'api/product-gallery';
        var blogFactory = {};

        blogFactory.get = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri
                + uri
                + '?SiteID=' + siteService.site.SiteID
                + '&CultureID=' + cultureService.currentCulture.CultureID);
        };
        blogFactory.getByID = function (BlogPostID) {
            return $http.get(ngAuthSettings.apiServiceBaseUri
                + uri
                + '/byID?BlogPostID=' + BlogPostID
                + '&CultureID=' + cultureService.currentCulture.CultureID);
        };
        blogFactory.save = function (model) {
            model.SiteID = siteService.site.SiteID;
            model.CultureID = cultureService.currentCulture.CultureID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        };

        //CATEGORIES
        blogFactory.getCategories = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri
                + '/category?SiteID=' + siteService.site.SiteID
                + "&CultureID=" + cultureService.currentCulture.CultureID);
        }
        //blogFactory.getByID = function (productID) {
        //    return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '/byID?ProductID=' + productID);
        //};
        //blogFactory.save = function (model) {
        //    model.SiteID = siteService.site.SiteID;
        //    return $http.post(ngAuthSettings.apiServiceBaseUri + uri, model);
        //};
        //blogFactory.remove = function (productID) {
        //    return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '?ProductID=' + productID);
        //};
        //blogFactory.removeGallery = function (galleryID) {
        //    return $http.delete(ngAuthSettings.apiServiceBaseUri + uri + '/gallery?GalleryID=' + galleryID);
        //};
        //blogFactory.move = function (productID, index) {
        //    return $http.post(ngAuthSettings.apiServiceBaseUri + uri + '/move', {
        //        ProductID: productID,
        //        Index: index
        //    });
        //};
        //blogFactory.uploadGallery = function (productID, file) {
        //    return $upload.upload({
        //        url: ngAuthSettings.apiServiceBaseUri + uriGallery + "/upload",
        //        fields: {
        //            'ProductID': productID
        //        },
        //        file: file
        //    });
        //};
        
        //blogFactory.getCategoriesUrl = function () {
        //    return ngAuthSettings.apiServiceBaseUri + uri + '/categories?SiteID=' + siteService.site.SiteID + "&Query=%QUERY";
        //}
        ////TAGS
        //blogFactory.getTags = function (query) {
        //    return $http.get(ngAuthSettings.apiServiceBaseUri + uri + '/tags?SiteID=' + siteService.site.SiteID + "&Query=" + query);
        //}
        return blogFactory;
    }]);