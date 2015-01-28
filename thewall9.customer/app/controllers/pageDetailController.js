'use strict';
app.controller('pageDetailController', ['$scope', "$routeParams", 'toastrService', 'pageService', 'cultureService', 'siteService',
    function ($scope, $routeParams, toastrService, pageService, cultureService, siteService) {
        $scope.metatagDescriptionMin = 150;
        $scope.metatagDescriptionMax = 160;
        
        $scope.save = function () {
            pageService.saveCulture($scope.page).then(function (data) {
                toastrService.success("Página Guardada");
            });

        };
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }

        /*INIT*/
        $scope.init = function () {
            pageService.getDetail($routeParams.pageID, cultureService.currentCulture.CultureID).then(function (page) {
                $scope.page = page;
            });
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