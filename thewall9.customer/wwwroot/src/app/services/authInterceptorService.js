'use strict';
app.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', 'blockUI',
    function ($q, $injector, $location, localStorageService, blockUI) {

        var authInterceptorServiceFactory = {};

        authInterceptorServiceFactory.request = function (config) {
            if (blockUI.noOpen == null) {
                blockUI.start();
            }
            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        authInterceptorServiceFactory.responseError = function (rejection) {
            if (blockUI.noOpen == null) {
                blockUI.stop();
            } else {
                blockUI.noOpen = null;
            }
            return $q.reject(rejection);
        }
        authInterceptorServiceFactory.response = function (response) {
            if (blockUI.noOpen == null) {
                blockUI.stop();
            } else {
                blockUI.noOpen = null;
            }

            return response || $q.when(response);
        }

        return authInterceptorServiceFactory;
    }
]);