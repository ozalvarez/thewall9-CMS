'use strict';
app.factory('toastrService', [function () {
    return {
        success: function (text) {
            toastr.success(text);
        },
        error: function (data) {
            if (data.ModelState != null) {
                for (var key in data.ModelState) {
                    for (var i = 0; i < data.ModelState[key].length; i++) {
                        toastr.error(data.ModelState[key][i], "Error");
                    }
                }
            }
            else if (data.Message != null) {
                toastr.error(data.Message, "Error");
            } else {
                toastr.error(data, "Error");
            }
        }
    };
}]);
