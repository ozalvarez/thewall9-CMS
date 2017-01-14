'use strict';
app.controller('appController', ['$scope','$rootScope',"$q", '$location', 'authService', "utilService",
    function ($scope,$rootScope, $q, $location, authService, utilService) {
        $scope.logOut = function () {
            authService.logOut();
        }
        $scope.authentication = authService.authentication;
        $scope.getPath = function (path) {
            return APP.ApplicationPath + path;
        }
    }
]);