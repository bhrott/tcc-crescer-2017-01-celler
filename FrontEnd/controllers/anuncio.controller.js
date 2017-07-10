modulo.controller('AnuncioController', function ($scope, authService, detalheService, $routeParams, $location) {
    var idAnuncioEspecifico = $routeParams.idAnuncio;
    $scope.idAnuncio = idAnuncioEspecifico;
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

});