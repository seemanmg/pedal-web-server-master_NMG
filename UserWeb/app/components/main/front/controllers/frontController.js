angular.module('mainApp').controller("frontController", ['$scope', 'frontService', function ($scope, frontService) {
   
    $scope.sendEmailSuccessful = false;
    $scope.isSending = false;

    $scope.sendContactForm = function () {

        $scope.isSending = true;

        var data = {
            email: $scope.email,
            name: $scope.name,
            message: $scope.message,
        };

        frontService.contactForm(data).then(function (resp) {
            if (resp.data.success == true) {
                $scope.sendEmailSuccessful = true;
            }
            else {
                $scope.isSending = false;
                $scope.errorMessage = resp.data.message;
            }

        });

    }


    var getExclusiveData = function () {
        return {
            firstname: $scope.firstname,
            lastname: $scope.lastname,
            emailaddress: $scope.emailaddress,
            phonenumber: $scope.phonenumber,
            state: $scope.state.shortName,
            city: $scope.city,

        }
    };

   // $scope.isRegistering = false;

    $scope.submitExclusiveUpdateForm() = function () {
        
       // $scope.isRegistering = true;

        var fullData = getExclusiveData();

        frontService.exclusiveUpdatesForm(fullData).then(function (resp) {
            if (resp.data.success) {
               // $scope.isRegistering = false;
                

            }
            else {
              //  $scope.isRegistering = false;
                $scope.errorMessage = resp.data.message;
                $scope.showErrorMessage = true;
            }

        });

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


    


}]);
