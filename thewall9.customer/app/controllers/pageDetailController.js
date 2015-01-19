'use strict';
app.controller('pageDetailController', ['$scope', "$routeParams", 'toastrService', 'pageService', 'cultureService', 'siteService',
    function ($scope, $routeParams, toastrService, pageService, cultureService, siteService) {
        $scope.metatagDescriptionMin = 150;
        $scope.metatagDescriptionMax = 160;
        $scope.init = function () {
            pageService.getDetail($routeParams.pageID, $scope.$parent.cultures[0].CultureID).then(function (page) {
                $scope.page = page;
            });
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
        $scope.save = function () {
            pageService.saveCulture($scope.page).then(function (data) {
                toastrService.success("Página Guardada");
            });

        };
    }
]);