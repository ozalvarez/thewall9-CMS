'use strict';
app.controller('productController', ['$scope', '$rootScope', 'productService', 'toastrService',
    function ($scope, $rootScope, productService, toastrService) {
        $scope.data = _Product;
        $scope.number = 1;

        $scope.updateNumber = function (number) {
            $scope.number += number;
        }
        $scope.addProduct = function (item) {
            productService.add(item, $scope.number);
            toastrService.success(item.ProductName + " " + _Message.AddedCart)
        };
    }
]);