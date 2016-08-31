"use strict";
var service = angular.module("Service", []);
var directive = angular.module("Directive", []);
var app = angular.module("ClientApp", ["ngRoute", "Service","Directive"]);

// Show Routing.
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/home",
        {
            caseInsensitiveMatch: true,
            redirectTo: "/"
        });
    $routeProvider.when("/",
        {
            caseInsensitiveMatch: true,
            templateUrl: "/Client/Home",
            controller: "HomeController"
            //resolve: {
            //    slides: ['SlideService', 'CommmonService', '$rootScope', '$q', function (SlideService, CommmonService, $rootScope, $q) {
            //        var promise = SlideService.getSlides();
            //        return CommmonService.checkHttpResult($q, promise, $rootScope.BaseUrl);
            //    }],
            //    liststatisticforhome: ['ProjectService', 'CommmonService', '$rootScope', '$q', function (ProjectService, CommmonService, $rootScope, $q) {
            //        var promise = ProjectService.GetStatisticListForHome();
            //        return CommmonService.checkHttpResult($q, promise, $rootScope.BaseUrl);
            //    }],
            //}
        });
    $routeProvider.when("/register",
        {
            caseInsensitiveMatch: true,
            templateUrl: "Client/Register",
            controller: "RegisterController"
        });

    $routeProvider.when("/error",
        {
            caseInsensitiveMatch: true,
            templateUrl: "/Client/Error"
        });
    $routeProvider.when("/notfound",
        {
            caseInsensitiveMatch: true,
            title: 'Not found',
            templateUrl: "/Client/NotFound"
        });
    $routeProvider.otherwise({
        redirectTo: "/"
    });

    //$locationProvider.html5Mode(false).hashPrefix("!");
}])

app.run(['$rootScope', '$window','$location','$route', '$anchorScroll', 'UserService',
    function ($rootScope, $window,$location,$route, $anchorScroll, UserService) {
        $rootScope.$on('$routeChangeError', function (e, curr, prev) {
            e.preventDefault();
        });

        // Scroll top when route change.
        $rootScope.$on("$viewContentLoaded", function () {
            $window.scrollTo(0, 0);
        });

        $rootScope.reload = function() {
            $route.reload();
        }

        $rootScope.$on("$routeChangeStart", function (e, curr, prev) {
            if (curr.$$route !== undefined) {
                $rootScope.Page = {
                    title: curr.$$route.title !== undefined ? curr.$$route.title : ""
                }
            }

        });

        // Base Url of web app.
        $rootScope.BaseUrl = angular.element($('#BaseUrl')).val();

        // Load authen info:
        $rootScope.UserInfo = {
            IsAuthen: false
        };
        // 1. define function
        function checkLoginStatus() {
            var promiseGet = UserService.checkLoginStatus();
            promiseGet.then(
                function (result) {
                    if (result.data.Status === "success") {
                        // Save authen info into $rootScope
                        $rootScope.UserInfo = result.data.Data;
                        $rootScope.UserInfo.IsAuthen = true;
                    } else {
                        $rootScope.UserInfo = {
                            IsAuthen: false
                        };
                    }
                },
                function (error) {
                    // todo here.
                });
        }
        // 2. call function
        checkLoginStatus();

        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/sw.js').then(function (registration) {
                // Registration was successful
                console.log('ServiceWorker registration successful with scope: ', registration.scope);
            }).catch(function (err) {
                // registration failed :(
                console.log('ServiceWorker registration failed: ', err);
            });
        }

        //$window.fbAsyncInit = function () {
        //    FB.init({
        //        appId: '412367302292593',
        //        status: false,
        //        cookie: true,
        //        xfbml: true,
        //        version: 'v2.4'
        //    });
        //    //FbService.watchLoginStatusChange();
        //};

        //(function (d) {
        //    // load the Facebook javascript SDK

        //    var js,
        //        id = 'facebook-jssdk',
        //        ref = d.getElementsByTagName('script')[0];

        //    if (d.getElementById(id)) {
        //        return;
        //    }

        //    js = d.createElement('script');
        //    js.id = id;
        //    js.async = true;
        //    js.src = "//connect.facebook.net/en_US/sdk.js";

        //    ref.parentNode.insertBefore(js, ref);

        //}(document));
    }]);
