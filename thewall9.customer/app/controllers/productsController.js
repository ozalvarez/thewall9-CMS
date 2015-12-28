app.controller('productsController', ['$scope', 'productService', 'siteService', 'toastrService',
    function ($scope, productService, siteService, toastrService) {
        $scope.get = function () {
            productService.get().then(function (data) {
                $scope.data = data;
            });
        }
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar este producto?")) {
                productService.remove(item.ProductID).then(function (data) {
                    $scope.get();
                    toastrService.success("Producto Eliminado");
                });
            }
        };
        $scope.enable = function (item, enabled) {
            productService.enable(item.ProductID, enabled, productService.ProductBooleanType.Enable).then(function (response) {
                item.Enabled = enabled;
            });
        };
        $scope.enableNew = function (item, enabled) {
            productService.enable(item.ProductID, enabled, productService.ProductBooleanType.New).then(function (response) {
                item.New = enabled;
            });
        };
        $scope.enableFeatured = function (item, enabled) {
            productService.enable(item.ProductID, enabled, productService.ProductBooleanType.Featured).then(function (response) {
                item.Featured = enabled;
            });
        };
        $scope.options = {
            dropped: function (event) {
                var sourceNode = event.source.nodeScope;
                var index = event.dest.index;
                var productID = sourceNode.$modelValue.ProductID
                productService.move(productID, index).then(function (data) {

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
