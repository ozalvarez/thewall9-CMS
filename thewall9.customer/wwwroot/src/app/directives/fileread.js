app.directive("imgRead", [function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            datasource: '='
        },
        link: function (scope, element, attributes) {
            var fileInput = element.find('input');
            
            fileInput.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        if (scope.datasource == null) {
                            scope.datasource = {};
                        }
                        scope.datasource.FileContent = loadEvent.target.result;
                        scope.datasource.FileName = changeEvent.target.files[0].name;
                        scope.datasource.Deleting = false;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        },
        template:
        '<div><div class="btn btn-default btn-file">'
            + '<span class="glyphicon glyphicon-open"></span> Open Image'
            + '<input type="file" />'
        + '</div>'
        + '<div class="container-btns">'
            + '<img ng-src="{{datasource.FileContent || datasource.FileUrl}}"'
            + 'ng-show="(datasource.FileContent || datasource.FileUrl) && !datasource.Deleting"'
            + 'class="img-responsive img-thumbnail" />'
            + '<button class="btn btn-danger"'
            + 'ng-show="(datasource.FileContent || datasource.FileUrl) && !datasource.Deleting"'
            + 'ng-click="datasource.Deleting=true;" type="button"><i class="fa fa-remove"></i>'
            + '</button>'
       + '</div></div>'
    }
}]);
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