app.controller('contentEditController2', ['$scope', '$routeParams', '$location', 'toastrService', 'contentService', 'cultureService', 'siteService','mediaService',
    function ($scope, $routeParams, $location, toastrService, contentService, cultureService, siteService, mediaService) {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $scope.$apply(function () {
                    $scope.showItemsFixed = true;
                });
            }
            else {
                $scope.$apply(function () {
                    $scope.showItemsFixed = false;
                });
            }
        });
        var isAdmin = $location.search()["isAdmin"];
        if (isAdmin != null && isAdmin) {
            $scope.isAdmin = true;
        } else {
            $scope.isAdmin = false;
        }

        $scope.tinymceOptions = {
            plugins: ["advlist autolink lists link image charmap print preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime media table contextmenu paste"
            ],
            toolbar: "mybutton insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
            extended_valid_elements: "iframe[src|width|height|name|align]",
            format: null
        };

        $scope.getMenu = function () {
            if ($scope.menu == null) {
                contentService.getMenu().then(function (data) {
                    if (data.length > 0) {
                        $scope.menu = data;
                        var ID = $routeParams.ContentPropertyID == null ? $scope.menu[0].ContentPropertyID : $routeParams.ContentPropertyID;
                        $scope.get(ID);
                    }
                });
            } else {
                $scope.get($routeParams.ContentPropertyID);
            }
        };
        $scope.get = function (ContentPropertyIDSelected) {
            if (ContentPropertyIDSelected == null) {
                ContentPropertyIDSelected = $scope.menuSelected.ContentPropertyID;
            }
            angular.forEach($scope.menu, function (item) {
                if (item.ContentPropertyID == ContentPropertyIDSelected) {
                    item.Selected = true;
                    item.Items = [];
                    $scope.menuSelected = item;
                } else {
                    item.Selected = false;
                }
            });
            contentService.getTreeByProperty(ContentPropertyIDSelected).then(function (data) {
                $scope.data = data;
            });
        };
        $scope.save = function () {
            contentService.saveTree($scope.data).then(function (data) {
                if ($scope.menuSelected.Edit) {
                    contentService.saveTree([$scope.menuSelected]).then(function (data) {
                        toastrService.success("Cambios guardados exitosamente");
                    });
                } else {
                    toastrService.success("Cambios guardados exitosamente");
                }
            });
        }
        $scope.duplicate = function (item) {
            contentService.duplicateTree(item).then(function (data) {
                if ($scope.data[0].ContentPropertyParentID == data.ContentPropertyParentID)
                    $scope.data.push(data);
                else
                    attach(data, $scope.data);
                toastrService.success("Nuevo " + item.Hint + " creado");
            });
        };
        function attach(newItem, tree) {
            angular.forEach(tree, function (item) {
                if (item.ContentPropertyID == newItem.ContentPropertyParentID) {
                    item.Items.push(newItem);
                } else if (item.Items != null && item.Items.length > 0) {
                    attach(newItem, item.Items);
                }
            });
        }
        $scope.delete = function (item) {
            if (confirm("¿Estás seguro que quieres eliminar esta propiedad?")) {
                contentService.remove(item).then(function (data) {
                    deleteFromTree(item, $scope.data);
                    toastrService.success("Propiedad eliminada");
                });
            }
        };
        function deleteFromTree(newItem, tree) {
            angular.forEach(tree, function (item, index) {
                if (item.ContentPropertyID == newItem.ContentPropertyID) {
                    tree.splice(index, 1);
                } else if (item.Items != null && item.Items.length > 0) {
                    deleteFromTree(newItem, item.Items);
                }
            });
        }
        $scope.enable = function (item, enabled) {
            contentService.enable(item.ContentPropertyID, enabled).then(function (response) {
                item.Enabled = enabled;
            });
        };
        $scope.upDown = function (item, upOrDown) {
            var _index = (upOrDown ? (item.Priority - 1) : (item.Priority + 1));
            //console.log("INDEX", _index);
            if (_index >= 0) {
                contentService.move(_index, item.ContentPropertyParentID, item.ContentPropertyID).then(function (data) {
                    $scope.get();
                    toastrService.success("Propiedad movida exitosamente");
                });
            }
        }
        $scope.edit = function (item) {
            $scope.modelToEdit = item;
            $('#modal-edit').modal({
                backdrop: true
            });
        }
        // Prevent bootstrap dialog from blocking focusin
        $(document).on('focusin', function (e) {
            if ($(e.target).closest(".mce-window").length) {
                e.stopImmediatePropagation();
            }
        });
        $scope.openImages = function () {
            mediaService.get().then(function (data) {
                $scope.mediaList = data;
                $('#modal-images').modal({
                    backdrop: false
                });
            });          
        }

        /*CULTURE*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        /*INIT*/
        $scope.init = function () {
            $scope.selectedCulture = cultureService.currentCulture;
            $scope.menu = null;
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
