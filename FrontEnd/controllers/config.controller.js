modulo.controller('ConfigController', function ($scope, configService, authService, $routeParams, $location) {

    // Caso usuário não esteja logado, redirecioná-lo para o login.
    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }

    // IIFE que carrega as configurações do usuário logado.
    (function carregarConfigs(){
        configService.carregarSettings().then(
            function(response){
                $scope.usuario = response.data.dados;
            }
        )      
    })();
    
    $scope.salvarConfigs = function(usuario){    
        configService.modificarConfigs(usuario).then(
            function(response){
                swal("Feito!", "Configs alteradas com sucesso!", "success");
                $location.path('/feed');
            },
            function (response) {
                console.log(response);
                swal("Eita!", "Erro ao alterar configurações!", "error");
            }
        )
    }


});