using Nkk.IT.Trial.Programing.Login.Factiries;
using Nkk.IT.Trial.Programing.Login.Models;
using Nkk.IT.Trial.Programing.Login.Services;
using Nkk.IT.Trial.Programing.Login.ViewModels;
using System.ComponentModel;

namespace Nkk.IT.Trial.Programing.Login.Views;

public partial class LoginWindow : Form, IView<LoginViewModel>, ILogWritable
{
	#region フィールド

	// ビューモデル
	private LoginViewModel? _viewModel = null;

	// デバッグウィンドウ
	private DebugWindow? _debugWindow = null;

	public event EventHandler<LoginViewModel?>? ViewModelChanged;

	public LoginViewModel? ViewModel
	{
		get => _viewModel;
		set
		{
			_viewModel = value;
			ViewModelChanged?.Invoke(this, _viewModel);
		}
	}

	public ILoggerService? Logger => ViewModel?.Logger;
	#endregion

	#region コンストラクター

	public LoginWindow() : this(Factory.GetLoginViewModel()) { }

	public LoginWindow(LoginViewModel? viewModel)
	{
		InitializeComponent();
		IntializeInterface();
		InitializeViewModel(viewModel);
	}

	private void InitializeViewModel(LoginViewModel? viewModel)
	{
		if (viewModel is null) return;
		ViewModelChanged += OnViewModelChanged;
		ViewModel = viewModel;
	}

	private void ShowDebugWindow(LoginViewModel? viewModel)
	{
		_debugWindow?.Close();
		_debugWindow = null;
		_debugWindow = new DebugWindow(viewModel);
		_debugWindow.Show();
	}

	private void OnViewModelChanged(object? sender, LoginViewModel? viewModel)
	{
		if (viewModel is null) return;

		viewModel.PropertyChanged += OnViewModelPropertyChanged;

		// データバインディング
		SetBinding(viewModel);
	}
	#endregion

	#region メソッド
	// インターフェースの一部を動的に初期化する
	private void IntializeInterface()
	{
		// 著作権表示(年度と組織名を動的に取得)
		lblCopyright.Text = lblCopyright.Text.Replace("{year}", DateTime.Now.Year.ToString()).Replace("{company}", Application.CompanyName);

		// パスワード表示ボタンの初期化
		InitializeEyeButton();

	}

	// パスワード表示(目)ボタンの位置とサイズを初期化する
	private void InitializeEyeButton()
	{
		btnEye.Parent = txtPassword.Parent;
		btnEye.Height = txtPassword.Height;
		btnEye.Left = txtPassword.Right + 1;
		btnEye.Top = txtPassword.Top;
	}

	private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
			case nameof(ViewModel.ActiveText):
				GetTextBox(ViewModel?.ActiveText)?.Focus();
				break;
			default:
				break;
		}
	}

	// コントロールとビューモデルのデータバインディングを設定する
	private void SetBinding(LoginViewModel? viewModel)
	{
		ResetBinding();
		if (viewModel is null) return;

		// フォーム
		// 表示状態
		DataBindings.Add(nameof(Visible), viewModel, nameof(viewModel.ViewVisible));
		// タイトルキャプション
		DataBindings.Add(nameof(Text), viewModel, nameof(viewModel.TitleText));

		// メッセージラベル
		// 表示文言
		lblMessage.DataBindings.Add(nameof(lblMessage.Text), viewModel, nameof(viewModel.MessageText));
		// 文字色
		lblMessage.DataBindings.Add(nameof(lblMessage.ForeColor), viewModel, nameof(viewModel.MessageColor));

		// ユーザーID欄
		// 入力テキスト
		txtUserId.DataBindings.Add(nameof(txtUserId.Text), ViewModel, nameof(ViewModel.BindUserId));

		// パスワード欄
		// 入力テキスト
		txtPassword.DataBindings.Add(nameof(txtPassword.Text), viewModel, nameof(viewModel.BindPassword));
		// パスワード文字
		txtPassword.DataBindings.Add(nameof(txtPassword.PasswordChar), viewModel, nameof(viewModel.PasswordChar));

		// 目(パスワード表示ボタン)
		// 可視状態
		btnEye.DataBindings.Add(nameof(btnEye.Visible), viewModel, nameof(viewModel.EyeButtonVisible));
	}

	private void ResetBinding()
	{
		DataBindings.Clear();
		lblMessage.DataBindings.Clear();
		txtUserId.DataBindings.Clear();
		txtPassword.DataBindings.Clear();
		btnEye.DataBindings.Clear();
	}

	// 指定のテキストボックスの実体を取得する
	private TextBox? GetTextBox(EnteredText? text)
	{
		return text switch
		{
			UserIdText => txtUserId,
			PasswordText => txtPassword,
			_ => null,
		};
	}

	#endregion

	#region イベントハンドラー

	// 画面がロードされたときの処理
	private void OnLoad(object sender, EventArgs e)
	{
		try
		{
			ViewModel?.OnViewLoad();
			ShowDebugWindow(ViewModel);
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Close();
		}
	}

	// ログインボタンが押されたときの処理
	private void OnLoginButtonClick(object sender, EventArgs e)
	{
		try
		{
			WriteLog($"ログインボタンが押されました。");
			_viewModel?.StartLoginProcess(txtUserId.Text, txtPassword.Text);
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Close();
			Application.Exit();
		}
	}

	// キャンセルボタンが押されたときの処理
	private void OnCancelButtonClick(object sender, EventArgs e)
	{
		try
		{
			WriteLog($"キャンセルボタンが押されました。");

			// 閉じる
			this.Close();
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Close();
		}
	}
	private void OnEyeButtonMouseDown(object sender, MouseEventArgs e)
	{
		try
		{
			WriteLog($"パスワード表示ボタンが押されました。");
			_viewModel?.SetEyeButtonState(true);
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Close();
		}
	}
	private void OnEyeByttonMouseUp(object sender, MouseEventArgs e)
	{
		try
		{
			WriteLog($"パスワード表示ボタンが離されました。");
			_viewModel?.SetEyeButtonState(false);
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Close();
		}
	}
	private void OnClosed(object sender, FormClosedEventArgs e)
	{
		try
		{
			WriteLog($"システムを終了します。");
			_debugWindow?.Close();
			Application.Exit();
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Application.Exit();
		}
	}

	public void WriteLog(object? obj = null) => ILogWritable.WriteLog(Logger, obj);
	public void SeparatorLog(char? c = null, int? count = null) => ILogWritable.SeparatorLog(Logger, c, count);
	public void SuspendLog() => ILogWritable.SuspendLog(Logger);
	public void ResumeLog(bool fource = false) => ILogWritable.ResumeLog(Logger, fource);
	#endregion

}
