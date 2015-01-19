'use strict';
app.controller('appController', ['$scope','$rootScope',"$q", '$location', 'authService', "utilService",
    function ($scope,$rootScope, $q, $location, authService, utilService) {

        $scope.logOut = function () {
            authService.logOut();
            window.location = "/"
        }
        $scope.authentication = authService.authentication;
    }
]);