using Nkk.IT.Trial.Programing.Login.Models;
using Nkk.IT.Trial.Programing.Login.ViewModels;
using System.ComponentModel;

namespace Nkk.IT.Trial.Programing.Login.Views
{
	public partial class DebugWindow : Form
    {
        private LoginViewModel? _viewModel = null;

        private DebugWindow()
        {
            InitializeComponent();
            ClearLog();
        }

        private void ClearLog()
        {
            lstLog.BeginUpdate();
            lstLog.Items.Clear();
            lstLog.EndUpdate();
        }

        public DebugWindow(LoginViewModel? viewModel = null) : this()
        {
            if (viewModel is null) return;
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnControllerPropertyChanged;

            SetBinding(_viewModel);
        }

        private void SetBinding(LoginViewModel viewModel)
        {
            BindLoginUsers(viewModel);
            BindVariable(lblVariableX, txtVariableX, viewModel, nameof(viewModel.x));
            BindVariable(lblVariableY, txtVariableY, viewModel, nameof(viewModel.y));
            BindVariable(lblVariableZ, txtVariableZ, viewModel, nameof(viewModel.z));

            var list = viewModel.LogList;
            if (list is not null)
            {
                lstLog.DataSource = list;
                list.ListChanged -= null;
                list.ListChanged += LogListChanged;
            }

        }

        private void LogListChanged(object? sender, ListChangedEventArgs e)
        {
            lstLog.SelectedIndex = lstLog.Items.Count > 0 ? 0 : -1;
        }

        private void BindVariable(Label label, TextBox textBox, LoginViewModel viewModel, string propertyName)
        {
            textBox.DataBindings.Add(nameof(textBox.Text), viewModel, propertyName);
            label.Text = $"変数 {textBox.DataBindings[nameof(textBox.Text)].BindingMemberInfo.BindingField}";
        }

        private void BindLoginUsers(LoginViewModel? viewModel)
        {
            dgvUsers.DataSource = null;
            if (viewModel is null) return;
            dgvUsers.DataSource = new BindingList<UserAccount>(viewModel.UserAccounts);
            SetupColumns(viewModel);
        }

        private void OnControllerPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_viewModel.UserAccounts):
                    BindLoginUsers(_viewModel);
                    break;
                case nameof(_viewModel.FailCountAvailable):
                case nameof(_viewModel.LoginStatusAvailable):
                    SetupColumns(_viewModel);
                    break;
            }
        }

        private void SetupColumns(LoginViewModel? viewModel)
        {
            if (viewModel is null) return;

            dgvUsers.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            DataGridViewColumn col = dgvUsers.Columns[nameof(UserAccount.Empty.UserId)];
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            col = dgvUsers.Columns[nameof(UserAccount.Empty.Password)];
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (viewModel.SecurityLevel.ShouldEncrypt())
            {
                col.HeaderText = "暗号化パスワード";
            }
            else if (viewModel.SecurityLevel.ShouldHash())
            {
                col.HeaderText = "パスワードハッシュ";
            }

            col = dgvUsers.Columns[nameof(UserAccount.Empty.PlainPassword)];
            col.Visible = false;// viewModel.SecurityLevel != SecurityLevel.None;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            col = dgvUsers.Columns[nameof(UserAccount.Empty.State)];
            col.Visible = viewModel.LoginStatusAvailable;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            col = dgvUsers.Columns[nameof(UserAccount.Empty.IsLocked)];
            col.Visible = false;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            col = dgvUsers.Columns[nameof(UserAccount.Empty.FailCount)];
            col.Visible = viewModel.FailCountAvailable;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            col = dgvUsers.Columns[nameof(UserAccount.Empty.IsEmpty)];
            col.Visible = false;

            col = dgvUsers.Columns[nameof(UserAccount.Empty.IsAny)];
            col.Visible = false;
        }

        private void OnUsersGridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_viewModel is null) return;

            var row = e.RowIndex;
            var col = e.ColumnIndex;

            if (row < 0 || row >= _viewModel.UserAccounts.Count) return;
            //if (col < 0 || col > 2) return;

            var item = _viewModel.UserAccounts.ElementAtOrDefault(row);
            if (item is null) return;
            switch (dgvUsers.Columns[col].Name)
            {
                case nameof(item.UserId):
                case nameof(item.UserName):
                    _viewModel.SetTextField(UserIdText.Instance, item.UserId, true);
                    break;
                case nameof(item.Password):
                case nameof(item.PlainPassword):
                    _viewModel.SetTextField(PasswordText.Instance, item.PlainPassword, true);
                    break;
                case nameof(item.FailCount):
                    item.FailCount++;
                    break;
            }

        }

    }

}
