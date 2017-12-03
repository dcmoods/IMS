(function () {
    angular.module('app')
        .controller('AddStockController', ['$log', '$location', 'dataService', AddStockController]);

    function AddStockController($log, $location, dataService) {
        var vm = this;

        vm.newStockItem = {};

        vm.addStockItem = function () {
            dataService.addStockItem(vm.newStockItem)
                .then(addStockSuccess)
                .catch(addStockError);
        };

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

        function addStockSuccess(response) {
            $log.info(response);
            $location.path('/');
        }

        function addStockError(err) {
            $log.error(err);
            vm.error = err;
        }
    }
})();