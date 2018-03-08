angular.module('gymApp').controller("navBarController", ['$scope', 'navBarService', '$cookies', function ($scope, navBarService, $cookies) {

    $scope.showNavBar = function () {
        $scope.gym = navBarService.gym;
        return navBarService.showNavBar;
    }

    $scope.goToMain = function () {
        var url = '/gym.html#/dashboard';
        window.location.href = url;
    }

    $scope.goToAccount = function () {
        var url = '/gym.html#/dashboard/account';
        window.location.href = url;
    }

    $scope.goToContact = function () {
        var url = '/gym.html#/dashboard/contact';
        window.location.href = url;
    }

    $scope.goToContract = function () {
        var url = '/gym.html#/dashboard/contract';
        window.location.href = url;
    }

    $scope.logOut = function () {
        $scope.gym = null;
        navBarService.gym = null;
        delete $cookies.authToken;
        delete $cookies.id;
        navBarService.showNavBar = false;
        window.location.href = "/gym.html#/login";
    }
    
}]);

