modulo.controller('FeedController', function ($scope, authService, feedService, $routeParams, $route, $location) {

    //  $scope.anuncios = [{Titulo:'Birlll', TipoAnuncio:'Vaquinha'},{Titulo:'Negativa bambam', TipoAnuncio:'Vaquinha'},{TipoAnuncio:'Produto'},{TipoAnuncio:'Evento'}];
    
    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }
    $scope.busca = {};
    $scope.anuncios = [];
    $scope.habilitarBuscarMais = true;
    $scope.pular = 0;
    $scope.logout = logout;
    $scope.carregarPosts = carregarPosts;

    if  ($routeParams.filtro1 !== undefined || $routeParams.filtro2 !== undefined || $routeParams.filtro3 !== undefined || $routeParams.search !== undefined){

        $routeParams.pagina = 0;

    }
    carregarPosts($routeParams);
    $scope.carregarMais = carregarMais;
    console.log($scope.anuncios);

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
            arrayTipos.push('Evento');
        }
        if(busca.anuncios == true){
            arrayTipos.push('Produto');
        }
        if(busca.vaquinhas == true){
            arrayTipos.push('Vaquinha');
        }

        var objetoBusca = {
            pagina:$scope.pular, filtro1:arrayTipos[0], filtro2:arrayTipos[1], filtro3:arrayTipos[2], search: busca.nome};
        $route.updateParams(objetoBusca);
        $scope.pular = 0;
        console.log(JSON.stringify(objetoBusca));
    }

    console.log($location.search());
    function carregarPosts(){
        console.log($scope.busca);
        $scope.busca.nome = $routeParams.search;
        $scope.busca.anuncios = ($routeParams.filtro1 == 'Produto' || $routeParams.filtro2 == 'Produto' || $routeParams.filtro3 == 'Produto');
        $scope.busca.eventos = ($routeParams.filtro1 == 'Evento' || $routeParams.filtro2 == 'Evento' || $routeParams.filtro3 == 'Evento');
      $scope.busca.vaquinhas = ($routeParams.filtro1 == 'Vaquinha' || $routeParams.filtro2 == 'Vaquinha' || $routeParams.filtro3 == 'Vaquinha');

        feedService.carregarPosts($routeParams).then(
            function(response){
                console.log(response.data.dados);
                for(resposta of response.data.dados){
                    $scope.anuncios.push(resposta);

                    if (resposta.TipoAnuncio == 'Produto' && resposta.ValorProduto == null){
                        resposta.ValorProduto = 0;
                    }
                    if (resposta.Foto1 == null){
                        resposta.Foto1 = 'https://placehold.it/256x256';
                    }
                    if (resposta.Foto2 == null){
                        resposta.Foto2 = 'https://placehold.it/256x256';
                    }
                    if (resposta.Foto3 == null){
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
        $routeParams.pagina += 3;
        carregarPosts($routeParams);
    }

    function logout(){
        authService.logout();
    }

});