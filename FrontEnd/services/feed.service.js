modulo.factory("feedService", function ($http) {
    return ({
        carregarPosts:carregarPosts,
        carregarNotificacoes:carregarNotificacoes
    });


    function carregarPosts(parametros){
        console.log(parametros);
        var endereco = "http://localhost:50694/api/anuncio/feed/?pagina="
        if(parametros.pagina == undefined){
            parametros.pagina = 0;
        }
        endereco += parametros.pagina;
        if(parametros.filtro1 !== undefined){
            endereco +="&filtro1=";
            endereco += parametros.filtro1;
        }
         if(parametros.filtro2 !== undefined){
            endereco +="&filtro2=";
            endereco += parametros.filtro2;
        }
        
        if(parametros.filtro3 !== undefined){
            endereco +="&filtro3=";
            endereco += parametros.filtro3;
        }
        if(parametros.search !== undefined){
            endereco +="&search=";
            endereco += parametros.search;
        }
        console.log(endereco);
        return $http.get(endereco);
    }
    
    function carregarNotificacoes(){
        
        return $http.get("http://localhost:50694/api/notificacao");
        
    }
    

});