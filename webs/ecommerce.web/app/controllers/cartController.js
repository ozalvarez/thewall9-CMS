'use strict';
app.controller('cartController', ['$scope', '$rootScope', 'productService', 'toastrService',
    function ($scope, $rootScope, productService, toastrService) {
        
        $scope.removeCart = function (item) {
            productService.removeCart(item);
        };
        $scope.updateNumber = function (item, number) {
            productService.updateNumber(item, number);
        }
    }
]);