'use strict';
app.controller('pagesController', ['$scope', 'toastrService', 'pageService',  'siteService',
    function ($scope, toastrService, pageService, siteService) {
        $scope.init = function () {
            pageService.get().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            $scope.init();
        }

        $scope.selectedItem = {};

        $scope.options = {
            dropped: function (event) {
                var sourceNode = event.source.nodeScope;
                var destNodes = event.dest.nodesScope;
                var index = event.dest.index;
                var parentID = (destNodes.$nodeScope != null) ? destNodes.$nodeScope.$modelValue.PageID : 0;
                pageService.move(index, parentID, sourceNode.$modelValue.PageID).then(function (data) {
                    toastrService.success("Página movida exitosamente");
                });
            }
        };
        $scope.removePage = function (scope) {
            if (confirm("¿Estás seguro que quieres eliminar esta sección? también se eliminarán las secciones hijas")) {
                pageService.remove(scope.$modelValue).then(function (data) {
                    scope.remove();
                    toastrService.success("Página y páginas hijas eliminados");
                });
            }
        };
        $scope.toggle = function (scope) {
            scope.toggle();
        };
        $scope.openNew = function (scope) {
            $scope.page = {};
            if (scope != null) {
                $scope.pageParent = scope.$modelValue;
                $scope.page.PageParentID = $scope.pageParent.PageID;
                $scope.page.PageParentAlias = $scope.pageParent.Alias;
            } else {
                $scope.pageParent = null;
            }
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.createPage = function () {
            pageService.save($scope.page).then(function (data) {
                if ($scope.pageParent == null) {
                    $scope.data.push({
                        PageID: data,
                        Alias: $scope.page.Alias,
                        PageParentID: $scope.page.PageParentID,
                        Items: []
                    });
                } else {
                    $scope.pageParent.Items.push({
                        PageID: data,
                        Alias: $scope.page.Alias,
                        PageParentID: $scope.page.PageParentID,
                        Items: []
                    });
                }
                $('#modal-new').modal('hide');
                toastrService.success("Página Creada");
            });

        };
        $scope.publish = function (page, published) {
            pageService.publish(page.PageID, published).then(function (data) {
                page.Published = published;
            });
        };
        $scope.inMenu = function (page, published) {
            pageService.inMenu(page.PageID, published).then(function (data) {
                page.InMenu = published;
            });
        };
    }]);