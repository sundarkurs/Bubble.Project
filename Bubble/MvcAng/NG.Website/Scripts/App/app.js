(function () {
    'use strict';

    angular
        .module("ct.customerServicePortal", ['ngRoute', 'ngCookies'])
        .run(run);

    run.$inject = ['$log', '$http', '$rootScope', '$location', '$cookies', 'globalConstants'];
    function run($log, $http, $rootScope, $location, $cookies, globalConstants) {

        $log.log('run customer service');
        $rootScope.clientName = "CustomerServicePortal";
        $rootScope.accessToken = $("#hdnAccessToken").val();

        $http.defaults.headers.common["ApplicationType"] = 'CustomerServicePortal';
        $http.defaults.headers.common["Authorization"] = 'Bearer ' + $rootScope.accessToken;

        angular.extend($rootScope, {
            dataWebApiUrl: 'http://dev.data.globusfamily.com.au/api/',
            webApiRequestTimeout: 300000,
            dateFormat: "dd/MM/yyyy"
        });

    }

})();
