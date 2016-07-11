app.controller('currencyController', ['$scope', 'currencyService', 'siteService', 'toastrService',
    function ($scope, currencyService, siteService, toastrService) {
        $scope.get = function () {
            currencyService.get().then(function (data) {
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
            currencyService.save($scope.model).then(function (data) {
                $scope.get();
            });
            $('#modal-new').modal('hide');
        };
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar esta moneda?")) {
                currencyService.remove(item.CurrencyID).then(function (data) {
                    $scope.get();
                    toastrService.success("Categoría Eliminada");
                });
            }
        };
        $scope.default = function (item) {
            currencyService.default(item.CurrencyID).then(function (data) {
                $scope.get();
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
            $scope.init();
        }
    }
]);
