'use strict';
app.factory('utilService', ["toastrService",
    function (nf) {
        return {
            errorCallback: function (data) {
                nf.error(data);
            }
        };
    }
]);
