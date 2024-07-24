namespace Nkk.IT.Trial.Programing.Login.Models
{
	internal static class Settings
	{
		public const string DataFolderPath = @"Data";
		public const string UserAccountsFilename = @"_usersaccounts.txt"; // ログイン情報データベース
		public const string MessageFilename = @"_message.txt"; // メッセージ文言データベース

		public static string GetFilePath(string folderName, string fileName) => Path.Combine(folderName, fileName);
		public static string GetFileTitle(string path) => Path.GetFileName(path);

	}
}
