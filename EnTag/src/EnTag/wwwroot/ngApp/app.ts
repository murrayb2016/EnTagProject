namespace EnTag {

    angular.module('EnTag', ['ui.router', 'ngResource', 'ui.bootstrap', 'youtube-embed', 'ngMaterial', 'ngMessages']).config((
        $mdThemingProvider,
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {

        var customPrimary = {
            '50': '#ffebd6',
            '100': '#ffdebd',
            '200': '#ffd2a3',
            '300': '#ffc58a',
            '400': '#ffb970',
            '500': '#ffac57',
            '600': '#ff9f3d',
            '700': '#ff9324',
            '800': '#ff860a',
            '900': '#f07900',
            'A100': '#fff8f0',
            'A200': '#ffffff',
            'A400': '#ffffff',
            'A700': '#d66d00'
        };
        $mdThemingProvider
            .definePalette('customPrimary',
            customPrimary);

        var customAccent = {
            '50': '#11282f',
            '100': '#183842',
            '200': '#1f4854',
            '300': '#265867',
            '400': '#2c687a',
            '500': '#33788c',
            '600': '#4198b2',
            '700': '#4ea5be',
            '800': '#60aec5',
            '900': '#73b8cc',
            'A100': '#4198b2',
            'A200': '#3a889f',
            'A400': '#33788c',
            'A700': '#86c1d3'
        };
        $mdThemingProvider
            .definePalette('customAccent',
            customAccent);

        var customWarn = {
            '50': '#ffded6',
            '100': '#ffcabd',
            '200': '#ffb5a3',
            '300': '#ffa08a',
            '400': '#ff8c70',
            '500': '#ff7757',
            '600': '#ff623d',
            '700': '#ff4e24',
            '800': '#ff390a',
            '900': '#f02e00',
            'A100': '#fff3f0',
            'A200': '#ffffff',
            'A400': '#ffffff',
            'A700': '#d62900'
        };
        $mdThemingProvider
            .definePalette('customWarn',
            customWarn);

        var customBackground = {
            '50': '#606060',
            '100': '#535353',
            '200': '#464646',
            '300': '#393939',
            '400': '#2d2d2d',
            '500': '#202020',
            '600': '#131313',
            '700': '#060606',
            '800': '#000000',
            '900': '#000000',
            'A100': '#6c6c6c',
            'A200': '#797979',
            'A400': '#868686',
            'A700': '#000000'
        };
        $mdThemingProvider
            .definePalette('customBackground',
            customBackground);

        $mdThemingProvider.theme('default')
            .primaryPalette('customPrimary', { 'default': '800' })
            .accentPalette('customAccent', { 'default': 'A200' })
            .warnPalette('customWarn', { 'default': '800' })
            .backgroundPalette('customBackground', { 'default': '500' });

        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: EnTag.Controllers.HomeController,
                controllerAs: 'controller'
            })

            .state('secret', {
                url: '/secret',
                templateUrl: '/ngApp/views/secret.html',
                controller: EnTag.Controllers.SecretController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: EnTag.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: EnTag.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: EnTag.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            })
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: EnTag.Controllers.AboutController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    })

        .controller('AppCtrl', function ($scope, $timeout, $mdSidenav) {
            $scope.toggleLeft = buildToggler('left');
            $scope.toggleRight = buildToggler('right');

            function buildToggler(componentId) {
                return function () {
                    $mdSidenav(componentId).toggle();
                }
            }
        });


    angular.module('EnTag').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('EnTag').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    //angular.module('EnTag').config(function ($sceDelegateProvider) {
    //    $sceDelegateProvider.resourceUrlWhitelist([
    //        'self',
    //        '*:/embed.spotify.com/**'
    //    ]);
    //});

    angular.module('EnTag').config(function ($sceDelegateProvider) {
        $sceDelegateProvider.resourceUrlWhitelist(['**']);
    });

}

function limitText(limitField, limitCount, limitNum) {
    if (limitField.value.length > limitNum) {
        limitField.value = limitField.value.substring(0, limitNum);
    } else {
        limitCount.value = limitNum - limitField.value.length;
    }
}
