using Nkk.IT.Trial.Programing.Login.Interfaces;
using Nkk.IT.Trial.Programing.Login.Services;

namespace Nkk.IT.Trial.Programing.Login.Repositories
{
    public interface IRepository : IElement
    {
		class EmptyRepository : IRepository
		{
			public string Name => "空リポジトリ";
		}

        public static new IRepository Empty = GetInstance<EmptyRepository>();
		public static new readonly List<IRepository> EmptyList = GetEmptyList<IRepository>();

        public static IEnumerable<T> Enumerate<T>(IEnumerable<IRepository> repositories) where T : IRepository => repositories.OfType<T>();
        public static IEnumerable<T> Enumerate<T>(params IRepository[] repositories) where T : IRepository => Enumerate<T>((IEnumerable<IRepository>)repositories);
        public static T? Get<T>(IEnumerable<IRepository> repositories) where T : IRepository => Enumerate<T>(repositories).FirstOrDefault();
        public static T? Get<T>(params IRepository[] repositories) where T : IRepository => Get<T>((IEnumerable<IRepository>)repositories);
    }
    public interface IRepository<T> : IRepository
    {
        IEnumerable<T> GetData();
    }
    public interface IRepository<T, TArg> : IRepository<T>
    {
        IEnumerable<T> GetData(TArg arg);
    }
    public interface IRepository<T, TArg1, TArg2> : IRepository<T, TArg1>
    {
        IEnumerable<T> GetData(TArg1 arg1, TArg2 arg2);
    }

    public abstract class DependRepository : Repository, IHasRepository
    {
        public List<IRepository> Repositories { get; set; } = IRepository.EmptyList;
        public event EventHandler<IRepository>? RepositoryAdd;
        public void AddRepositories(IEnumerable<IRepository>? repositories) => IHasElements.AddRange(Repositories, repositories, this, RepositoryAdd);
        public void AddRepositories(params IRepository[] repositories) => AddRepositories((IEnumerable<IRepository>)repositories);
        public void AddRepository(IRepository? repository) => IHasElements.Add(Repositories, repository, this, RepositoryAdd);
        public T? GetRepository<T>(IEnumerable<IRepository> repositories) where T : IRepository => IRepository.Get<T>(repositories);
        protected DependRepository(IEnumerable<IService>? services = null, IEnumerable<IRepository>? repositories = null)
        {
            AddServices(services);
            AddRepositories(repositories);
        }
        protected DependRepository(IEnumerable<IService>? services = null, params IRepository[] repositories) : this(services, (IEnumerable<IRepository>)repositories) { }
        protected DependRepository(IEnumerable<IRepository>? repositories = null, params IService[] services) : this(services, repositories) { }
        protected DependRepository(params IRepository[] repositories) : this(Enumerable.Empty<IService>(), (IEnumerable<IRepository>)repositories) { }
        protected DependRepository(params IService[] services) : this((IEnumerable<IService>)services) { }
    }

    public abstract class Repository : IHasServices
    {
        public List<IService> Services { get; set; } = IService.EmptyList;
        public event EventHandler<IService>? ServiceAdd;
        public void AddService(IService? service) => IHasElements.Add(Services, service, this, ServiceAdd);
        public void AddServices(IEnumerable<IService>? services) => IHasElements.AddRange(Services, services, this, ServiceAdd);
        public void AddServices(params IService[] services) => AddServices((IEnumerable<IService>)services);
        public T? GetService<T>(IEnumerable<IService> services) where T : IService => IService.Get<T>(services);
        protected Repository(IEnumerable<IService>? services = null) => AddServices(services);
        protected Repository(params IService[] services) : this((IEnumerable<IService>)services) { }
    }

}