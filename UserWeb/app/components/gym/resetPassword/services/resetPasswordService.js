angular.module('gymApp').service('resetPasswordService', function ($http) {
    var baseUrl = window.apiUrl;

    this.updatePassword = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'UpdateGymPassword',
            data: data
        });
    };

});