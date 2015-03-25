app.controller('productsController', ['$scope', 'productService', 'siteService', 'toastrService',
    function ($scope, productService, siteService, toastrService) {
        $scope.get = function () {
            productService.get().then(function (data) {
                $scope.data = data;
            });
        }
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar este producto?")) {
                productService.remove(item.productID).then(function (data) {
                    $scope.get();
                    toastrService.success("Producto Eliminado");
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
