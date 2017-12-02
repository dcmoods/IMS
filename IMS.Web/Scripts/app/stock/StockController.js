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

        var stockHub = signalRHubProxy('stockHub', { logging: true });

        stockHub.on('addItem', function (data) {
            vm.stockItems.push(data);
        });

        stockHub.on('updateItem', function (data) {
            var array = vm.stockItems;
            for (var i = array.length - 1; i >= 0; i--) {
                if (array[i].StockItemId === data.StockItemId) {
                    array[i].Name = data.Name;
                    array[i].Description = data.Description;
                    array[i].MinimumLevel = data.MinimumLevel;
                    array[i].MaximumLevel = data.MaximumLevel;
                    array[i].LevelUnit = data.LevelUnit;
                }
            }
        });

        $log.info(stockHub.connection);

        //init();
        
    }
})();

