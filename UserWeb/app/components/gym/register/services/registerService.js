angular.module('gymApp').service('registerService', function ($http) {
    var baseUrl = window.apiUrl;
    
    this.register = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'AddGymFromMarketing',
            data: data
        });
    };

});