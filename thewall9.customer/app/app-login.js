
var app = angular.module('AngularAuthApp', ['LocalStorageModule', 'blockUI']);

app.config([ "blockUIConfig", function ( blockUIConfig) {
    // Change the default overlay message
    blockUIConfig.message = 'Cargando...';

    // Change the default delay to 100ms before the blocking is visible
    blockUIConfig.delay = 100;
}]);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});


app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);