modulo.controller('PostController', function ($scope, authService,postService, $routeParams, $location) {

    // Se usuário não estiver logado, é redirecionado para a tela de Login.
    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }

    $scope.habilitarNotificacoes = false;
    $scope.cadastrarProduto = cadastrarProduto;
    $scope.cadastrarEvento = cadastrarEvento;
    $scope.cadastrarVaquinha = cadastrarVaquinha;

    function cadastrarProduto(produto){
        postService.cadastrarProduto(produto)
            .then(
            function(response){
                console.log(response);
                swal("Sucesso", "Produto Cadastrado com sucesso.", "success");
                $location.path('/anuncio/'+response.data.dados.Id);
            }
        );
    }

    //Ainda não implementado.
    function cadastrarEvento(evento){
        console.log(evento);
    }
    //Ainda não implementado.
    function cadastrarVaquinha(vaquinha){
        console.log(vaquinha);
    }


});