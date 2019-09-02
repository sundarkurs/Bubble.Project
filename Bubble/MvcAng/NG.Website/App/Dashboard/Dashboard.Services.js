(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .factory('dashboardService', dashboardService);

    dashboardService.$inject = ['$http', '$rootScope', 'dataWebApiRequest'];

    function dashboardService($http, $rootScope, dataWebApiRequest) {
        var service = {};
        service.GetPendingChanges = getPendingChanges;
        service.GetUsers = getUsers;

        return service;

        function getPendingChanges(callback) {
            dataWebApiRequest.get("pendingchanges", callback);
        }

        function getUsers(callback) {

            $http({
                url: "Dashboard/GetUsers",
                method: "GET",
            }).then(function (response) {
                callback(response.data);
            }).catch(function (error) {
                console.log(error);
            });

        };
    }
})();


