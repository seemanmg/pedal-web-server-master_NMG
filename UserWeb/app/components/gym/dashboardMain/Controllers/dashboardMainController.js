angular.module('gymApp').controller("dashboardMainController", ['$scope', '$cookies', '$interval', 'Upload', 'navBarService', 'dashboardMainService', function ($scope, $cookies, $interval, Upload, navBarService, dashboardMainService) {
    'use strict';
    Stripe.setPublishableKey(window.stripeKey);
    var baseUrl = window.apiUrl;

    // toggles
    $scope.Loading = true;
    var displaySuccessMessage = function (resp) {
        if (resp.data.success) {
            toastr.success("Successfully updated!");
        } else {
            toastr.success("Oops! There was an issue saving your information.");
        }
    }


    $scope.recentCheckings = [];
    $scope.searchResults = [];

    $scope.loadCheckins = function () {
        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId,
            page: 1,
            count: 100
        };

        dashboardMainService.loadCheckins(data).then(function (resp) {
            if (resp.data.success == false) {
                $scope.gym = null;
                navBarService.gym = null;
                delete $cookies.authToken;
                delete $cookies.id;
                navBarService.showNavBar = false;
                var url = '/gym.html#/login';
                window.location.href = url;
            } else {
                $scope.recentCheckIns = resp.data.items;
            }
        });
    }

    var loadGymAndCheckinInfo = function () {

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
                $scope.loadCheckins();
            }

        });
    }

    var init = function () {

        $scope.token = $cookies.authToken;
        $scope.gymId = $cookies.id;

        if ($scope.token && $scope.gymId) {
            // if coming in from another page, set gym
            $scope.gym = navBarService.gym;
            navBarService.showNavBar = true;

            if (!$scope.gym) {
                loadGymAndCheckinInfo();
            }
            else {
                $scope.loadCheckins();
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


}]);

