modulo.controller('FeedController', function ($scope, authService, feedService, postService, $routeParams, $route, $location, $localStorage) {
    // Se usuário não estiver logado, é redirecionado para a tela de Login.
    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }


    if(intervalo == null || intervalo == undefined){
        clearInterval(intervalo);
    }
    // variáveis de inicialização.
    $scope.isFeed = true;
    $scope.carregarMeusAds = carregarMeusAds;
    $localStorage.idsNotificacoes = [];
    carregarNotificacoes();
    //IIFE responsável por carregar e criar as notificações do usuário.
    function carregarNotificacoes(){
        feedService.carregarNotificacoes().then(
            function(response){
                var resposta = response.data.dados;
                $scope.notificacoes=resposta.filter(x => x.status == 'n');
                for(notificacao of $scope.notificacoes){
                    idsNotificacoes = $localStorage.idsNotificacoes;
                    if(idsNotificacoes.includes(notificacao.id) == false){
                        var title = 'Celler';
                        var options = {
                            body: notificacao.texto,
                            icon: 'https://thumb1.shutterstock.com/display_pic_with_logo/221737/101774380/stock-photo-sales-and-market-concept-in-word-tag-cloud-on-white-101774380.jpg'
                        }
                        $localStorage.idsNotificacoes.push(notificacao.id);
                        var n = new Notification(title,options);
                        n.onclick = function(event) {
                            event.preventDefault(); // prevent the browser from focusing the Notification's tab
                            window.open('http://127.0.0.1:8080/' +  notificacao.link + '?idNotificacao=' + notificacao.id, '_blank');
                        }
                        setTimeout(n.close.bind(n), 10000);
                    }
                }
            }
        );
    };

    var intervalo = setInterval(function(){ 

        carregarNotificacoes();

    }, 60000);


    //Inicialização.
    $scope.busca = {};
    $scope.anuncios = [];
    $scope.habilitarBuscarMais = true;
    $scope.pular = 0;
    $scope.logout = logout;
    $scope.carregarPosts = carregarPosts;

    if  ($routeParams.filtro1 !== undefined || $routeParams.filtro2 !== undefined || $routeParams.filtro3 !== undefined || $routeParams.search !== undefined){
        $routeParams.pagina = 0;
    }

    //Controlar exibição da NavBar.
    if ($routeParams.meusAds == true ){
        $scope.isMyAds = true;
        $scope.isFeed = false;
    }
    carregarPosts($routeParams);
    $scope.carregarMais = carregarMais;



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
            pagina:$scope.pular, 
            filtro1:arrayTipos[0],
            filtro2:arrayTipos[1], 
            filtro3:arrayTipos[2], 
            search: busca.nome
        };
        $route.updateParams(objetoBusca);
        $scope.pular = 0;
    }

    function carregarPosts(){
        // Carregar os routeParams visualmente nos campos de busca.
        $scope.busca.nome = $routeParams.search;
        $scope.busca.anuncios = ($routeParams.filtro1 == 'Produto' || $routeParams.filtro2 == 'Produto' || $routeParams.filtro3 == 'Produto');
        $scope.busca.eventos = ($routeParams.filtro1 == 'Evento' || $routeParams.filtro2 == 'Evento' || $routeParams.filtro3 == 'Evento');
        $scope.busca.vaquinhas = ($routeParams.filtro1 == 'Vaquinha' || $routeParams.filtro2 == 'Vaquinha' || $routeParams.filtro3 == 'Vaquinha');

        feedService.carregarPosts($routeParams).then(
            function(response){
                for(resposta of response.data.dados){
                    
                    $scope.anuncios.push(resposta);
                    if (resposta.TipoAnuncio == 'Produto' && resposta.ValorProduto == null){
                        resposta.ValorProduto = 0;
                    }
                    //Placeholder de imagem.
                    if (resposta.Foto1 == null){
                        resposta.Foto1 = 'http://placehold.it/256x256?text=Sem+Imagem+:(';
                    }
                }
                // Se recebeu menos de 9, desabilita paginação.
                if(response.data.dados.length != 9){
                    $scope.habilitarBuscarMais = false;
                }
            }
        );
    }

    function carregarMais(){
        $routeParams.pagina += 9;
        carregarPosts($routeParams);
    }

    function carregarMeusAds(){
        $scope.anuncios = [];
        $scope.isFeed = false;
        $scope.isMyAds = true;
        $routeParams = {};
        var objetoBusca = {
            pagina : 0,
            meusAds : true
        }
        $route.updateParams(objetoBusca);
        carregarPosts();
    }

    function logout(){
        authService.logout();
    }


    $scope.interessar = function confirmarInteresse(produto){
        postService.interessarProduto($localStorage.usuarioLogado.Id, produto.Id).then(

            function(response){
                produto.TemInteresse = true;
                produto.NumeroInteressados += 1;
            }

        );
    }

    $scope.desinteressar = function retirarInteresse(produto){
        postService.desinteressarProduto($localStorage.usuarioLogado.Id, produto.Id).then(
            function(response){
                produto.TemInteresse = false;
                produto.NumeroInteressados -= 1;
            }

        );
    }

    $scope.confirmar = function confirmarPresenca(evento){
        postService.confirmarEvento($localStorage.usuarioLogado.Id, evento.Id).then(

            function(response){
                evento.TemInteresse = true;
                evento.NumeroInteressados += 1;
            }

        );
    }

    $scope.desconfirmar = function retirarPresenca(evento){
        postService.desconfirmarEvento($localStorage.usuarioLogado.Id, evento.Id).then(

            function(response){
                evento.TemInteresse = false;
                evento.NumeroInteressados -= 1;

            }

        );
    }


});