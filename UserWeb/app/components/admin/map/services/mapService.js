angular.module('adminApp').service('mapService', function ($http) {
    var baseUrl = window.apiUrl;

    this.getGyms = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllGyms',
            data: data
        });
    };

    this.getUsers = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllUsers',
            data: data
        });
    };

    this.getGymPasses = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllGymPasses',
            data: data
        });
    };

    this.getSearchRequests = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllSearchRequests',
            data: data
        });
    };

    this.getGiveCredits = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllGiveCredits',
            data: data
        });
    };

    this.getGymInvoices = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllGymInvoices',
            data: data
        });
    };

    this.getLogs = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'getAllLogs',
            data: data
        });
    };

    this.deleteGym = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'deleteGym',
            data: data
        });
    };

});