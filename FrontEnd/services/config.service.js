modulo.factory("configService", function ($http) {
    return ({
        carregarSettings:carregarSettings,
        modificarConfigs:modificarConfigs
    });

    function carregarSettings(){

        return $http.get('http://localhost:50694/api/usuario/configuracoes');

    }
    
    function modificarConfigs(usuario){
        return $http.put('http://localhost:50694/api/usuario/configuracoes', usuario);
    }

});