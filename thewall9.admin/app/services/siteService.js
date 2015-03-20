'use strict';
app.factory('siteService', ["$myhttp",
    function ($http) {
        var _Url = APIURL + 'api/site';
        return {
            get: function () {
                return $http.get(_Url + '/all');
            },
            save: function (object) {
                return $http.post(_Url, object);
            },
            remove: function (id) {
                return $http.delete(_Url + '?SiteID=' + id);
            },
            enable: function (id, enable) {
                return $http.post(_Url + '/enable', {
                    SiteID: id,
                    Enabled: enable
                });
            },
            enableECommerce: function (id, enable) {
                return $http.post(_Url + '/enable-ecommerce', {
                    SiteID: id,
                    Enabled: enable
                });
            },
            addUser: function (object) {
                return $http.post(_Url + '/user', object);
            },
            getUsers: function (id) {
                return $http.get(_Url + '/users?SiteID=' + id);
            },
            addRol: function (userID, siteUserType, enable, customerID) {
                var _object = {
                    UserID: userID,
                    SiteUserType: siteUserType,
                    Enabled: enable,
                    SiteID: customerID
                };
                return $http.post(_Url + '/rol', _object);
            },
            removeUser: function (userID, customerID) {
                return $http.delete(_Url + '/user?SiteID=' + customerID + '&UserID=' + userID);
            },
            exportSite: function (SiteID) {
                return $http.get(_Url + '/export?SiteID=' + SiteID);
            },
            importSite: function (content) {
                return $http.post(_Url + '/import',content);
            },
            duplicate: function (SiteID) {
                return $http.post(_Url + '/duplicate', SiteID);
            }
        };
    }
]);
