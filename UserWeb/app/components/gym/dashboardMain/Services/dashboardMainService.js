angular.module('gymApp').service('dashboardMainService', function ($http) {
    var baseUrl = window.apiUrl;

    this.loadCheckins = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'GetGymCheckIns',
            data: data
        });
    };
});