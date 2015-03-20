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
                siteServiceFactory.site = data[0];
                siteServiceFactory.sites = data;
                siteServiceFactory.sitesLoaded = true
                localStorageService.set('site', siteServiceFactory.site);
                deferred.resolve(data);
            });
            return deferred.promise;
        };
        siteServiceFactory.save = function (object) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/site', object);
        };
        siteServiceFactory.change = function (site) {
            siteServiceFactory.site = site;
            localStorageService.set('site', site);
        };

        return siteServiceFactory;
    }
]);