﻿angular.module('mainApp').service('frontService', function ($http) {
    var baseUrl = window.apiUrl;

    this.contactForm = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'contactForm',
            data: data
        });
    };


    this.exclusiveUpdatesForm = function (data) {
        return $http({
            method: 'POST',
            url: baseUrl + 'exclusiveUpdatesForm',
            data: data
        });
    };

});