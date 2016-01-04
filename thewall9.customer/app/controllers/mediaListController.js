app.controller('mediaListController', ['$scope', '$routeParams', '$location', 'siteService', 'toastrService','mediaService',
    function ($scope, $routeParams, $location, siteService, toastrService, mediaService) {
        $scope.removeMedia = function (item) {
            item.Deleting = true;
            item.Adding = false;
            if (typeof $scope.deleteMedia == 'function') {
                $scope.deleteMedia(item);
            }
        }
        $scope.addMedia = function () {
            $scope.mediaModel.SiteID = siteService.site.SiteID;
            mediaService.upload($scope.mediaModel).then(function (data) {
                data.Adding = true;
                $scope.mediaList.push(data);
            });
            
        }
    }
]);
