(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .factory('dashboardService', dashboardService);

    dashboardService.$inject = ['$http', '$rootScope'];

    function dashboardService($http, $rootScope) {
        var service = {};
        service.GetAuthMessage = getAuthMessage;

        return service;

        function getAuthMessage(callback) {
            return [];
            //dataWebApiRequest.get("pendingchanges/auth", callback);
        }
    }
})();


