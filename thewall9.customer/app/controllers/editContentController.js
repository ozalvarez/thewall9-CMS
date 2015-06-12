app.controller('editContentController', ['$scope', '$location', 'toastrService', 'contentService', 'cultureService', 'siteService',
    function ($scope, $location, toastrService, contentService, cultureService, siteService) {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $scope.$apply(function () {
                    $scope.showSaveButton = true;
                });
            }
            else {
                $scope.$apply(function () {
                    $scope.showSaveButton = false;
                });
            }
        });
        var isAdmin = $location.search()["isAdmin"];
        if (isAdmin != null && isAdmin) {
            $scope.isAdmin = true;
        } else {
            $scope.isAdmin = false;
        }

        $scope.get = function () {
            contentService.getTree().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.save = function () {
            contentService.saveTree($scope.data).then(function (data) {
                toastrService.success("Cambios guardados exitosamente");
            });
        }
        $scope.duplicate = function (item) {
            contentService.duplicate(item).then(function (data) {
                $scope.get();
                toastrService.success("Nuevo " + item.Hint + " creado");
            });
        };
        $scope.delete = function (item) {
            if (confirm("¿Estás seguro que quieres eliminar esta propiedad?")) {
                contentService.remove(item).then(function (data) {
                    $scope.get();
                    toastrService.success("Propiedad eliminada");
                });
            }
        };
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        $scope.enable = function (item, enabled) {
            contentService.enable(item.ContentPropertyID, enabled).then(function (response) {
                item.Enabled = enabled;
            });
        };
        /*INIT*/
        $scope.init = function () {
            $scope.get();
            $scope.selectedCulture = cultureService.currentCulture;
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
