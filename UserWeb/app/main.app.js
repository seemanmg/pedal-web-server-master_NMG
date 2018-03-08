var mainApp = angular.module('mainApp', [
  'ngRoute',
]);

mainApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/terms', {
            templateUrl: '/app/components/main/terms/views/termsView.html',
        }).
        when('/front', {
            templateUrl: '/app/components/main/front/views/frontView.html',
            controller: 'frontController',
        }).
        when('/privacy', {
            templateUrl: '/app/components/main/privacy/views/privacyView.html',
        }).
        otherwise({
            redirectTo: '/front'
        });

  }]);
