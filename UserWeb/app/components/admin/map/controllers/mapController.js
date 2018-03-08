angular.module('adminApp').controller("mapController", ['$scope', '$routeParams', 'mapService', '$window', '$timeout', function ($scope, $routeParams, mapService, $window, $timeout) {

    var mapOptions = {
        zoom: 4,
        center: new google.maps.LatLng(40.806862, -96.681679),
        mapTypeId: google.maps.MapTypeId.TERRAIN
    }

    $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);


    var infoWindow = new google.maps.InfoWindow();

    var createMarker = function (marker) {

        google.maps.event.addListener(marker, 'click', function () {
            infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
            infoWindow.open($scope.map, marker);
        });

        return marker;

    }

    $scope.allMarkers = [];


    $scope.openInfoWindow = function (e, selectedMarker) {
        e.preventDefault();
        google.maps.event.trigger(selectedMarker, 'click');
    }

    $scope.deleteMarkers = function () {
        for (var i = 0; i < $scope.allMarkers.length; i++) {
            $scope.allMarkers[i].setMap(null);
        }
        $scope.allMarkers = [];
    }

    $scope.formatCash = function (number) {
        if (number) {
            var numberDollars = number / 100;
            return "$" + numberDollars.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        }
        else {

            return "NULL";
        }
       }


    // delete Gym function
    $scope.deleteGym = function (gymId) {

        if (confirm("Are you sure?") == true) {
            var data = {
                authToken: $routeParams.key,
                gymId: gymId,
            };

            mapService.deleteGym(data).then(function (resp) {
                if (resp.data.success) {

                }
            });
        }

        
    }



    // Gyms

    $scope.gyms = [];
    $scope.totalStatsGyms = {};
    $scope.showGyms = [];

    $scope.currentGymPage = 0;
    $scope.showLastGymIsntReady = true;
    $scope.showNextGymIsntReady = true;

    $scope.isShowGyms = false;

    var printShowGyms = function(index) {


        $scope.showGyms = [];

        for (var i = index; i < (index + 300) ; i++) {
            if ($scope.gyms[i]) {
                $scope.showGyms.push($scope.gyms[i]);
            }
        }

        if (($scope.currentGymPage + 10) < $scope.gyms.length) {
            $scope.showNextGymIsntReady = false;
        }
        else {
            $scope.showNextGymIsntReady = true;
        }

        if (($scope.currentGymPage - 10) >= 0) {
            $scope.showLastGymIsntReady = false;
        }
        else {
            $scope.showLastGymIsntReady = true;
        }
    }



    $scope.showLastGym = function () {

        $scope.currentGymPage = $scope.currentGymPage - 10;
        var index = $scope.currentGymPage;

        printShowGyms(index);
    }


    $scope.showNextGym = function () {

        $scope.currentGymPage = $scope.currentGymPage + 10;
        var index = $scope.currentGymPage;

        printShowGyms(index);
    }

    $scope.showGymsClick = function () {
        // load gyms


        $scope.gyms = [];

        var data = {
            authToken: $routeParams.key,
        };

        mapService.getGyms(data).then(function (resp) {
            if (resp.data.success) {

                $scope.totalStatsGyms = resp.data.totalStatsGyms;

                for (var i = 0; i < resp.data.gyms.length; i++) {
                    $scope.gyms.push(resp.data.gyms[i]);

                    if ($scope.gyms[i].Position) {

                        var marker = new google.maps.Marker({
                            map: $scope.map,
                            position: new google.maps.LatLng(
                                $scope.gyms[i].Position.Latitude,
                                $scope.gyms[i].Position.Longitude
                            ),
                            title: $scope.gyms[i].GymName
                        });

                        if ($scope.gyms[i].GymInfo == "COMING SOON!") {
                            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red.png')
                        }
                        else {
                            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png')
                        }

                        marker.content = '<div class="infoWindowContent">' + $scope.gyms[i].GymInfo + '</div>';

                        var setMarker = createMarker(marker);
                        $scope.allMarkers.push(setMarker);
                        $scope.gyms[i].marker = setMarker;
                    }

                }


                printShowGyms($scope.currentGymPage);
                $scope.isShowGyms = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }





    // USERS

    $scope.users = [];
    $scope.totalStatsUsers = {};
    $scope.showUsers = [];

    $scope.currentUserPage = 0;
    $scope.showLastUserIsntReady = true;
    $scope.showNextUserIsntReady = true;

    $scope.isShowUsers = false;


    var printShowUsers = function (index) {


        $scope.showUsers = [];

        for (var i = index; i < (index + 10) ; i++) {
            if ($scope.users[i]) {
                $scope.showUsers.push($scope.users[i]);
            }
        }

        if (($scope.currentUserPage + 10) < $scope.users.length) {
            $scope.showNextUserIsntReady = false;
        }
        else {
            $scope.showNextUserIsntReady = true;
        }

        if (($scope.currentUserPage - 10) >= 0) {
            $scope.showLastUserIsntReady = false;
        }
        else {
            $scope.showLastUserIsntReady = true;
        }
    }



    $scope.showLastUser = function () {

        $scope.currentUserPage = $scope.currentUserPage - 10;
        var index = $scope.currentUserPage;

        printShowUsers(index);
    }


    $scope.showNextUser = function () {

        $scope.currentUserPage = $scope.currentUserPage + 10;
        var index = $scope.currentUserPage;

        printShowUsers(index);
    }

    $scope.showUsersClick = function () {
        // load gyms


        $scope.users = [];

        var data = {
            authToken: $routeParams.key,
        };

        mapService.getUsers(data).then(function (resp) {
            if (resp.data.success) {

                $scope.totalStatsUsers = resp.data.totalStatsUsers;


                for (var i = 0; i < resp.data.users.length; i++) {
                    $scope.users.push(resp.data.users[i]);
                }


                printShowUsers($scope.currentUserPage);
                $scope.isShowUsers = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }




    // GymPasses

    $scope.gymPasses = [];
    $scope.totalStatsGymPasses = {};
    $scope.showGymPasses = [];

    $scope.currentGymPassPage = 0;
    $scope.showLastGymPassIsntReady = true;
    $scope.showNextGymPassIsntReady = true;

    $scope.isShowGymPasses = false;

    var printShowGymPasses = function (index) {


        $scope.showGymPasses = [];

        for (var i = index; i < (index + 10) ; i++) {
            if ($scope.gymPasses[i]) {
                $scope.showGymPasses.push($scope.gymPasses[i]);
            }
        }

        if (($scope.currentGymPassPage + 10) < $scope.gymPasses.length) {
            $scope.showNextGymPassIsntReady = false;
        }
        else {
            $scope.showNextGymPassIsntReady = true;
        }

        if (($scope.currentGymPassPage - 10) >= 0) {
            $scope.showLastGymPassIsntReady = false;
        }
        else {
            $scope.showLastGymPassIsntReady = true;
        }
    }



    $scope.showLastGymPass = function () {

        $scope.currentGymPassPage = $scope.currentGymPassPage - 10;
        var index = $scope.currentGymPassPage;

        printShowGymPasses(index);
    }


    $scope.showNextGymPass = function () {

        $scope.currentGymPassPage = $scope.currentGymPassPage + 10;
        var index = $scope.currentGymPassPage;

        printShowGymPasses(index);
    }

    $scope.showGymPassesClick = function () {
        // load gyms



        $scope.gymPasses = [];

        var data = {
            authToken: $routeParams.key,
        };

        mapService.getGymPasses(data).then(function (resp) {
            if (resp.data.success) {

                $scope.totalStatsGymPasses = resp.data.totalStatsGymPasses;


                for (var i = 0; i < resp.data.gymPasses.length; i++) {
                    $scope.gymPasses.push(resp.data.gymPasses[i]);

                    if ($scope.gymPasses[i].Position) {

                        var marker = new google.maps.Marker({
                            map: $scope.map,
                            position: new google.maps.LatLng(
                                $scope.gymPasses[i].Position.Latitude,
                                $scope.gymPasses[i].Position.Longitude
                            ),
                            title: $scope.gymPasses[i].UserName + " at " + $scope.gymPasses[i].GymName + " on " + $scope.gymPasses[i].LocalDateBought,
                        });

                        var markerContent = "Total Amount: $" + (parseInt($scope.gymPasses[i].CreditsUsed) + parseInt($scope.gymPasses[i].AmountCharged));
                           
                        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')

                        marker.content = '<div class="infoWindowContent">' + markerContent + '</div>';

                        var setMarker = createMarker(marker);
                        $scope.allMarkers.push(setMarker);
                        $scope.gymPasses[i].marker = setMarker;
                    }

                }


                printShowGymPasses($scope.currentGymPassPage);
                $scope.isShowGymPasses = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }








    // SearchRequests

    $scope.searchRequests = [];
    $scope.showSearchRequests = [];

    $scope.currentSearchRequestPage = 0;
    $scope.showLastSearchRequestIsntReady = true;
    $scope.showNextSearchRequestIsntReady = true;

    $scope.isShowSearchRequests = false;

    var printShowSearchRequests = function (index) {


        $scope.showSearchRequests = [];

        for (var i = index; i < (index + 10) ; i++) {
            if ($scope.searchRequests[i]) {
                $scope.showSearchRequests.push($scope.searchRequests[i]);
            }
        }

        if (($scope.currentSearchRequestPage + 10) < $scope.searchRequests.length) {
            $scope.showNextSearchRequestIsntReady = false;
        }
        else {
            $scope.showNextSearchRequestIsntReady = true;
        }

        if (($scope.currentSearchRequestPage - 10) >= 0) {
            $scope.showLastSearchRequestIsntReady = false;
        }
        else {
            $scope.showLastSearchRequestIsntReady = true;
        }
    }



    $scope.showLastSearchRequest = function () {

        $scope.currentSearchRequestPage = $scope.currentSearchRequestPage - 10;
        var index = $scope.currentSearchRequestPage;

        printShowSearchRequests(index);
    }


    $scope.showNextSearchRequest = function () {

        $scope.currentSearchRequestPage = $scope.currentSearchRequestPage + 10;
        var index = $scope.currentSearchRequestPage;

        printShowSearchRequests(index);
    }

    $scope.showSearchRequestsClick = function () {
        // load gyms

        $scope.searchRequests = [];

        var data = {
            authToken: $routeParams.key,
        };


        mapService.getSearchRequests(data).then(function (resp) {
            if (resp.data.success) {

                for (var i = 0; i < resp.data.searchRequests.length; i++) {
                    $scope.searchRequests.push(resp.data.searchRequests[i]);

                    if ($scope.searchRequests[i].Position) {

                        var marker = new google.maps.Marker({
                            map: $scope.map,
                            position: new google.maps.LatLng(
                                $scope.searchRequests[i].Position.Latitude,
                                $scope.searchRequests[i].Position.Longitude
                            ),
                            title: $scope.searchRequests[i].UserName + " on " + $scope.searchRequests[i].SearchDate,
                        });

                        var markerContent = "Search Query: " + $scope.searchRequests[i].Request;

                        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')

                        marker.content = '<div class="infoWindowContent">' + markerContent + '</div>';

                        var setMarker = createMarker(marker);
                        $scope.allMarkers.push(setMarker);
                        $scope.searchRequests[i].marker = setMarker;
                    }

                }


                printShowSearchRequests($scope.currentSearchRequestPage);
                $scope.isShowSearchRequests = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }




    // GiveCredits

    $scope.giveCredits = [];
    $scope.showGiveCredits = [];

    $scope.currentGiveCreditPage = 0;
    $scope.showLastGiveCreditIsntReady = true;
    $scope.showNextGiveCreditIsntReady = true;

    $scope.isShowGiveCredits = false;

    var printShowGiveCredits = function (index) {


        $scope.showGiveCredits = [];

        for (var i = index; i < (index + 10) ; i++) {
            if ($scope.giveCredits[i]) {
                $scope.showGiveCredits.push($scope.giveCredits[i]);
            }
        }

        if (($scope.currentGiveCreditPage + 10) < $scope.giveCredits.length) {
            $scope.showNextGiveCreditIsntReady = false;
        }
        else {
            $scope.showNextGiveCreditIsntReady = true;
        }

        if (($scope.currentGiveCreditPage - 10) >= 0) {
            $scope.showLastGiveCreditIsntReady = false;
        }
        else {
            $scope.showLastGiveCreditIsntReady = true;
        }
    }



    $scope.showLastGiveCredit = function () {

        $scope.currentGiveCreditPage = $scope.currentGiveCreditPage - 10;
        var index = $scope.currentGiveCreditPage;

        printShowGiveCredits(index);
    }


    $scope.showNextGiveCredit = function () {

        $scope.currentGiveCreditPage = $scope.currentGiveCreditPage + 10;
        var index = $scope.currentGiveCreditPage;

        printShowGiveCredits(index);
    }

    $scope.showGiveCreditsClick = function () {
        // load gyms

        $scope.giveCredits = [];

        var data = {
            authToken: $routeParams.key,
        };

        mapService.getGiveCredits(data).then(function (resp) {
            if (resp.data.success) {

                for (var i = 0; i < resp.data.giveCredits.length; i++) {
                    $scope.giveCredits.push(resp.data.giveCredits[i]);
                }


                printShowGiveCredits($scope.currentGiveCreditPage);
                $scope.isShowGiveCredits = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }





    // GymInvoices

    $scope.gymInvoices = [];
    $scope.showGymInvoices = [];

    $scope.currentGymInvoicePage = 0;
    $scope.showLastGymInvoiceIsntReady = true;
    $scope.showNextGymInvoiceIsntReady = true;

    $scope.isShowGymInvoices = false;

    var printShowGymInvoices = function (index) {


        $scope.showGymInvoices = [];

        for (var i = index; i < (index + 10) ; i++) {
            if ($scope.gymInvoices[i]) {
                $scope.showGymInvoices.push($scope.gymInvoices[i]);
            }
        }

        if (($scope.currentGymInvoicePage + 10) < $scope.gymInvoices.length) {
            $scope.showNextGymInvoiceIsntReady = false;
        }
        else {
            $scope.showNextGymInvoiceIsntReady = true;
        }

        if (($scope.currentGymInvoicePage - 10) >= 0) {
            $scope.showLastGymInvoiceIsntReady = false;
        }
        else {
            $scope.showLastGymInvoiceIsntReady = true;
        }
    }



    $scope.showLastGymInvoice = function () {

        $scope.currentGymInvoicePage = $scope.currentGymInvoicePage - 10;
        var index = $scope.currentGymInvoicePage;

        printShowGymInvoices(index);
    }


    $scope.showNextGymInvoice = function () {

        $scope.currentGiveGymInvoice = $scope.currentGymInvoicePage + 10;
        var index = $scope.currentGymInvoicePage;

        printShowGymInvoices(index);
    }

    $scope.showGymInvoicesClick = function () {
        // load gyms

        $scope.gymInvoices = [];

        var data = {
            authToken: $routeParams.key,
        };

        mapService.getGymInvoices(data).then(function (resp) {
            if (resp.data.success) {

                for (var i = 0; i < resp.data.gymInvoices.length; i++) {
                    $scope.gymInvoices.push(resp.data.gymInvoices[i]);
                }


                printShowGymInvoices($scope.currentGymInvoicePage);
                $scope.isShowGymInvoices = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }







    // Logs

    $scope.logs = [];
    $scope.showLogs = [];

    $scope.currentLogPage = 0;
    $scope.showLastLogIsntReady = true;
    $scope.showNextLogIsntReady = true;

    $scope.isShowLogs = false;

    var printShowLogs = function (index) {


        $scope.showLogs = [];

        for (var i = index; i < (index + 10) ; i++) {
            if ($scope.logs[i]) {
                $scope.showLogs.push($scope.logs[i]);
            }
        }

        if (($scope.currentLogPage + 10) < $scope.logs.length) {
            $scope.showNextLogIsntReady = false;
        }
        else {
            $scope.showNextLogIsntReady = true;
        }

        if (($scope.currentLogPage - 10) >= 0) {
            $scope.showLastLogIsntReady = false;
        }
        else {
            $scope.showLastLogIsntReady = true;
        }
    }



    $scope.showLastLog = function () {

        $scope.currentLogPage = $scope.currentLogPage - 10;
        var index = $scope.currentLogPage;

        printShowLogs(index);
    }


    $scope.showNextLog = function () {

        $scope.currentLogPage = $scope.currentLogPage + 10;
        var index = $scope.currentLogPage;

        printShowLogs(index);
    }

    $scope.showLogsClick = function (isError, isAction) {
        // load logs

        $scope.logs = [];

        var data = {
            authToken: $routeParams.key,
            getError : isError,
            getAction : isAction,
        };

        mapService.getLogs(data).then(function (resp) {
            if (resp.data.success) {

                for (var i = 0; i < resp.data.logs.length; i++) {
                    $scope.logs.push(resp.data.logs[i]);
                }


                printShowLogs($scope.currentLogPage);
                $scope.isShowLogs = true;
            }
            else {
                $window.location.href = 'http://pedal.com';
            }
        });
    }



}]);

