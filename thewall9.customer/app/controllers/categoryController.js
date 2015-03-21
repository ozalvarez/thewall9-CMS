app.controller('categoryController', ['$scope', 'categoryService', 'siteService',
    function ($scope, categoryService, siteService) {
        $scope.get = function () {
            categoryService.get().then(function (data) {
                $scope.data = data;
            });
        }
        $scope.openNew = function (CategoryParentID) {
            $scope.model = {
                CategoryParentID: CategoryParentID
            };
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            categoryService.save($scope.model).then(function (data) {
                $scope.get();
            });
            $('#modal-new').modal('hide');
        };
        /*INIT*/
        $scope.init = function () {
            $scope.get();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
