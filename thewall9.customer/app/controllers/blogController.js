app.controller('blogController', ['$scope', '$routeParams', '$location', 'blogService'
    , 'siteService', 'toastrService', 'cultureService',
    function ($scope, $routeParams, $location, blogService, siteService, toastrService
        , cultureService) {
        $scope.get = function () {
            blogService.get().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.remove = function (item) {
            if (confirm("¿Desea Eliminar el Post?")) {
                blogService.remove(item.BlogPostID).then(function (data) {
                    $scope.get();
                });
            }
        }
        /*INIT*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        $scope.init = function () {
            $scope.selectedCulture = cultureService.currentCulture;
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
