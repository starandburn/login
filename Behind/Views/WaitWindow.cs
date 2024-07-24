namespace Nkk.IT.Trial.Programing.Login.Views
{
    public partial class WaitWindow : Form
    {
        public WaitWindow()
        {
            InitializeComponent();
            timWait.Interval = 1;
            timWait.Enabled = false;
        }

        public WaitWindow(int interval) : this()
        {
            timWait.Interval = Math.Max(1, interval);
            timWait.Enabled = false;
        }

        private void timWait_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WaitWindow_Load(object sender, EventArgs e)
        {
            timWait.Enabled = true;
        }

        private void pbProgress_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
