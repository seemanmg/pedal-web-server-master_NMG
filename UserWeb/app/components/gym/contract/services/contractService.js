angular.module('gymApp').service('contractService', function ($http) {
    var baseUrl = window.apiUrl;

    this.contractFromPriceTier = function (data) {

        return $http({
            method: 'POST',
            url: baseUrl + 'ContractFromPriceTier',
            data: data
        });
    };

});