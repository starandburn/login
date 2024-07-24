using Nkk.IT.Trial.Programing.Login.Models;
using Nkk.IT.Trial.Programing.Login.Services;
using System.Text;

namespace Nkk.IT.Trial.Programing.Login.Repositories
{
	public interface IMessageRepository : IRepository
	{
	}

	public class LoginMessageRepository : IMessageRepository, IRepository<string, int>, ILogWritable
	{
		public string Name => "ログインメッセージCSVリポジトリ";

		public event EventHandler? Loaded = null;

		private Dictionary<int, string> _messages = new();
		public ILoggerService? Logger { get; private set; }
		public LoginMessageRepository(ILoggerService? logger = null)
		{
			Logger = logger;
		}
		public LoginMessageRepository(string? filename, Encoding? encoding = null, ILoggerService? logger = null) : this(logger)
		{
			if (Load(filename, encoding))
			{

			}
		}
		public LoginMessageRepository(string? filename, ILoggerService? logger = null) : this(filename, null, logger) { }


		// ユーザーアカウント情報をファイルから読み込んでデータベースとして保持する
		public bool Load(string? filename, Encoding? encoding = null)
		{
			try
			{
				using var cr = new CsvReader(filename ?? string.Empty, encoding ?? Encoding.Default);

				foreach(var msg in cr.ReadCsvRecords())
				{
					if (int.TryParse(msg.ElementAtOrDefault(0), out var no))
					{
						if (_messages.ContainsKey(no)) _messages.Remove(no);
						var text = msg.ElementAtOrDefault(1)?.Trim() ?? string.Empty;
						_messages.TryAdd(no, text);
					}
				}
			}
			catch (IOException)
			{
				return false;
			}
			catch
			{
				throw;
			}

			try
			{
				if (_messages.Any())
				{
					WriteLog($"ログインメッセージ情報を{_messages.Count()}件読み込みました。");
					return true;
				}
				else
				{
					WriteLog($"ログインメッセージ情報は0件です。");
					return false;
				}
			}
			finally
			{
				Loaded?.Invoke(this, new EventArgs());
			}
		}

		public void WriteLog(object? obj = null) => ILogWritable.WriteLog(Logger, obj);
		public void SeparatorLog(char? c = null, int? count = null) => ILogWritable.SeparatorLog(Logger, c, count);
		public void SuspendLog() => ILogWritable.SuspendLog(Logger);
		public void ResumeLog(bool fource = false) => ILogWritable.ResumeLog(Logger, fource);

		public IEnumerable<string> GetData(int no)
		{
			if (!_messages.TryGetValue(no, out var ret))
			{
				return Enumerable.Empty<string>();
			}
			return new[] { ret?.Trim() ?? string.Empty };
		}
		public IEnumerable<string> GetData() => _messages.Values;

		public string GetMessage(int no, string? defaultMessage = null) => (GetData(no).FirstOrDefault() ?? defaultMessage)?.Trim() ?? string.Empty;

	}
}
