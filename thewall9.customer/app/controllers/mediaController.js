app.controller('mediaController', ['$scope', '$routeParams', '$location', 'mediaService', 'siteService', 'toastrService', 'cultureService',
    function ($scope, $routeParams, $location, mediaService, siteService, toastrService, cultureService) {

        $scope.get = function () {
            mediaService.get().then(function (data) {
                $scope.mediaList = data;
            });
        };
        $scope.deleteMedia = function (item) {
            mediaService.remove(item).then(function (data) {
                
            });
        };
        /*INIT*/
        $scope.init = function () {
            $scope.get();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            if (cultureService.currentCulture.CultureID) {
                $scope.init();
            }
        }
    }
]);
