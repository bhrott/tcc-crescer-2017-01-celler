modulo.controller('AnuncioController', function ($scope, authService, postService, detalheService, $routeParams, $location, $localStorage) {
    
     if(!authService.isAutenticado()){
        $location.path("#!/login");
    }
    
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
        }

    );


    function postar(texto){
        postService.postarComentario(texto, idAnuncioEspecifico).then(

            function(response){
                console.log(response.data.dados);
                
                $scope.produto.Comentario = '';

            },
            function(response){
                console.log('jabulani');
            }

        );
    }
});