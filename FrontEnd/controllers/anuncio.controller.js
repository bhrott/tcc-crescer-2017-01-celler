modulo.controller('AnuncioController', function ($scope, authService, postService, detalheService, $routeParams, $location, $localStorage) {

    // Se usuário não estiver logado, é redirecionado para a tela de Login.
    if (!authService.isAutenticado()){
        $location.path("#!/login");
    }

    // Se usuário veio aqui através de uma notificação, exclui ela da lista de notificações pendentes.
    if ($routeParams.idNotificacao !== undefined){
        postService
            .cancelarNotificacao($routeParams.idNotificacao)
            .then(
            function(response){

            });
    }

    // Variáveis do escopo.
    $scope.logout = logout;
    $scope.exibirAModal = exibirModal;
    $scope.esconderAModal = esconderModal;
    var idAnuncioEspecifico = $routeParams.idAnuncio;
    $scope.idAnuncio = idAnuncioEspecifico;
    $scope.postar = postar;
    $scope.produto = {};
    var produto = $scope.produto;
    $scope.idUsuarioLogado = $localStorage.usuarioLogado.Id;
    // Funções.
    function logout(){
        authService.logout();
    }

    function exibirModal(){
        $scope.exibirModal = true;
    }

    function esconderModal(){
        $scope.exibirModal = false;
    }




    // Função invocada ao iniciar controller, para carregar detalhes do anúncio.
    detalheService.carregarDetalhes(idAnuncioEspecifico).then(
        function(response){
            if(response.data.dados.Foto1 == null){
                response.data.dados.Foto1 = 'http://placehold.it/256x256?text=Sem+Imagem+:(';
            }
            $scope.anuncioEspecifico = response.data.dados;
            checarInteresse();
            checarPresenca();
        }
    );



    $scope.venderProduto = function (idInteressado){
        postService.venderProduto(idInteressado, $scope.anuncioEspecifico.Id).then(
            function(response){
                $scope.exibirModal = false;
                swal("Feito!", "Vendido com sucesso!", "success");
                $location.url('#!/feed');
            }
        );

    }

    $scope.confirmarInteresse = function confirmarInteresse(){
        postService.interessarProduto($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(
            function(response){
                $scope.anuncioEspecifico.TemInteresse = true;
                $scope.anuncioEspecifico.NumeroInteressados += 1;
            }
        );
    }

    $scope.retirarInteresse = function retirarInteresse(){
        postService.desinteressarProduto($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(
            function(response){
                $scope.anuncioEspecifico.TemInteresse = false;
                $scope.anuncioEspecifico.NumeroInteressados -= 1;
            }
        );
    }

    $scope.confirmarPresenca = function confirmarPresenca(){
        postService.confirmarEvento($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(
            function(response){
                $scope.estaConfirmado = true;
                $scope.anuncioEspecifico.Confirmados.push({Email:$localStorage.usuarioLogado.Email, Nome:$localStorage.usuarioLogado.Nome});
            }
        );
    }

    $scope.retirarPresenca = function retirarPresenca(){
        postService.desconfirmarEvento($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(
            function(response){
                $scope.estaConfirmado = false;
                $scope.anuncioEspecifico.Confirmados.pop();
            }
        );
    }

    $scope.doarVaquinha = function doarVaquinha(valorDoado){
        postService.doarVaquinha($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id, valorDoado).then(
            function(response){
                $scope.doou = true;
            }
        )
    }

    $scope.aceitarDoacao = function aceitarDoacao(usuario, anuncio, idDoacao){
        postService.aceitarDoacao(usuario, anuncio, idDoacao).then(
            function(response){
                var doacao = $scope.anuncioEspecifico.Doadores.filter( x => x.Id == idDoacao);
                doacao[0].Status = 'p';
            })
    }


    function checarInteresse(){
        $scope.temInteresse = $scope.anuncioEspecifico.Confirmados.some( x=> x.Id == $localStorage.usuarioLogado.Id );
    }


    function checarPresenca(){
        $scope.estaConfirmado = $scope.anuncioEspecifico.Confirmados.some( x=> x.Id == $localStorage.usuarioLogado.Id );
    }


    function postar(texto){
        postService.postarComentario(texto, idAnuncioEspecifico).then(
            function(response){
                console.log(response.data.dados);
                var novoComentario = {Usuario : {Nome:$localStorage.usuarioLogado.Nome, Email:$localStorage.usuarioLogado.Email}, Texto:texto};
                $scope.anuncioEspecifico.Comentarios.push(novoComentario);
                $scope.produto.Comentario = '';
            },
            function(response){
                swal("Error", response.data.dados, "error");
            }
        );
    }
});