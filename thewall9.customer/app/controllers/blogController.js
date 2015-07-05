﻿app.controller('blogController', ['$scope', '$routeParams', '$location', 'blogService'
    , 'siteService', 'toastrService', 'cultureService',
    function ($scope, $routeParams, $location, blogService, siteService, toastrService
        , cultureService) {
        $scope.get = function () {
            blogService.get().then(function (data) {
                $scope.data = data;
            });
        };

        /*INIT*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        $scope.init = function () {
            $scope.get();
            $scope.selectedCulture = cultureService.currentCulture;
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);