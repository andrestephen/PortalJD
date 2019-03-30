(function () {
    var app = angular.module('appProjeto', []);

    app.controller('controllerProjeto', ['$scope', '$http', function ($scope, $http) {
        $scope.projeto = {};
        //$scope.classeCorAngular = 'nome-da-classe';

        $scope.atualizarStatusProjeto = function (id, status) {
            var params = {
                idProjeto: id,
                statusProjeto: status
            };

            $http.post('/Projetos/AtualizarStatusProjeto', params).then(
                function (successResponse) {
                    $scope.classeCorDrop = status == 1 ? 'btn-warning' : status == 2 ? 'btn-success' : status == 3 ? 'btn-danger' : status == 4 ? 'btn-primary' : '';
                    $scope.classeCorBox = status == 1 ? 'box-warning' : status == 2 ? 'box-success' : status == 3 ? 'box-danger' : status == 4 ? 'box-primary' : '';
                    $scope.textoStatusAngular = status == 1 ? 'Novo' : status == 2 ? 'Aprovado' : status == 3 ? 'Não aprovado' : status == 4 ? 'Concluido' : '';;
                },
                function (errorResponse) {
                    console.log('erro');
                });
        };


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
    }]);
})();