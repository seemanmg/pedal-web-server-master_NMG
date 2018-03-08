angular.module('gymApp').controller("dashboardAccountController", ['$scope', '$cookies', '$interval', 'Upload', 'dashboardAccountService', 'navBarService', '$timeout', function ($scope, $cookies, $interval, Upload, dashboardAccountService, navBarService, $timeout) {
    'use strict';
    Stripe.setPublishableKey(window.stripeKey);
    var baseUrl = window.apiUrl;

    $scope.imageIsCropping = false;
    //var imageCanvas = angular.element(document.querySelector('#imageCropper'));

    $scope.isLoading = false;
    $scope.result = false;

    $scope.upload = function (dataUrl, name) {
        $scope.isLoading = true;

        Upload.upload({
            url: baseUrl + 'upload/' + $scope.token + '/' + $scope.gymId,
            data: {
                file: Upload.dataUrltoBlob(dataUrl, name)
            },

        }).then(function (response) {
            $timeout(function () {

                if (response.data.success == true) {
                    $scope.gym.pictures.push({
                        url: response.data.picture.url,
                        isCover: response.data.picture.isCoverPhoto,
                        id: response.data.picture.pictureId,
                    });
                    $scope.isLoading = false;
                    $scope.result = true;
                    toastr.success(response.data.message);
                }
                else {
                    $scope.isLoading = false;
                    $scope.errorMessage = response.data.message;
                    toastr.error(response.data.message);
                }
                $scope.datat = response.data;
            });
        }, function (data) {
            // client side error
            $scope.errorMessage = "There was an error with your browser. Please contact the Pedal team."
            
        }, function (evt) {
            $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
        });
    };


    $scope.onLoadDisplay = function() {
        $("#cropZone").css('height', 1000);
        $("#cropZone").css('display', 'block');
        $scope.result = false;
    }

    $scope.onLoadDisplayFinish = function () {
        $("#cropZone").css('display', 'block');
        $scope.imageIsCropping = true;
    }

    $scope.addGymHours = function () {
        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId,
            startTime: $scope.open.startTime,
            endTime: $scope.open.endTime,
            day: $scope.open.day,
        };

        dashboardAccountService.addGymHours(data).then(function (resp) {
            if (resp.data.success) {
                toastr.success(resp.data.message);
                $scope.gym.opens.push({
                    id: resp.data.gymHourId,
                    day: $scope.open.day,
                    startTime: $scope.open.startTime,
                    endTime: $scope.open.endTime,
                    readable: formatOpen($scope.open.day, $scope.open.startTime, $scope.open.endTime)
                });
                $scope.open = {};
            } else {
                toastr.error(resp.data.message);
            }

        });
    }

    var formatOpen = function (day, startTime, endTime) {
        
        var startAmOrPm = "AM";
        var endAmOrPm = "AM";

        if (startTime.hour > 12) {
            startAmOrPm = "PM";
            startTime.hour = startTime.hour - 12;
        }

        if (endTime.hour > 12) {
            endAmOrPm = "PM";
            endTime.hour = endTime.hour - 12;
        }

        if (startTime.minute == 0) {
            startTime.minute = "00";
        }

        if (endTime.minute == 0) {
            endTime.minute = "00";
        }

        var print = day + " "
            + startTime.hour + ":" + startTime.minute + " " + startAmOrPm
            + " - "
            + endTime.hour + ":" + endTime.minute + " " + endAmOrPm;

        return print;
    }

    $scope.removeGymHours = function (open, index) {
        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId,
            gymHourId: open.id,
        };

        dashboardAccountService.removeGymHours(data).then(function (resp) {
            if (resp.data.success) {
                toastr.success(resp.data.message);
                $scope.gym.opens.splice(index, 1);
            } else {
                toastr.error(resp.data.message);
            }

        });
    }

  

    $scope.open = {};

    $scope.days = [
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
    ];

    $scope.startTimes = [
        { readable: "12:00 AM", coded: { hour: 0, minute: 0 } },
        { readable: "12:30 AM", coded: { hour: 0, minute: 30 } },
        { readable: "1:00 AM", coded: { hour: 1, minute: 0 } },
        { readable: "1:30 AM", coded: { hour: 1, minute: 30 } },
        { readable: "2:00 AM", coded: { hour: 2, minute: 0 } },
        { readable: "2:30 AM", coded: { hour: 2, minute: 30 } },
        { readable: "3:00 AM", coded: { hour: 3, minute: 0 } },
        { readable: "3:30 AM", coded: { hour: 3, minute: 30 } },
        { readable: "4:00 AM", coded: { hour: 4, minute: 0 } },
        { readable: "4:30 AM", coded: { hour: 4, minute: 30 } },
        { readable: "5:00 AM", coded: { hour: 5, minute: 0 } },
        { readable: "5:30 AM", coded: { hour: 5, minute: 30 } },
        { readable: "6:00 AM", coded: { hour: 6, minute: 0 } },
        { readable: "6:30 AM", coded: { hour: 6, minute: 30 } },
        { readable: "7:00 AM", coded: { hour: 7, minute: 0 } },
        { readable: "7:30 AM", coded: { hour: 7, minute: 30 } },
        { readable: "8:00 AM", coded: { hour: 8, minute: 0 } },
        { readable: "8:30 AM", coded: { hour: 8, minute: 30 } },
        { readable: "9:00 AM", coded: { hour: 9, minute: 0 } },
        { readable: "9:30 AM", coded: { hour: 9, minute: 30 } },
        { readable: "10:00 AM", coded: { hour: 10, minute: 0 } },
        { readable: "10:30 AM", coded: { hour: 10, minute: 30 } },
        { readable: "11:00 AM", coded: { hour: 11, minute: 0 } },
        { readable: "11:30 AM", coded: { hour: 11, minute: 30 } },
        { readable: "12:00 PM", coded: { hour: 12, minute: 0 } },
        { readable: "12:30 PM", coded: { hour: 12, minute: 30 } },
        { readable: "1:00 PM", coded: { hour: 13, minute: 0 } },
        { readable: "1:30 PM", coded: { hour: 13, minute: 30 } },
        { readable: "2:00 PM", coded: { hour: 14, minute: 0 } },
        { readable: "2:30 PM", coded: { hour: 14, minute: 30 } },
        { readable: "3:00 PM", coded: { hour: 15, minute: 0 } },
        { readable: "3:30 PM", coded: { hour: 15, minute: 30 } },
        { readable: "4:00 PM", coded: { hour: 16, minute: 0 } },
        { readable: "4:30 PM", coded: { hour: 16, minute: 30 } },
        { readable: "5:00 PM", coded: { hour: 17, minute: 0 } },
        { readable: "5:30 PM", coded: { hour: 17, minute: 30 } },
        { readable: "6:00 PM", coded: { hour: 18, minute: 0 } },
        { readable: "6:30 PM", coded: { hour: 18, minute: 30 } },
        { readable: "7:00 PM", coded: { hour: 19, minute: 0 } },
        { readable: "7:30 PM", coded: { hour: 19, minute: 30 } },
        { readable: "8:00 PM", coded: { hour: 20, minute: 0 } },
        { readable: "8:30 PM", coded: { hour: 20, minute: 30 } },
        { readable: "9:00 PM", coded: { hour: 21, minute: 0 } },
        { readable: "9:30 PM", coded: { hour: 21, minute: 30 } },
        { readable: "10:00 PM", coded: { hour: 22, minute: 0 } },
        { readable: "10:30 PM", coded: { hour: 22, minute: 30 } },
        { readable: "11:00 PM", coded: { hour: 23, minute: 0 } },
        { readable: "11:30 PM", coded: { hour: 23, minute: 30 } },
        { readable: "End of Day", coded: { hour: 24, minute: 0 } },
    ];

    $scope.endTimes = [
        { readable: "12:00 AM", coded: { hour: 0, minute: 0 } },
        { readable: "12:30 AM", coded: { hour: 0, minute: 30 } },
        { readable: "1:00 AM", coded: { hour: 1, minute: 0 } },
        { readable: "1:30 AM", coded: { hour: 1, minute: 30 } },
        { readable: "2:00 AM", coded: { hour: 2, minute: 0 } },
        { readable: "2:30 AM", coded: { hour: 2, minute: 30 } },
        { readable: "3:00 AM", coded: { hour: 3, minute: 0 } },
        { readable: "3:30 AM", coded: { hour: 3, minute: 30 } },
        { readable: "4:00 AM", coded: { hour: 4, minute: 0 } },
        { readable: "4:30 AM", coded: { hour: 4, minute: 30 } },
        { readable: "5:00 AM", coded: { hour: 5, minute: 0 } },
        { readable: "5:30 AM", coded: { hour: 5, minute: 30 } },
        { readable: "6:00 AM", coded: { hour: 6, minute: 0 } },
        { readable: "6:30 AM", coded: { hour: 6, minute: 30 } },
        { readable: "7:00 AM", coded: { hour: 7, minute: 0 } },
        { readable: "7:30 AM", coded: { hour: 7, minute: 30 } },
        { readable: "8:00 AM", coded: { hour: 8, minute: 0 } },
        { readable: "8:30 AM", coded: { hour: 8, minute: 30 } },
        { readable: "9:00 AM", coded: { hour: 9, minute: 0 } },
        { readable: "9:30 AM", coded: { hour: 9, minute: 30 } },
        { readable: "10:00 AM", coded: { hour: 10, minute: 0 } },
        { readable: "10:30 AM", coded: { hour: 10, minute: 30 } },
        { readable: "11:00 AM", coded: { hour: 11, minute: 0 } },
        { readable: "11:30 AM", coded: { hour: 11, minute: 30 } },
        { readable: "12:00 PM", coded: { hour: 12, minute: 0 } },
        { readable: "12:30 PM", coded: { hour: 12, minute: 30 } },
        { readable: "1:00 PM", coded: { hour: 13, minute: 0 } },
        { readable: "1:30 PM", coded: { hour: 13, minute: 30 } },
        { readable: "2:00 PM", coded: { hour: 14, minute: 0 } },
        { readable: "2:30 PM", coded: { hour: 14, minute: 30 } },
        { readable: "3:00 PM", coded: { hour: 15, minute: 0 } },
        { readable: "3:30 PM", coded: { hour: 15, minute: 30 } },
        { readable: "4:00 PM", coded: { hour: 16, minute: 0 } },
        { readable: "4:30 PM", coded: { hour: 16, minute: 30 } },
        { readable: "5:00 PM", coded: { hour: 17, minute: 0 } },
        { readable: "5:30 PM", coded: { hour: 17, minute: 30 } },
        { readable: "6:00 PM", coded: { hour: 18, minute: 0 } },
        { readable: "6:30 PM", coded: { hour: 18, minute: 30 } },
        { readable: "7:00 PM", coded: { hour: 19, minute: 0 } },
        { readable: "7:30 PM", coded: { hour: 19, minute: 30 } },
        { readable: "8:00 PM", coded: { hour: 20, minute: 0 } },
        { readable: "8:30 PM", coded: { hour: 20, minute: 30 } },
        { readable: "9:00 PM", coded: { hour: 21, minute: 0 } },
        { readable: "9:30 PM", coded: { hour: 21, minute: 30 } },
        { readable: "10:00 PM", coded: { hour: 22, minute: 0 } },
        { readable: "10:30 PM", coded: { hour: 22, minute: 30 } },
        { readable: "11:00 PM", coded: { hour: 23, minute: 0 } },
        { readable: "11:30 PM", coded: { hour: 23, minute: 30 } },
        { readable: "End of Day", coded: { hour: 24, minute: 0 } },
    ];

    $scope.updateOpenTimes = function () {
        $scope.open.startTime = { hour: 0, minute: 0 };
        $scope.open.endTime = { hour: 24, minute: 0 };
    }

    // toggles
    $scope.Loading = true;
   

    // scope objects
    $scope.ErrorMessages = [];
    $scope.bankAccountText = "Add";
    $scope.invalidPayment = true;

    $scope.isUpdatingInfo = false;
    $scope.updateInfoError = false;
    $scope.updateInfoSuccess = false;
    $scope.updateInfoErrorMsg = '';
    $scope.updateInfoSuccessMsg = '';

    $scope.updateInfo = function () {

        $scope.isUpdatingInfo = true;
        $scope.updateInfoError = false;
        $scope.updateInfoSuccess = false;

        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId,
            data: $scope.gym,
        };

        dashboardAccountService.updateGymInfo(data).then(function (resp) {
            if (resp.data.success) {
                $scope.isUpdatingInfo = false;
                $scope.updateInfoSuccess = true;
                $scope.updateInfoSuccessMsg = resp.data.message;
            } else {
                $scope.isUpdatingInfo = false;
                $scope.updateInfoError = true;
                $scope.updateInfoErrorMsg = resp.data.message;
            }
        });
    }




    $scope.updateCoverPhoto = function (pictureId) {
        // unselect the rest
        for (var i = 0; i < $scope.gym.pictures.length; i++) {
            if ($scope.gym.pictures[i].id != pictureId) {
                $scope.gym.pictures[i].isCover = false;
            } else {
                $scope.gym.pictures[i].isCover = true;
            }
        }

        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId,
            pictureId: pictureId,
        };

        //update server
        dashboardAccountService.updateCoverPhoto(data).then(function (resp) {
            if (resp.data.success) {
                toastr.success(resp.data.message);
            } else {
                toastr.error(resp.data.message);
            }
        });
    }


    $scope.deletePicture = function (pictureId, $index) {
        //update server

        var data = {
            authToken: $scope.token,
            accountId: $scope.gymId,
            pictureId: pictureId,
        };

        dashboardAccountService.deletePicture(data).then(function (resp) {
            if (resp.data.success) {
                toastr.success(resp.data.message);
                $scope.gym.pictures.splice($index, 1);
            } else {
                toastr.error(resp.data.message);
            }
        });
    }

    $scope.validateBankNumbers = function () {
        $scope.payment.errors = [];
        if ($scope.payment.routing && $scope.payment.account) {
            var routingIsValid = Stripe.bankAccount.validateRoutingNumber($scope.payment.routing, 'US');
            if (!routingIsValid) {
                $scope.payment.errors.push("Routing number is not in the correct format");
            }
            var accountIsValid = Stripe.bankAccount.validateAccountNumber($scope.payment.account, 'US');
            if (!accountIsValid) {
                $scope.payment.errors.push("Account number is not in the correct format");
            }
            $scope.invalidPayment = $scope.payment.errors.length > 0;
        } else {
            $scope.invalidPayment = true;
        }


    }

    $scope.isUpdatingPayment = false;
    $scope.paymentError = false;
    $scope.paymentSuccess = false;
    $scope.paymentErrorMsg = '';
    $scope.paymentSuccessMsg = '';

    $scope.updateBankAccount = function () {

        $scope.isUpdatingPayment = true;
        $scope.paymentError = false;
        $scope.paymentSuccess = false;

        var callback = function (status, response) {
            var data = {
                authToken: $scope.token,
                stripeToken: response.id,
                accountId: $scope.gymId,
            };

            dashboardAccountService.updateBankInfo(data).then(function (resp) {
                if (resp.data.success) {
                    $scope.isUpdatingPayment = false;
                    $scope.paymentSuccess = true;
                    $scope.paymentSuccessMsg = resp.data.message;
                } else {
                    $scope.isUpdatingPayment = false;
                    $scope.paymentError = true;
                    $scope.paymentErrorMsg = resp.data.message;
                }
            });
        }
        toastr.info("Saving information....");
        Stripe.bankAccount.createToken({
            country: "US",
            currency: "USD",
            routing_number: $scope.payment.routing,
            account_number: $scope.payment.account,
        }, callback);
    }

    var loadGymInfo = function () {

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

                for (var i = 0; i < navBarService.gym.opens.length; i++) {
                    navBarService.gym.opens[i].readable = formatOpen(
                        navBarService.gym.opens[i].day,
                        navBarService.gym.opens[i].startTime,
                        navBarService.gym.opens[i].endTime);
                }
                $scope.gym = navBarService.gym;
                if (resp.data.hasAccount) {
                    $scope.bankAccountText = "Update";
                }
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
                loadGymInfo();
            }
            else {
                for (var i = 0; i < navBarService.gym.opens.length; i++) {
                    navBarService.gym.opens[i].readable = formatOpen(
                        navBarService.gym.opens[i].day,
                        navBarService.gym.opens[i].startTime,
                        navBarService.gym.opens[i].endTime);
                }
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








}])
.directive('tooltip', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(element).hover(function () {
                // on mouseenter
                $(element).tooltip('show');
            }, function () {
                // on mouseleave
                $(element).tooltip('hide');
            });
        }
    };
});