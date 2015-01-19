'use strict';
app.factory('cultureService', ["$myhttp","$q", "ngAuthSettings", "siteService",
    function ($http,$q, ngAuthSettings, siteService) {
        var cultureFactory = {};

        cultureFactory.cultures = [];
        cultureFactory.currentCulture = {};

        cultureFactory.get = function () {
            var deferred = $q.defer();
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/culture?SiteID=' + siteService.site.SiteID).then(function (data) {
                cultureFactory.cultures = data;
                cultureFactory.currentCulture = data[0];
                deferred.resolve(data);
            });
        };

        return cultureFactory;
    }]);