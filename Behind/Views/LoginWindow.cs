using Nkk.IT.Trial.Programing.Login.Factiries;
using Nkk.IT.Trial.Programing.Login.Models;
using Nkk.IT.Trial.Programing.Login.Services;
using Nkk.IT.Trial.Programing.Login.ViewModels;
using System.ComponentModel;

namespace Nkk.IT.Trial.Programing.Login.Views;

public partial class LoginWindow : Form, IView<LoginViewModel>, ILogWritable
{
	#region �t�B�[���h

	// �r���[���f��
	private LoginViewModel? _viewModel = null;

	// �f�o�b�O�E�B���h�E
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

	#region �R���X�g���N�^�[

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

		// �f�[�^�o�C���f�B���O
		SetBinding(viewModel);
	}
	#endregion

	#region ���\�b�h
	// �C���^�[�t�F�[�X�̈ꕔ�𓮓I�ɏ���������
	private void IntializeInterface()
	{
		// ���쌠�\��(�N�x�Ƒg�D���𓮓I�Ɏ擾)
		lblCopyright.Text = lblCopyright.Text.Replace("{year}", DateTime.Now.Year.ToString()).Replace("{company}", Application.CompanyName);

		// �p�X���[�h�\���{�^���̏�����
		InitializeEyeButton();

	}

	// �p�X���[�h�\��(��)�{�^���̈ʒu�ƃT�C�Y������������
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

	// �R���g���[���ƃr���[���f���̃f�[�^�o�C���f�B���O��ݒ肷��
	private void SetBinding(LoginViewModel? viewModel)
	{
		ResetBinding();
		if (viewModel is null) return;

		// �t�H�[��
		// �\�����
		DataBindings.Add(nameof(Visible), viewModel, nameof(viewModel.ViewVisible));
		// �^�C�g���L���v�V����
		DataBindings.Add(nameof(Text), viewModel, nameof(viewModel.TitleText));

		// ���b�Z�[�W���x��
		// �\������
		lblMessage.DataBindings.Add(nameof(lblMessage.Text), viewModel, nameof(viewModel.MessageText));
		// �����F
		lblMessage.DataBindings.Add(nameof(lblMessage.ForeColor), viewModel, nameof(viewModel.MessageColor));

		// ���[�U�[ID��
		// ���̓e�L�X�g
		txtUserId.DataBindings.Add(nameof(txtUserId.Text), ViewModel, nameof(ViewModel.BindUserId));

		// �p�X���[�h��
		// ���̓e�L�X�g
		txtPassword.DataBindings.Add(nameof(txtPassword.Text), viewModel, nameof(viewModel.BindPassword));
		// �p�X���[�h����
		txtPassword.DataBindings.Add(nameof(txtPassword.PasswordChar), viewModel, nameof(viewModel.PasswordChar));

		// ��(�p�X���[�h�\���{�^��)
		// �����
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

	// �w��̃e�L�X�g�{�b�N�X�̎��̂��擾����
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

	#region �C�x���g�n���h���[

	// ��ʂ����[�h���ꂽ�Ƃ��̏���
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

	// ���O�C���{�^���������ꂽ�Ƃ��̏���
	private void OnLoginButtonClick(object sender, EventArgs e)
	{
		try
		{
			WriteLog($"���O�C���{�^����������܂����B");
			_viewModel?.StartLoginProcess(txtUserId.Text, txtPassword.Text);
		}
		catch (Exception ex)
		{
			MessageService.ShowException(ex);
			Close();
			Application.Exit();
		}
	}

	// �L�����Z���{�^���������ꂽ�Ƃ��̏���
	private void OnCancelButtonClick(object sender, EventArgs e)
	{
		try
		{
			WriteLog($"�L�����Z���{�^����������܂����B");

			// ����
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
			WriteLog($"�p�X���[�h�\���{�^����������܂����B");
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
			WriteLog($"�p�X���[�h�\���{�^����������܂����B");
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
			WriteLog($"�V�X�e�����I�����܂��B");
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
