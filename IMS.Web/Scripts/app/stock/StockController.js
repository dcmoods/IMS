(function () {
    angular.module('app')
        .controller('StockController',['dataService', StockController]);

    function StockController(dataService) {
        var vm = this;

        dataService.getAllStockItems()
            .then(getStockSuccess)
            .catch(getStockError);

        function getStockSuccess(stockItems) {
            vm.stockItems = stockItems;
        }

        function getStockError(err) {
            console.log(err);
        }
    }
})();

