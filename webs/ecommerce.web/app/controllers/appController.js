'use strict';
app.controller('appController', ['$scope', '$rootScope', 'productService', 'searchService',
    function ($scope, $rootScope, productService, searchService) {
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

        $scope.search = function (query) {
            if (query.length > 3) {
                searchService.get(query, 6).then(function (data) {
                    $scope.searchData = data;
                });
            }
        };
        $scope.enter = function (keyEvent) {
            if (keyEvent.which === 13) {
                if ($scope.searchData.length > 0) {
                    window.location="/p/" + $scope.searchData[0].FriendlyUrl;
                }
            }
        }
    }
]);