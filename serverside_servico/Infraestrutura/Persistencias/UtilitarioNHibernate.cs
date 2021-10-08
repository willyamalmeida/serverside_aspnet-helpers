using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace serverside_servico.Infraestrutura.Persistencias
{
    public class UtilitarioNHibernate
    {
        private static ISession _sessao;
        private static ISessionFactory _sessionFactory;

        public static ISession Sessao
        {
            get
            {
                if (_sessao == null)
                {
                    Inicialize();
                }

                if (!_sessao.IsOpen)
                {
                    _sessao = _sessionFactory.OpenSession();
                }

                return _sessao;
            }
        }

        public static void FinalizeSessao()
        {
            if (_sessao != null && _sessao.IsOpen)
            {
                _sessao.Close();
                _sessao.Dispose();
            }

            if (_sessionFactory != null && _sessionFactory.IsClosed == false)
            {
                _sessionFactory.Close();
                _sessionFactory.Dispose();
            }
        }

        private static void Inicialize()
        {
            var configuracoes = Fluently
                .Configure()
                .ExposeConfiguration(Configuracoes)
                .Database(CrieConexaoComBanco())
                .Cache(CrieCache)
                .Mappings(CrieMapeamento);

            _sessionFactory = configuracoes.BuildSessionFactory();

            _sessao = _sessionFactory.OpenSession();
            _sessao.FlushMode = FlushMode.Commit;
        }

        private static void Configuracoes(Configuration configuracoes)
        {
            configuracoes.SetInterceptor(new InterceptadorNHibernate());
        }

        private static IPersistenceConfigurer CrieConexaoComBanco()
        {
            var stringDeConexao = "Data Source=.;Database=TREINAMENTO;User ID=sa;Password=@dmin1@dmin1;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var configuracaoConexao = MsSqlConfiguration.MsSql2012.ConnectionString(stringDeConexao);

            return configuracaoConexao;
        }

        private static void CrieCache(CacheSettingsBuilder cache)
        {
            ////cache
            ////    .UseQueryCache()
            ////    .UseSecondLevelCache()
            ////    .ProviderClass<NHibernate.Cache.HashtableCacheProvider>();
        }

        private static void CrieMapeamento(MappingConfiguration mapConfiguration)
        {
            var diretorio = AppDomain.CurrentDomain.BaseDirectory;
            var dllPersistencia = Directory.GetFiles(diretorio, "serverside_servico.dll").FirstOrDefault();

            if (dllPersistencia == null)
            {
                throw new NotImplementedException("Não foi encontrado a dll de persistencia.");
            }

            var assemblie = Assembly.LoadFrom(dllPersistencia);
            mapConfiguration.FluentMappings.AddFromAssembly(assemblie);
        }
    }
}
