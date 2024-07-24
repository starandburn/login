using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nkk.IT.Trial.Programing.Login.Models
{
	public class UserAccount : INotifyPropertyChanged
	{
		public static UserAccount Empty => new UserAccount();

		public event PropertyChangedEventHandler? PropertyChanged;
		private bool OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			if (PropertyChanged is null) return false;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}
		private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
		{
			if (Equals(storage, value)) return false;
			storage = value;
			return OnPropertyChanged(propertyName);
		}

		[DisplayName("ユーザーID")]
		public string UserId { get; set; } = string.Empty;

		[DisplayName("ユーザー名")]
		public string UserName { get; set; } = string.Empty;

		[DisplayName("パスワード")]
		public string Password { get; set; } = string.Empty;

		[DisplayName("平文パスワード")]
		public string PlainPassword { get; set; } = string.Empty;

		[DisplayName("状態")]
		public string State => IsLocked ? "ロック" : "正常";

		private bool _isLocked = false;
		[DisplayName("ロック")]
		public bool IsLocked
		{
			get => _isLocked;
			set
			{
				if (!SetProperty(ref _isLocked, value)) return;
				OnPropertyChanged(nameof(State));
			}
		}

		private int _failuresCount = 0;
		[DisplayName("失敗")]
		public int FailCount
		{
			get => _failuresCount;
			set => SetProperty(ref _failuresCount, value);
		}

		public UserAccount(string? userId = null, string? password = null, string? userName = null)
		{
			UserId = userId ?? string.Empty;
			Password = (string.IsNullOrWhiteSpace(userId) ? null : password) ?? string.Empty;
			PlainPassword = Password;
			UserName = userName ?? string.Empty;
			ResetCount();
			Unlock();
		}

		public bool IsEmpty => string.IsNullOrWhiteSpace(UserId);
		public bool IsAny => !IsEmpty;

		public void CountUp()
		{
			FailCount++;
		}
		public void ResetCount()
		{
			FailCount = 0;
		}
		public void Lockout()
		{
			IsLocked = true;
		}
		public void Unlock()
		{
			IsLocked = false;
		}

		public override string ToString() => $"{nameof(UserId)}:\"{UserId}\", {nameof(Password)}:\"{Password}\", {nameof(IsLocked)}: {IsLocked}, {nameof(FailCount)}: {FailCount}";

		public static List<UserAccount> EmptyList => Enumerable.Empty<UserAccount>().ToList();

    }
}
