using Nkk.IT.Trial.Programing.Login.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace Nkk.IT.Trial.Programing.Login.Services
{

	public interface ILogWritable
	{
		public static ILoggerService? GetLoggerService(object? obj)
		{
			switch (obj)
			{
				case IService svc:
					return svc as ILoggerService;
				case IEnumerable<IService> svcs:
					return IService.Get<ILoggerService>(svcs);
				case IHasServices hs:
					return hs?.GetService<ILoggerService>(hs.Services);
			}
			return null;
		}
		ILoggerService? Logger { get; }

		public static void WriteLog(ILoggerService? logger, object? obj) => logger?.Write(obj);
		public static void SeparatorLog(ILoggerService? logger, char? c = null, int? count = null) => logger?.Separator(c, count);
		public static void SuspendLog(ILoggerService? logger) => logger?.Suspend();
		public static void ResumeLog(ILoggerService? logger, bool fource = false) => logger?.Resume(fource);

		public void WriteLog(object? obj = null);
		public void SeparatorLog(char? c = null, int? count = null);
		public void SuspendLog();
		public void ResumeLog(bool fource = false);
	}

	public interface ILoggerService : IService
	{
		protected const char DefaultSeparatorChar = '-';
		protected const int DefaultSeparatorCharCount = 80;
		protected const string DefaultTimeStampFormat = "yyyy/MM/dd-hh:mm:ss";

		void Write(string? text);
		void Write(object? obj);
		void Separator(char? c = null, int? count = null);
		bool TimeStamp { get; set; }
		string TimeStampFormat { get; set; }

		void Suspend();
		void Resume(bool fource = false);
		bool IsSuspending { get; }

		public static string Quote(string? text) => $"'{text ?? string.Empty}'";
		public static string WQuote(string? text) => $"\"{text ?? string.Empty}\"";
	}

	public abstract class LoggerService : ILoggerService
	{
		public virtual string Name => "ログサービス";

		public virtual bool TimeStamp { get; set; }
		public virtual string TimeStampFormat { get; set; }
		public bool IsSuspending => _suspendCount > 0;


		private int _suspendCount = 0;

		public void Suspend() => _suspendCount++;
		public void Resume(bool fource = false)
		{
			if (fource || _suspendCount < 1)
			{
				_suspendCount = 0;
			}
			else
			{
				_suspendCount--;
			}
		}

		protected LoggerService(bool timeStamp = true, string? timeStampFormat = null)
		{
			TimeStamp = timeStamp;
			TimeStampFormat = timeStampFormat ?? ILoggerService.DefaultTimeStampFormat;
		}

		public abstract void WriteCore(string? text = null);

		public void Write(string? text)
		{
			if (IsSuspending) return;
			string ts;
			try
			{
				ts = DateTime.Now.ToString(TimeStampFormat);
			}
			catch
			{
				ts = DateTime.Now.ToShortTimeString();
			}
			WriteCore($"{(TimeStamp ? $"[{ts}]" : string.Empty)}{text?.Trim() ?? string.Empty}");
		}

		public void Write(object? obj)
		{
			Write(obj?.ToString());
		}
		public virtual void Separator(char? c = null, int? count = null)
		{
			if (IsSuspending) return;
			WriteCore(new string(c ?? ILoggerService.DefaultSeparatorChar, count ?? ILoggerService.DefaultSeparatorCharCount));
		}

	}

	public class DebugLogger : LoggerService
	{
		public override string Name => "デバッグログサービス";

		public DebugLogger(bool timeStamp = true, string? timeStampFormat = null) : base(timeStamp, timeStampFormat)
		{
		}

		public override void WriteCore(string? text = null)
		{
			Debug.WriteLine(text);
		}
	}

	public class FileLogger : LoggerService
	{
		public override string Name => "ファイルログサービス";
		public override string ToString() => $"{Name}({_filename})";

		private string _filename;

		public FileLogger(string filename, bool timeStamp = true, string? timeStampFormat = null) : base(timeStamp, timeStampFormat)
		{
			_filename = filename;
		}

		public override void WriteCore(string? text = null)
		{
			try
			{
				using var sw = new StreamWriter(_filename, true);
				sw.WriteLineAsync(text);
			}
			catch { }
		}
	}

	public class ListLogger : LoggerService
	{
		public override string Name => "リストログサービス";

		private bool _insertTop;
		public BindingList<string> List = new();

		public ListLogger(bool insertTop = true, bool timeStamp = true, string? timeStampFormat = null) : base(timeStamp, timeStampFormat)
		{
			_insertTop = insertTop;
		}

		public override void WriteCore(string? text = null)
		{
			text = text ?? string.Empty;
			if (_insertTop)
			{
				List.Insert(0, text);
			}
			else
			{
				List.Add(text);
			}
		}
	}

	public class MultiLogger : LoggerService
	{
		public override string Name => "複数ログサービス";
		public override string ToString() => $"{Name}({string.Join(", ", _loggers.Select(x => x.Name))})";

		private List<ILoggerService> _loggers;
		public MultiLogger()
		{
			base.TimeStamp = false;
			_loggers = new();
		}

		public MultiLogger(IEnumerable<ILoggerService> loggers)
		{
			base.TimeStamp = false;
			_loggers = loggers.DistinctBy(x => x.GetType()).ToList();
		}

		public MultiLogger(params ILoggerService[] loggers) : this((IEnumerable<ILoggerService>)loggers) { }

		public override bool TimeStamp
		{
			get => false;
			set => base.TimeStamp = value;
		}
		public override string TimeStampFormat
		{
			get => base.TimeStampFormat;
			set
			{
				base.TimeStampFormat = value;
				_loggers?.ForEach(x => x.TimeStampFormat = value);
			}
		}

		public void Add(ILoggerService logger)
		{
			if (_loggers.Any(x => x.GetType() == logger.GetType())) return;
			_loggers.Add(logger);
		}
		public void Remove(ILoggerService logger)
		{
			_loggers = _loggers.Where(x => x.GetType() == logger.GetType()).ToList();
		}

		public override void WriteCore(string? text = null)
		{
			_loggers?.ForEach(x => x.Write(text));
		}

		public override void Separator(char? c = '-', int? count = 80)
		{
			_loggers?.ForEach(x => x.Separator());
		}

		public T? GetLogger<T>() where T : ILoggerService
		{
			return _loggers.OfType<T>().FirstOrDefault();
		}
	}

}
