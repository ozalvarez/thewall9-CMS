app.controller('blogPostController', ['$scope', '$routeParams', '$location', 'blogService'
    , 'siteService', 'toastrService', 'cultureService', 'blockUI',
    function ($scope, $routeParams, $location, blogService, siteService, toastrService
        , cultureService, blockUI) {

        $scope.tinymceOptions = {
            plugins: ["advlist autolink lists link image charmap print preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime media table contextmenu paste"
            ],
            toolbar: "fontsizeselect  | insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
            extended_valid_elements: "iframe[src|width|height|name|align]",
            format: null
        };
        $scope.blogPostID = $routeParams.blogPostID;
        $scope.get = function () {
            if ($scope.blogPostID != null) {
                blogService.getByID($scope.blogPostID).then(function (data) {
                    $scope.model = data;
                    $scope.mediaList = data.ImagesFileRead;
                    angular.forEach($scope.model.Categories, function (myCAT) {
                        angular.forEach($scope.categories, function (allCAT) {
                            if (allCAT.BlogCategoryID == myCAT.BlogCategoryID) {
                                allCAT.Enabled = true
                            }
                        });
                    });
                });
            } else {
                $scope.model = {
                    Tags: []
                }
            }
        };

        $scope.save = function (Published) {
            $scope.model.Published = Published;
            blogService.save($scope.model).then(function (data) {
                toastrService.success("Post Guardado");
                if ($scope.blogPostID == null) {
                    $location.path("/blog/post/" + data);
                } else {
                    $scope.init();
                }
            });
        };
        //CATEGORIES
        $scope.getCategories = function () {
            return blogService.getCategories().then(function (data) {
                $scope.categories = data;
                $scope.get();
            });
        };
        $scope.selectCategory = function (item) {
            var _myCAT = null;
            angular.forEach($scope.model.Categories, function (myCAT) {
                if (myCAT.BlogCategoryID == item.BlogCategoryID) {
                    item.Enabled = !item.Enabled;
                    _myCAT = myCAT;
                    _myCAT.Deleting = !item.Enabled;
                    _myCAT.Adding = item.Enabled;
                }
            });
            if (_myCAT == null) {
                item.Enabled = true;
                $scope.model.Categories.push({
                    BlogCategoryID: item.BlogCategoryID,
                    Adding: true,
                    Deleting: false
                });
            }
        }

        //TAGS
        $scope.getTags = function ($query) {
            blockUI.noOpen = true;
            return blogService.getTags($query)
        };

        /*INIT*/
        $scope.updateCulture = function () {
            cultureService.currentCulture = $scope.selectedCulture;
            $scope.init();
        }
        $scope.init = function () {
            $scope.selectedCulture = cultureService.currentCulture;
            $scope.getCategories();
        };
        $scope.$on('initDone', function (event) {
            $scope.init();
        });
        if (siteService.sitesLoaded) {
            if (cultureService.currentCulture.CultureID) {
                $scope.init();
            }
        }
    }
]);
