using Nkk.IT.Trial.Programing.Login.Repositories;
using Nkk.IT.Trial.Programing.Login.Services;

namespace Nkk.IT.Trial.Programing.Login.Interfaces
{
	public interface IHasElements
	{
		public static void Add<T>(List<T> list, T? value, object? sender = null, EventHandler<T>? handler = null)
		{
			if (value is null) return;
			list.Add(value);
			handler?.Invoke(sender, value);
		}
		public static void AddRange<T>(List<T> list, IEnumerable<T>? values, object? sender = null, EventHandler<T>? handler = null)
		{
			var targets = values?.Where(x => x is not null) ?? Enumerable.Empty<T>();
			list?.AddRange(targets.ToList());
			foreach (var target in targets)
			{
				handler?.Invoke(sender, target);
			}
		}
	}

	public interface IHasServices : IHasElements
	{
		List<IService> Services { get; set; }

		void AddService(IService? service);
		void AddServices(IEnumerable<IService>? services);
		void AddServices(params IService[] services);
		T? GetService<T>(IEnumerable<IService> services) where T : IService;

		event EventHandler<IService>? ServiceAdd;
	}

	public interface IHasRepository : IHasElements
	{
		List<IRepository> Repositories { get; set; }

		void AddRepository(IRepository? repository);
		void AddRepositories(IEnumerable<IRepository> repositories);
		void AddRepositories(params IRepository[] repositories);
		T? GetRepository<T>(IEnumerable<IRepository> repositories) where T : IRepository;

		event EventHandler<IRepository>? RepositoryAdd;
	}

}
