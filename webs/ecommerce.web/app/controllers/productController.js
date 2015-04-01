'use strict';
app.controller('productController', ['$scope', '$rootScope', 'productService', 'toastrService',
    function ($scope, $rootScope, productService, toastrService) {
        $scope.data = _Products;
        $scope.data.NumberPagesArray = [];
        for (var i = 0; i < $scope.data.NumberPages; i++) {
            $scope.data.NumberPagesArray.push(i);
        }
        $scope.addProduct = function (item) {
            productService.add(item);
            toastrService.success(item.ProductName + " en el Carrito")
        };
    }
]);