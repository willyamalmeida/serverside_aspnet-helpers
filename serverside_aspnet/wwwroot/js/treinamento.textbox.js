definaNamespace("treinamento.textbox", {});

treinamento.textbox = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.textbox", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options) : this.options;

    this.inicialize();
}

treinamento.textbox.prototype = {
    inicialize: function () {
        // inicializar propriedades                                

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function () {
    },

    ligaEventos: function () {                        
    },

    habilite: function () {
        this.$el.removeAttr("disabled");
    },

    desabilite: function () {
        this.$el.attr("disabled", 'disabled');
    }
}