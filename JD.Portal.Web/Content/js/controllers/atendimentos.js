(function () {
    var app = angular.module('appAtendimento', ['ngFileUpload']);

    app.controller('controllerAtendimento', ['$scope', '$http', 'Upload', '$timeout', function ($scope, $http, Upload, $timeout) {
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

        $scope.UploadFiles = function (files) {

            $scope.SelectedFiles = files;
            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                Upload.upload({
                    url: '/Atendimentos/Upload/',
                    data: {
                        files: $scope.SelectedFiles,
                        idAtendimento: $scope.atendimento.idAtendimento
                    }
                }).then(function (response) {
                    $timeout(function () {
                        $scope.listaArquivos = response.data.listaArquivos;
                        $scope.Progress = 0;
                    });
                }, function (response) {
                    if (response.status > 0) {
                        var errorMsg = response.status + ': ' + response.data;
                        alert(errorMsg);
                    }
                }, function (evt) {
                    //var element = angular.element(document.querySelector('#dvProgress'));
                    $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                    //element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                });
            }
        };


        $('#modalDiaconos').on('show.bs.modal', function (e) {
            console.log('entrou');
            $scope.listarTodosDiaconos();
            console.log('passou');
        });

        $scope.excluirArquivo = function (id) {

            var params = {
                idArquivo: id,
                idAtendimento: $scope.atendimento.idAtendimento
            };
            console.log('Anexo: ' + params.idArquivo);
            console.log('Atendimento: ' + params.idAtendimento);
            $http.post('/Atendimentos/ExcluirArquivo', params).then(
                function (successResponse) {
                    $scope.listaArquivos = successResponse.data.listaArquivos;
                },
                function (errorResponse) {
                    console.log(errorResponse);
                    console.log('erro');
                });
        };
    }]);
})();