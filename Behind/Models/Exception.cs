namespace Nkk.IT.Trial.Programing.Login.Models
{
	public class LoginException : Exception
	{
		public LoginException() : base() { }
		public LoginException(string? message) : base(message) { }
	}
	public class SuccessException : LoginException
	{
		public SuccessException() : base() { }
		public SuccessException(string? message) : base(message) { }
	}
	public class FailException : LoginException
	{
		public FailException() : base() { }
		public FailException(string? message) : base(message) { }
	}
}
