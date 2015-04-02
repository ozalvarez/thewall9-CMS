'use strict';
app.factory('productService', ["$myhttp", "$q", 'localStorageService',
    function ($http, $q, localStorageService) {
        var uri = 'api/product';
        var uriOrder = 'api/order';
        var productFactory = {};

        productFactory.cart = [];

        productFactory.initCart = function () {
            var _Cart = localStorageService.get('_cart');
            productFactory.cart = _Cart == null ? [] : _Cart;
        }

        productFactory.add = function (model, number) {
            var added = false;
            angular.forEach(productFactory.cart, function (item) {
                if (item.ProductID == model.ProductID) {
                    item.Number += number;
                    added = true;
                }
            });
            if (!added) {
                productFactory.cart.push({
                    ProductID: model.ProductID,
                    ProductName: model.ProductName,
                    Number: number,
                    Price: model.Price,
                    IconPath: model.IconPath
                });
            }
            localStorageService.set('_cart', productFactory.cart);
        };
        productFactory.totalCart = function () {
            var _Total = 0;
            angular.forEach(productFactory.cart, function (item) {
                _Total += (item.Number * item.Price);
            });
            return _Total;
        }
        productFactory.removeCart = function (item) {
            var index = productFactory.cart.indexOf(item);
            productFactory.cart.splice(index, 1);
            localStorageService.set('_cart', productFactory.cart);
        }
        productFactory.updateNumber = function (item, number) {
            if (item.Number > 0) {
                item.Number += number;
                localStorageService.set('_cart', productFactory.cart);
            }
        }
        productFactory.sendOrder = function () {
            var deferred = $q.defer();
            $http.post(_ServiceBase + uriOrder, {
                CurrencyID: _CurrentCurrencyID,
                SiteID:_SiteID,
                Products: productFactory.cart
            }).then(function (data) {
                productFactory.cart = [];
                localStorageService.remove('_cart');
                deferred.resolve(data);
            });
            return deferred.promise;
        }
        return productFactory;
    }
]);