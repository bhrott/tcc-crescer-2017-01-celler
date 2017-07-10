modulo.factory("postService", function ($http) {
    return ({
        postarComentario:postarComentario
    });


    function postarComentario(textoEnviar, idAnuncio){
        var enviar = {texto: textoEnviar, IdAnuncio:idAnuncio};
        console.log(enviar);
        return $http.post("http://localhost:50694/api/anuncio/comentar", enviar);
    }

});