//#define FILELOG

using Nkk.IT.Trial.Programing.Login.Repositories;
using Nkk.IT.Trial.Programing.Login.Services;
using Nkk.IT.Trial.Programing.Login.ViewModels;
using static Nkk.IT.Trial.Programing.Login.Models.Settings;

namespace Nkk.IT.Trial.Programing.Login.Factiries
{
	public static class Factory
	{
		public static LoginViewModel GetLoginViewModel()
		{
			int mode = YourWorkshop.Mode;
			switch(mode)
			{
				case 1:
					return new SampleViewModel();
				case 99:
					return new CompleteViewModel();
				default:
					return new YourWorkshop();
			}
		}

		private static ILoggerService _logger;
		static Factory()
		{
			_logger = new MultiLogger(  // 複数のログ指定
				new ILoggerService[] {
					new DebugLogger(),                              // IDEの出力ウィンドウに表示(Debug.WriteLine)
					new ListLogger(),                               // リスト項目に追加(デバッグウィンドウにバインド)
				#if FILELOG
					new FileLogger(@"..\..\..\debuglog.txt"),       // ファイルに出力(プロジェクトのルートフォルダ)
				#endif
				});
		}
		public static ILoggerService GetLogger() => _logger;

		public static IUserAccountsRepository GetUserAccountsRepository()
			=> new UserAccountsRepository(GetFilePath(DataFolderPath, UserAccountsFilename), null, GetLogger());

		public static IMessageRepository GetLoginMessageRepository()
			=> new LoginMessageRepository(GetFilePath(DataFolderPath, MessageFilename), null, GetLogger());

	}
}
