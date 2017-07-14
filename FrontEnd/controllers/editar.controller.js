modulo.controller('EditarController', function ($scope, authService, postService, $routeParams, detalheService, $location) {


    if(!authService.isAutenticado()){
        $location.path("#!/login");
    }
    console.log('Entrei aqui');
    console.log($routeParams.idAnuncio);

    $scope.editarProduto = function(produto){
        postService.editarProduto(produto)
            .then(


            function(response){
                swal("Sucesso!", "Produto alterado com sucesso.", "success");
                $location.path('/anuncio/'+$routeParams.idAnuncio);
            });


    }

    detalheService.carregarDetalhes($routeParams.idAnuncio).then(
        function(response){
            console.log($routeParams.idAnuncio);
            $scope.anuncioEspecifico = response.data.dados;
            console.log($scope.anuncioEspecifico);
            if(response.data.dados.TipoAnuncio == 'Evento'){
                $scope.anuncioEspecifico.DataRealizacao = new Date($scope.anuncioEspecifico.DataRealizacao);
                $scope.anuncioEspecifico.DataMaximaConfirmacao = new Date($scope.anuncioEspecifico.DataMaximaConfirmacao);
            }
            if(response.data.dados.TipoAnuncio == 'Evento'){
                $scope.anuncioEspecifico.DataRealizacao = new Date($scope.anuncioEspecifico.DataRealizacao);
                $scope.anuncioEspecifico.DataMaximaConfirmacao = new Date($scope.anuncioEspecifico.DataMaximaConfirmacao);
            }
            if(response.data.dados.TipoAnuncio == 'Vaquinha'){
                $scope.anuncioEspecifico.DateTermino = new Date($scope.anuncioEspecifico.DateTermino);
            }
        }

    );
});