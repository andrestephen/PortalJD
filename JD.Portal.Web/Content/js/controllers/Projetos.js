(function () {
    var app = angular.module('appProjeto', []);

    app.controller('controllerProjeto', ['$scope', '$http', function ($scope, $http) {
        $scope.projeto = {};

        //FUNCAO DE TESTE
        $scope.retornaProjeto = function () {
            var params = {
                id: 1
            };

            $http.get('/Projetos/RetornaProjeto', { params: params }).then(
                function (successResponse) {
                    $scope.projeto = successResponse.data.projeto;
                },
                function (errorResponse) {
                    console.log('erro');
                });
        };

        //FUNCAO DE TESTE
        $scope.atualizarStatusProjeto = function (id, status) {
            var params = {
                idProjeto: id,
                statusProjeto: status
            };

            $http.post('/Projetos/AtualizarStatusProjetoJSON', params).then(
                function (successResponse) {
                    console.log('success');
                },
                function (errorResponse) {
                    console.log('erro');
                });
        };
    }]);
})();