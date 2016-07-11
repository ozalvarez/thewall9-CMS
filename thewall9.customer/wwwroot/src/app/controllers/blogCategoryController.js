app.controller('blogCategoryController', ['$scope', '$routeParams', '$location', 'blogService'
    , 'siteService', 'toastrService', 'cultureService',
    function ($scope, $routeParams, $location, blogService, siteService, toastrService
        , cultureService) {
        //CATEGORIES
        $scope.getCategories = function () {
            return blogService.getCategories().then(function (data) {
                $scope.data = data;
            });
        }
        $scope.edit = function (item) {
            blogService.editCategory(item, $scope);
        }
        /*INIT*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        $scope.init = function () {
            $scope.selectedCulture = cultureService.currentCulture;
            $scope.getCategories();
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
