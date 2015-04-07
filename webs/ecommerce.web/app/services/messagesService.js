'use strict';
app.factory('messagesService', [
    function () {
        var productFactory = {};

        productFactory.get = function (alias) {
            var _R = "";
            angular.forEach(_Messages, function (item) {
                if (item.Alias == alias) {
                    _R = item.Value;
                }
            });
            return _R;
        }
        return productFactory;
    }
]);
