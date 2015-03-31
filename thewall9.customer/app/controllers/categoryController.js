app.controller('categoryController', ['$scope', 'categoryService', 'siteService', 'toastrService',
    function ($scope, categoryService, siteService, toastrService) {
        function createCategories(list, prefix) {
            angular.forEach(list, function (item) {
                $scope.categories.push(angular.copy(item));
                createCategories(item.CategoryItems);
            });
        }
        $scope.get = function () {
            categoryService.get().then(function (data) {
                $scope.categories = [];
                createCategories(data);
                $scope.data = data;
            });
        }
        $scope.openNew = function (itemParent) {
            $scope.selectedCategory = {}
            if (itemParent != null) {
                angular.forEach($scope.categories, function (item) {
                    if (item.CategoryID == itemParent.CategoryID) {
                        $scope.selectedCategory = item;
                    }
                });
            }
            $scope.model = {
                CategoryParentID: $scope.selectedCategory.CategoryID,
                CategoryCultures:[]
            };
            angular.forEach($scope.cultures, function (item) {
                $scope.model.CategoryCultures.push({
                    CultureID: item.CultureID,
                    CultureName: item.Name,
                    CategoryName: ""
                });
            });
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.edit = function (item) {
            $scope.selectedCategory = {}
            if (item != null) {
                angular.forEach($scope.categories, function (item2) {
                    if (item2.CategoryID == item.CategoryParentID) {
                        $scope.selectedCategory = item2;
                    }
                });
            }
            $scope.model = item;
            angular.forEach($scope.cultures, function (itemC) {
                var exist = false;
                angular.forEach($scope.model.CategoryCultures, function (itemCC) {
                    if (itemCC.CultureID == itemC.CultureID) {
                        exist = true;
                    }
                });
                if (!exist) {
                    $scope.model.CategoryCultures.push({
                        CultureID: itemC.CultureID,
                        CultureName: itemC.Name,
                        CategoryName: "",
                        CategoryID: item.CategoryID,
                        Adding: true,
                        FriendlyUrl:item.FriendlyUrl
                    })
                }
            });
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.save = function () {
            categoryService.save($scope.model).then(function (data) {
                $scope.get();
            });
            $('#modal-new').modal('hide');
        };
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar esta categoría?")) {
                categoryService.remove(item.CategoryID).then(function (data) {
                    $scope.get();
                    toastrService.success("Categoría Eliminada");
                });
            }
        };
        $scope.up = function (item, up) {
            categoryService.up(item.CategoryID, up).then(function (data) {
                $scope.get();
            });
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
