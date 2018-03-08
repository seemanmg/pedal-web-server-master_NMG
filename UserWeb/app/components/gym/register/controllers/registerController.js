angular.module('gymApp').controller("registerController", ['$scope', '$routeParams', 'registerService', 'contractService', '$cookies', 'navBarService', 'dashboardContactService', function ($scope, $routeParams, registerService, contractService, $cookies, navBarService, dashboardContactService) {

   
    $scope.graph = {};
    $scope.graph.labels = ["50", "100", "200", "300", "400", "500", "600"];
    $scope.graph.series = ['Equivalent Gym Memberships'];
    $scope.graph.data = [
      [5, 11, 22, 34, 46, 58, 70]
    ];
    $scope.graph.options = {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Equivalent Memberships',

                }
            }],
            xAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: 'Pedal Visits',
                }
            }]
        }
    };
    $scope.graph.colors = ['#555555', '#555555', '#555555', '#555555'];

    var fillInAddress = function () {
        // Get the place details from the autocomplete object.
        var place = autocomplete.getPlace();

        // Get each component of the address from the place details
        // and fill the corresponding field on the form.
        for (var i = 0; i < place.address_components.length; i++) {
            var addressType = place.address_components[i].types[0];
            if (addressType == "country") {
                country = place.address_components[i].short_name;
            }
            if (addressType == "administrative_area_level_1") {
                state = { "shortName": place.address_components[i].short_name, "fullName": place.address_components[i].long_name };
                $scope.state = state;
            }
            if (addressType == "locality") {
                city = place.address_components[i].long_name;
            }
        }
    }

    $scope.isRegistering = false;

    $scope.submitGym = function () {
        $scope.isRegistering = true;
        var fullData = getGymData();
        
        registerService.register(fullData).then(function (resp) {
            if (resp.data.success) {
                $scope.isRegistering = false;
                $cookies.authToken = resp.data.token;
                $cookies.id = resp.data.gymId;
                navBarService.gym = resp.data;
                var url = '/gym.html#/dashboard/account';
                window.location.href = url;
                
            }
            else {
                $scope.isRegistering = false;
                $scope.errorMessage = resp.data.message;
                $scope.showErrorMessage = true;
            }

        });
    }


    var initialize = function () {
        // Create the autocomplete object, restricting the search
        //  to geographical location types.

        if ($cookies.authToken && $cookies.id) {
            var url = '/gym.html#/dashboard';
            window.location.href = url;
        }
        else {
            navBarService.showNavBar = false;


            var options = {
                types: ['(cities)'],
                componentRestrictions: { country: "us" },
            };


            autocomplete = new google.maps.places.Autocomplete(
                /** @type {HTMLInputElement} */(document.getElementById('autocomplete')),
                options);
            // When the user selects an address from the dropdown,
            // populate the address fields in the form.
            google.maps.event.addListener(autocomplete, 'place_changed', function () {
                fillInAddress();
                $scope.$apply();
            });
        }
    }


    var getGymData = function () {
        return {
            gymName: $scope.gymName,
            gymPhone: $scope.gymPhone,
            contactName: $scope.contactName,
            contactEmail: $scope.contactEmail,
            password: $scope.password,
            contactPhone: $scope.contactPhone,
            website: $scope.website,
            address: $scope.address,
            state: state.shortName || $scope.state.shortName,
            city: city,
            zip: $scope.postal_code,
            country: country,
            priceTier: $scope.priceTier,
        }
    };

    $scope.showContract = function showContract() {
        var _data = getGymData();
        var _name = _data.gymName;
        var _priceTier = _data.priceTier;

        if (!_name) {
            _name = "YOUR GYM";
        }

        var _url = "#/contract/" + _name + "/" + _priceTier;
        window.open(_url, '_blank');
    }

    $scope.states = [
            { shortName: 'AB', fullName: 'Alberta' },
            { shortName: 'AL', fullName: 'Alabama' },
            { shortName: 'AK', fullName: 'Alaska' },
            { shortName: 'AZ', fullName: 'Arizona' },
            { shortName: 'AR', fullName: 'Arkansas' },
            { shortName: 'BC', fullName: 'British' },
            { shortName: 'CA', fullName: 'California' },
            { shortName: 'CO', fullName: 'Colorado' },
            { shortName: 'CT', fullName: 'Connecticut' },
            { shortName: 'DE', fullName: 'Delaware' },
            { shortName: 'DC', fullName: 'District Of Columbia' },
            { shortName: 'FL', fullName: 'Florida' },
            { shortName: 'GA', fullName: 'Georgia' },
            { shortName: 'HI', fullName: 'Hawaii' },
            { shortName: 'ID', fullName: 'Idaho' },
            { shortName: 'IL', fullName: 'Illinois' },
            { shortName: 'IN', fullName: 'Indiana' },
            { shortName: 'IA', fullName: 'Iowa' },
            { shortName: 'KS', fullName: 'Kansas' },
            { shortName: 'KY', fullName: 'Kentucky' },
            { shortName: 'LA', fullName: 'Louisiana' },
            { shortName: 'ME', fullName: 'Maine' },
            { shortName: 'MB', fullName: 'Manitoba' },
            { shortName: 'MD', fullName: 'Maryland' },
            { shortName: 'MA', fullName: 'Massachusetts' },
            { shortName: 'MI', fullName: 'Michigan' },
            { shortName: 'MN', fullName: 'Minnesota' },
            { shortName: 'MS', fullName: 'Mississippi' },
            { shortName: 'MO', fullName: 'Missouri' },
            { shortName: 'MT', fullName: 'Montana' },
            { shortName: 'NE', fullName: 'Nebraska' },
            { shortName: 'NV', fullName: 'Nevada' },
            { shortName: 'NB', fullName: 'New Brunswick' },
            { shortName: 'NH', fullName: 'New Hampshire' },
            { shortName: 'NJ', fullName: 'New Jersey' },
            { shortName: 'NM', fullName: 'New Mexico' },
            { shortName: 'NY', fullName: 'New York' },
            { shortName: 'NL', fullName: 'Newfoundland' },
            { shortName: 'NC', fullName: 'North Carolina' },
            { shortName: 'ND', fullName: 'North Dakota' },
            { shortName: 'NT', fullName: 'Northwest Territories' },
            { shortName: 'NS', fullName: 'Nova Scotia' },
            { shortName: 'NU', fullName: 'Nunavut' },
            { shortName: 'OH', fullName: 'Ohio' },
            { shortName: 'OK', fullName: 'Oklahoma' },
            { shortName: 'ON', fullName: 'Ontario' },
            { shortName: 'OR', fullName: 'Oregon' },
            { shortName: 'PA', fullName: 'Pennsylvania' },
            { shortName: 'PE', fullName: 'Prince Edward Island' },
            { shortName: 'QC', fullName: 'Quebec' },
            { shortName: 'RI', fullName: 'Rhode Island' },
            { shortName: 'SK', fullName: 'Saskatchewan' },
            { shortName: 'SC', fullName: 'South Carolina' },
            { shortName: 'SD', fullName: 'South Dakota' },
            { shortName: 'TN', fullName: 'Tennessee' },
            { shortName: 'TX', fullName: 'Texas' },
            { shortName: 'UT', fullName: 'Utah' },
            { shortName: 'VT', fullName: 'Vermont' },
            { shortName: 'VA', fullName: 'Virginia' },
            { shortName: 'WA', fullName: 'Washington' },
            { shortName: 'WV', fullName: 'West Virginia' },
            { shortName: 'WI', fullName: 'Wisconsin' },
            { shortName: 'WY', fullName: 'Wyoming' },
            { shortName: 'YT', fullName: 'Yukon' },
    ];


    var placeSearch, autocomplete;
    var componentForm = {
        locality: 'long_name',
        administrative_area_level_1: 'short_name',
        postal_code: 'short_name'
    };

    var city = "";
    var state = "";
    var country = "";
    $scope.state = "";
    $scope.showErrorMessage = false;
    $scope.priceTier = "budget";
    $scope.gymName = "";
    initialize();


    // contact form 
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
                $scope.errorMessageContact = resp.data.message;
            }

        });

    }

}]);