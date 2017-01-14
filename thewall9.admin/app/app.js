var app = angular.module('app',['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(["$routeProvider", function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: APP.ApplicationPath + "/app/views/home.html"
    });
    $routeProvider.when("/sites", {
        controller: "siteController",
        templateUrl: APP.ApplicationPath + "/app/views/sites.html"
    });
    $routeProvider.otherwise({ redirectTo: "/" });
}]);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: APIURL,
    clientId: 'app'
});

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


