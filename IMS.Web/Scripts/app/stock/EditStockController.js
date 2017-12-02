(function () {
    angular.module('app')
        .controller('EditStockController', ['$log', '$location', '$routeParams', 'dataService', EditStockController]);

    function EditStockController($log, $location, $routeParams, dataService) {
        var vm = this;

        dataService.getStockItemById($routeParams.stockItemId)
            .then(getStockItemSuccess)
            .catch(getStockItemError);

        function getStockItemSuccess(stockItem) {
            vm.stockItem = stockItem;
        }

        function getStockItemError(err) {
            vm.error = err;
        }

        vm.saveStockItem = function () {
            dataService.updateStockItem(vm.stockItem)
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