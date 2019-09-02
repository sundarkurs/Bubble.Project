(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$rootScope', '$scope', '$http', '$location', 'dashboardService'];

    function dashboardController($rootScope, $scope, $http, $location, dashboardService) {

        angular.extend($scope, {
            message: "Welcome to customer service portal"
        });

        (function init() {
            console.log('init dashboardController');

            dashboardService.GetAuthMessage(function () {
                debugger;
            });

        })();

    }

})();
