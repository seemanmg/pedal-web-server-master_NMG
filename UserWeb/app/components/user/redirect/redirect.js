angular.module('userApp').controller("redirect", ['$scope', function ($scope) {

    var baseUrl = window.webUrl;
    window.location.href = baseUrl;

}]);
