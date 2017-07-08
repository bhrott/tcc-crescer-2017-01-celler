modulo.controller('FeedController', function ($scope, authService, $routeParams, $location) {
  
    
   $scope.text = '*altas* \n- **emoções** 1\n- [Link](http://example.com)\n- [Custom Link 1](herp://is.this.working?)\n- [Custom Link 2](derp://is.this.working?)';
    
    $scope.redirectToNewPost = redirectToNewPost;
    
    
    function redirectToNewPost(){
        $location.url('newPost');
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

});