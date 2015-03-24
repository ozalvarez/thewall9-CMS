'use strict';
app.controller('configurationController', ['$scope', "siteService","cultureService", 'authService', "toastrService",
    function ($scope, siteService,cultureService, authService, toastrService) {
        $scope.saveSite = function () {
            siteService.save($scope.site).then(function (data) {
                toastrService.success("Configuración Guardada");
            });
        };
        $scope.save = function (item) {
            cultureService.save(item).then(function (data) {
                toastrService.success("Configuración Guardada");
            });
        }


        $scope.init = function () {
            $scope.site = siteService.site;
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);