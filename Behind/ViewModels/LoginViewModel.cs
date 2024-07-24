using Nkk.IT.Trial.Programing.Login.Factiries;
using Nkk.IT.Trial.Programing.Login.Models;
using Nkk.IT.Trial.Programing.Login.Repositories;
using Nkk.IT.Trial.Programing.Login.Services;
using Nkk.IT.Trial.Programing.Login.Views;
using System.ComponentModel;
using System.Security.Cryptography;
namespace Nkk.IT.Trial.Programing.Login.ViewModels;
public abstract partial class LoginViewModel : ViewModelBase, ILogWritable
{
	#region 定数

	private const string WindowTitle = "ログイン画面";

	private const char NullChar = '\0'; // ヌル文字
	private const char PasswordMaskChar = '*'; // パスワードマスク文字

	// 各種プロパティのデフォルト値
	private const int DefaultFailCountLimit = 5;
	private const string DefaultMessageText = "システムのご利用にはログインが必要です。";
	private static readonly Color DefaultMessageColor = Color.Black;

	// 状態の制御
	public bool on = true;  // 有効化
	public bool off = false;// 無効化

	// 文字列比較方法
	public bool strict = false; // 厳密な比較
	public bool nocase = true;  // 英字大小無視

	#endregion

	#region フィールド

	// プロパティ用バッキングフィールド
	private List<UserAccount> _userAccounts = new();
	private string _workX = string.Empty;
	private string _workY = string.Empty;
	private string _workZ = string.Empty;
	private string _bindUserId = string.Empty;
	private string _bindPassword = string.Empty;
	private SecurityLevel _securityLevel = SecurityLevel.None;
	private int _cryptKey = 1;
	private bool _passwordMaskAvailable = false;
	private bool _eyeButtonAvailable = false;
	private bool _eyeButtonState = false;
	private bool _failCountAvailable = false;
	private bool _loginStatusAvailable = false;
	private string _messageText = DefaultMessageText;
	private Color _messageColor = DefaultMessageColor;
	private bool _viewVisible = true;
	private EnteredText? _activeText = null;
	#endregion

	private DebugWindow? _debugWindow = null;

	#region プロパティ

	protected virtual string Description => "基本形";

	// 実装側で使用できる変数(プロパティ)

	[EditorBrowsable(EditorBrowsableState.Never)]
	public string TitleText => $"{WindowTitle} - {Application.ProductName}【{Description}】";

	// 自由に使用できる作業変数
	public string x
	{
		get => _workX;
		set => SetProperty(ref _workX, value);
	}
	public string y
	{
		get => _workY;
		set => SetProperty(ref _workY, value);
	}
	public string z
	{
		get => _workZ;
		set => SetProperty(ref _workZ, value);
	}

	// 入力欄のテキストを参照する変数
	public UserIdText id => BindUserId;
	public PasswordText pass => BindPassword;

	// 目ボタン(パスワードマスク一時解除ボタン)を有効にするか
	public bool EyeButtonAvailable
	{
		get => _eyeButtonAvailable;
		// 値の変更時は自身と目ボタンの表示状態も更新する
		set => SetProperty(ref _eyeButtonAvailable, value, nameof(EyeButtonAvailable), nameof(EyeButtonVisible));
	}

	// パスワードマスクを有効にするか
	public bool PasswordMaskAvailable
	{
		get => _passwordMaskAvailable;
		// 値の変更時は自身と目ボタンの表示状態も更新する
		set => SetProperty(ref _passwordMaskAvailable, value, nameof(PasswordMaskAvailable), nameof(EyeButtonVisible));
	}

	// 目ボタンの押下状態(押下中:true / 解放中:false)
	public bool EyeButtonState
	{
		get => _eyeButtonState;
		// 値の変更時は自身とパスワードのマスク文字も更新する
		set => SetProperty(ref _eyeButtonState, value, nameof(EyeButtonState), nameof(PasswordChar));
	}

	// パスワードのセキュリティレベル
	//0 - None                  : なし
	//1 - CaesarCrypt           : シーザー暗号
	//2 - AesCrypt              : AES暗号
	//3 - MD5Hash               : MD5ハッシュ
	//4 - MD5HashWithSalt       : MD5ハッシュ(ソルトあり)
	//5 - Sha256Hash            : Sha256ハッシュ
	//6 - Sha256HashWithSalt    : Sha256ハッシュ(ソルトあり)
	public SecurityLevel SecurityLevel
	{
		get => _securityLevel;
		set => SetProperty(ref _securityLevel, value);
	}

	// 暗号化キー
	public int CryptKey
	{
		get => _cryptKey;
		set => SetProperty(ref _cryptKey, value);
	}

	// 失敗回数を有効にするかどうか
	public bool FailCountAvailable
	{
		get => _failCountAvailable;
		set => SetProperty(ref _failCountAvailable, value);
	}

	// ログインステータスを有効にするかどうか
	public bool LoginStatusAvailable
	{
		get => _loginStatusAvailable;
		set => SetProperty(ref _loginStatusAvailable, value);
	}

	public EnteredText? ActiveText
	{
		get => _activeText;
		set
		{
			_activeText = value;
			OnPropertyChanged(nameof(ActiveText));
		}
	}

	// メッセージ表示ラベルのテキスト
	public string MessageText
	{
		get => _messageText;
		set => SetProperty(ref _messageText, value);
	}
	public Color MessageColor
	{
		get => _messageColor;
		set => SetProperty(ref _messageColor, value);
	}

	// ユーザーID入力欄のテキスト
	public string BindUserId
	{
		get => _bindUserId;
		set => SetProperty(ref _bindUserId, value);
	}

	// パスワード入力欄のテキスト
	public string BindPassword
	{
		get => _bindPassword;
		set => SetProperty(ref _bindPassword, value);
	}

	// パスワード入力欄のマスク文字(マスク有効でかつ目ボタン押下中でない場合のみマスク文字を設定)
	public char PasswordChar => PasswordMaskAvailable & !EyeButtonState ? PasswordMaskChar : NullChar;

	// 目ボタンの表示状態(マスク有効でかつ目ボタンも有効な場合のみ表示)
	public bool EyeButtonVisible => PasswordMaskAvailable && EyeButtonAvailable;

	public bool ViewVisible
	{
		get => _viewVisible;
		set => SetProperty(ref _viewVisible, value);
	}

	// ログ出力機能の分離
	public ILoggerService? Logger => ILogWritable.GetLoggerService(this);
	public void WriteLog(object? obj = null) => ILogWritable.WriteLog(Logger, obj);
	public void SeparatorLog(char? c = null, int? count = null) => ILogWritable.SeparatorLog(Logger, c, count);
	public void SuspendLog() => ILogWritable.SuspendLog(Logger);
	public void ResumeLog(bool fource = false) => ILogWritable.ResumeLog(Logger, fource);
	public BindingList<string> LogList => (Logger as MultiLogger)?.GetLogger<ListLogger>()?.List ?? new();

	// ユーザーアカウントデータ一覧(デバッグ画面にバインドするためpublic)
	public List<UserAccount> UserAccounts
	{
		get => _userAccounts;
		set => SetProperty(ref _userAccounts, value);
	}


	#endregion

	#region コンストラクター

	protected LoginViewModel(IEnumerable<IService>? services = null, IEnumerable<IRepository>? repositories = null) : base(services, repositories)
	{
		foreach (var s in services ?? IService.EmptyList)
		{
			OnServiceAdd(this, s);
		}

		foreach (var r in repositories ?? IRepository.EmptyList)
		{
			OnRepositoryAdd(this, r);
		}

		RepositoryAdd += OnRepositoryAdd;
		ServiceAdd += OnServiceAdd;
	}

	private void OnServiceAdd(object? sender, IService service)
	{
		switch (service)
		{
			default:
				System.Diagnostics.Debug.WriteLine(service.ToString());
				WriteLog($"{service}が追加されました。");
				break;
		}
	}

	private void OnRepositoryAdd(object? sender, IRepository repository)
	{
		switch (repository)
		{
			case UserAccountsRepository uaRepo:
				WriteLog($"ユーザーアカウント情報リポジトリが追加されました。");
				UserAccounts = uaRepo.GetData()?.ToList() ?? UserAccount.EmptyList;
				break;
		}
	}

	protected LoginViewModel()
		: this(new IService[]
			{
				Factory.GetLogger(),
			}
			, new IRepository[]
			{
				Factory.GetUserAccountsRepository(),
				Factory.GetLoginMessageRepository(),
			}
		)
	{
		AddService(new MessageService(Repositories, GetService<ILoggerService>()));
	}

	protected LoginViewModel(IEnumerable<IService>? services = null, params IRepository[] repositories) : this(services, (IEnumerable<IRepository>)repositories) { }
	protected LoginViewModel(IEnumerable<IRepository>? repositories = null, params IService[] services) : this(services, repositories) { }
	protected LoginViewModel(params IRepository[] repositories) : this(Enumerable.Empty<IService>(), (IEnumerable<IRepository>)repositories) { }
	protected LoginViewModel(params IService[] services) : this((IEnumerable<IService>)services) { }

	#endregion

	#region 実習で使用する命令

	// ようこそ画面を表示する
	protected void welcome(string? userName = null) => ShowWelcome(userName ?? string.Empty);

	// メッセージを表示して処理を完了する
	// ログイン成功
	protected void ok(string? message = null) => Success(int.MinValue, message ?? string.Empty);
	protected void ok(int no) => Success(no, string.Empty);

	// ログイン失敗
	protected void ng(string? message = null) => Fail(int.MinValue, message ?? string.Empty);
	protected void ng(int no) => Fail(no, string.Empty);

	// 変数に値を代入する( = 使って代入してもいいけど余計な文法教えたくないときに)
	protected void setx(object? value) => SetValue(nameof(x), value);
	protected void sety(object? value) => SetValue(nameof(y), value);
	protected void setz(object? value) => SetValue(nameof(z), value);

	// 判定(ifと一緒に使う)
	// 文字が空文字かどうか
	protected bool empty(string? text) => IsEmpty(text);

	// ２つの文字が同じかどうか(strictをonにすると大文字小文字判定を厳格化)
	protected bool same(string? textA, string? textB, bool strict = false) => IsSameText(textA, textB, strict);

	// 文字列に特殊な処理を施したものを取得する
	// 前後の空白文字を除去する
	protected string trim(string? text = null) => GetTrimedText(text);
	// 小文字を大文字に変換する
	protected string upper(string? text = null)
	{
		var ret = text?.ToUpper() ?? string.Empty;
		WriteLog($"{ILoggerService.WQuote(text)}を大文字に変換した{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}
	// 大文字を小文字に変換する
	protected string lower(string? text = null)
	{
		var ret = text?.ToLower() ?? string.Empty;
		WriteLog($"{ILoggerService.WQuote(text)}を小文字に変換した{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}

	// 暗号化処理(セキュリティレベルに応じたアルゴリズムを使用)
	// 文字列を暗号化したものを取得する
	protected string encrypt(string text, int key = 1) => GetEncryptedText(text, key);
	// 文字列を復号化したものを取得する
	protected string decrypt(string text, int key = 1) => GetDecryptedText(text, key);

	// ハッシュ処理(セキュリティレベルに応じたアルゴリズム使用)
	// 文字列をハッシュ化したものを取得する(必要があればソルトを不可する)
	protected string hash(string text, string? salt = null) => GetHashCode(text, salt);

	// ユーザーの存在判定
	// 存在するユーザーかどうか
	protected bool okuser(string userId, bool strict = false) => IsUserAccount(userId, strict);
	// 存在しないユーザーかどうか
	protected bool nguser(string userId, bool strict = false) => IsBadUserAccount(userId, strict);

	// ユーザーIDに対応したユーザーアカウント情報の取得
	// 正しいパスワード
	protected string okpass(string userId, bool strict = false) => GettPassword(userId, strict);
	// ユーザー名
	protected string name(string userId, bool strict = false) => GettUserName(userId, strict);

	// ウィンドウ側入力欄操作
	// 入力欄から値を読み出す(最初からid, passに直接入ってるので使わなくてもよい)
	protected string read(EnteredText text) => ReadEnteredText(text);
	// 入力欄を消去する(必要あればフォーカスを当てる)
	protected void clear(EnteredText text, bool focus = true) => ClearTextField(text, focus);

	// 追加機能のON/OFF
	// パスワードマスク機能(*で隠す)
	protected void mask(bool state = true) => SetPasswordMaskEnabled(state);
	// パスワード表示(目)ボタン
	protected void eye(bool state = true) => SetEyeButtonVisible(state);
	// セキュリティレベル(on/off、 数字による指定、列挙型による指定)
	protected void security(bool state = true) => security(state ? 1 : 0);
	protected void security(int level, int key = 1) => security(Security.FromInt(level), key);
	protected void security(SecurityLevel level, int key = 1) => SetSecurity(level, key);

	protected void wait(string userId, int delayMilleseconds = 500)
	{
		var user = GetUserAccount(userId);
		var interval = delayMilleseconds * (user?.FailCount ?? 1);
		if (interval <= 0) return;
		using var waitWindow = new WaitWindow(interval);
		WriteLog($"プログレスウィンドウを表示して{interval / 1000.0}秒待機します。");
		waitWindow.ShowDialog();
	}

	protected bool count(string userId, int limit = DefaultFailCountLimit) => CountUp(userId, limit);
	protected void reset(string userId) => ResetCount(userId);
	protected void lockout(string userId) => Lockout(userId);
	protected void unlock(string userId) => unlock(userId);
	protected bool locking(string userId) => IsLocked(userId);

	protected void greet(string? message = null)
	{
		SetMessage(message ?? "Hello!");
	}

	#endregion

	//public event EventHandler<(EnteredText text, bool focus)>? TextChanged;

	// デバッグウィンドウを表示する
	private void ShowDebugWindow()
	{
		_debugWindow = new DebugWindow(this);
		_debugWindow.Show();
	}

	#region ビューからのイベントを委譲するメソッド

	// ログイン画面がロードされた
	public void OnViewLoad()
	{
		// デバッグウィンドウを表示する
		ShowDebugWindow();

		SeparatorLog();
		WriteLog($"システムの準備を行います。");

		//// メッセージ文言を読み込む
		//LoadMessage(IRepository.GetFilePath(Settings.DataFolderPath, Settings.MessageFilename));

		// システムの準備処理の実装を呼び出す
		StartUp();

		WriteLog($"ログイン画面が表示されました。");
	}

	// ログインボタンが押された
	public void StartLoginProcess(string userIdText, string passwordText)
	{
		try
		{
			SeparatorLog();
			WriteLog($"ログイン処理を開始します。(入力ID:{ILoggerService.WQuote(userIdText)}  入力パスワード:{ILoggerService.WQuote(passwordText)})");
			//WriteLog($"ユーザーID入力欄には{ILoggerService.WQuote(userIdText)}が入力されています。");
			//WriteLog($"パスワード入力欄には{ILoggerService.WQuote(passwordText)}が入力されています。");

			SetMessage("処理中です。しばらくお待ちください…。");
			WriteLog($"実装された処理を実行します。");
			LoginProcess();

			ResetMessage();
		}
		catch (SuccessException)
		{
			WriteLog($"成功したあとの処理を行います。");
			try { WhenSuccessed(); }
			catch { }
		}
		catch (FailException)
		{
			WriteLog($"失敗したあとの処理を行います。");
			try { WhenFailed(); }
			catch { }
		}
		catch
		{
			throw;
		}
	}



	#endregion

	private string GetTrimedText(string? text)
	{
		var ret = text?.Trim() ?? string.Empty;
		WriteLog($"{ILoggerService.WQuote(text)}の両端空白を除去した{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}
	private UserAccount? GetUserAccount(string userId, bool strict = false)
	{
		var ret = GetRepository<UserAccountsRepository>()?.GetSingle(userId, strict);
		if (ret is null)
		{
			WriteLog($"ユーザーID:{ILoggerService.Quote(userId)}のアカウント情報が取得できませんでした。");
		}
		else
		{
			WriteLog($"ユーザーID:{ILoggerService.Quote(ret.UserId)}のアカウント情報を取得しました。");
		}
		return ret;
	}
	private bool IsUserAccount(string userId, bool strict)
	{
		try
		{
			SuspendLog();
			var ret = GetUserAccount(userId, strict) is not null;
			WriteLog($"ユーザー{ILoggerService.Quote(userId)}{(ret ? "は存在します。" : "が存在しません。")}");
			return ret;
		}
		finally
		{
			ResumeLog();
		}
	}
	private bool IsBadUserAccount(string userId, bool strict) => !IsUserAccount(userId, strict);
	private string GettPassword(string userId, bool strict)
	{
		try
		{
			SuspendLog();
			var ret = GetUserAccount(userId, strict)?.Password ?? string.Empty;
			WriteLog($"ユーザー{ILoggerService.Quote(userId)}の正しいパスワード{ILoggerService.WQuote(ret)}を取得しました。");
			return ret;
		}
		finally
		{
			ResumeLog();
		}
	}
	private string GettUserName(string userId, bool strict)
	{
		try
		{
			SuspendLog();
			var user = GetUserAccount(userId, strict);
			var ret = user?.UserName ?? string.Empty;
			if (string.IsNullOrWhiteSpace(ret)) ret = user?.UserId ?? string.Empty;
			WriteLog($"ユーザー{ILoggerService.Quote(userId)}のユーザー名{ILoggerService.WQuote(ret)}を取得しました。");
			return ret;
		}
		finally
		{
			ResumeLog();
		}
	}

	private void Success(int no, string message)
	{
		WriteLog($"成功判定を行いました。");
		//message = Models.Message.GetMessage(no, message);
		message = GetRepository<LoginMessageRepository>()?.GetMessage(no, message) ?? message;

		if (string.IsNullOrWhiteSpace(message)) message = "成功しました。";
		SetMessage(message, Color.Blue);
		throw new SuccessException(message);
	}
	private void Fail(int no, string message)
	{
		WriteLog($"失敗判定を行いました。");
		//message = Models.Message.GetMessage(no, message);
		message = GetRepository<LoginMessageRepository>()?.GetMessage(no, message) ?? message;
		if (string.IsNullOrWhiteSpace(message)) message = "失敗しました。";
		SetMessage(message, Color.Red);
		throw new FailException(message);
	}

	public void SetMessage(string? message = null, Color? color = null)
	{
		MessageText = message?.Trim() ?? DefaultMessageText;
		MessageColor = color ?? DefaultMessageColor;
		WriteLog($"メッセージ「{MessageText}」を表示しました。");
	}
	public void ResetMessage()
	{
		SuspendLog();
		SetMessage(null, null);
		ResumeLog();
		WriteLog($"メッセージ欄を初期状態に戻しました。");
	}
	private bool GetTextProperty(EnteredText text, out string result)
	{
		if (text is UserIdText)
		{
			result = id;
			return true;
		}

		if (text is PasswordText)
		{
			result = pass;
			return true;
		}

		result = string.Empty;
		return false;
	}
	private string ReadEnteredText(EnteredText text)
	{
		if (!GetTextProperty(text, out var ret)) return string.Empty;
		WriteLog($"{text.Description}の値{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}
	private bool IsEmpty(string? text)
	{
		var ret = string.IsNullOrWhiteSpace(text);
		WriteLog($"文字列{ILoggerService.WQuote(text)}は空文字{(ret ? "です。" : "ではありません。")}");
		return ret;
	}
	private void SetEyeButtonVisible(bool state)
	{
		EyeButtonAvailable = state;
		WriteLog($"パスワード表示ボタンを{(EyeButtonAvailable ? "有効" : "無効")}にしました。");
	}
	private void SetPasswordMaskEnabled(bool state)
	{
		PasswordMaskAvailable = state;
		WriteLog($"パスワードマスクを{(PasswordMaskAvailable ? "有効" : "無効")}にしました。");
	}
	public void SetEyeButtonState(bool state) => EyeButtonState = state;
	public bool SetTextField(EnteredText text, string? newText = null, bool focus = true)
	{
		newText = newText ?? string.Empty;
		if (text is UserIdText)
		{
			BindUserId = newText;
		}
		else if (text is PasswordText)
		{
			BindPassword = newText;
		}
		else
		{
			return false;
		}

		if (focus)
		{
			ActiveText = text;
		}

		WriteLog($"{text.Description}の内容を{ILoggerService.WQuote(newText)}に変更しました。");
		return true;
	}
	private void ClearTextField(EnteredText text, bool focus)
	{
		try
		{
			SuspendLog();
			if (SetTextField(text, null, focus))
			{
				WriteLog($"{text.Description}の内容を消去しました。");
			}
		}
		finally
		{
			ResumeLog();
		}
	}
	private bool IsSameText(string? textA, string? textB, bool strict)
	{
		var ret = string.Equals(textA ?? string.Empty, textB ?? string.Empty, strict ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
		WriteLog($"文字列{ILoggerService.WQuote(textA)}と文字列{ILoggerService.WQuote(textB)}は{(ret ? "同じです。" : "違います")}");
		return ret;
	}
	private string GetEncryptedText(string text, int key)
	{
		var alg = SecurityLevel.GetCryptAlgorithm();
		if (alg == CryptAlgorithm.Plain) alg = CryptAlgorithm.Caesar;
		var ret = Security.Encrypt(alg, text, key);
		WriteLog($"平文{ILoggerService.WQuote(text)}を暗号化して暗号文{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}
	private string GetDecryptedText(string text, int key)
	{
		var alg = SecurityLevel.GetCryptAlgorithm();
		if (alg == CryptAlgorithm.Plain) alg = CryptAlgorithm.Caesar;
		var ret = Security.Decrypt(alg, text, key);
		WriteLog($"暗号文{ILoggerService.WQuote(text)}を復号化して平文{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}
	private string GetHashCode(string text, string? salt)
	{
		var alg = SecurityLevel.GetHashAlgorithm() ?? HashAlgorithm.Create(HashAlgorithmName.MD5.Name ?? string.Empty);
		if (alg is null) return text;
		var slt = SecurityLevel.NeedsSalt() ? salt?.Trim()?.ToLower() : null;
		var ret = Security.ComputeHash(alg, text, slt);
		WriteLog($"文字列{ILoggerService.WQuote(text)}を{(slt is null ? string.Empty : $"ソルト{ILoggerService.WQuote(slt)}")}ハッシュ化してハッシュ文字列{ILoggerService.WQuote(ret)}を取得しました。");
		return ret;
	}

	private void SetSecurity(SecurityLevel level, int key = 1)
	{
		SecurityLevel = level;
		CryptKey = key;

		var text = level.GetDescription(key);

		WriteLog($"パスワードのセキュリティを{text}に設定しました。");
		UpdatePasswords();
	}
	private void UpdatePasswords()
	{
		try
		{
			SuspendLog();
			if (SecurityLevel == SecurityLevel.None) return;
			foreach (var user in UserAccounts)
			{
				if (SecurityLevel.ShouldEncrypt())
				{
					user.Password = GetEncryptedText(user.PlainPassword, CryptKey);
				}
				else if (SecurityLevel.ShouldHash())
				{
					user.Password = GetHashCode(user.PlainPassword, user.UserId);
				}
			}
		}
		finally
		{
			ResumeLog();
		}
		WriteLog($"データ上のパスワード情報をセキュリティレベルに従い更新しました。");
	}

	private void SetValue(string variable, object? value)
	{
		var strValue = value?.ToString() ?? string.Empty;

		switch (variable)
		{
			case nameof(x):
				x = strValue;
				break;
			case nameof(y):
				y = strValue;
				break;
			case nameof(z):
				z = strValue;
				break;
			default:
				return;
		}
		WriteLog($"変数{variable}に値{ILoggerService.WQuote(strValue)}を設定しました。");
	}

	private bool CountUp(string userId, int limit)
	{
		var user = GetUserAccount(userId);
		if (user?.IsEmpty ?? true) return false;

		user.CountUp();
		FailCountAvailable = true;
		WriteLog($"ユーザー{ILoggerService.Quote(userId)}の失敗回数が{user.FailCount}に増加しました。");
		return user.FailCount > limit;
	}
	private void ResetCount(string userId)
	{
		var user = GetUserAccount(userId);
		if (user?.IsEmpty ?? true) return;
		user.ResetCount();
		WriteLog($"ユーザー{ILoggerService.Quote(userId)}の失敗回数をリセットしました。");
	}
	private void Lockout(string userId)
	{
		var user = GetUserAccount(userId);
		if (user?.IsEmpty ?? true) return;
		user.Lockout();
		LoginStatusAvailable = true;
		WriteLog($"ユーザー{ILoggerService.Quote(userId)}のアカウントをロックしました。");
	}
	private void Unlock(string userId)
	{
		var user = GetUserAccount(userId);
		if (user?.IsEmpty ?? true) return;
		user.Unlock();
		WriteLog($"ユーザー{ILoggerService.Quote(userId)}のロックを解除しました。");
	}
	private bool IsLocked(string userId)
	{
		var user = GetUserAccount(userId);
		if (user?.IsEmpty ?? true) return false;

		var ret = user.IsLocked;
		WriteLog($"ユーザー{ILoggerService.Quote(userId)}は{(ret ? "アカウントがロックされています。" : "ロックされていません。")}");
		return ret;
	}

	protected void ShowWelcome(string? userName = null)
	{
		WriteLog($"ユーザー{ILoggerService.Quote(userName)}のウエルカム画面を表示します。");

		ViewVisible = false;
		using var window = new WelcomeWindow(userName);
		var result = window.ShowDialog();
		ViewVisible = true;

		if (result == DialogResult.Cancel)
		{
			WriteLog($"アプリケーションを終了します。");
			Application.Exit();
		}
		ResetMessage();
	}

	protected abstract void StartUp();
	protected abstract void LoginProcess();
	protected abstract void WhenSuccessed();
	protected abstract void WhenFailed();

}
