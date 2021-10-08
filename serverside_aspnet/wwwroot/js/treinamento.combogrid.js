definaNamespace("treinamento.combogrid", {});

treinamento.combogrid = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.combogrid", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options) : this.options;

    this.inicialize();
}

treinamento.combogrid.prototype = {
    inicialize: function () {
        // inicializar propriedades                
        this.$elPesquisar = this.$el.find("[data-pesquisar]");

        this.$elTabela = this.$el.find("[name='tabela']");
        this.$elThead = this.$elTabela.find('thead');
        this.$elTbody = this.$elTabela.find("tbody");

        this.$elPropriedadeSelecionada = this.$el.find("[name='"+ this.modelParaOClientSide.propriedadeSelecionada + "']");

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function () {
        this.defineColunasDaTabela();
        this.defineItemSelecionado(this.modelParaOClientSide.itemSelecionado);
    },

    ligaEventos: function () {
        var _this = this;

        this.$elPesquisar.on("click", function () {
            _this.pesquisar();
        });

        this.$elPesquisar.on("keyup", function () {
            var filtro = _this.$elPesquisar.val();
            _this.pesquisar(filtro);
        });

        this.$elTbody.on("click", ".item", function () {
            var el = $(this);
            _this.selecionar(el);
        });
    },

    pesquisar: function (filtro) {
        var _this = this;
        var url = location.origin + "/" + this.modelParaOClientSide.controller + "/" + this.modelParaOClientSide.action;

        var data = {
            filtro,
            quantidade: this.modelParaOClientSide.quantidade
        };

        $.ajax({
            type: 'GET',
            url: url,
            data: data,
            dataType: 'json',
            success: function (resultado) {
                _this.preencheTabela(resultado);
            }
        });
    },

    defineColunasDaTabela: function () {
        var ths = JSON.parse(this.modelParaOClientSide.colunas).reduce(function (colunas, coluna) {
            var col = "<th data-name='" + coluna.name + "'>" + coluna.label + "</th>";

            colunas = colunas.concat(col);
            return colunas;
        }, "");

        var thead = "<tr scope='col'>" + ths + "</tr>";
        this.$elThead.html(thead);
    },

    preencheTabela: function (dados) {
        var _this = this;
        var tbody = dados.reduce(function (linhas, item) {
            var valorChave = null;

            var cols = JSON.parse(_this.modelParaOClientSide.colunas).reduce(function (colunas, coluna) {
                var valor = item[coluna.name];

                if (_this.modelParaOClientSide.campoChave == coluna.name) {
                    valorChave = valor;
                }

                var col = "<td data-name='" + coluna.name + "'>" + valor + "</td>";

                return colunas.concat(col);
            }, "");

            var linha = "<tr class='item' data-item='"+ valorChave + "'>".concat(cols).concat("</tr>");

            return linhas.concat(linha);
        }, "");

        this.$elTbody.html(tbody);
    },

    selecionar: function (el) {
        var _this = this;
        _this.modelParaOClientSide.itemSelecionado = {};

        var itemSelecionado = this.modelParaOClientSide.camposTemplate.reduce(function (t, tCampo) {
            var elDataName = el.find('[data-name="' + tCampo + '"]');

            if (elDataName && elDataName.length == 1) {
                var conteudo = elDataName.text();
                t[tCampo] = conteudo;
            }

            return t;
        }, {});

        this.defineItemSelecionado(itemSelecionado);
    },

    defineItemSelecionado: function (itemSelecionado) {
        if (!itemSelecionado) {
            return;
        }

        var _this = this;
        _this.modelParaOClientSide.itemSelecionado = {};

        var templateConteudo = this.modelParaOClientSide.camposTemplate.reduce(function (t, tCampo) {
            var conteudo = itemSelecionado[tCampo];

            if (t) {
                t = t + " - " + conteudo;
            }
            else {
                t = conteudo;
            }

            _this.modelParaOClientSide.itemSelecionado[tCampo] = conteudo;
            

            return t;
        }, null);

        var valorChave = _this.modelParaOClientSide.itemSelecionado[this.modelParaOClientSide.campoChave];

        this.$elPesquisar.val(templateConteudo);
        this.$elPropriedadeSelecionada.val(valorChave);
    },

    obtenhaItemSelecionado: function () {
        return this.modelParaOClientSide.itemSelecionado;
    }
}