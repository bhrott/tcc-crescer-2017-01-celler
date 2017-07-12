modulo.factory("postService", function ($http) {
    return ({
        postarComentario:postarComentario,
        interessarProduto:interessarProduto,
        desinteressarProduto:desinteressarProduto,
        confirmarEvento:confirmarEvento,
        desconfirmarEvento:desconfirmarEvento
    });


    function postarComentario(textoEnviar, idAnuncio){
        var enviar = {texto: textoEnviar, IdAnuncio:idAnuncio};
        console.log(enviar);
        return $http.post("http://localhost:50694/api/anuncio/comentar", enviar);
    }
    
    function interessarProduto(idUser, idProduto){
        var objetoEnvio = {
            IdUsuario:idUser,
            IdProduto:idProduto
        }
        return $http.post("http://localhost:50694/api/produto/interessar", objetoEnvio);
    }
    
     function desinteressarProduto(idUser, idProduto){
        var objetoEnvio = {
            IdUsuario:idUser,
            IdProduto:idProduto
        }
        return $http.post("http://localhost:50694/api/produto/desinteressar", objetoEnvio);
    }
    
     function confirmarEvento(idUser, idEvento){
        var objetoEnvio = {
            IdUsuario:idUser,
            IdEvento:idEvento
        }
        return $http.post("http://localhost:50694/api/evento/participar", objetoEnvio);
    }
    
     function desconfirmarEvento(idUser, idEvento){
        var objetoEnvio = {
            IdUsuario:idUser,
            IdEvento:idEvento
        }
        return $http.post("http://localhost:50694/api/evento/desistir", objetoEnvio);
    }

});