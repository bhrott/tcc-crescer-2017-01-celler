modulo.controller('LoginController', function ($scope, authService, $routeParams, $location) {
  
     if(!authService.isAutenticado()){
        $location.path("#!/login");
    }
    
  $scope.login = function (usuario) {

    authService.login($scope.usuario)
      .then(
        function (response) {
          console.log(response);
          swal("Feito!", "Login com sucesso!", "success");

        },
        function (response) {
          console.log(response);
          swal("Eita!", "Usuário ou Senha inválidos!", "error");
        });
  };

});