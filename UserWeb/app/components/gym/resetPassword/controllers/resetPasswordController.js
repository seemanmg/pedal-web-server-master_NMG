angular.module('gymApp').controller("resetPasswordController", ['$scope', '$routeParams', 'resetPasswordService', function ($scope, $routeParams, resetPasswordService) {

    $scope.passwordUpdateSuccessful = false;
    $scope.passwordUpdating = false;

    $scope.updatePassword = function () {

        $scope.passwordUpdating = true;

        var data = {
            authToken: $routeParams.authToken,
            accountId: $routeParams.accountId,
            password: $scope.password,
            confirmPassword: $scope.confirmPassword,
        };

        resetPasswordService.updatePassword(data).then(function (resp) {
            if (resp.data.success) {
                $scope.passwordUpdating = false;
                $scope.passwordUpdateSuccessful = true;
                $scope.message = resp.data.message;
                $scope.password = "";
                $scope.confirmPassword = "";
            } else {
                $scope.passwordUpdating = false;
                $scope.errorMessage = resp.data.message;
            }

        });
    }

}]);
