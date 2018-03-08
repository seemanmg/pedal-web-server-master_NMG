angular.module('gymApp').service('forgotPasswordService', function ($http) {
    var baseUrl = window.apiUrl;

    this.sendResetPasswordEmail = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'resetGymPasswordEmail',
            data: data
        });
    };
});