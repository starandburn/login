namespace Nkk.IT.Trial.Programing.Login.Views
{
	public partial class WelcomeWindow : Form
    {
        public WelcomeWindow(string? userName = null)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(userName))
            {
                lblUser.Text = string.Empty;
            }
            else
            {
                lblUser.Text = lblUser.Text.Replace("{user}", userName);
            }
        }
    }
}
