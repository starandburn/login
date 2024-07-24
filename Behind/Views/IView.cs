using Nkk.IT.Trial.Programing.Login.ViewModels;

namespace Nkk.IT.Trial.Programing.Login.Views
{
	public interface IView 
	{
	}
	public interface IView<T> : IView where T : IViewModel
	{
		T? ViewModel { get; set; }
		event EventHandler<T?>? ViewModelChanged;
	}
}
