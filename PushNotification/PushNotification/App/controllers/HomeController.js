"use strict";

app.controller('HomeController', function ($scope, $rootScope, $sce, $window, UserService) {
    
    // Function check string startwith 'http'
    $scope.checkHTTP = function (input) {
        var lowerStr = (input + "").toLowerCase();
        return lowerStr.indexOf('http') === 0;
    }

    $scope.trustSrc = function (src) {
        return $sce.trustAsResourceUrl(src);
    }

    $scope.resizeWindow = function () {
        $(window).triggerHandler('resize');
    };

    $scope.test = "Suprise!";

    $scope.PokeOtherUser = function () {
        requestPushPermission();
        //var promisePost = UserService.pokeOtherUsers($rootScope.UserInfo);

        //promisePost.then(
        //    function (result) {
        //        if (result.data.Status === "success") {
                    
        //        } else if (result.data.Status === "error") {
                    
        //        }
        //    },
        //    function (error) {
        //        $scope.Error = error.data.Message;
        //        //toastr.error($scope.Error, 'Lỗi');
        //    });
    }

    function requestPushPermission() {
        if ("serviceWorker" in navigator) {
            navigator.serviceWorker.register("~/App/sw.js", { scope: "~/App/" })
            .then(function (reg) {
                // registration worked
                console.log("Registration succeeded. Scope is " + reg.scope);
            }).catch(function (error) {
                // registration failed
                console.log("Registration failed with " + error);
            });
        }
    }
});