using Nkk.IT.Trial.Programing.Login.Models;
using Nkk.IT.Trial.Programing.Login.Services;
using System.Text;

namespace Nkk.IT.Trial.Programing.Login.Repositories
{
	public interface IUserAccountsRepository : IRepository
	{
	}

	public class UserAccountsRepository : IUserAccountsRepository, IRepository<UserAccount, string, bool>, ILogWritable
	{
		public string Name => "ユーザーアカウント情報CSVリポジトリ";

		public static readonly IEnumerable<UserAccount> Empty = Enumerable.Empty<UserAccount>();

		public event EventHandler? Loaded = null;
		public IEnumerable<UserAccount> GetData() => _userAccounts;
		public IEnumerable<UserAccount> GetData(string userId) => GetData(userId, false);
		public IEnumerable<UserAccount> GetData(string userId, bool strict)
		{
			var compareId = strict ? userId : userId.Trim();
			return _userAccounts.Where(x => string.Equals(x.UserId, compareId, strict ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
		}
		public UserAccount? GetSingle(string userId, bool strict = false) => GetData(userId, strict).FirstOrDefault();

		private List<UserAccount> _userAccounts = UserAccount.EmptyList;

		public ILoggerService? Logger { get; private set; }


		public UserAccountsRepository(ILoggerService? logger = null)
		{
			Logger = logger;
		}
		public UserAccountsRepository(string? filename, Encoding? encoding = null, ILoggerService? logger = null) : this(logger)
		{
			if (Load(filename, encoding))
			{

			}
		}
		public UserAccountsRepository(string? filename, ILoggerService? logger = null) : this(filename, null, logger) { }


		// ユーザーアカウント情報をファイルから読み込んでデータベースとして保持する
		public bool Load(string? filename, Encoding? encoding = null)
		{
			try
			{
				using var cr = new CsvReader(filename ?? string.Empty, encoding ?? Encoding.Default);
				_userAccounts = cr.ReadCsvRecords().Select(x => new UserAccount(
						x.ElementAtOrDefault(0)?.Trim() ?? string.Empty,
						x.ElementAtOrDefault(1)?.Trim() ?? string.Empty,
						x.ElementAtOrDefault(2)?.Trim() ?? string.Empty
					)).Where(x => !string.IsNullOrWhiteSpace(x.UserId))?.ToList() ?? UserAccount.EmptyList;
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
				if (_userAccounts.Any())
				{
					WriteLog($"ユーザーアカウント情報を{_userAccounts.Count()}件読み込みました。");
					return true;
				}
				else
				{
					WriteLog($"ユーザーアカウント情報は0件です。");
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
	}
}
