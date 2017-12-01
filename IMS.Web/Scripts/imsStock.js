(function () {
    var app = angular.module('imsApp', []),
        uri = 'api/stockitems',
        errorMessage = function (data, status) {
            return 'Error: ' + status +
                (data.Message !== undefined ? (' ' + data.Message) : '');
        },
        hub = $.connection.imsStock; // create a proxy to signalr hub on web server
    
    app.controller('imsCtrl', ['$http', '$scope', function ($http, $scope) {
        
        $scope.stockItems = [];
        $scope.customerIdSubscribed;

        var init = function () {       
            $http.get(uri)
                .then(function (data, status) {
                    $scope.stockItems = data.data;
                    hub.server.subscribe();
                    
                },
                (function (data, status) {
                    $scope.stockItems = [];
                    $scope.errorToSearch = errorMessage(data, status);
                }))
        };
        $scope.postOne = function () {
            $http.post(uri, {
                StockItemId: 0,                
                Name: $scope.Name,
                Description: $scope.Description,
                MinimumLevel: $scope.MinimumLevel,
                MaximumLevel: $scope.MaximumLevel,
                LevelUnit: $scope.LevelUnit,
                PricePerUnit: $scope.PricePerUnit,
                Quantity: $scope.Quantity,
                Temperature: $scope.Temperature                
            })
            .then(function (data, status) {
                $scope.errorToAdd = null;                
            },
            function (data, status) {
                $scope.errorToAdd = errorMessage(data, status);
            })
        };
        //$scope.putOne = function () {
        //    $http.put(uri + '/' + $scope.idToUpdate, {
        //        COMPLAINT_ID: $scope.idToUpdate,
        //        CUSTOMER_ID: $scope.customerId,
        //        DESCRIPTION: $scope.descToUpdate
        //    })
        //        .success(function (data, status) {
        //            $scope.errorToUpdate = null;
        //            $scope.idToUpdate = null;
        //            $scope.descToUpdate = null;
        //        })
        //        .error(function (data, status) {
        //            $scope.errorToUpdate = errorMessage(data, status);
        //        })
        //};
        //$scope.deleteOne = function (item) {
        //    $http.delete(uri + '/' + item.COMPLAINT_ID)
        //        .success(function (data, status) {
        //            $scope.errorToDelete = null;
        //        })
        //        .error(function (data, status) {
        //            $scope.errorToDelete = errorMessage(data, status);
        //        })
        //};
        //$scope.editIt = function (item) {
        //    $scope.idToUpdate = item.COMPLAINT_ID;
        //    $scope.descToUpdate = item.DESCRIPTION;
        //};
        $scope.toShow = function () {
            return $scope.stockItems && $scope.stockItems.length > 0;
        };

        $scope.orderProp = 'StockItemId';

        //signalr client functions
        hub.client.addItem = function (item) {
            $scope.stockItems.push(item);
            $scope.$apply(); // this is outside of angularjs, so need to apply
        }
        //hub.client.deleteItem = function (item) {
        //    var array = $scope.complaints;
        //    for (var i = array.length - 1; i >= 0; i--) {
        //        if (array[i].COMPLAINT_ID === item.COMPLAINT_ID) {
        //            array.splice(i, 1);
        //            $scope.$apply();
        //        }
        //    }
        //}
        //hub.client.updateItem = function (item) {
        //    var array = $scope.complaints;
        //    for (var i = array.length - 1; i >= 0; i--) {
        //        if (array[i].COMPLAINT_ID === item.COMPLAINT_ID) {
        //            array[i].DESCRIPTION = item.DESCRIPTION;
        //            $scope.$apply();
        //        }
        //    }
        //}

        
        $.connection.hub.start().done(function () {
            init();
        }); // connect to signalr hub
        
    }]);
        
})();