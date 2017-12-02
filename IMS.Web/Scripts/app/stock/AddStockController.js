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