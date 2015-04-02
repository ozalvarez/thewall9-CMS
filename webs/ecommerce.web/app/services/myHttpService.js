'use strict';
app.factory('$myhttp', ["$http", '$q', 'utilService',
    function ($http, $q, utilService) {
        var myHttpFactory = {};

        myHttpFactory.get = function (url) {
            var deferred = $q.defer();
            $http.get(url).success(function (data) {
                deferred.resolve(data);
            }).error(function (err, statusCode) {
                utilService.errorCallback(err);
                deferred.reject(err);
            });
            return deferred.promise;
        };
        myHttpFactory.post = function (url, object) {
            var deferred = $q.defer();
            $http.post(url, object).success(function (data) {
                deferred.resolve(data);
            }).error(function (err, statusCode) {
                utilService.errorCallback(err);
                deferred.reject(err);
            });
            return deferred.promise;
        };
        myHttpFactory.put = function (url, object) {
            var deferred = $q.defer();
            $http.put(url, object).success(function (data) {
                deferred.resolve(data);
            }).error(function (err, statusCode) {
                utilService.errorCallback(err);
                deferred.reject(err);
            });
            return deferred.promise;
        }
        myHttpFactory.delete = function (url) {
            var deferred = $q.defer();
            $http.delete(url).success(function (data) {
                deferred.resolve(data);
            }).error(function (err, statusCode) {
                utilService.errorCallback(err);
                deferred.reject(err);
            });
            return deferred.promise;
        }
        return myHttpFactory;
    }
]);