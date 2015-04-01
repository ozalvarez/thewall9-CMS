'use strict';
app.controller('appController', ['$scope', '$rootScope','productService',
    function ($scope, $rootScope, productService) {
        $scope.active = _Active;
        $scope.moneySymbol = _MoneySymbol.CurrencyName;

        productService.initCart();

        $scope.totalCart = function () {
            return productService.totalCart();
        }
        $scope.cart = productService.cart;
    }
]);