definaNamespace("treinamento.tabela", {});

treinamento.tabela = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.tabela", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options) : this.options;

    this.itensDaTabela = [];

    this.inicialize();
}

treinamento.tabela.prototype = {
    inicialize: function () {
        // inicializar propriedades    
        if (this.modelParaOClientSide.habiliteFiltroDePesquisa) {
            this.$elFiltro = this.$el.find("[name='filtro']");
        }

        var identificadorTabela = "#" + this.modelParaOClientSide.identificador;

        this.$elTabelaItens = this.$el.find(identificadorTabela + " tbody");
        this.$elTabelaTotal = this.$el.find("[name='total']");
        this.$elAtualizar = this.$el.find("[name='atualizar']");

        this.$elPaginacao = this.$el.find(".treinamento-paginacao");

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function () {
        this.pesquisar();
    },

    ligaEventos: function () {
        var _this = this;

        if (this.modelParaOClientSide.habiliteFiltroDePesquisa) {
            this.$elFiltro.on("keyup", function () {
                _this.pesquisar(function (retorno) {
                    var apiPaginacao = _this.$elPaginacao.data("treinamento.paginacao");
                    apiPaginacao.atualizePagina(1);
                    apiPaginacao.totalDeItens = retorno.totalDeItens;
                });
            });
        }

        this.$elAtualizar.on("click", function () {
            _this.pesquisar();
        });

        this.$elTabelaItens.on("click", ".item", function () {
            var codigo = $(this).find("[data-codigo]").text();
            _this.editar(codigo);
        });
    },

    preencheTabela: function (dados) {

        var montaCampoChave = (ehChave, valor) => ehChave ? "data-codigo='" + valor + "'" : "";

        var montaColuna = (coluna, objeto) => {
            var valor = objeto[coluna.data];
            var campoChave = montaCampoChave(coluna.ehCampoChave, valor);
            var td = "<td " + campoChave + " >" + valor + "</td>";

            return td;
        };

        var montaLinha = (colunas) => {            
            var linha = "<tr class='item'>" + colunas + "</tr>";
            return linha;
        };

        var montaLinhas = (linhas, objeto) => {
            var colunas = this.modelParaOClientSide.colunas.reduce((acc, coluna) => {                
                acc = acc + montaColuna(coluna, objeto);
                return acc;
            }, "");

            var linha = montaLinha(colunas);
                        
            linhas = linhas.concat(linha);

            return linhas;
        };

        var novasLinhas = dados.lista.reduce((acc, objeto) => montaLinhas(acc, objeto), "");
        
        var colunaAdicional = new Array(this.modelParaOClientSide.colunas.length).fill("").reduce((acc, col) => {
            acc = acc.concat("<td></td>");
            return acc;
        }, "");

        var tbody = novasLinhas.concat("<tr>").concat(colunaAdicional).concat("</tr>");

        this.$elTabelaItens.html(tbody);
        this.$elTabelaTotal.text(dados.totalDeItens);
    },

    pesquisar: function (callback) {
        var _this = this;
        var url = treinamento.obtenhaUrlAction(this.modelParaOClientSide.controller, this.modelParaOClientSide.actionConsulte);

        var filtro = this.modelParaOClientSide.habiliteFiltroDePesquisa
            ? this.$elFiltro.val()
            : "";

        filtro = !filtro ? "" : filtro;

        var data = {
            filtro: filtro,
            pagina: this.modelParaOClientSide.pagina,
            quantidade: this.modelParaOClientSide.quantidade
        }

        $.ajax({
            type: 'GET',
            url: url,
            data: data,
            dataType: 'json',
            success: function (retorno) {
                _this.itensDaTabela = retorno.lista;
                _this.preencheTabela(retorno);

                if (callback) {
                    callback(retorno);
                }

                _this.$el.trigger("tabelaCarregada", retorno);
            }
        });
    },

    editar: function (codigo) {
        var urlEditar = treinamento.obtenhaUrlAction(this.modelParaOClientSide.controller, this.modelParaOClientSide.actionConsulte);
        window.location.href = urlEditar + "/" + codigo;
    }
}