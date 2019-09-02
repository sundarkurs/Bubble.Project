(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .constant('globalConstants', {
            brands: [
                "Avalon",
                "Cosmos",
                "Globus",
                "Monograms"
            ],

            userType: {
                Customer: "Customer",
                Agent: "Agent",
                Pilgrim: "Pilgrim",
                CustomerService: "CustomerService"
            }
        });
})();
