var adminApp = angular.module('adminApp', [
  'ngRoute',
]);

adminApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/map/:key', {
            templateUrl: '/app/components/admin/map/views/mapView.html',
            controller: 'mapController'
        }).
        otherwise({
            redirectTo: 'http://pedal.com'
        });

  }]);