definaNamespace("treinamento.views.departamento.index", {});

treinamento.views.departamento.index = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.views.departamento.index", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options): this.options;
    
    this.inicialize();
}

treinamento.views.departamento.index.prototype = {
    inicialize: function() {
        // inicializar propriedades                

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function() {
    },

    ligaEventos: function() {          
    }
}