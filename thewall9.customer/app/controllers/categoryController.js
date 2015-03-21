app.controller('categoryController', ['$scope', 'categoryService', 'siteService',
    function ($scope, categoryService, siteService) {
        function createCategories(list) {
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
        $scope.openNew = function (itemParent, itemUpdate) {
            $scope.selectedCategory = {}
            if (itemParent != null) {
                angular.forEach($scope.categories, function (item) {
                    if (item.CategoryID == itemParent.CategoryID) {
                        $scope.selectedCategory = item;
                    }
                });
            }
            $scope.model = {
                CategoryParentID:$scope.selectedCategory.CategoryID
            };
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
