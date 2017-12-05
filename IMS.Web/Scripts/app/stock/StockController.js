(function () {
    angular.module('app')
        .controller('StockController', ['dataService', 'signalRHubProxy', '$log', StockController]);

    function StockController(dataService, signalRHubProxy, $log) {
        var vm = this;
        
        dataService.getAllStockItems()
            .then(getStockSuccess)
            .catch(getStockError);

        function getStockSuccess(stockItems) {
            vm.stockItems = stockItems;
        }

        function getStockError(err) {
            $log.error(err);
            vm.error = err;
        }


        vm.useStockItem = function (stockItemId) {
            dataService.useStockItem(stockItemId)
                .then(callbackStockItemSuccess)
                .catch(callbackStockItemError);
        }

        function callbackStockItemSuccess(data) {
            $log.data;
        }

        function callbackStockItemError(err) {
            $log.error(err);
            vm.error = err;
        }

        vm.deleteStockItem = function (stockItemId) {
            dataService.deleteStockItem(stockItemId)
                .then(callbackStockItemSuccess)
                .catch(callbackStockItemError);
        }

        //Connect to Stock Hub for live updates
        var stockHub = signalRHubProxy('stockHub');
        $log.info(stockHub.connection);


        stockHub.on('addItem', function (data) {
            vm.stockItems.push(data);
        });

        stockHub.on('updateItem', function (data) {
            var array = vm.stockItems;
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].StockItemId === data.StockItemId) {
                    array[i] = data;
                }
            }
        });

        stockHub.on('addItemEntry', function (data) {
            var array = vm.stockItems;
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].StockItemId === data.StockItemId) {
                    array[i].ItemEntries.push(data);                   
                }
            }
        });

        stockHub.on('deleteStockItem', function (data) {
            vm.stockItems = data;
        })

        

    }
})();

