(function () {
    angular.module('app')
        .controller('EditStockController', ['$log', '$location', '$routeParams', 'dataService', EditStockController]);

    function EditStockController($log, $location, $routeParams, dataService) {
        var vm = this;

        //get stock item by key
        dataService.getStockItemById($routeParams.stockItemId)
            .then(getStockItemSuccess)
            .catch(getStockItemError);
        
        function getStockItemSuccess(stockItem) {
            vm.stockItem = stockItem;
        }

        function getStockItemError(err) {
            vm.error = err;
        }

        //get categories for stock items
        dataService.getCategories()
            .then(getCategoriesSuccess)
            .catch(getCategoriesError);

        function getCategoriesSuccess(data) {
            vm.categories = data;
            $log.info(data);
        }

        function getCategoriesError(response) {
            $log.error(response)
        }


        //save stock item 
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