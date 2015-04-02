app.filter('fromNow', function () {
    return function (date) {
        return moment.utc(date).utcOffset(new Date().getTimezoneOffset()).locale("es").fromNow();
    }
});