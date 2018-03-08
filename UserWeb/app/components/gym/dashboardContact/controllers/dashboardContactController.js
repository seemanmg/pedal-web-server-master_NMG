angular.module('gymApp').controller("dashboardContactController", ['$scope', '$cookies', 'navBarService', 'dashboardContactService', function ($scope, $cookies, navBarService, dashboardContactService) {


    var loadGymInfo = function (data) {

        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId
        };

        navBarService.loadGymInfo(data).then(function (resp) {
            if (resp.data.success == false) {
                $scope.gym = null;
                navBarService.gym = null;
                delete $cookies.authToken;
                delete $cookies.id;
                navBarService.showNavBar = false;
                var url = '/gym.html#/login';
                window.location.href = url;
            } else {
                navBarService.gym = resp.data;
                $scope.gym = navBarService.gym;
            }

        });
    }
   
    var init = function () {

        $scope.token = $cookies.authToken;
        $scope.gymId = $cookies.id;

        if ($scope.token && $scope.gymId) {
            $scope.gym = navBarService.gym;
            navBarService.showNavBar = true;

            if (!$scope.gym) {
                navBarService.loadGymInfo();
            }
        }
        else {

            $scope.gym = null;
            navBarService.gym = null;
            delete $cookies.authToken;
            delete $cookies.id;
            navBarService.showNavBar = false;
            window.location.href = "/gym.html#/login";
        }

    }

    // "init"
    init();

    $scope.sendEmailSuccessful = false;
    $scope.isSending = false;

    $scope.sendContactForm = function () {

        $scope.isSending = true;

        var data = {
            email: $scope.email,
            name: $scope.name,
            message: $scope.message,
        };

        dashboardContactService.contactForm(data).then(function (resp) {
            if (resp.data.success == true) {
                $scope.sendEmailSuccessful = true;
            }
            else {
                $scope.isSending = false;
                $scope.errorMessage = resp.data.message;
            }

        });

    }

}]);

