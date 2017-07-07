modulo.controller('FeedController', function ($scope, authService, $routeParams, $location) {
  
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

});