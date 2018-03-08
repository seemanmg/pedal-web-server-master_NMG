angular.module('gymApp').service('navBarService', function ($http) {
    var baseUrl = window.apiUrl;

    this.showNavBar = false;
    this.gym = null;

    this.loadGymInfo = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'GetGymInfo',
            data: data
        });
    };

});