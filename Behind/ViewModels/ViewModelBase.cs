using Nkk.IT.Trial.Programing.Login.Interfaces;
using Nkk.IT.Trial.Programing.Login.Repositories;
using Nkk.IT.Trial.Programing.Login.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nkk.IT.Trial.Programing.Login.ViewModels
{
	public interface IViewModel
	{
	}

    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged, IHasServices, IHasRepository
	{
        public List<IService> Services { get; set; } = IService.EmptyList;
        public List<IRepository> Repositories { get; set; } = IRepository.EmptyList;

        public T? GetService<T>() where T : IService => GetService<T>(Services);
		public T? GetService<T>(IEnumerable<IService> services) where T : IService => IService.Get<T>(services);

		public T? GetRepository<T>() where T : IRepository => GetRepository<T>(Repositories);
		public T? GetRepository<T>(IEnumerable<IRepository> repositories) where T : IRepository => IRepository.Get<T>(repositories);

		protected ViewModelBase(IEnumerable<IService>? services = null, IEnumerable<IRepository>? repositories = null)
		{
			AddServices(services);
			AddRepositories(repositories);
		}
		protected ViewModelBase(IEnumerable<IService>? services = null, params IRepository[] repositories) : this(services, (IEnumerable<IRepository>)repositories) { }
		protected ViewModelBase(IEnumerable<IRepository>? repositories = null, params IService[] services) : this(services, repositories) { }
		protected ViewModelBase(params IRepository[] repositories) : this(Enumerable.Empty<IService>(), (IEnumerable<IRepository>)repositories) { }
		protected ViewModelBase(params IService[] services) : this((IEnumerable<IService>)services) { }

        public void AddService(IService? service) => IHasElements.Add(Services, service, this, ServiceAdd);
        public void AddServices(IEnumerable<IService>? services) => IHasElements.AddRange(Services, services, this, ServiceAdd);
        public void AddServices(params IService[] services) => AddServices((IEnumerable<IService>)services);
        public void AddRepository(IRepository? repository) => IHasElements.Add(Repositories, repository, this, RepositoryAdd);
        public void AddRepositories(IEnumerable<IRepository>? repositories) => IHasElements.AddRange(Repositories, repositories, this, RepositoryAdd);
        public void AddRepositories(params IRepository[] repositories) => AddRepositories((IEnumerable<IRepository>)repositories);

		#region プロパティ通知用
		public event PropertyChangedEventHandler? PropertyChanged = null;
		public event EventHandler<IRepository>? RepositoryAdd = null;
        public event EventHandler<IService>? ServiceAdd;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		protected void SetProperty<T>(ref T storage, T value, IEnumerable<string> propertyNames)
		{
			if (Equals(storage, value)) return;
			storage = value;
			foreach (var name in propertyNames.Where(x => !string.IsNullOrWhiteSpace(x)))
			{
				OnPropertyChanged(name);
			}
		}
		protected void SetProperty<T>(ref T storage, T value, params string[] propertyNames)
		{
			SetProperty(ref storage, value, (IEnumerable<string>)propertyNames);
		}
		protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
		{
			SetProperty(ref storage, value, Enumerable.Repeat(propertyName ?? string.Empty, 1));
		}



		#endregion
	}
}
