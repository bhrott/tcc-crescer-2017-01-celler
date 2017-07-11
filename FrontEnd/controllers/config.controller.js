modulo.controller('ConfigController', function ($scope, authService, $routeParams, $location) {
  
    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }

});