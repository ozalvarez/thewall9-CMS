'use strict';
app.factory('searchService', ["$myhttp", "$q",
    function ($http, $q) {
        var uri = 'api/search';
        var searchFactory = {};

        searchFactory.get = function (Query, Take) {
            return $http.get(_ServiceBase + uri + "?SiteID=" + _SiteID + "&Lang=" + _Lang + "&CurrencyID=" + _CurrentCurrencyID + "&Query=" + Query + "&Take=" + Take + "&Page=" + _SearchPage);
        }
        return searchFactory;
    }
]);