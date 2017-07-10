modulo.controller('FeedController', function ($scope, authService, $routeParams, $location) {
  
    $scope.anuncios = [{Titulo:'Birlll', TipoAnuncio:'Vaquinha'},{Titulo:'Negativa bambam', TipoAnuncio:'Vaquinha'},{TipoAnuncio:'Produto'},{TipoAnuncio:'Evento'}];
    console.log($scope.anuncios);
   $scope.text = '*altas* \n- **emoções** 1\n- [Link](http://example.com)\n- [Custom Link 1](herp://is.this.working?)\n- [Custom Link 2](derp://is.this.working?)';
    
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