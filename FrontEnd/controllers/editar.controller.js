modulo.controller('EditarController', function ($scope, authService, $routeParams, detalheService, $location) {


    console.log('Entrei aqui');
    console.log($routeParams.idAnuncio);
    
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