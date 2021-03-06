﻿(function () {
    var app = angular.module('appProjeto', ['ngFileUpload']);

    app.controller('controllerProjeto', ['$scope', '$http', 'Upload', '$timeout', function ($scope, $http, Upload, $timeout) {
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
                    url: '/Projetos/Upload/',
                    data: {
                        files: $scope.SelectedFiles,
                        idProjeto: $scope.projeto.idProjeto
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
                idProjeto: $scope.projeto.idProjeto
            };
            console.log('Anexo: ' + params.idArquivo);
            console.log('Projeto: ' + params.idProjeto);
            $http.post('/Projetos/ExcluirArquivo', params).then(
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