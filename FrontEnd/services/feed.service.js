modulo.factory("feedService", function ($http) {
    return ({
        carregarPosts:carregarPosts
    });


    function carregarPosts(pular){
        console.log(pular);
        return $http.get("http://localhost:50694/api/anuncio/feed/" + pular + '');
    }

});