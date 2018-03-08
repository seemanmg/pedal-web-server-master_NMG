var gymApp = angular.module('gymApp', [
  'ngRoute',
  'ngFileUpload',
  'ngCookies',
  'chart.js',
  'ngFileUpload',
  'ngImgCrop',
]);

gymApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/login', {
            templateUrl: '/app/components/gym/login/views/loginView.html',
            controller: 'loginController'
        }).
        when('/register', {
              templateUrl: '/app/components/gym/register/views/registerView.html',
              controller: 'registerController'
        }).
        when('/contract/:name/:priceTier', {
            templateUrl: '/app/components/gym/register/views/contractView.html',
            controller: 'contractController'
        }).
        when('/dashboard', {
            templateUrl: '/app/components/gym/dashboardMain/views/dashboardMainView.html',
            controller: 'dashboardMainController'
        }).
        when('/dashboard/account', {
            templateUrl: '/app/components/gym/dashboardAccount/views/dashboardAccountView.html',
            controller: 'dashboardAccountController'
        }).
        when('/dashboard/contact', {
            templateUrl: '/app/components/gym/dashboardContact/views/dashboardContactView.html',
            controller: 'dashboardContactController'
        }).
        when('/dashboard/contract', {
            templateUrl: '/app/components/gym/dashboardContract/views/dashboardContractView.html',
            controller: 'dashboardContractController'
        }).
        when('/forgotPassword', {
            templateUrl: '/app/components/gym/forgotPassword/views/forgotPasswordView.html',
            controller: 'forgotPasswordController'
        }).
        when('/resetPassword/:accountId/:authToken', {
            templateUrl: '/app/components/gym/resetPassword/views/resetPasswordView.html',
            controller: 'resetPasswordController'
        }).
        otherwise({
            redirectTo: '/login'
        });

  }]).run(['$rootScope', '$location', function ($rootScope, $location) {
      function getPosition(str, m, i) {
          return str.split(m, i).join(m).length;
      }
      var path = function () { return $location.path(); };
      $rootScope.$watch(path, function (newVal, oldVal) {
          var secondSlash = getPosition(newVal, '/', 2);
          //$rootScope.activetab = newVal;
          $rootScope.activetabWholeUrl = newVal;
          $rootScope.activetab = newVal.substr(0, secondSlash);
      });
  }]);