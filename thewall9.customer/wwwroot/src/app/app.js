var app = angular.module('app',
    ['ngRoute', 'LocalStorageModule', 'blockUI', 'ui.tree', 'ngFileUpload'
        , 'ui.tinymce', 'ui.bootstrap']);

app.config(["$routeProvider", "blockUIConfig", function ($routeProvider, blockUIConfig) {
    $routeProvider.when("/", {
        redirectTo: '/content/edit2'
    });
    $routeProvider.when('/content/edit2/:ContentPropertyID?', {
        controller: 'contentEditController2',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/content-edit2.html'
    });

    $routeProvider.when("/configuration", {
        controller: "configurationController",
        templateUrl: APP.ApplicationPath + "/wwwroot/views/configuration.html"
    });
    $routeProvider.when("/pages", {
        controller: "pagesController",
        templateUrl: APP.ApplicationPath + "/wwwroot/views/pages.html"
    });
    $routeProvider.when('/page/edit/:pageID', {
        controller: 'pageDetailController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/page-detail.html'
    });
    $routeProvider.when('/content', {
        controller: 'contentController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/content.html'
    });
    $routeProvider.when("/password", {
        controller: "passwordController",
        templateUrl: APP.ApplicationPath + "/wwwroot/views/password.html"
    });
    $routeProvider.when('/content/edit', {
        controller: 'contentEditController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/content-edit.html'
    });


    $routeProvider.when('/categories', {
        controller: 'categoryController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/categories.html'
    });
    $routeProvider.when('/currencies', {
        controller: 'currencyController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/currencies.html'
    });
    $routeProvider.when('/tags', {
        controller: 'tagController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/tags.html'
    });
    $routeProvider.when('/products/:CategoryID?', {
        controller: 'productsController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/products.html'
    });

    $routeProvider.when('/product/:productID?', {
        controller: 'productController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/product.html'
    });
    $routeProvider.when('/orders', {
        controller: 'ordersController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/orders.html'
    });
    $routeProvider.when('/brands', {
        controller: 'brandsController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/brands.html'
    });

    //BLOG
    $routeProvider.when('/blog', {
        controller: 'blogController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/blog.html'
    });
    $routeProvider.when('/blog/post/:blogPostID?', {
        controller: 'blogPostController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/blogPost.html'
    });
    $routeProvider.when('/blog/categories?', {
        controller: 'blogCategoryController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/blogCategory.html'
    });

    //FILES & IMAGES
    $routeProvider.when('/media', {
        controller: 'mediaController',
        templateUrl: APP.ApplicationPath + '/wwwroot/views/media.html'
    });

    $routeProvider.otherwise({ redirectTo: "/" });

    // Change the default overlay message
    blockUIConfig.message = 'Cargando...';

    //// Change the default delay to 100ms before the blocking is visible
    blockUIConfig.delay = 100;
    blockUIConfig.autoBlock = false;

}]);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


