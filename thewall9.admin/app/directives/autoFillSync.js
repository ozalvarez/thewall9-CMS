'use strict';
app.directive('autoFillSync', ['$timeout', function ($timeout) {
    return {
        restrict: "A",
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            scope.$on("autofill:update", function () {
                ngModel.$setViewValue(element.val());
            });
        }
    };
}]);