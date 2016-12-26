'use strict';
app.controller('appController', ['$scope', '$rootScope', "$q", '$location', 'authService', "siteService", "cultureService",
    function ($scope, $rootScope, $q, $location, authService, siteService, cultureService) {
        $scope.logOut = function () {
            authService.logOut();
            window.location = "/"
        }
        $scope.authentication = authService.authentication;

        siteService.getSites().then(function (data) {
            if (siteService.sites.length == 0) {
                $scope.logOut();
            } else {
                $scope.site = siteService.site;
                $scope.sites = siteService.sites;
                $scope.updateSiteInfo();
            }
        });

        $scope.changeSite = function (site) {
            siteService.change(site);
            $scope.site = siteService.site;
            $scope.updateSiteInfo();
        };
        $scope.updateSiteInfo = function () {
            authService.getRoles().then(function (data) {
                $scope.isAdmin = authService.isAdmin;
                cultureService.get().then(function (cultures) {
                    $scope.cultures = cultureService.cultures;
                    $rootScope.$broadcast('initDone');
                });
            });
        }
    }
]);