angular.module('gymApp').controller("contractController", ['$scope', '$routeParams', 'contractService', function ($scope, $routeParams, contractService) {
   


    var init = function() {

        // query string is name and priceTier
        $scope.gymName = $routeParams.name;
        $scope.priceTier = $routeParams.priceTier;

        if (!$scope.gymName) {
            $scope.gymName = "YOUR GYM";
        }

        if (!$scope.priceTier) {
            $scope.priceTier = "unknown";
        }


        var data = {
            gymName: $scope.gymName,
            priceTier: $scope.priceTier,
        };

        contractService.contractFromPriceTier(data).then(function (resp) {
            $scope.price = resp.data.price;
            $scope.date = resp.data.date;
            $scope.gymName = resp.data.gymName;
        });

    }

    init();
  
}]);