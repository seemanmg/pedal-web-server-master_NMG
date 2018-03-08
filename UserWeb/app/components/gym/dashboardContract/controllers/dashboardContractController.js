angular.module('gymApp').controller("dashboardContractController", ['$scope', '$cookies', 'navBarService', 'dashboardContractService', function ($scope, $cookies, navBarService, dashboardContractService) {

    

    var init = function () {

        $scope.token = $cookies.authToken;
        $scope.gymId = $cookies.id;

        if ($scope.token && $scope.gymId) {

            navBarService.showNavBar = true;

            var data = {
                accountId: $scope.gymId,
                authToken: $scope.token,
            };

            dashboardContractService.contractFromGymId(data).then(function (resp) {
                $scope.price = resp.data.price;
                $scope.date = resp.data.date;
                $scope.gymName = resp.data.gymName;
            });

        }

        else {
            navBarService.gym = null;
            delete $cookies.authToken;
            delete $cookies.id;
            navBarService.showNavBar = false;
            window.location.href = "/gym.html#/login";
        

        }

    }

    init();

}]);