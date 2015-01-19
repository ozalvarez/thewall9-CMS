'use strict';
app.controller('configurationController', ['$scope', "siteService", 'authService', "toastrService",
    function ($scope, siteService, authService, toastrService) {
        $scope.init = function () {
            $scope.site = siteService.site;
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }

        $scope.saveSite = function () {
            siteService.save($scope.site).then(function (data) {
                toastrService.success("Configuración Guardada");
            });
        };
    }
]);