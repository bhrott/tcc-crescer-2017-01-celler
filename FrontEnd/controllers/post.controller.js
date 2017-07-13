modulo.controller('PostController', function ($scope, authService,postService, $routeParams, $location) {


    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }

    $scope.habilitarNotificacoes = false;
    $scope.buscar = buscar;
    $scope.cadastrarProduto = cadastrarProduto;
    $scope.cadastrarEvento = cadastrarEvento;
    $scope.cadastrarVaquinha = cadastrarVaquinha;

    function buscar(busca){
        var arrayTipos = [];
        if(busca.eventos == true){
            arrayTipos.push('Eventos');
        }
        if(busca.anuncios == true){
            arrayTipos.push('Anuncios');
        }
        if(busca.vaquinhas == true){
            arrayTipos.push('Vaquinhas');
        }

        var objetoBusca = {nome: busca.nome, tipos:arrayTipos};
        console.log(objetoBusca);
    }

    function cadastrarProduto(produto){
        
            postService.cadastrarProduto(produto).then(
            
                function(response){
                    console.log(response);
                }
                
            );
    }

    function cadastrarEvento(evento){
        console.log(evento);
    }

    function cadastrarVaquinha(vaquinha){
        console.log(vaquinha);
    }


});