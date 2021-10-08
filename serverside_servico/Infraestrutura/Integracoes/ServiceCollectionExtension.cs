using Microsoft.Extensions.DependencyInjection;
using serverside_servico.Conversores;
using serverside_servico.Dtos;
using serverside_servico.Infraestrutura.Conversores;
using serverside_servico.Interfaces.Repositorios;
using serverside_servico.Interfaces.Servicos;
using serverside_servico.Negocio;
using serverside_servico.Repositorios;
using serverside_servico.Servicos;

namespace serverside_servico.Infraestrutura.Integracoes
{
    public static class ServiceCollectionExtension
    {
        public static void AddIntegracaoServico(this IServiceCollection services)
        {
            // automappers
            services.AddAutoMapper(typeof(AutoMapperDepartamento));
            services.AddAutoMapper(typeof(AutoMapperFuncionario));

            // servicos
            services.AddSingleton<IServicoDeDepartamento, ServicoDeDepartamento>();
            services.AddSingleton<IServicoDeFuncionario, ServicoDeFuncionario>();

            // conversores
            services.AddSingleton<IConversorComCodigoNumerico<DtoDepartamento, Departamento>, ConversorDepartamento>();
            services.AddSingleton<IConversorComCodigoNumerico<DtoFuncionario, Funcionario>, ConversorFuncionario>();

            // repositorios
            services.AddSingleton<IRepositorioDepartamento, RepositorioDepartamento>();
            services.AddSingleton<IRepositorioFuncionario, RepositorioFuncionario>();
        }
    }
}
