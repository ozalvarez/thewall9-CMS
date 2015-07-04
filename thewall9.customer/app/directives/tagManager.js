app.directive('tagManager', function () {
    return {
        restrict: 'E',
        scope: { tags: '=' },
        template:
            '<div>' +
                '<a ng-repeat="item in tags" class="" ng-click="remove(item)">'
                    + '{{item}}</a>' +
            '</div>' 
           +'<div class="input-group">'
            + '<input type="text" class="form-control" placeholder="Add Tag" ng-model="new_value">'
            + '<span class="input-group-btn">'
                + '<button class="btn btn-default" type="button" ng-click="add()">Add</button>'
            + '</span>'
           +'</div>',
        link: function ($scope, $element, $attributes) {
            // FIXME: this is lazy and error-prone
            var input = angular.element($element.children()[1]);
            console.log($attributes.keyvalue);
            // This adds the new tag to the tags array
            $scope.add = function () {
                eval('$scope.tags.push({'
                    + $attributes.valueproperty + ': $scope.new_value'
                + '});');
                $scope.new_value = "";
            };

            // This is the ng-click handler to remove an item
            $scope.remove = function (item) {
                $scope.tags.splice(item, 1);
            };

            // Capture all keypresses
            input.bind('keypress', function (event) {
                // But we only care when Enter was pressed
                if (event.keyCode == 13) {
                    // There's probably a better way to handle this...
                    $scope.$apply($scope.add);
                }
            });
        }
    };
});