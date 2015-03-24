app.controller('tagController', ['$scope', 'tagService', 'siteService', 'toastrService',
    function ($scope, tagService, siteService, toastrService) {
        $scope.get = function () {
            tagService.get().then(function (data) {
                $scope.data = data;
            });
        }
        $scope.open = function (item) {
            if (item == null) {
                $scope.model = {};
            } else {
                $scope.model = angular.copy(item);
            }
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            tagService.save($scope.model).then(function (data) {
                $scope.get();
            });
            $('#modal-new').modal('hide');
        };
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar este Tag?")) {
                tagService.remove(item.TagID).then(function (data) {
                    $scope.get();
                    toastrService.success("Tag Eliminadp");
                });
            }
        };
        /*INIT*/
        $scope.init = function () {
            $scope.get();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
