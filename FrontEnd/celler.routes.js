var modulo = angular.module('CellerApp', ['ngRoute', 'auth','ngStorage',  'ngSanitize',
  'markdown']);
modulo.config(function ($routeProvider) {

    $routeProvider
        .when('/login', {
        controller: 'LoginController',
        templateUrl: 'Login.html',
        css: 'login.css'
    }).when('/feed', {
        controller: 'FeedController',
        templateUrl: 'Feed.html',
        css: 'feed.css'
    }).when('/test', {
        controller: 'FeedController',
        templateUrl: 'test.html',
        css: 'feed.css'
    }).when('/novoProduto', {
        controller: 'PostController',
        templateUrl: 'novoProduto.html',
        css: 'post.css'
    }).when('/novoEvento', {
        controller: 'PostController',
        templateUrl: 'novoEvento.html',
        css: 'post.css'
    }).when('/novaVaquinha', {
        controller: 'PostController',
        templateUrl: 'novaVaquinha.html',
        css: 'post.css'
    })
        .otherwise({redirectTo: '/login'});
});

modulo.config(function ($compileProvider) {
    // Add optional support for custom schema links: "herp://" and "derp://"
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(herp|derp):/);
  });
modulo.constant('authConfig', {

    // Obrigatória - URL da API que retorna o usuário
    urlUsuario: 'http://localhost:50694/api/Usuario',

    // Obrigatória - URL da aplicação que possui o formulário de login
    urlLogin: '/login',

    // Opcional - URL da aplicação para onde será redirecionado (se for informado) após o LOGIN com sucesso
    urlPrivado: '/feed',

    // Opcional - URL da aplicação para onde será redirecionado (se for informado) após o LOGOUT
    urlLogout: '/editora'
});

