// できることをすべて実装した完全版
namespace Nkk.IT.Trial.Programing.Login.ViewModels
{
    public class CompleteViewModel : LoginViewModel
    {
        protected override string Description => "全機能搭載完全版";

        protected override void StartUp()
        {
            security(6);    // ソルト付きSHA256ハッシュでパスワードをハッシュ化する
            mask(on);       // パスワードマスクを使用する
            eye(on);        // 目ボタンを利用する
        }

        protected override void LoginProcess()
        {
            // ユーザーIDが入力されていなかったら失敗
            if (empty(id)) ng(6);

            // ユーザーIDのは前後の空白を除去して変数ｘへ入れる
            x = trim(id);

            // 失敗回数に応じた時間数分、強制的に待機画面を表示する
            wait(x, 500);

            // ユーザーIDの大文字小文字を無視したうえでユーザーが存在しなければ失敗
            if (nguser(x)) ng(5);

            // ユーザーIDがロックされていたら失敗
            if (locking(x)) ng(10);

            // パスワードをIDをソルトとしてハッシュ化して変数ｙへ入れる
            y = hash(pass, id);

            // データベースから正しいパスワード(ハッシュ)を取得する
            z = okpass(x);                     

            if (same(y, z, true)) ok(1);       // ２つのパスワードハッシュが正しければ成功

            ng(9);
        }

        protected override void WhenFailed()
        {
            clear(pass);
            if (count(id, 5)) lockout(id);
        }

        protected override void WhenSuccessed()
        {
            x = name(id);
            welcome(x);

            clear(pass);
            clear(id);
        }

    }
}
