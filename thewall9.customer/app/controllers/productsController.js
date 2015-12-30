app.controller('productsController', ['$scope', '$routeParams', '$location', 'productService', 'siteService', 'toastrService',
    function ($scope, $routeParams, $location, productService, siteService, toastrService) {
        $scope.get = function () {
            productService.get($scope.selectedCategory.CategoryID).then(function (data) {
                $scope.data = data;
            });
        }
        function getCategories() {
            productService.getCategories().then(function (dataCategories) {
                $scope.categories = dataCategories;
                if ($routeParams.CategoryID != null) {
                    angular.forEach($scope.categories, function (item) {
                        if (item.CategoryID == $routeParams.CategoryID) {
                            $scope.selectedCategory = item;
                        }
                    });
                } else {
                    $scope.selectedCategory = {
                        CategoryID: 0
                    }
                }
                $scope.get();
            });
        }
        $scope.changeCategory = function () {
            if ($scope.selectedCategory == null)
                $location.path("/products/");
            else
                $location.path("/products/" + $scope.selectedCategory.CategoryID);
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
                productService.move(productID, $scope.selectedCategory.CategoryID, index).then(function (data) {
                    sourceNode.$modelValue.Priority = data;
                });
            }
        };

        /*INIT*/
        $scope.init = function () {
            getCategories();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
