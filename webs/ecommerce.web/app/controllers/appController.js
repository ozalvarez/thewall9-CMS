'use strict';
app.controller('appController', ['$scope', '$rootScope', 'productService',
    function ($scope, $rootScope, productService) {
        $scope.active = _Active;

        productService.initCart();

        $scope.totalCart = function () {
            return productService.totalCart();
        }
        $scope.cart = productService.cart;

        $scope.currencies = _Currencies;
        $scope.currentCurrency = _CurrentCurrencyID;

        if (_CurrentCurrencyID == 0) {
            $scope.moneySymbol = $scope.currencies[0].MoneySymbol;
            $scope.currentCurrency = $scope.currencies[0];
        } else {
            angular.forEach($scope.currencies, function (item) {
                if (item.CurrencyID == _CurrentCurrencyID) {
                    $scope.moneySymbol = item.MoneySymbol;
                    $scope.currentCurrency = item;
                }
            });
        }
    }
]);