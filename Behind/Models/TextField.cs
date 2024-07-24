namespace Nkk.IT.Trial.Programing.Login.Models
{
	public class EnteredText
    {
        public virtual string Description => "入力欄";

        private string _text = string.Empty;
        public string? Text
        {
            get => _text ?? string.Empty;
            set => _text = value ?? string.Empty;
        }
        public EnteredText(string? value = null) => Text = value;
        public override string ToString() => Text ?? string.Empty;

        public bool IsEmpty => string.IsNullOrWhiteSpace(Text);
        public EnteredText IfEmpty(EnteredText text) => IsEmpty ? text : this;

        public static implicit operator string(EnteredText text) => text?.Text?.ToString() ?? string.Empty;
        public static implicit operator EnteredText(string text) => new EnteredText(text);
    }
    public class UserIdText : EnteredText
    {
        public static readonly UserIdText Instance = new();
        public override string Description => $"ユーザーID{base.Description}";
        public UserIdText(string? value = null) : base(value) { }
        public static implicit operator string(UserIdText text) => text?.Text?.ToString() ?? string.Empty;
        public static implicit operator UserIdText(string text) => new UserIdText(text);
    }
    public class PasswordText : EnteredText
    {
        public static readonly PasswordText Instance = new();
        public override string Description => $"パスワード{base.Description}";
        public PasswordText(string? value = null) : base(value) { }
        public static implicit operator string(PasswordText text) => text?.Text?.ToString() ?? string.Empty;
        public static implicit operator PasswordText(string text) => new PasswordText(text);
    }

}
