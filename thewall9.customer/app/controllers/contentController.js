app.controller('contentController', ['$scope', 'toastrService', 'contentService', 'cultureService', 'siteService',
    function ($scope, toastrService, contentService, cultureService, siteService) {
        $scope.properties = [{
            TypeName: 'IMG',
            TypeID: 1
        }, {
            TypeName: 'TXT',
            TypeID: 2
        },
        {
            TypeName: 'LIST',
            TypeID: 3
        },
        {
            TypeName: 'HTML',
            TypeID: 4
        }];
        $scope.fileImport = {}
        $scope.selectedItem = {};

        $scope.get = function () {
            $scope.cultures = cultureService.cultures;
            contentService.get().then(function (data) {
                $scope.data = data;
            });
        };

        $scope.options = {
            dropped: function (event) {
                var sourceNode = event.source.nodeScope;
                var destNodes = event.dest.nodesScope;
                var index = event.dest.index;
                var ContentPropertyParentID = (destNodes.$nodeScope != null) ? destNodes.$nodeScope.$modelValue.ContentPropertyID : 0;
                contentService.move(index, ContentPropertyParentID, sourceNode.$modelValue.ContentPropertyID).then(function (data) {
                    toastrService.success("Propiedad movida exitosamente");
                });
            }
        };
        $scope.removeContent = function (scope) {
            if (confirm("¿Estás seguro que quieres eliminar esta propiedad? también se eliminarán las propiedades hijas")) {
                contentService.remove(scope.$modelValue).then(function (data) {
                    scope.remove();
                    toastrService.success("Propiedad y propiedades hijas eliminadas");
                });
            }
        };
        $scope.toggle = function (scope) {
            scope.toggle();
        };

        $scope.openNew = function (scope) {
            $scope.content = {
                ContentPropertyTypeOptions: $scope.properties[2]
            };
            if (scope != null) {
                $scope.contentParent = scope.$modelValue;
                $scope.content.ContentPropertyParentID = $scope.contentParent.ContentPropertyID;
            } else {
                $scope.contentParent = null;
            }
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.edit = function (item) {
            $scope.content = item;
            angular.forEach($scope.properties, function (item) {
                if (item.TypeID == $scope.content.ContentPropertyType) {
                    $scope.content.ContentPropertyTypeOptions = item;
                }
            });
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.create = function () {
            $scope.content.ContentPropertyType = $scope.content.ContentPropertyTypeOptions.TypeID
            contentService.save($scope.content).then(function (data) {
                if ($scope.content.ContentPropertyID == null) {
                    if ($scope.contentParent == null) {
                        $scope.data.push({
                            ContentPropertyID: data,
                            ContentPropertyAlias: $scope.content.ContentPropertyAlias,
                            ContentPropertyParentID: 0,
                            ContentPropertyType: $scope.content.ContentPropertyType,
                            Items: [],
                            Priority: $scope.data[$scope.data.length - 1].Priority + 1
                        });
                    } else {
                        var _Priority = 0;
                        if ($scope.contentParent.Items.length != 0) {
                            _Priority = $scope.contentParent.Items[$scope.contentParent.Items.length - 1].Priority + 1
                        }
                        $scope.contentParent.Items.push({
                            ContentPropertyID: data,
                            ContentPropertyAlias: $scope.content.ContentPropertyAlias,
                            ContentPropertyParentID: $scope.contentParent.ContentPropertyID,
                            ContentPropertyType: $scope.content.ContentPropertyType,
                            Items: [],
                            Priority: _Priority
                        });
                    }
                }
                $('#modal-new').modal('hide');
                toastrService.success("Propiedad Creada");
            });

        };
        $scope.duplicate = function (scope, item) {
            var _parent = $scope.data;
            if (scope.$parentNodeScope != null) {
                _parent = scope.$parentNodeScope.$modelValue.Items;
            }
            contentService.duplicate(item).then(function (data) {
                _parent.push(data);
                toastrService.success("Propiedades duplicadas");
            });
        };
        //IMPORT/EXPORT
        $scope.fileImport = {};
        $scope.export = function (item) {
            contentService.exportContent(item == null ? 0 : item.ContentPropertyID).then(function (data) {
                window.open(data.Url, '_blank', '');
            });
        }
        $scope.import = function (item) {
            contentService.importContent(item == null ? 0 : item.ContentPropertyID, item.FileImport).then(function (data) {
                if (item == null) {
                    $scope.fileImport = null;
                } else {
                    item.FileImport == null;
                }
                toastrService.success("Propiedades creadas exitosamente, cargando nuevas propiedades..");
                $scope.get();
            });
        };

        $scope.lock = function (item, enabled) {
            contentService.lock(item.ContentPropertyID, enabled).then(function (response) {
                item.Lock = enabled;
            });
        };

        $scope.lockAll = function () {
            contentService.lockAll().then(function (response) {
                $scope.get();
            });
        };
        $scope.showInContent = function (item, enabled) {
            contentService.showInContent(item.ContentPropertyID, enabled).then(function (response) {
                item.ShowInContent = enabled;
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
