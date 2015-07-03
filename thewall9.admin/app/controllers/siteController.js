'use strict';
app.controller('siteController', ['$scope', 'siteService', 'toastrService',
    function ($scope, siteService, toastrService) {
        $scope.get = function () {
            siteService.get().then(function (data) {
                $scope.data = data;
            });
        };
        $scope.get();
        $scope.new = function (item) {
            $scope.c = { Cultures: [] };
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.edit = function (item) {
            $scope.c = item;
            $('#modal-new').modal({
                backdrop: true
            });
        };
        $scope.addCulture = function (CultureName) {
            $scope.c.CultureName = "";
            $scope.c.Cultures.push({
                Name: CultureName
            });
        };
        $scope.removeCulture = function (item) {
            var index = $scope.c.Cultures.indexOf(item)
            $scope.c.Cultures.splice(index, 1);
        };
        $scope.save = function () {
            siteService.save($scope.c).then(function (data) {
                $scope.get();
                $('#modal-new').modal('hide');
                toastrService.success("Customer Saved");
            });
        };
        $scope.remove = function (item) {
            if (confirm("Are you sure?")) {
                siteService.remove(item.SiteID).then(function (data) {
                    $scope.get();
                    toastrService.success("Deleted");
                });
            }
        };
        $scope.enable = function (item, enable) {
            siteService.enable(item.SiteID, enable).then(function (data) {
                item.Enabled = enable;
                $scope.get();
                if (item.Enabled)
                    toastrService.success("Enabled");
                else
                    toastrService.success("Disabled");
            });
        };
        $scope.enableECommerce = function (item, enable) {
            siteService.enableECommerce(item.SiteID, enable).then(function (data) {
                item.ECommerce = enable;
                $scope.get();
                if (item.ECommerce)
                    toastrService.success("Enabled");
                else
                    toastrService.success("Disabled");
            });
        };
        $scope.enableBlog = function (item, enable) {
            siteService.enableBlog(item.SiteID, enable).then(function (data) {
                item.Blog= enable;
                $scope.get();
                if (item.Blog)
                    toastrService.success("Enabled");
                else
                    toastrService.success("Disabled");
            });
        };
        $scope.openAddUser = function (item) {
            $scope.u = {
                SiteID: item.SiteID
            };
            $('#modal-add-user').modal({
                backdrop: true
            });
        };
        $scope.addUser = function () {
            siteService.addUser($scope.u).then(function (data) {
                $('#modal-add-user').modal('hide');
                toastrService.success("Usuario Agregado");
            });
        };
        $scope.openViewUser = function (item) {
            $scope.SiteIDSelected = item.SiteID
            siteService.getUsers(item.SiteID).then(function (data) {
                $scope.users = data;
                $('#modal-view-user').modal({
                    backdrop: true
                });
            });
        };
        $scope.addRol = function (item, siteUserType, enable) {
            siteService.addRol(item.UserID, siteUserType, enable, $scope.SiteIDSelected).then(function (data) {
                siteService.getUsers($scope.SiteIDSelected).then(function (data) {
                    $scope.users = data;
                    toastrService.success("Rol Cambiado");
                });
            });
        };
        $scope.removeUser = function (item) {
            if (confirm("Are you sure?")) {
                siteService.removeUser(item.UserID, $scope.SiteIDSelected).then(function (data) {
                    siteService.getUsers($scope.SiteIDSelected).then(function (data) {
                        $scope.users = data;
                        toastrService.success("User Deleted");
                    });
                });
            }
        };

        /*IMPORT/EXPORT/DUPLICATE*/
        $scope.fileImport = {};
        $scope.export = function (item) {
            siteService.exportSite(item.SiteID).then(function (data) {
                window.open(data.Url, '_blank', '');
            });
        };
        $scope.import = function () {
            siteService.importSite($scope.fileImport).then(function (data) {
                toastrService.success("Site Creado");
                $scope.get();
            });
        };
        $scope.duplicate = function (item) {
            siteService.duplicate(item.SiteID).then(function (data) {
                toastrService.success("Site Duplicado");
                $scope.get();
            });
        };
    }
]);