'use strict';
app.factory('siteService', ["$myhttp", "$q", "ngAuthSettings", 'localStorageService',
    function ($http, $q, ngAuthSettings, localStorageService) {
        var siteServiceFactory = {};

        siteServiceFactory.site = {};
        siteServiceFactory.sites = [];
        siteServiceFactory.sitesLoaded = false;
        siteServiceFactory.getSites = function () {
            var deferred = $q.defer();
            $http.get(ngAuthSettings.apiServiceBaseUri + 'api/site').then(function (data) {
                var defaultSite = localStorageService.get('site');
                var go = false;
                if (defaultSite != null) {
                    angular.forEach(data, function (item) {
                        if (item.SiteID == defaultSite.SiteID) {
                            go = true;
                        }
                    });
                }
                if (!go) {
                    siteServiceFactory.site = data[0];
                    defaultSite = siteServiceFactory.site;
                } else {
                    siteServiceFactory.site = defaultSite;
                }
                siteServiceFactory.sites = data;
                siteServiceFactory.sitesLoaded = true
                localStorageService.set('site', siteServiceFactory.site);
                deferred.resolve(data);
            });
            return deferred.promise;
        };
        siteServiceFactory.save = function (object) {
            var deferred = $q.defer();
            $http.put(ngAuthSettings.apiServiceBaseUri + 'api/site', object).then(function (data) {
                siteServiceFactory.site = object;
                deferred.resolve(data);
            });
            return deferred.promise;
        };
        siteServiceFactory.change = function (site) {
            siteServiceFactory.site = site;
            localStorageService.set('site', site);
        };

        return siteServiceFactory;
    }
]);