modulo.controller('AnuncioController', function ($scope, authService, $routeParams, $location) {
  
var idAnuncioEspecifico = $routeParams.idAnuncio;
$scope.idAnuncio = idAnuncioEspecifico;
});