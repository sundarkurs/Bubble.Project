(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$rootScope', '$scope', '$http', '$location', 'dashboardService'];

    function dashboardController($rootScope, $scope, $http, $location, dashboardService) {

        angular.extend($scope, {
            message: "Welcome to customer service portal",
            pendingChanges: []
        });

        (function init() {
            console.log('init dashboardController');
            dashboardService.GetPendingChanges(function (result) {

                if (result && result.IsSuccess) {
                    if (result.Data && result.Data.length > 0) {
                        $scope.pendingChanges = result.Data;
                    }
                }

            });

        })();

        $scope.getUsers = function () {
            dashboardService.GetUsers(function (result) {
                $scope.users = result;
            });
        }

    }

})();
