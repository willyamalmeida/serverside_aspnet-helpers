definaNamespace("treinamento.views.departamento.editar", {});

treinamento.views.departamento.editar = function ($el, modelParaOClientSide, options) {
    'use strict';

    if (!$el) {
        throw "Elemento não definido";
    }

    this.$el = $el;
    this.$el.data("treinamento.views.departamento.editar", this);
    this.modelParaOClientSide = modelParaOClientSide;
    this.options = options ? jQuery.extend({}, this.options, options): this.options;

    this.inicialize();
}

treinamento.views.departamento.editar.prototype = {
    inicialize: function() {
        // inicializar propriedades        
        this.$elForm = this.$el.find("form");
        this.$elSalvar = this.$el.find("[name='salvar']");

        // prepare componentes
        this.prepareComponente();

        // eventos
        this.ligaEventos();
    },

    prepareComponente: function () {
    },

    ligaEventos: function() {  
        var _this = this;

        this.$elSalvar.on('click', function () {
            _this.salvar();
        });
    },

    salvar: function () {
        this.$elForm.submit();
    }
}