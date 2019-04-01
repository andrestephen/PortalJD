(function () {
    var app = angular.module('appAtendimento', []);

    app.controller('controllerAtendimento', ['$scope', '$http', function ($scope, $http) {
        $scope.atualizarStatusAtendimento = function (id, status) {
            var params = {
                idAtendimento: id,
                statusAtendimento: status
            };

            $http.post('/Atendimentos/AtualizarStatusAtendimento', params).then(
                function (successResponse) {
                    $scope.classeCorDrop = status == true ? 'btn-success' : 'btn-danger';
                    $scope.classeCorBox = status == true ? 'box-success' : 'box-danger';
                    $scope.textoStatusAngular = status == true ? 'Arquivado' : 'Em Aberto';
                },
                function (errorResponse) {
                    console.log('erro');
                });
        };

        $scope.atualizarInformacaoAtendimento = function () {            
            var params = {
                idAtendimento: $scope.atendimento.idAtendimento,
                idDiacono: $scope.diacono.idDiacono,
                descricaoAtualizacao: $scope.atualizacaoAtendimento.descricao
            };
            console.log('Atendimento: ' + params.idAtendimento);
            console.log('Diacono:' + params.idDiacono);
            $http.post('/Atendimentos/AtualizarInformacaoAtendimento', params).then(
                function (successResponse) {
                    //console.log(successResponse);
                    $scope.listaAtualizacoes = successResponse.data.listaAtualizacoes;

                    $scope.atualizacaoAtendimento.descricao = '';
                    $scope.frmAcompanhamento.$setUntouched();
                    //console.log('sucesso?');
                },
                function (errorResponse) {
                    console.log(errorResponse);
                    console.log('erro');
                });
        }
    }]);
})();