app.controller('productController', ['$scope', '$routeParams','$location', 'productService', 'siteService', 'toastrService',
    function ($scope, $routeParams,$location, productService, siteService, toastrService) {

        $scope.get = function () {
            $scope.model = {
                ProductCultures: [],
                ProductCategories:[]
            };
            if ($routeParams.productID != null) {
                productService.getByID($routeParams.productID).then(function (data) {
                    $scope.data = data;
                });
            } else {
                angular.forEach($scope.cultures, function (item) {
                    $scope.model.ProductCultures.push({
                        CultureID: item.CultureID,
                        CultureName: item.Name,
                        ProductName: ""
                    });
                });
            }
        };
        //CATEGORIES
        $scope.loadCategories = function (query) {
            productService.getCategories(query).then(function (data) {
                $scope.categories = data;
            });
        };
        $scope.addCategory = function (item) {
            $scope.model.ProductCategories.push(item);
            $scope.categories = [];
            $scope.queryCategories = "";
        };
        $scope.removeCategory = function (item) {
            var index=$scope.model.ProductCategories.indexOf(item);
            $scope.model.ProductCategories.splice(index, 1);
        };





        $scope.open = function (item) {
            $scope.model = {
                ProductCultures: []
            };
            if (item == null) {
                angular.forEach($scope.cultures, function (itemC) {
                    $scope.model.ProductCultures.push({
                        CultureID: itemC.CultureID,
                        CultureName: itemC.Name,
                        ProductName: ""
                    });
                });
            } else {
                angular.forEach($scope.cultures, function (itemC) {
                    var exist = false;
                    angular.forEach($scope.model.ProductCultures, function (itemCC) {
                        if (itemCC.CultureID == itemC.CultureID) {
                            exist = true;
                        }
                    });
                    if (!exist) {
                        $scope.model.CategoryCultures.push({
                            CultureID: itemC.CultureID,
                            CultureName: itemC.Name,
                            ProductName: "",
                            ProductID: item.ProductID,
                            Adding: true
                        })
                    }
                });
            }
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            if ($scope.model.ProductCategories != null && $scope.model.ProductCategories.length > 0) {
                productService.save($scope.model).then(function (data) {
                    $location.path('/products')
                });
            } else {
                toastrService.error("Tienes que agregar al menos una categoría");
            }
        };
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
