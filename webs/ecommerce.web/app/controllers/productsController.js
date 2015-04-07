'use strict';
app.controller('productsController', ['$scope', '$rootScope', 'productService', 'toastrService','messagesService',
    function ($scope, $rootScope, productService, toastrService, messagesService) {
        $scope.data = _Products;
        $scope.data.NumberPagesArray = [];
        for (var i = 0; i < $scope.data.NumberPages; i++) {
            $scope.data.NumberPagesArray.push(i);
        }
        $scope.addProduct = function (item) {
            productService.add(item,1);
            toastrService.success(item.ProductName + " " + messagesService.get("added-cart"));
        };
    }
]);