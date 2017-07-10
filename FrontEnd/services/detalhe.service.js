modulo.factory("detalheService", function ($http) {
    return ({
        carregarDetalhes:carregarDetalhes
    });


    function carregarDetalhes(idAnuncio){
        return $http.get("http://localhost:50694/api/anuncio/" + idAnuncio + '');
    }

});