definaNamespace("treinamento.views.funcionario.index", {});

treinamento.views.funcionario.index = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.views.funcionario.index", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options): this.options;

    this.itensDaTabela = [];

    this.inicialize();
}

treinamento.views.funcionario.index.prototype = {
    inicialize: function() {
        // inicializar propriedades        
        this.$elTabelaItens = this.$el.find("[name='tabela'] tbody");
        this.$elTabelaTotal = this.$el.find("[name='tabela'] tfoot #total");

        this.$elFiltro = this.$el.find("[name='filtro']");
        this.$elAtualizar = this.$el.find("[name='atualizar']");

        this.$elPaginacao = this.$el.find(".treinamento-paginacao");

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function() {
        this.pesquisar();
    },

    ligaEventos: function() {  
        var _this = this;

        this.$elFiltro.on("keyup", function () {
            _this.pesquisar(function (retorno) {
                var apiPaginacao = _this.$elPaginacao.data("treinamento.paginacao");
                apiPaginacao.atualizePagina(1);
                apiPaginacao.totalDeItens = retorno.totalDeItens;
            });
        });  
        
        this.$elAtualizar.on("click", function() {
            _this.pesquisar();
        });

        this.$elTabelaItens.on("click", ".item", function(){
            var id = $(this).find("[data-codigo]").text();
            _this.editar(id);
        });
    },

    preencheTabela: function(dados) {
        var tbody = dados.lista.reduce(function(linhas, funcionario) {
            var linha = "<tr class='item'>"
                + "<td data-codigo='" + funcionario.codigo + "'>" + funcionario.codigo + "</td>"
                + "<td>" + funcionario.nome + "</td>"
                + "<td>" + funcionario.departamento.descricao + "</td>"
                + "</tr>"

            linhas = linhas.concat(linha);
            
            return linhas;
        }, "");

        this.$elTabelaItens.html(tbody.concat("<tr><td></td><td></td><td></td></tr>"));
        this.$elTabelaTotal.text(dados.totalDeItens);
    },

    pesquisar: function(callback) {
        var _this = this;
        var url = location.origin + "/Funcionario/ConsulteParcial";

        var data = {
            filtro: this.$elFiltro.val(),
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

    editar: function(id){
        window.location.href = location.origin + "/Funcionario/Editar/" + id;
    }
}