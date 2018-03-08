angular.module('userApp').controller("airbnbRegisterController", ['$scope', '$routeParams', 'airbnbRegisterService', function ($scope, $routeParams, airbnbRegisterService) {

    $scope.airbnbRegisterSuccessful = false;
    $scope.airbnbRegistering = false;

    $scope.airbnbRegister = function () {

        $scope.airbnbRegistering = true;

        var data = {
            firstName: $scope.firstName,
            lastName: $scope.lastName,
            email: $scope.email,
            password: $scope.password,
            confirmPassword: $scope.confirmPassword,
            airbnbHostId: $routeParams.airbnbHostId,
        };

        airbnbRegisterService.airbnbRegister(data).then(function (resp) {
            if (resp.data.success) {
                $scope.airbnbRegistering = false;
                $scope.airbnbRegisterSuccessful = true;
                $scope.message = resp.data.message;
            } else {
                $scope.airbnbRegistering = false;
                $scope.ErrorMessage = resp.data.message;
            }

        });
    }

}]);