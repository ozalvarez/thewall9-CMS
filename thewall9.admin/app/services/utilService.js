'use strict';
app.factory('utilService', ["cfpLoadingBar", "toastrService",
    function (lb, nf) {
        return {
            errorCallback: function (data) {
                nf.error(data);
            }
        };
    }
]);
