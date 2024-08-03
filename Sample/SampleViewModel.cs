namespace Nkk.IT.Trial.Programing.Login.ViewModels
{
    public class SampleViewModel : LoginViewModel
    {
        protected override string Description => "動作サンプル";

        protected override void LoginProcess()
        {

            if (nguser(id)) ng(5);
            x = okpass(id);

            if (same(pass, x)) ok(1);
            ng(9);
        }

		protected override void WhenFailed()
        {
            clear(pass);
        }

		protected override void WhenSuccessed()
        {
            welcome(id);

            clear(pass);
            clear(id);
        }

		protected override void StartUp()
        {

        }
    }
}
