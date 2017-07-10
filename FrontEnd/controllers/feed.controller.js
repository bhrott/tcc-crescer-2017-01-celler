modulo.controller('FeedController', function ($scope, authService, feedService, $routeParams, $location) {

    //  $scope.anuncios = [{Titulo:'Birlll', TipoAnuncio:'Vaquinha'},{Titulo:'Negativa bambam', TipoAnuncio:'Vaquinha'},{TipoAnuncio:'Produto'},{TipoAnuncio:'Evento'}];

    $scope.anuncios = [];
    $scope.habilitarBuscarMais = true;
    $scope.pular = 0;
    $scope.logout = logout;
    $scope.carregarPosts = carregarPosts;
    carregarPosts();
    $scope.carregarMais = carregarMais;
    console.log($scope.anuncios);
    $scope.text = '*altas* \n- **emoções** 1\n- [Link](http://example.com)\n- [Custom Link 1](herp://is.this.working?)\n- [Custom Link 2](derp://is.this.working?)';

    $scope.redirectNovoProduto = redirectNovoProduto;
    $scope.redirectNovoEvento = redirectNovoEvento;
    $scope.redirectNovaVaquinha = redirectNovaVaquinha;

    function redirectNovoProduto(){
        $location.url('novoProduto');
    }
    function redirectNovoEvento(){
        $location.url('novoEvento');
    }
    function redirectNovaVaquinha(){
        $location.url('novaVaquinha');
    }
    $scope.habilitarNotificacoes = false;
    $scope.buscar = buscar;



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

    function carregarPosts(){
        feedService.carregarPosts($scope.pular).then(
            function(response){
                console.log(response.data.dados);
                for(resposta of response.data.dados){
                    $scope.anuncios.push(resposta);
                    
                    if(resposta.TipoAnuncio == 'Produto' && resposta.ValorProduto == null){
                        resposta.ValorProduto = 0;
                    }
                    if(resposta.Foto1 == null){
                        resposta.Foto1 = 'https://placehold.it/256x256';
                    }
                    if(resposta.Foto2 == null){
                        resposta.Foto2 = 'https://placehold.it/256x256';
                    }
                    if(resposta.Foto3 == null){
                        resposta.Foto3 = 'https://placehold.it/256x256';
                    }
                }
                console.log($scope.anuncios);
                if(response.data.dados.length != 3){
                    $scope.habilitarBuscarMais = false;
                }
            }
        );
    }

    function carregarMais(){
        console.log('entrei aqui');
        $scope.pular += 3;
        carregarPosts();
    }

    function logout(){
        authService.logout();
    }

});