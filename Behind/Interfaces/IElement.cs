namespace Nkk.IT.Trial.Programing.Login.Interfaces
{
	public interface IElement
	{
		string Name { get; }

		class EmptyElement : IElement
		{
			public string Name => "空要素";
		}

		public static T GetInstance<T>() where T : IElement, new() => new T();
		public static List<T> GetEmptyList<T>() => Enumerable.Empty<T>().ToList();

		public static readonly List<IElement> EmptyList = GetEmptyList<IElement>();
		public static readonly IElement Empty = GetInstance<EmptyElement>();

		public static IEnumerable<T> Enumerate<T>(IEnumerable<IElement> elements) where T : IElement => elements.OfType<T>();
		public static IEnumerable<T> Enumerate<T>(params IElement[] elements) where T : IElement => Enumerate<T>((IEnumerable<IElement>)elements);

		public static T? Get<T>(IEnumerable<IElement> elements) where T : IElement => Enumerate<T>(elements).FirstOrDefault();
		public static T? Get<T>(params IElement[] element) where T : IElement => Get<T>((IEnumerable<IElement>)element);
	}

}
