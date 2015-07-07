app.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        if (scope.fileread == null) {
                            scope.fileread = {};
                        }
                        scope.fileread.FileContent = loadEvent.target.result;
                        scope.fileread.FileName = changeEvent.target.files[0].name;
                        scope.fileread.Edit = true;
                        scope.fileread.Deleting = false;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}]);
app.directive('attrIf', [function () {
    return {
        restrict: "A",
        link: function ($scope, $element, $attr) {
            $scope.$watch($attr.attrIf, function attrIfWatchAction(value) {
                for (var key in value) {
                    if (value[key]) {
                        $element.attr(key, true);
                    }
                }
            });
        }
    };
}]);