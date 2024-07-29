#define MODE1
/*****************************************************************************

	JIIC 学校法人朝日学園 日本国際工科専門学校  https://www.nkk.ac.jp
	IT専門課程【高度情報処理科/情報処理科/高度情報技術科4年制】体験学習

	システムエンジニア体験【実装編】
	とある業務システムのユーザーログイン機能をプログラミングしてみよう

	Copyright(c) 2024,Japan Institute of Cybernetics all rights reserved. 
/*****************************************************************************/
//
// ※始めから書かれている部分は消したり変更したりしないでください
//
namespace Nkk.IT.Trial.Programing.Login.ViewModels;
public class YourWorkshop : LoginViewModel
{
    protected override void StartUp()
    {
        // ①システム開始前の準備処理をここに書いてください
        ///////////////////////////////////////////////




        ///////////////////////////////////////////////
    }

    protected override void LoginProcess()
    {
        // ①ログイン処理をここに書いてください
        ///////////////////////////////////////////////




        ///////////////////////////////////////////////
    }

    protected override void WhenSuccessed()
    {
        // ③成功した後の処理をここに書いてください
        ///////////////////////////////////////////////




        ///////////////////////////////////////////////
    }

    protected override void WhenFailed()
    {
        // ④失敗した後の処理をここに書いてください
        ///////////////////////////////////////////////




        ///////////////////////////////////////////////
    }















    #region
    protected override string Description => "実習してみよう";

#if MODE1
    public const int Mode = 1;
#elif MODE99
    public const int Mode = 99;
#else
    public const int Mode = 0;
#endif

    #endregion
}
