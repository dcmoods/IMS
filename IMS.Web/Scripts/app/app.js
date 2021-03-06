﻿(function () {

    var app = angular.module('app', ['ngRoute']);

    app.config(['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/',
                {
                    templateUrl: 'Home/AllStockItems',
                    controller: 'StockController',
                    controllerAs: 'vm'
                })
                .when('/stock/new',
                {
                    templateUrl: 'Home/AddStockItem',
                    controller: 'AddStockController',
                    controllerAs: 'addStock'
                })
                .when('/stock/edit/:stockItemId',
                {
                    templateUrl: 'Home/EditStockItem',
                    controller: 'EditStockController',
                    controllerAs: 'editStock'
                })
                .when('/stock/:stockItemId/itemEntries',
                {
                    templateUrl: 'Home/ItemEntries',
                    controller: 'ItemEntryController',
                    controllerAs: 'stockItem'
                })
                .otherwise('/');

            $locationProvider.html5Mode(true).hashPrefix('');
    }]);
})();