using Nkk.IT.Trial.Programing.Login.Repositories;

namespace Nkk.IT.Trial.Programing.Login.Services
{
	public class MessageService : Service, ILogWritable
	{
		public override string Name => "メッセージサービス";

		private string _title = string.Empty;

		public MessageService(IEnumerable<IRepository>? repositories = null, ILoggerService? logger = null) : base(repositories)
		{
			Logger = logger;
		}
		public MessageService(ILoggerService? logger = null) : this(null, logger) { }

		public string Title
		{
			get => string.IsNullOrWhiteSpace(_title) ? Application.ProductName : _title?.Trim() ?? string.Empty;
			set => _title = value?.Trim() ?? string.Empty;
		}

		public ILoggerService? Logger { get; private set; }

		public DialogResult Show(string? message, MessageBoxIcon icon = MessageBoxIcon.Information, ILoggerService? logger = null) => ShowDialog(message, _title, icon, logger);

		public static DialogResult ShowDialog(string? message, string? title = null, MessageBoxIcon icon = MessageBoxIcon.Information, ILoggerService? logger = null)
		{
			if (string.IsNullOrWhiteSpace(message)) return DialogResult.OK;

			var text = message?.Trim();

			logger?.Write($"メッセージ表示:\"{text}\"");
			return MessageBox.Show(message?.Trim(), title ?? Application.ProductName, MessageBoxButtons.OK, icon);
		}

		public static void ShowException(Exception ex)
		{
			ShowDialog($@"予期しないエラーが発生しました。
[OK]ボタンを押して実行を中止してください。
やり直しても出る場合は担当講師に相談してください。
------------------------------------------------------------
Exception:{ex.GetType()}
{ex.Message}"
			, null, MessageBoxIcon.Error);
		}

		public void WriteLog(object? obj = null) => ILogWritable.WriteLog(Logger, obj);
		public void SeparatorLog(char? c = null, int? count = null) => ILogWritable.SeparatorLog(Logger, c, count);
		public void SuspendLog() => ILogWritable.SuspendLog(Logger);
		public void ResumeLog(bool fource = false) => ILogWritable.ResumeLog(Logger, fource);
	}
}
