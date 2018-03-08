angular.module('gymApp').service('dashboardAccountService', function ($http) {
    var baseUrl = window.apiUrl;

    this.updateGymInfo = function (data) {
        
        return $http({
            method: 'POST',
            url: baseUrl + 'UpdateGymInfo',
            data: data
        });
    };
    this.updateCoverPhoto = function (data) {
  
        return $http({
            method: 'POST',
            url: baseUrl + 'UpdateSelectedCoverPhoto',
            data: data
        });
    };
    this.deletePicture = function (data) {
 
        return $http({
            method: 'POST',
            url: baseUrl + 'DeletePhoto',
            data: data
        });
    };
    this.updateBankInfo = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'UpdateBankInfo',
            data: data
        });
    };

    this.addGymHours = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'addGymSchedule',
            data: data
        });
    };

    this.removeGymHours = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'removeGymHours',
            data: data
        });
    };
    



});