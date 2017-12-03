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
                .then(useStockItemSuccess)
                .catch(useStockItemError);
        }

        function useStockItemSuccess(data) {
            //vm.stockItems = stockItems;
        }

        function useStockItemError(err) {
            $log.error(err);
            vm.error = err;
        }

        //Connect to Stock Hub for live updates
        var stockHub = signalRHubProxy('stockHub');
        $log.info(stockHub.connection);


        stockHub.on('addItem', function (data) {
            vm.stockItems.push(data);
        });

        stockHub.on('updateItem', function (data) {
            $log.info(data);
            var array = vm.stockItems;
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].StockItemId === data.StockItemId) {
                    array[i].Name = data.Name;
                    array[i].Description = data.Description;
                    array[i].MinimumLevel = data.MinimumLevel;
                    array[i].MaximumLevel = data.MaximumLevel;
                    array[i].LevelUnit = data.LevelUnit;
                    for (var j = array[i].ItemEntries.length - 1; j >= 0; j--) {
                        array[i].ItemEntries[j].Quantity = data.ItemEntries[j].Quantity;
                        array[i].ItemEntries[j].PricePerUnit = data.ItemEntries[j].PricePerUnit;
                        array[i].ItemEntries[j].ReceivedDate = data.ItemEntries[j].ReceivedDate;
                        array[i].ItemEntries[j].ExpirationDate = data.ItemEntries[j].ExpirationDate;
                        array[i].ItemEntries[j].Temperature = data.ItemEntries[j].Temperature;
                    }
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

        

    }
})();

