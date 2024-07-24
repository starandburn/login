using Nkk.IT.Trial.Programing.Login.Interfaces;
using Nkk.IT.Trial.Programing.Login.Repositories;
namespace Nkk.IT.Trial.Programing.Login.Services
{

	public interface IService : IElement
	{
		class EmptyService : IService
		{
			public string Name => "空サービス";
		}
		public static new IService Empty = GetInstance<EmptyService>();
		public static new readonly List<IService> EmptyList = GetEmptyList<IService>();
	}

	public abstract class DependService : Service, IHasServices
	{
		public override string Name => "依存サービス";

		public List<IService> Services { get; set; } = IService.EmptyList;

		protected DependService(IEnumerable<IService>? services = null) => AddServices(services);
		protected DependService(IEnumerable<IService>? services, IEnumerable<IRepository>? repositories = null) : this(services) => AddRepositories(repositories);

		protected DependService(IEnumerable<IService>? services = null, params IRepository[] repositories) : this(services, (IEnumerable<IRepository>)repositories) { }
		protected DependService(IEnumerable<IRepository>? repositories = null, params IService[] services) : this(services, repositories) { }

		protected DependService(params IRepository[] repositories) : this(Enumerable.Empty<IService>(), (IEnumerable<IRepository>)repositories) { }
		protected DependService(params IService[] services) : this((IEnumerable<IService>)services) { }

		public event EventHandler<IService>? ServiceAdd;

		public void AddService(IService? service) => IHasElements.Add(Services, service, this, ServiceAdd);

		public void AddServices(IEnumerable<IService>? services) => IHasElements.AddRange(Services, services, this, ServiceAdd);
		public void AddServices(params IService[] services) => AddServices((IEnumerable<IService>)services);

		public T? GetService<T>(IEnumerable<IService> services) where T : IService => IElement.Get<T>(services);
	}

	public abstract class Service : IService, IHasRepository
	{
		public virtual string Name => "抽象サービス";

		public List<IRepository> Repositories { get; set; } = IRepository.EmptyList;

		public event EventHandler<IRepository>? RepositoryAdd;
		public void AddRepositories(IEnumerable<IRepository>? repositories) => IHasElements.AddRange(Repositories, repositories, this, RepositoryAdd);
		public void AddRepositories(params IRepository[] repositories) => AddRepositories((IEnumerable<IRepository>)repositories);
		public void AddRepository(IRepository? repository) => IHasElements.Add(Repositories, repository, this, RepositoryAdd);
		public T? GetRepository<T>(IEnumerable<IRepository> repositories) where T : IRepository => IRepository.Get<T>(repositories);

		protected Service(IEnumerable<IRepository>? repositories = null) => AddRepositories(repositories);
		protected Service(params IRepository[] repositories) : this((IEnumerable<IRepository>)repositories) { }

		public override string ToString() => Name;
	}

}
