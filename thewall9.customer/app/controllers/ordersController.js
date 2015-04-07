app.controller('ordersController', ['$scope', 'orderService', 'siteService', 'toastrService',
    function ($scope, orderService,siteService, toastrService) {
        $scope.get = function () {
            orderService.get().then(function (data) {
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
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar esta Orden?")) {
                orderService.remove(item.OrderID).then(function (data) {
                    $scope.get();
                    toastrService.success("Orden Eliminada");
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
