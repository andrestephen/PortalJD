(function () {
    var app = angular.module('appProjeto', []);

    app.controller('controllerProjeto', ['$scope', '$http', function ($scope, $http) {
        $scope.listaTodosDiaconos = {};
        //$scope.diacono = {};
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
                    $scope.textoStatusAngular = status == 1 ? 'Novo' : status == 2 ? 'Aprovado' : status == 3 ? 'Não aprovado' : status == 4 ? 'Concluido' : '';
                },
                function (errorResponse) {
                    console.log('erro');
                });
        };

        $scope.atualizarInformacaoProjeto = function () {
            
            var params = {
                idProjeto: $scope.projeto.idProjeto,
                idDiacono: $scope.diacono.idDiacono,
                descricaoAtualizacao: $scope.atualizacaoProjeto.descricao
            };
            console.log('Projeto: ' + params.idProjeto);
            console.log('Diacono:' + params.idDiacono);
            $http.post('/Projetos/AtualizarInformacaoProjeto', params).then(
                function (successResponse) {
                    //console.log(successResponse);
                    $scope.listaAtualizacoes = successResponse.data.listaAtualizacoes;

                    $scope.atualizacaoProjeto.descricao = '';
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
                idProjeto: $scope.projeto.idProjeto
            };

            $http.get('/Projetos/ListarTodosDiaconos', { params: params }).then(
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


        $scope.atualizarResponsaveisProjeto = function () {

            //console.log('Antes de pegar parametros');
            var params = {
                idProjeto: $scope.projeto.idProjeto,
                listaDiaconosResponsaveis: $scope.listaTodosDiaconos
            };
            //console.log('Depois de pegar parametros');
            //console.log($scope.listaTodosDiaconos);

            $http.post('/Projetos/AtualizarResponsaveisProjeto', params).then(
                function (successResponse) {
                    $scope.listaDiaconosResponsaveis = successResponse.data.listaDiaconosResponsaveis;
                    console.log('sucesso?');
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