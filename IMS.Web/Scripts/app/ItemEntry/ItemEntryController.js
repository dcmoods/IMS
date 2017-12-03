(function () {
    angular.module('app')
        .controller('ItemEntryController', ['$log', '$location', '$routeParams', 'dataService', ItemEntryController]);

    function ItemEntryController($log, $location, $routeParams, dataService) {
        var vm = this;

        vm.newItemEntry = {};

        dataService.getStockItemById($routeParams.stockItemId)
            .then(getStockItemSuccess)
            .catch(getStockItemError);

        function getStockItemSuccess(stockItem) {
            vm.stockItem = stockItem;
            vm.newItemEntry.StockItemId = stockItem.StockItemId;
        }

        function getStockItemError(err) {
            vm.error = err;
        }

        vm.saveStockItem = function () {
            //vm.stockItem.ItemEntries.push(vm.newItemEntry);
            dataService.insertItemEntry(vm.newItemEntry)
                .then(updateStockItemSuccess)
                .catch(updateStockItemError);
        }

        function updateStockItemSuccess(response) {
            $log.info(response);
            $location.path('/');
        }

        function updateStockItemError(err) {
            $log.error(err);
        }

    }
})();