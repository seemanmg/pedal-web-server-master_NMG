angular.module('gymApp').service('dashboardContractService', function ($http) {
    var baseUrl = window.apiUrl;

    this.contractFromGymId = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'ContractFromGymId',
            data: data
        });
    };

});