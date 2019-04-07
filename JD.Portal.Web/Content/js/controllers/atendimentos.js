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
                    $scope.classeCorDrop = status == true ? 'btn-primary' : 'btn-warning';
                    $scope.classeCorBox = status == true ? 'box-primary' : 'box-warning';
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

        $scope.listarTodosDiaconos = function () {
            var params = {
                idAtendimento: $scope.atendimento.idAtendimento
            };

            $http.get('/Atendimentos/ListarTodosDiaconos', { params: params }).then(
                function (successResponse) {
                    console.log('vai chamar')
                    $scope.listaTodosDiaconos = successResponse.data.listaDiaconos;

                    console.log($scope.listaTodosDiaconos);

                    console.log('sucesso?');
                },
                function (errorResponse) {
                    console.log(errorResponse);
                    console.log('erro');
                });
        };


        $scope.atualizarResponsaveisAtendimento = function () {

            //console.log('Antes de pegar parametros');
            var params = {
                idAtendimento: $scope.atendimento.idAtendimento,
                listaDiaconosResponsaveis: $scope.listaTodosDiaconos
            };
            //console.log('Depois de pegar parametros');
            //console.log($scope.listaTodosDiaconos);

            $http.post('/Atendimentos/AtualizarResponsaveisAtendimento', params).then(
                function (successResponse) {
                    $scope.listaDiaconosResponsaveis = successResponse.data.listaDiaconosResponsaveis;
                    $('#modalDiaconos').modal('hide');
                },
                function (errorResponse) {
                    console.log(errorResponse);
                    console.log('erro');
                });
        };


        $('#modalDiaconos').on('show.bs.modal', function (e) {
            console.log('entrou');
            $scope.listarTodosDiaconos();
            console.log('passou');
        })
    }]);
})();