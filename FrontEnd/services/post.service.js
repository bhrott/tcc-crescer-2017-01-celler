modulo.factory("postService", function ($http) {
    return ({
        postarComentario:postarComentario,
        interessarProduto:interessarProduto,
        desinteressarProduto:desinteressarProduto,
        confirmarEvento:confirmarEvento,
        desconfirmarEvento:desconfirmarEvento,
        doarVaquinha:doarVaquinha,
        venderProduto:venderProduto,
        cadastrarVaquinha:cadastrarVaquinha,
        cadastrarEvento:cadastrarEvento,
        cadastrarProduto:cadastrarProduto,
        cancelarNotificacao:cancelarNotificacao,
        aceitarDoacao:aceitarDoacao,
        editarProduto:editarProduto
    });


    function postarComentario(textoEnviar, idAnuncio){
        var enviar = {texto: textoEnviar, IdAnuncio:idAnuncio};
        console.log(enviar);
        return $http.post("http://localhost:50694/api/anuncio/comentar", enviar);
    }

    function editarProduto(produto){

        console.log(produto);
        var objetoEnvio = {

            Id: produto.Id,
            Titulo: produto.Titulo,
            Descricao: produto.Descricao,
            Foto1: produto.Foto1,
            Foto2: produto.Foto2,
            Foto3: produto.Foto3,
            Valor: produto.Valor


        }
        
        return $http.put('http://localhost:50694/api/produto/editar', objetoEnvio);


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

    function doarVaquinha(idUser, idVaquinha, valorDoado){

        var objetoEnvio = {
            IdUsuario:idUser,
            IdVaquinha:idVaquinha,
            ValorDoado:valorDoado
        }
        return $http.post("http://localhost:50694/api/vaquinha/doar", objetoEnvio);

    }

    function venderProduto(idInteressado, idAnuncio){

        var objetoEnvio = {
            IdUsuario:idInteressado,
            IdProduto:idAnuncio
        }

        return $http.post("http://localhost:50694/api/produto/vender", objetoEnvio);
    }

    function cadastrarProduto(produto){

        var objetoEnvio = {

            Titulo:produto.Titulo,
            Descricao:produto.Descricao,
            Foto1:produto.Foto1,
            Foto2:produto.Foto2,
            Foto3:produto.Foto3,
            Valor:produto.Valor
        }
        console.log(objetoEnvio);

        return $http.post("http://localhost:50694/api/produto/adicionar", objetoEnvio)

    }

    function cadastrarEvento(evento){

        console.log(evento);
    }

    function cadastrarVaquinha(vaquinha){

        console.log(vaquinha);
    }

    function cancelarNotificacao(idNotificacao){

        return $http.put("http://localhost:50694/api/notificacao?id=" + idNotificacao);

    }

    function aceitarDoacao(usuario, anuncio, idDoacao){

        var objetoEnvio = {
            IdUsuario:usuario.Id,
            IdVaquinha:anuncio.Id,
            IdDoacao:idDoacao
        }
        console.log(objetoEnvio);
        return $http.post("http://localhost:50694/api/vaquinha/confirmar", objetoEnvio);

    }

});