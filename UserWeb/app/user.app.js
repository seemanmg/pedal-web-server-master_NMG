var userApp = angular.module('userApp', [
  'ngRoute',
  'ngCookies',
]);

userApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/resetPassword/:accountId/:authToken', {
            templateUrl: '/app/components/user/resetPassword/views/resetPasswordView.html',
            controller: 'resetPasswordController'
        }).
        when('/airbnb/:airbnbHostId', {
            templateUrl: '/app/components/user/airbnbRegister/views/airbnbRegisterView.html',
            controller: 'airbnbRegisterController'
        }).
        otherwise({
            controller: "redirect",
            template: "<div></div>"
        });






  }]).run(['$rootScope', '$location', function ($rootScope, $location) {
      var path = function () { return $location.path(); };
      $rootScope.$watch(path, function (newVal, oldVal) {
          $rootScope.activetab = newVal;
      });
  }]);