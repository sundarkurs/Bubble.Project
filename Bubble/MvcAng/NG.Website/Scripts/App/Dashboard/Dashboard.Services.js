(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .factory('dashboardService', dashboardService);

    dashboardService.$inject = ['$http', '$rootScope', 'dataWebApiRequest'];

    function dashboardService($http, $rootScope, dataWebApiRequest) {
        var service = {};
        service.GetPendingChanges = getPendingChanges;

        return service;

        function getPendingChanges(callback) {
            dataWebApiRequest.get("pendingchanges", callback);
        }
    }
})();


