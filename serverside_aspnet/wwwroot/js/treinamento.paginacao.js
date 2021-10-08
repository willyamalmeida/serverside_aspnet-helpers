definaNamespace("treinamento.paginacao", {});

treinamento.paginacao = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.paginacao", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options) : this.options;

    this.paginaAnterior = this.modelParaOClientSide.pagina;
    this.paginaAtual = this.modelParaOClientSide.pagina;
    this.totalDeItens = 0;

    this.inicialize();
}

treinamento.paginacao.prototype = {
    inicialize: function () {
        // inicializar propriedades                        
        this.$elPaginacao = this.$el.find("[name='paginacao'] ul");

        this.$elApiTabela = this.$el.parent().parent().parent();
        
        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function () {
    },

    ligaEventos: function () {
        var _this = this;

        this.$elApiTabela.on("tabelaCarregada", function (e, totalDeItens) {
            _this.preenchePaginacao(totalDeItens);
        });

        this.$elPaginacao.on("click", "[name='anterior']", function () {
            _this.verifiqueAnterior();
        });

        this.$elPaginacao.on("click", "[name='proximo']", function () {
            _this.verifiqueProximo();
        });

        this.$elPaginacao.on("click", "[name='paginaNumerada']", function () {
            var pagina = $(this).find('a').text();
            _this.verifiquePagina(Number(pagina));
        });
    },

    verifiqueAnterior: function () {
        var novaPagina = this.modelParaOClientSide.pagina - 1;

        if (novaPagina > 0) {            
            this.atualizePagina(novaPagina);
            this.pesquisar();
        }
    },

    verifiqueProximo: function () {        
        var novaPagina = this.modelParaOClientSide.pagina + 1;

        if (novaPagina <= this.obtenhaQuantidadeDePaginacao(this.totalDeItens)) {            
            this.atualizePagina(novaPagina);
            this.pesquisar();
        }
    },

    verifiquePagina: function (pagina) {
        if (this.modelParaOClientSide.pagina != pagina) {            
            this.atualizePagina(pagina);
            this.pesquisar();
        }
    },

    atualizePagina: function (novaPagina) {
        this.paginaAnterior = this.paginaAtual;
        this.paginaAtual = novaPagina;
        this.modelParaOClientSide.pagina = novaPagina;
    },

    pesquisar: function (filtro) {
        var _this = this;
        var url = location.origin + "/" + this.modelParaOClientSide.controller + "/" + this.modelParaOClientSide.action;

        var apiTabela = this.$elApiTabela.data(this.modelParaOClientSide.apiTabela);

        var data = {
            filtro: apiTabela.$elFiltro.val(),
            pagina: this.modelParaOClientSide.pagina,
            quantidade: this.modelParaOClientSide.quantidade
        };
                
        $.ajax({
            type: 'GET',
            url: url,
            data: data,
            dataType: 'json',
            success: function (resultado) {
                _this.totalDeItens = resultado.totalDeItens;
                apiTabela.preencheTabela(resultado);
                _this.preenchePaginacao(resultado);
            },
            error: function () {
                _this.atualizePagina(_this.paginaAnterior);
            }
        });
    },

    preenchePaginacao: function (dados) {
        var _this = this;
        this.totalDeItens = dados.totalDeItens;        
                
        var quantidadePaginas = this.obtenhaQuantidadeDePaginacao(dados.totalDeItens);

        var elPaginas = new Array(quantidadePaginas).fill("").reduce(function (paginas, item, i) {
            var index = i + 1;
            var ativo = _this.modelParaOClientSide.pagina == index;
            var pagina = _this.obtenhaElPagina(ativo, index);
            paginas = paginas + pagina;

            return paginas;
        }, "");
                
        var elAnterior = this.obtenhaElAnterior(this.modelParaOClientSide.pagina > 1);
        var elProximo = this.obtenhaElProximo(this.modelParaOClientSide.pagina < quantidadePaginas);

        this.$elPaginacao.html(elAnterior.concat(elPaginas).concat(elProximo));
    },

    obtenhaQuantidadeDePaginacao: function (totalDeItens) {
        var calculo = totalDeItens / this.modelParaOClientSide.quantidade;
        var parteInteira = Math.trunc(calculo);
        var parteDecimal = (calculo + "").split(".").length > 1 ? 1 : 0;
        var quantidadePaginas = parteInteira + parteDecimal;

        return quantidadePaginas;
    },

    obtenhaElAnterior: function (habilitado) {
        var atributoDesabilitado = !habilitado ? "disabled" : "";

        var elAnterior =
            '<li class="page-item ' + atributoDesabilitado + '" name="anterior">' +
            '  <a class="page-link" href="#" aria-label="Anterior">' +
            '     <span aria-hidden="true">&laquo;</span>' +
            '     <span class="sr-only">Anterior</span>' +
            '  </a>' +
            '</li >';

        return elAnterior;
    },

    obtenhaElProximo: function (habilitado) {
        var atributoDesabilitado = !habilitado ? "disabled" : "";

        var elProximo =
            '<li class="page-item ' + atributoDesabilitado + '" name="proximo">' +
            '  <a class="page-link" href="#" aria-label="Proximo">' +
            '     <span aria-hidden="true">&raquo;</span>' +
            '     <span class="sr-only">Proximo</span>' +
            '  </a>' +
            '</li >';

        return elProximo;
    },

    obtenhaElPagina: function (ativo, pagina) {
        var atributoAtivo = ativo ? "active" : "";
        var elPagina = '<li class="page-item ' + atributoAtivo + '" name="paginaNumerada"><a class="page-link" href="#">' + pagina + '</a></li>';
        return elPagina;
    }
}