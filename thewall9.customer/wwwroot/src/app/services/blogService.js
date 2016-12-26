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
        blogFactory.remove = function (BlogPostID) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + uri+'?BlogPostID='+BlogPostID);
        };
        //CATEGORIES
        blogFactory.getCategories = function () {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri
                + '/category?SiteID=' + siteService.site.SiteID
                + "&CultureID=" + cultureService.currentCulture.CultureID);
        }
        blogFactory.editCategory = function (item, $scope) {
            if (item == null) {
                $scope.itemToSave = {
                    CategoryCultures: []
                };
            } else {
                $scope.itemToSave = item;
            }

            angular.forEach(cultureService.cultures, function (allCulture) {
                var _containCulture = false;
                angular.forEach($scope.itemToSave.CategoryCultures, function (itemCulture) {
                    if (allCulture.CultureID == itemCulture.CultureID) {
                        _containCulture = true;
                    }
                });
                if (!_containCulture) {
                    $scope.itemToSave.CategoryCultures.push({
                        CultureID: allCulture.CultureID,
                        CultureName: allCulture.Name
                    });
                }
            });
            $('#modal-new').modal({
                backdrop: true
            });
        };
        blogFactory.saveCategory = function (model) {
            model.SiteID = siteService.site.SiteID;
            return $http.post(ngAuthSettings.apiServiceBaseUri + uri + '/category', model);
        };

        //TAGS
        blogFactory.getTags = function ($query) {
            return $http.get(ngAuthSettings.apiServiceBaseUri + uri
                + '/tag?Query=' + $query);
        }
        return blogFactory;
    }]);