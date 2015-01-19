'use strict';
app.controller('passwordController', ['$scope', '$rootScope', 'authService',  'toastrService', 'siteService',
    function ($scope, $rootScope, authService,  toastrService, siteService) {
        $scope.change = function () {
            authService.changePassword($scope.p).then(function (data) {
                toastrService.success("Clave Cambiada");
            });
        }
        /*INIT*/
        $scope.init = function () {

        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);