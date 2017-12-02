(function () {

    angular.module('app')
        .factory('signalRHubProxy', ['$rootScope', 'constants',
            function ($rootScope, constants) {
                function signalRHubProxyFactory(hubName, startOptions) {
                    var connection = $.hubConnection();
                    var proxy = connection.createHubProxy(hubName);
                    connection.start({ logging: true }).done(function () {
                        console.log('Now connected, connection ID=' + connection.id);
                        proxy.invoke('subscribe');
                    });

                    return {
                        on: function (eventName, callback) {
                            proxy.on(eventName, function (result) {
                                $rootScope.$apply(function () {
                                    if (callback) {
                                        callback(result);
                                    }
                                });
                            });
                        },
                        off: function (eventName, callback) {
                            proxy.off(eventName, function (result) {
                                $rootScope.$apply(function () {
                                    if (callback) {
                                        callback(result);
                                    }
                                });
                            });
                        },
                        invoke: function (methodName, callback) {
                            proxy.invoke(methodName)
                                .done(function (result) {
                                    $rootScope.$apply(function () {
                                        if (callback) {
                                            callback(result);
                                        }
                                    });
                                });
                        },
                        connection: connection
                    };
                };

                return signalRHubProxyFactory;
            }]);
})();