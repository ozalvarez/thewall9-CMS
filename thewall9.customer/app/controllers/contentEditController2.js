app.controller('contentEditController2', ['$scope', '$location', 'toastrService', 'contentService', 'cultureService', 'siteService',
    function ($scope, $location, toastrService, contentService, cultureService, siteService) {
        //$(window).scroll(function () {
        //    if ($(this).scrollTop() > 100) {
        //        $scope.$apply(function () {
        //            $scope.showItemsFixed = true;
        //        });
        //    }
        //    else {
        //        $scope.$apply(function () {
        //            $scope.showItemsFixed = false;
        //        });
        //    }
        //});
        //var isAdmin = $location.search()["isAdmin"];
        //if (isAdmin != null && isAdmin) {
        //    $scope.isAdmin = true;
        //} else {
        //    $scope.isAdmin = false;
        //}
        $scope.getMenu = function () {
            contentService.getMenu().then(function (data) {
                $scope.menu = data;
            });
        };
        //$scope.get = function () {
        //    contentService.getTree().then(function (data) {
        //        $scope.data = data;
        //    });
        //};
        //$scope.save = function () {
        //    contentService.saveTree($scope.data).then(function (data) {
        //        toastrService.success("Cambios guardados exitosamente");
        //    });
        //}
        //$scope.duplicate = function (item) {
        //    contentService.duplicateTree(item).then(function (data) {
        //        console.log("DUPLICATING", data);
        //        // $scope.get();
        //        attach(data, $scope.data)
        //        toastrService.success("Nuevo " + item.Hint + " creado");
        //    });
        //};
        //function attach(newItem, tree) {
        //    angular.forEach(tree, function (item) {
        //        if (item.ContentPropertyID == newItem.ContentPropertyParentID) {
        //            item.Items.push(newItem);
        //        } else if (item.Items != null && item.Items.length > 0) {
        //            attach(newItem, item.Items);
        //        }
        //    });
        //}
        //$scope.delete = function (item) {
        //    if (confirm("¿Estás seguro que quieres eliminar esta propiedad?")) {
        //        contentService.remove(item).then(function (data) {
        //            $scope.get();
        //            toastrService.success("Propiedad eliminada");
        //        });
        //    }
        //};
        
        //$scope.enable = function (item, enabled) {
        //    contentService.enable(item.ContentPropertyID, enabled).then(function (response) {
        //        item.Enabled = enabled;
        //    });
        //};
        //$scope.upDown = function (item, upOrDown) {
        //    console.log("prio", item.Priority);
        //    var _index = (upOrDown ? (item.Priority - 1) : (item.Priority + 1));
        //    console.log("INDEX", _index);
        //    if (_index >= 0) {
        //        contentService.move(_index, item.ContentPropertyParentID, item.ContentPropertyID).then(function (data) {
        //            $scope.get();
        //            toastrService.success("Propiedad movida exitosamente");
        //        });
        //    }
        //}
        /*CULTURE*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        /*INIT*/
        $scope.init = function () {
            $scope.selectedCulture = cultureService.currentCulture;
            if ($scope.selectedCulture.CultureID) {
                $scope.getMenu();
            }
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
