app.controller('blogCategorySaveController', ['$scope', '$routeParams', '$location', 'blogService'
    , 'siteService', 'toastrService', 'cultureService',
    function ($scope, $routeParams, $location, blogService, siteService, toastrService
        , cultureService) {
        $scope.save = function () {
            blogService.saveCategory($scope.itemToSave).then(function (data) {
                toastrService.success("Post Guardado");
                $('#modal-new').modal('hide');
                $scope.getCategories();
            });
        }
        /*INIT*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        $scope.init = function () {
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
