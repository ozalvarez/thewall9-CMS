app.controller('brandsController', ['$scope', 'brandService', 'siteService', 'toastrService',
    function ($scope, brandService, siteService, toastrService) {
        $scope.get = function () {
            brandService.get().then(function (data) {
                $scope.data = data;
            });
        }
        $scope.open = function (item) {
            if (item == null) {
                $scope.model = {};
            } else {
                $scope.model = angular.copy(item);
            }
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            brandService.save($scope.model).then(function (data) {
                $scope.get();
            });
            $('#modal-new').modal('hide');
        };
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar este brand?")) {
                brandService.remove(item.BrandID).then(function (data) {
                    $scope.get();
                    toastrService.success("Brand Deleted");
                });
            }
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
