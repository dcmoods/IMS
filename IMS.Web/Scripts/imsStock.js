(function () {
    var app = angular.module('imsApp', []),
        uri = 'api/stockitems',
        errorMessage = function (data, status) {
            return 'Error: ' + status +
                (data.Message !== undefined ? (' ' + data.Message) : '');
        },
        hub = $.connection.stockHub; // create a proxy to signalr hub on web server
    
    app.controller('imsCtrl', ['$http', '$scope', function ($http, $scope) {
        $scope.stockItems = [];
        $scope.customerIdSubscribed;

        var init = function () {       
            $http.get(uri)
                .then(function (data, status) {
                    $scope.stockItems = data.data;
                    console.log(data.data);
                    
                    //if ($scope.customerIdSubscribed &&
                    //    $scope.customerIdSubscribed.length > 0 &&
                    //    $scope.customerIdSubscribed !== $scope.customerId) {
                    //    // unsubscribe to stop to get notifications for old customer
                    //    hub.server.unsubscribe($scope.customerIdSubscribed);
                    //}
                    // subscribe to start to get notifications for new customer
                    hub.server.subscribe();
                    //$scope.customerIdSubscribed = $scope.customerId;
                },
                (function (data, status) {
                    $scope.stockItems = [];
                    $scope.errorToSearch = errorMessage(data, status);
                }))
        };

        $scope.postOne = function () {
            $http.post(uri, {
                StockItemId: 0,                
                Name: $scope.NameToAdd,
                Description: $scope.DescriptionToAdd,
                MinimumLevel: $scope.MinimumLevelToAdd,
                MaximumLevel: $scope.MaximumLevelToAdd,
                LevelUnit: $scope.LevelUnitToAdd,
                PricePerUnit: $scope.PricePerUnitToAdd,
                Quantity: $scope.QuantityToAdd,
                Temperature: $scope.TemperatureToAdd                
            })
            .then(function (data, status) {
                $scope.errorToAdd = null;
                $scope.stockItemIdToAdd = null;
                $scope.NameToAdd = null;
                $scope.DescriptionToAdd = null;
                $scope.MinimumLevelToAdd = null;
                $scope.MaximumLevelToAdd = null;
                $scope.LevelUnitToAdd = null;
                $scope.PricePerUnitToAdd = null;
                $scope.QuantityToAdd = null;
                $scope.TemperatureToAdd = null;
            },
            function (data, status) {
                $scope.errorToAdd = errorMessage(data, status);
            })
        };

        $scope.putOne = function () {
            $http.put(uri + '/' + $scope.stockItemIdToUpdate, {
                StockItemId: $scope.stockItemIdToUpdate,
                Name: $scope.NameToUpdate,
                Description: $scope.DescriptionToUpdate,
                MinimumLevel: $scope.MinimumLevelToUpdate,
                MaximumLevel: $scope.MaximumLevelToUpdate,
                LevelUnit: $scope.LevelUnitToUpdate,
                PricePerUnit: $scope.PricePerUnitToUpdate,
                Quantity: $scope.QuantityToUpdate,
                Temperature: $scope.TemperatureToUpdate
            })
                .then(function (data, status) {
                    $scope.errorToUpdate = null;
                    $scope.stockItemIdToUpdate = null;
                    $scope.NameToUpdate = null;
                    $scope.DescriptionToUpdate = null;
                    $scope.MinimumLevelToUpdate = null;
                    $scope.MaximumLevelToUpdate = null;
                    $scope.LevelUnitToUpdate = null;
                    $scope.PricePerUnitToUpdate = null;
                    $scope.QuantityToUpdate = null;
                    $scope.TemperatureToUpdate = null;
                }, function (data, status) {
                    $scope.errorToUpdate = errorMessage(data, status);
                })
        };
        //$scope.deleteOne = function (item) {
        //    $http.delete(uri + '/' + item.COMPLAINT_ID)
        //        .success(function (data, status) {
        //            $scope.errorToDelete = null;
        //        })
        //        .error(function (data, status) {
        //            $scope.errorToDelete = errorMessage(data, status);
        //        })
        //};
        $scope.editIt = function (item) {
            $scope.stockItemIdToUpdate = item.StockItemId;
            $scope.NameToUpdate = item.Name;
            $scope.DescriptionToUpdate = item.Description;
            $scope.MinimumLevelToUpdate = item.MinimumLevel;
            $scope.MaximumLevelToUpdate = item.MaximumLevel;
            $scope.LevelUnitToUpdate = item.LevelUnit;
            $scope.PricePerUnitToUpdate = item.PricePerUnit;
            $scope.QuantityToUpdate = item.Quantity;
            $scope.TemperatureToUpdate = item.Temperature;
        };

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

        hub.client.updateItem = function (item) {
            var array = $scope.complaints;
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].COMPLAINT_ID === item.COMPLAINT_ID) {
                    array[i].DESCRIPTION = item.DESCRIPTION;
                    $scope.$apply();
                }
            }
        }

        

        $.connection.hub.start().done(function(){
            init();
        }); // connect to signalr hub
    }]);
        
})();