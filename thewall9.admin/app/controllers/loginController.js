'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings',
    function ($scope, $location, authService, ngAuthSettings) {
        var _APPUrl = "/intern";
        if (authService.authentication.isAuth) {
            window.location = _APPUrl;
        }
        $scope.message = "";

        $scope.login = function () {
            /*THIS LINE IS FOR FIX BUG ON AUTOCOMPLETE FORM*/
            $scope.$broadcast("autofill:update");
            authService.login($scope.loginData).then(function (response) {
                window.location = _APPUrl;
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    }
]);
