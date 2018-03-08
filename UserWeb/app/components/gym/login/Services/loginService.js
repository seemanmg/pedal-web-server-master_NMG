angular.module('gymApp').service('loginService', function ($http) {
    var baseUrl = window.apiUrl;

    this.login = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'GymLogin',
            data: data
        });
    };

});