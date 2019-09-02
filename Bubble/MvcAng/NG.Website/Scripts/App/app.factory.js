(function () {
    'use strict';

    angular
        .module('ct.customerServicePortal')
        .factory('utils', function () {
            var service = {
                isUndefinedOrNull: function (obj) {
                    return !angular.isDefined(obj) || obj === null;
                }
            }
            return service;
        })

        .factory('dataWebApiRequest', dataWebApiRequest);

    dataWebApiRequest.$inject = ['$http', '$rootScope'];
    function dataWebApiRequest($http, $rootScope) {

        var service = {};
        service.get = httpGet;
        service.put = httpPut;
        service.post = httpPost;
        service.delete = httpDelete;

        function httpGet(actionMethod, callback, data, parameters, headers) {
            $http({
                url: $rootScope.dataWebApiUrl + actionMethod,
                method: "GET",
                data: data,
                params: parameters,
                headers: headers,
                timeout: $rootScope.webApiRequestTimeout
            }).then(function (response) {
                callback(response.data);
            }).catch(function (error) {
                console.log(error);
                console.log(error.config.url);
                // Angular timeout handle
                //if (error.status <= -1 && error.data == null) {
                //    ct.utls.showTimeoutMessage();
                //    ct.utls.hideLoading();
                //}
                //else {
                //    ct.utls.showApiStatusMessage(error.statusText);
                //}

            });
        }

        function httpPost(actionMethod, callback, data) {
            $http({
                url: $rootScope.dataWebApiUrl + actionMethod,
                method: "POST",
                data: data,
                timeout: $rootScope.webApiRequestTimeout
            }).then(function (response) {
                callback(response.data);
            }).catch(function (error) {
                console.log(error);
                console.log(error.config.url);
                // Angular timeout handle
                //if (error.status <= -1 && error.data == null) {
                //    ct.utls.showTimeoutMessage();
                //    ct.utls.hideLoading();
                //}
                //else {
                //    ct.utls.showApiStatusMessage(error.statusText);
                //}

            });
        }

        function httpPut(actionMethod, callback, data, parameters, headers) {
            $http({
                url: $rootScope.dataWebApiUrl + actionMethod,
                method: "PUT",
                data: data,
                params: parameters,
                headers: headers,
                timeout: $rootScope.webApiRequestTimeout
            }).then(function (response) {
                callback(response.data);
            }).catch(function (error) {
                console.log(error);
                console.log(error.config.url);
                // Angular timeout handle
                //if (error.status <= -1 && error.data == null) {
                //    ct.utls.showTimeoutMessage();
                //    ct.utls.hideLoading();
                //}
                //else {
                //    ct.utls.showApiStatusMessage(error.statusText);
                //    ct.utls.hideLoading();
                //}

            });
        }

        function httpDelete(actionMethod, callback, data, parameters, headers) {
            $http({
                url: $rootScope.dataWebApiUrl + actionMethod,
                method: "DELETE",
                data: data,
                params: parameters,
                headers: headers,
                timeout: $rootScope.webApiRequestTimeout
            }).then(function (response) {
                callback(response.data);
            }).catch(function (error) {
                console.log(error);
                console.log(error.config.url);
                // Angular timeout handle
                //if (error.status <= -1 && error.data == null) {
                //    ct.utls.showTimeoutMessage();
                //    ct.utls.hideLoading();
                //}
                //else {
                //    ct.utls.showApiStatusMessage(error.statusText);
                //    ct.utls.hideLoading();
                //}
            });
        }

        return service;
    };

})();
