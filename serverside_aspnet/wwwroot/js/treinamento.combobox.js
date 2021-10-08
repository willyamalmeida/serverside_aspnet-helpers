definaNamespace("treinamento.combobox", {});

treinamento.combobox = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.combobox", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options) : this.options;

    this.inicialize();
}

treinamento.combobox.prototype = {
    inicialize: function () {
        // inicializar propriedades        
        this.$elCombox = this.$el.find("select");

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function () {
        this.consulte();
    },

    ligaEventos: function () {
    },

    consulte: function () {
        var _this = this;
        var url = location.origin
            + "/" + this.modelParaOClientSide.controller
            + "/" + this.modelParaOClientSide.action;

        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            success: function (retorno) {                
                _this.preencheCombo(retorno);
            }
        });
    },

    preencheCombo: function (dados) {
        var _this = this;

        var combo = dados.reduce(function (linhas, objeto) {
            var valorChave = objeto[_this.modelParaOClientSide.campoChave];

            var template = _this.modelParaOClientSide.colunas.reduce(function (acc, propriedade) {                
                var valor = objeto[propriedade]

                if (acc) {
                    acc = acc + " - " + valor;
                }
                else {
                    acc = valor;                
                }

                return acc;

            }, null);

            var linha = "<option class='item' value='" + valorChave + "'>"
                              + template
                      + " </option > ";

            linhas = linhas + linha;
            return linhas;
        }, "<option></option>");

        this.$elCombox.html(combo);
    },

    habilite: function () {
        this.$elCombox.removeAttr("disabled");
    },

    desabilite: function () {
        this.$elCombox.attr("disabled", 'disabled');
    }
}