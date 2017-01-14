'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings',
    function ($scope, $location, authService, ngAuthSettings) {
        var AppPath = APP.ApplicationPath + APP.AppInternUrl;
        if (authService.authentication.isAuth) {
            window.location = AppPath;
        }
        $scope.message = "";

        $scope.login = function () {
            /*THIS LINE IS FOR FIX BUG ON AUTOCOMPLETE FORM*/
            $scope.$broadcast("autofill:update");
            authService.login($scope.loginData).then(function (response) {
                window.location = AppPath;
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    }
]);
