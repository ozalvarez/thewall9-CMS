var app = angular.module('AngularAuthApp',
    ['ngRoute', 'LocalStorageModule', 'blockUI', 'ui.tree']);

app.config(["$routeProvider", "blockUIConfig", function ($routeProvider, blockUIConfig) {

    $routeProvider.when("/", {
        redirectTo:'/edit-content'
    });

    $routeProvider.when("/configuration", {
        controller: "configurationController",
        templateUrl: "/app/views/configuration.html"
    });
    $routeProvider.when("/pages", {
        controller: "pagesController",
        templateUrl: "/app/views/pages.html"
    });
    $routeProvider.when('/page/edit/:pageID', {
        controller: 'pageDetailController',
        templateUrl: '/app/views/page-detail.html'
    });
    $routeProvider.when('/content', {
        controller: 'contentController',
        templateUrl: '/app/views/content.html'
    });
    $routeProvider.when("/password", {
        controller: "passwordController",
        templateUrl: "/app/views/password.html"
    });
    $routeProvider.when('/edit-content', {
        controller: 'editContentController',
        templateUrl: '/app/views/edit-content.html'
    });
    $routeProvider.when('/categories', {
        controller: 'categoryController',
        templateUrl: '/app/views/categories.html'
    });

    $routeProvider.otherwise({ redirectTo: "/" });

    // Change the default overlay message
    blockUIConfig.message = 'Cargando...';

    // Change the default delay to 100ms before the blocking is visible
    blockUIConfig.delay = 100;
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


