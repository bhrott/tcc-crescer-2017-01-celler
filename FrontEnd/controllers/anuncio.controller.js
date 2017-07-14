modulo.controller('AnuncioController', function ($scope, authService, postService, detalheService, $routeParams, $location, $localStorage) {

    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }

    if  ($routeParams.idNotificacao !== undefined){

        console.log($routeParams.idNotificacao);
        postService.cancelarNotificacao($routeParams.idNotificacao).then(


            function(response){

                console.log(response);

            }

        )

    }

    $scope.logout = logout;
    $scope.exibirAModal = exibirModal;
    $scope.esconderAModal = esconderModal;
    var idAnuncioEspecifico = $routeParams.idAnuncio;
    $scope.idAnuncio = idAnuncioEspecifico;
    $scope.postar = postar;
    $scope.produto = {};
    var produto = $scope.produto;
    $scope.idUsuarioLogado = $localStorage.usuarioLogado.Id;
    console.log($scope.idUsuarioLogado);
    console.log(idAnuncioEspecifico);

    detalheService.carregarDetalhes(idAnuncioEspecifico).then(
        function(response){
            console.log(idAnuncioEspecifico);
            if(response.data.dados.Foto1 == null){
                response.data.dados.Foto1 = 'https://placehold.it/256x256';
            }
            if(response.data.dados.Foto2 == null){
                response.data.dados.Foto2 = 'https://placehold.it/128x128';
            }
            if(response.data.dados.Foto3 == null){
                response.data.dados.Foto3 = 'https://placehold.it/128x128';
            }
            $scope.anuncioEspecifico = response.data.dados;
            console.log($scope.anuncioEspecifico);
            checarInteresse();
            checarPresenca();
        }

    );



    $scope.venderProduto = function (idInteressado){
        postService.venderProduto(idInteressado, $scope.anuncioEspecifico.Id).then(


            function(response){

                console.log(response);
                $scope.exibirModal = false;
                swal("Feito!", "Vendido com sucesso!", "success");
                $location.url('#!/feed');
            }

        );

    }

    $scope.confirmarInteresse = function confirmarInteresse(){
        postService.interessarProduto($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(

            function(response){
                console.log(response);
                $scope.temInteresse = true;
                $scope.anuncioEspecifico.Interessados.push({Email:$localStorage.usuarioLogado.Email, Nome:$localStorage.usuarioLogado.Nome});
            }

        );
    }

    $scope.retirarInteresse = function retirarInteresse(){
        postService.desinteressarProduto($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(

            function(response){
                console.log(response);
                $scope.temInteresse = false;
                $scope.anuncioEspecifico.Interessados.pop();

            }

        );
    }

    $scope.confirmarPresenca = function confirmarPresenca(){
        postService.confirmarEvento($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(

            function(response){
                console.log(response);
                $scope.estaConfirmado = true;
                $scope.anuncioEspecifico.Confirmados.push({Email:$localStorage.usuarioLogado.Email, Nome:$localStorage.usuarioLogado.Nome});
            }

        );
    }

    $scope.retirarPresenca = function retirarPresenca(){
        postService.desconfirmarEvento($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id).then(

            function(response){
                console.log(response);
                $scope.estaConfirmado = false;
                $scope.anuncioEspecifico.Confirmados.pop();

            }

        );
    }

    $scope.doarVaquinha = function doarVaquinha(valorDoado){

        console.log(valorDoado);
        postService.doarVaquinha($localStorage.usuarioLogado.Id, $scope.anuncioEspecifico.Id, valorDoado).then(

            function(response){

                console.log(response);
                $scope.doou = true;

            }

        )

    }


    function logout(){
        authService.logout();
    }

    $scope.aceitarDoacao = function aceitarDoacao(usuario, anuncio, idDoacao){
        console.log(usuario);
        console.log(anuncio);
        postService.aceitarDoacao(usuario, anuncio, idDoacao).then(

            function(response){
                var doacao = $scope.anuncioEspecifico.Doadores.filter( x => x.Id == idDoacao);
                doacao[0].Status = 'p';
                console.log(doacao);
                console.log(response);

            }

        )

    }


    function checarInteresse(){

        $scope.temInteresse = $scope.anuncioEspecifico.Confirmados.some( x=> x.Id == $localStorage.usuarioLogado.Id );

    }


    function checarPresenca(){

        $scope.estaConfirmado = $scope.anuncioEspecifico.Confirmados.some( x=> x.Id == $localStorage.usuarioLogado.Id );

    }
    function exibirModal(){
        $scope.exibirModal = true;
    }

    function esconderModal(){
        $scope.exibirModal = false;
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
                console.log('jabulani');
            }

        );
    }
});