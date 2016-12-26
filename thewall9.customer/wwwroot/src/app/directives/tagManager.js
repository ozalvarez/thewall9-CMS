app.directive('tagManager', function () {
    return {
        restrict: 'E',
        scope: { tags: '=', autocomplete: '=' },
        templateUrl: '/wwwroot/src/app/directives/_tagManager.html',
        link: function ($scope, $element, $attributes) {
            // FIXME: this is lazy and error-prone
            var input = angular.element($element.children()[1]);
            // This adds the new tag to the tags array
            $scope.add = function (value) {
                if (value == null) {
                    eval('value = {' + $attributes.keyproperty + ':0}');
                } else {
                    $scope.new_value = eval('value.' + $attributes.valueproperty);
                }
                var _item = null;
                angular.forEach($scope.tags, function (item) {
                    if (eval('item.' + $attributes.valueproperty) == $scope.new_value) {
                        _item = item
                    }
                });
                if (_item == null) {
                    eval('$scope.tags.push({'
                        + $attributes.valueproperty + ': $scope.new_value,'
                        + 'Adding:true,'
                        + 'Deleting:false,'
                        + $attributes.keyproperty + ':value.' + $attributes.keyproperty
                    + '})');
                } else {
                    _item.Adding = true;
                    _item.Deleting = false;
                }
                $scope.new_value = "";
                $scope.tagList = null;
            };
            $scope.getValue = function (item) {
                return eval('item.' + $attributes.valueproperty);
            }

            $scope.change = function () {
                if ($scope.new_value.length > 1) {
                    $scope.autocomplete($scope.new_value).then(function (data) {
                        $scope.tagList = data;
                    });
                }
            }
            // This is the ng-click handler to remove an item
            $scope.remove = function (item) {
                //$scope.tags.splice(item, 1);
                if (item.Deleting == null) {
                    item.Deleting = true;
                    item.Adding = false;
                } else {
                    item.Deleting = !item.Deleting;
                    item.Adding = !item.Adding;
                }
            };

            // Capture all keypresses
            input.bind('keypress', function (event) {
                // But we only care when Enter was pressed
                if (event.keyCode == 13) {
                    // There's probably a better way to handle this...
                    $scope.$apply($scope.add(null));
                    event.preventDefault();
                }
            });
        }
    };
});