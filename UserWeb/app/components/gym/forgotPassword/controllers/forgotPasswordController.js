angular.module('gymApp').controller("forgotPasswordController", ['$scope', 'forgotPasswordService', function ($scope, forgotPasswordService) {

    $scope.sendEmailSuccessful = false;
    $scope.sendingEmail = false;

    $scope.sendResetPasswordEmail = function () {

        $scope.sendingEmail = true;

        var data = {
            email: $scope.email,
        };

        forgotPasswordService.sendResetPasswordEmail(data).then(function (resp) {


            if (resp.data.success == true) {
                $scope.sendingEmail = false;
                $scope.message = resp.data.message;
                $scope.sendEmailSuccessful = true;
            }
            else {
                $scope.sendingEmail = false;
                $scope.errorMessage = resp.data.message;
            }

        });

    }
}]);
