app.controller('productController', ['$scope', '$routeParams', '$location', 'productService', 'siteService', 'currencyService', 'toastrService',
    function ($scope, $routeParams, $location, productService, siteService, currencyService, toastrService) {
        $scope.get = function () {
            $scope.model = {
                ProductCultures: [],
                ProductCategories: [],
                ProductCurrencies: [],
                ProductTags: []
            };
            if ($routeParams.productID != null) {
                productService.getByID($routeParams.productID).then(function (data) {
                    $scope.model = data;
                    //CATEGORIES
                    angular.forEach($scope.cultures, function (item) {
                        var exist = false;
                        angular.forEach($scope.model.ProductCultures, function (itemPC) {
                            if (itemPC.CultureID == item.CultureID) {
                                exist = true;
                            }
                        });
                        if (!exist) {
                            $scope.model.ProductCultures.push({
                                CultureID: item.CultureID,
                                CultureName: item.Name,
                                ProductName: "",
                                ProductID: data.ProductID,
                                Adding: true
                            })
                        }
                    });
                    //CURRENCIES
                    angular.forEach($scope.currencies, function (item) {
                        var exist = false;
                        angular.forEach($scope.model.ProductCurrencies, function (itemPC) {
                            if (itemPC.CurrencyID == item.CurrencyID) {
                                exist = true;
                            }
                        });
                        if (!exist) {
                            $scope.model.ProductCurrencies.push({
                                CurrencyID: item.CurrencyID,
                                CurrencyName: item.CurrencyName,
                                ProductID: data.ProductID,
                                Adding: true
                            })
                        }
                    });
                });
            } else {
                angular.forEach($scope.cultures, function (item) {
                    $scope.model.ProductCultures.push({
                        CultureID: item.CultureID,
                        CultureName: item.Name,
                        ProductName: "",
                        Adding: true
                    });
                });
                angular.forEach($scope.currencies, function (item) {
                    $scope.model.ProductCurrencies.push({
                        CurrencyID: item.CurrencyID,
                        CurrencyName: item.CurrencyName,
                        Adding: true
                    });
                });
            }
        };
        //CATEGORIES
        $scope.loadCategories = function (query) {
            productService.getCategories(query).then(function (data) {
                $scope.categories = data;
            });
        };
        $scope.addCategory = function (item) {
            item.Adding = true;
            item.Deleting = false;
            $scope.model.ProductCategories.push(item);
            $scope.categories = [];
            $scope.queryCategories = "";
        };
        $scope.removeCategory = function (item) {
            item.Deleting = true;
            item.Adding = false;
        };
        //TAGS
        $scope.loadTags = function (query) {
            productService.getTags(query).then(function (data) {
                $scope.tags = data;
            });
        };
        $scope.addTag = function (item) {
            item.Adding = true;
            item.Deleting = false;
            $scope.model.ProductTags.push(item);
            $scope.tags = [];
            $scope.queryTags = "";
        };
        $scope.removeTag = function (item) {
            item.Deleting = true;
            item.Adding = false;
        };
        $scope.save = function () {
            if ($scope.model.ProductCategories != null && $scope.model.ProductCategories.length > 0) {
                productService.save($scope.model).then(function (data) {
                    var _ProductID = $scope.model.ProductID == 0 ? data : $scope.model.ProductID;
                    //UPLOAD FILES
                    if ($scope.model.Files && $scope.model.Files.length) {
                        for (var i = 0; i < $scope.model.Files.length; i++) {
                            var file = $scope.model.Files[i];
                            productService.uploadGallery(_ProductID, file).progress(function (evt) {
                                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                                //console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
                            }).success(function (data, status, headers, config) {
                                //console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
                                $scope.model.ProductGalleries.push(data);
                                $scope.model.Files = [];
                            });
                        }
                    }
                    
                });
            } else {
                toastrService.error("Tienes que agregar al menos una categoría");
            }
        };
        $scope.delete = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar este producto?")) {
                productService.remove(item.productID).then(function (data) {
                    $scope.get();
                    toastrService.success("Producto Eliminado");
                });
            }
        };
        $scope.deleteGallery = function (item) {
            if (confirm("¿Estas seguro que deseas eliminar esta imagen?")) {
                productService.removeGallery(item.ProductGalleryID).then(function (data) {
                    var index = $scope.model.ProductGalleries.indexOf(item);
                    $scope.model.ProductGalleries.splice(index, 1);
                });
            }
        };
        /*INIT*/
        $scope.init = function () {
            currencyService.get().then(function (data) {
                $scope.currencies = data;
                $scope.get();
            })
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }
    }
]);
