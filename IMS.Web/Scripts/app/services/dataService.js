(function () {

    angular.module('app')
        .factory('dataService', ['$q', '$http', dataService]);

    //Data service used for gathering data from API
    //Returns service functions to be used in the controllers
    //Client contoller is required to handle errors
    function dataService($q, $http) {
        return {
            getAllStockItems: getAllStockItems,
            getStockItemById: getStockItemById,
            addStockItem: addStockItem,
            updateStockItem: updateStockItem,
            deleteStockItem: deleteStockItem,
            insertItemEntry: insertItemEntry,
            useStockItem: useStockItem,
            getCategories: getCategories
        };

        //get stock items and callbacks
        function getAllStockItems() {
            return $http.get('api/stockitems/')
                .then(sendResponseData)
                .catch(sendGetErrors);
        }

        //get stock item by id
        function getStockItemById(stockItemId) {
            return $http.get('api/stockitems/' + stockItemId)
                .then(sendResponseData)
                .catch(sendGetErrors);
        }

        function sendResponseData(response) {
            return response.data;
        }

        function sendGetErrors(response) {
            $q.reject('Error retrieving item(s). (HTTP status: ' + response.status + ')');
        }

        //Add stock item and callbacks
        function addStockItem(stockItem) {
            return $http({
                method: 'POST',
                url: 'api/stockitems',
                data: stockItem
            })
                .then(addStockItemSuccess)
                .catch(addStockItemError)
        }

        function addStockItemSuccess(response) {
            return 'Stock item added: ' + response.config.data.StockItemId;
        }

        function addStockItemError(response) {
            $q.reject('Error adding item. (HTTP status: ' + response.status + ')');
        }

        //Update stock item and callbacks
        function updateStockItem(stockItem) {
            return $http.put('api/stockitems/' + stockItem.StockItemId, stockItem)
                .then(updateStockItemSuccess)
                .catch(updateStockItemError);
        }

        function updateStockItemSuccess(response) {
            return 'Stock item updated: ' + response.config.data.StockItemId;
        }

        function updateStockItemError(response) {
            $q.reject('Error updating item. (HTTP status: ' + response.status + ')');
        }

        //Delete stock item and callbacks
        function deleteStockItem(stockItemId) {
            return $http.delete('api/stockitems/' + stockItemId)
                .then(deleteStockItemSuccess)
                .catch(deleteStockItemError);
        }

        function deleteStockItemSuccess(response) {
            return 'Stock item deleted.'
        }

        function deleteStockItemError(response) {
            $q.reject('Error deleting item. (HTTP status: ' + response.status + ')');
        }


        function useStockItem(stockItemId) {
            return $http.post('api/stockitems/useStockItem/' + stockItemId)
                .then(useStockItemSuccess)
                .catch(useStockItemError);
        }

        function useStockItemSuccess(response) {
            return response.data;
        }

        function useStockItemError(response) {
            $q.reject('Error using item. (HTTP status: ' + response.status + ')');
        }


        function insertItemEntry(item) {
            return $http.post('api/stockitems/addItemEntry/' + item.StockItemId  ,  item)
               .then(updateStockItemSuccess)
               .catch(updateStockItemError);
        }

        function getCategories() {
            return $http.get('api/categories/')
                    .then(sendResponseData)
                    .catch(sendGetErrors);
        }
    }


})();