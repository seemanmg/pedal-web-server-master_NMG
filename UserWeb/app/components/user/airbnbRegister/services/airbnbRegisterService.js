angular.module('userApp').service('airbnbRegisterService', function ($http) {
    var baseUrl = window.apiUrl;

    this.airbnbRegister = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'airbnbRegister',
            data: data
        });
    };

});