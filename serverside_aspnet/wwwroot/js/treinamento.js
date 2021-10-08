jQuery.browser = {};
(function () {
    jQuery.browser.msie = false;
    jQuery.browser.version = 0;
    if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
        jQuery.browser.msie = true;
        jQuery.browser.version = RegExp.$1;
    }
})();


/* Função que define o valor de uma propriedade a partir de uma string separada por ".". */
/* Definido no objeto window para ser invocado pela função definaNamespace. */
var setaValor = (function () {

    var setaValorString = function (objetoPai, caminho, valor) {
        var arrayCaminho = caminho.split('.');
        setaValorArray(objetoPai, arrayCaminho, valor);
    };

    var setaValorArray = function funcaoSetaValorArray(objetoPai, caminho, valor) {
        var nome = caminho[0];
        if (caminho.length > 1) {
            if (!objetoPai[nome]) {
                objetoPai[nome] = {};
            }

            funcaoSetaValorArray(objetoPai[nome], caminho.slice(1, caminho.length), valor);
        } else {
            definaValor(objetoPai, nome, valor);
        }
    };

    var definaValor = function (objetoPai, nomePropriedade, valor) {
        var valorAtual = objetoPai[nomePropriedade];
        if (valorAtual) {
            objetoPai[nomePropriedade] = jQuery.extend(valorAtual, valor);
        } else {
            objetoPai[nomePropriedade] = valor;
        }
    };

    return setaValorString;
})();

/* Define um namespace dentro do global treinamento. */
var definaNamespace = function (namespace, conteudo) {
    if (namespace.substr(0, 11) != "treinamento") {
        namespace = "treinamento." + namespace;
    }

    setaValor(window, namespace, conteudo);
};

/* Obtém o tipo de um objeto. Obs.: para tipos construídos, o tipo retornado será "Object". A invocação a objeto.constructor.name */
/* retorna o nome da função usada como construtor do objeto, porém não foi implementada no IE 9 ou versões anteriores. */
var obtenhaTipo = function obtenhaTipo(objeto) {
    return Object.prototype.toString.call(objeto).match(/^\[object (.*)\]$/)[1];
};

definaNamespace("treinamento", (function () {

    /*
    * Variável que armazenará todos os scripts aguardando carregamento
    */
    var scriptsAguardandoCarregamento = [];

    /*
    * Variável que armazenará todos os scripts aguardando execução
    * para os casos em que o script precise de execução antes do 
    * seu .js seja carregado, evidanto erros.
    */
    var funcoesAguardandoExecucaoQuandoScriptCarregado = [];


    /**
    * Método responsável por gerencias a exclusão da fila de scripts
    */
    var removeScriptCarregado = function (url) {
        for (var i = scriptsAguardandoCarregamento.length - 1; i >= 0; i--) {
            if (scriptsAguardandoCarregamento[i] == url) {
                scriptsAguardandoCarregamento.splice(i, 1);
                $(document).trigger("scriptCarregado", [url]);
            }
        }

        if (scriptsAguardandoCarregamento.length == 0)
            $(document).trigger("scriptsCarregados");
    }

    /*
    * Função responsável por desempilhar os scripts aguarndo execução
    * para uma determinada url. Esta ação é disparada no carregamento de
    * cada arquivo js, que utilizou a função carregueJavascript.
    */
    var executaFuncoesAguardandoCarregadoDoScript = function (url) {
        for (var i = funcoesAguardandoExecucaoQuandoScriptCarregado.length - 1; i >= 0; i--) {

            if (funcoesAguardandoExecucaoQuandoScriptCarregado[i].url == url) {
                funcoesAguardandoExecucaoQuandoScriptCarregado[i].callback();
                funcoesAguardandoExecucaoQuandoScriptCarregado.splice(i, 1);
            }
        }

    };

    var carregueJavascript = function (url, options) {

        var urlAbsoluta = obtenhaCaminhoAbsolutoParaUrl(url);

        var scriptJaCarregado = $("script[src='" + urlAbsoluta + "']");

        if (scriptJaCarregado.length > 0) {
            if (options && options.carregamentoUnico) {
                if (options.callback) {
                    if (scriptJaCarregado.attr("data-carregado") == "true")
                        options.callback();
                    else
                        funcoesAguardandoExecucaoQuandoScriptCarregado.push({
                            url: urlAbsoluta,
                            callback: options.callback
                        });
                }

                return undefined;
            } else {
                scriptJaCarregado.remove();
            }
        }

        var head = document.getElementsByTagName("head")[0];
        var script = document.createElement("script");
        var $script = $(script);

        options = options || {};
        script.type = "text/javascript";
        script.src = urlAbsoluta;

        if (options.charset) {
            script.charset = options.charset;
        }

        /* Handle Script loading				 */
        var done = false;

        /* Attach handlers for all browsers */
        script.onload = script.onreadystatechange = function () {
            if (!done && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) {
                done = true;
                if (options.callback) {
                    options.callback();
                }

                // Flag para dizer se o script foi completamente carregado.
                $script.attr("data-carregado", "true");

                removeScriptCarregado(urlAbsoluta);

                // Executa os scripts aguardando execução, caso a necessidade de execução
                // ocorrer antes do carregamento completo do script.
                executaFuncoesAguardandoCarregadoDoScript(urlAbsoluta);

                /* Handle memory leak in IE */
                script.onload = script.onreadystatechange = null;
            }
        };

        script.onerror = function () {
            if (!done) {
                done = true;
                removeScriptCarregado(urlAbsoluta);
                $(document).trigger("scriptNaoEncontrado", [urlAbsoluta]);
            }
        }

        script.src = urlAbsoluta;

        // Adiciona o script a fila informativa de carregamento e 
        // dispara o evento para o document dizendo que um script
        // está carregando
        scriptsAguardandoCarregamento.push(urlAbsoluta);
        $(document).trigger("scriptCarregando", [urlAbsoluta]);

        head.appendChild(script);

        /* We handle everything using the script element injection */
        return undefined;
    };

    var carregueMultiposArquivosJavaScript = function (listaDeArquivosJavaScripts, options) {

        var proximoScriptASerCarregado = listaDeArquivosJavaScripts.shift();

        carregueJavascript(proximoScriptASerCarregado, {
            carregamentoUnico: options.carregamentoUnico,
            callback: function () {
                if (listaDeArquivosJavaScripts.length == 0) {
                    if (options.callback) {
                        options.callback();
                    }
                } else {
                    carregueMultiposArquivosJavaScript(listaDeArquivosJavaScripts, options);
                }
            }
        });
    };

    /* Adiciona um arquivo CSS a partir de uma partial view. */
    var carregueCss = function (url) {
        var urlAbsoluta = obtenhaCaminhoAbsolutoParaUrl(url);

        // Caso não seja IE, a engine adiona uma tag no body, a DOM API
        // já se encarrega de fazer o download e aplicar os estilos.
        if (!jQuery.browser.msie) {
            if ($("link[href='" + urlAbsoluta + "']").length > 0)
                return;

            $('head').append('<link rel="stylesheet" href="' + urlAbsoluta + '" type="text/css" />');
        }

        // Caso contrário, como o IE tem uma limiteção para qnd de arquivos CSS, criamos
        // uma tag style, que servirá de repositório para os arquivos, e nela 
        // adicionamos parametros @import, que tem o mesmo efeito para os browser
        // modernos, carregando e aplicando de maneira assincrona os estilos.
        else {

            var $elRepositoriosDeEstilos = $("head style[data-repositorio-de-estilos-do-ie]");

            // O repositório não exista, ou o atual já está cheio, 
            // criamos um novo.
            if ($elRepositoriosDeEstilos.length == 0)
                $('head').append('<style data-repositorio-de-estilos-do-ie="" />');
            else if ($elRepositoriosDeEstilos.last()[0].styleSheet.imports.length > 25)
                $('head').append('<style data-repositorio-de-estilos-do-ie="" />');

            // Roda o seletor novamente
            $elRepositoriosDeEstilos = $("head style[data-repositorio-de-estilos-do-ie]");
            var estiloExiste = false;

            // Busca verificando se o estilo a ser adicionado já existe.
            for (var j = 0; j < $elRepositoriosDeEstilos.length; j++) {
                var styleSheetAtual = $elRepositoriosDeEstilos[j].styleSheet;
                for (var i = 0; i < styleSheetAtual.imports.length; i++) {
                    if (styleSheetAtual.imports[i].href == urlAbsoluta)
                        estiloExiste = true;
                }
            }

            // Caso não, o inclui, com uma tag @important.
            if (!estiloExiste)
                $elRepositoriosDeEstilos.last()[0].styleSheet.addImport(urlAbsoluta);
        }
    };

    /**
    * Função que transfere a referencia dos arquivos CSS (<link rel="stylesheet"/>)
    * para dentro do repositório de arquivos CSS, com o um @Import.
    * Essa medida, visa contornar a limitação do IE com o numero 
    * máximo de arquivos CSS na página.
    */
    var otimizeArquivosCssCarregadosParaOInternetExplorer = function () {
        if (!jQuery.browser.msie)
            return;

        var listaDeUrls = $("link[rel='stylesheet']").map(function () {
            return this.href;
        });

        // Remove as referencias.
        $("link[rel='stylesheet']").remove();

        // Itera sobre os arquivos CSS forçando a entrar na rotina
        // padrão de adicão de arquivos para o internet explorer,
        // Para logo após serem removidos do DOM.
        for (var i = 0; i < listaDeUrls.length; i++) {
            carregueCss(listaDeUrls[i]);
        }
    };


    /* Adiciona os valores informados ao objeto base e retorna o resultado da extensão. A função altera os valores */
    /* equivalentes (se algum) no objeto base. */
    var extendaObjeto = function (objetoBase, novosValores) {
        var resultadoExtensao = jQuery.extend(objetoBase, novosValores);
        return resultadoExtensao;
    };

    var adicioneRegraCss = function (seletor, style) {
        var styleSheet = document.styleSheets[document.styleSheets.length - 1];

        if (styleSheet.insertRule) {
            styleSheet.insertRule(seletor + " { " + style + " }", 0);
        } else if (styleSheet.addRule) {
            styleSheet.addRule(seletor, style, 0);
        }
    };

    /* Restringe por aba o acesso e a escrita para o módulo de nome informado. */
    var definaModuloPorAba = function (nome) {
        // Faz a compatibilidade com browsers mais antigos.
        if (Object.prototype.hasOwnProperty.call(window, nome)) {
            return;
        }

        Object.defineProperty(window, nome, {
            get: function () {
                return $me()[nome];
            },
            set: function (value) {
                $me()[nome] = value;
            }
        });
    };

    // Mostra uma descrição de um erro no console do browser.
    var logErroNoConsole = function (mensagem, erro) {
        console.error(mensagem + "\n" + erro.stack);
    };

    /**
    * Função que retorna o caminho absoluto de uma url relativa.
    *
    * @param {String} url - Url relativa.
    */
    var obtenhaCaminhoAbsolutoParaUrl = function (url) {
        var link = document.createElement("a");
        link.href = url;

        // a DOM API faz a conversão automaticamente a travez
        // do da propriedade oferecida para o link elemment.
        return link.href;
    };

    var obtenhaUrlAction = function (controller, action) {
        var urlAction = location.origin + "/" + controller + "/" + action;
        return urlAction;
    };

    /* Elementos expostos. */
    return {
        carregueJavascript: carregueJavascript,
        carregueMultiposArquivosJavaScript: carregueMultiposArquivosJavaScript,
        carregueCss: carregueCss,
        scriptsAguardandoCarregamento: scriptsAguardandoCarregamento,
        otimizeArquivosCssCarregadosParaOInternetExplorer: otimizeArquivosCssCarregadosParaOInternetExplorer,
        extendaObjeto: extendaObjeto,
        adicioneRegraCss: adicioneRegraCss,
        definaModuloPorAba: definaModuloPorAba,
        obtenhaCaminhoAbsolutoParaUrl: obtenhaCaminhoAbsolutoParaUrl,
        logErroNoConsole: logErroNoConsole,
        obtenhaUrlAction: obtenhaUrlAction
    };
})());