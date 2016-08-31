"use strict";

service.service('UserService', function ($http) {

    // Function to create new Account
    this.register = function (newUser) {
        var request = $http({
            method: 'post',
            url: '/api/UserApi/Register',
            data: newUser
        });

        return request;
    }
    // Function to check login status
    this.checkLoginStatus = function () {
        var request = $http({
            method: 'get',
            url: '/api/UserApi/CheckLoginStatus',
        });

        return request;
    }

    // Function to poke other user
    this.pokeOtherUsers = function (user) {
        var request = $http({
            method: 'post',
            url: '/api/UserApi/PokeUser',
            data: user
        });

        return request;
    }
});