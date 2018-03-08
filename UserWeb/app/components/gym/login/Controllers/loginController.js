angular.module('gymApp').controller("loginController", ['$scope', '$cookies', 'loginService', 'navBarService', function ($scope, $cookies, loginService, navBarService) {

    // toggles
    $scope.Loading = true;

    // scope objects
    $scope.buttonText = "Sign In";
    $scope.signingIn = false;
    $scope.payment = {};
    $scope.LogIn = function () {
        var data = {
            emailAddress: $scope.username,
            password: $scope.password
        }
        $scope.buttonText = "Signing in...";
        $scope.signingIn = true;
        loginService.login(data).then(function (resp) {
            $scope.signingIn = false;
            $scope.buttonText = "Sign In";
            if (resp.data.success == true) {
                $cookies.authToken = resp.data.token;
                $cookies.id = resp.data.gymId;
                navBarService.gym = resp.data;
                var url = '/gym.html#/dashboard';
                window.location.href = url;
            }
            else {
                $scope.ErrorMessage = resp.data.message;
            }
        });
        
    }

    var init = function () {
        if ($cookies.authToken && $cookies.id) {
            var url = '/gym.html#/dashboard';
            window.location.href = url;
        }
        else {
            navBarService.showNavBar = false;
        }
    }

    // "init"
    init();
}]);