(function () {

    var app = angular.module('app', ['ngRoute']);

    app.config(['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            debugger;
            $routeProvider
                .when('/',
                {
                    templateUrl: 'Home/AllStockItems',
                    controller: 'StockController',
                    controllerAs: 'vm'
                })
                .otherwise('/');

            $locationProvider.html5Mode(true).hashPrefix('');
    }]);
})();