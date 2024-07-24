using System.Text;

namespace Nkk.IT.Trial.Programing.Login.Models
{
	public class CsvReader : StreamReader
	{
		private const char SeparatorChar = ',';
		private const char DoubleQuoteChar = '\"';

		private static readonly string[] CommentMarks = { "#", ";", "//" };

		// コンストラクター(すべて基底クラスに委譲)
		public CsvReader(Stream stream) : base(stream) { }
		public CsvReader(string path) : base(path) { }
		public CsvReader(Stream stream, bool detectEncodingFromByteOrderMarks) : base(stream, detectEncodingFromByteOrderMarks) { }
		public CsvReader(Stream stream, Encoding encoding) : base(stream, encoding) { }
		public CsvReader(string path, FileStreamOptions options) : base(path, options) { }
		public CsvReader(string path, bool detectEncodingFromByteOrderMarks) : base(path, detectEncodingFromByteOrderMarks) { }
		public CsvReader(string path, Encoding encoding) : base(path, encoding) { }
		public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: base(stream, encoding, detectEncodingFromByteOrderMarks) { }
		public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: base(path, encoding, detectEncodingFromByteOrderMarks) { }
		public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize) { }
		public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: base(path, encoding, detectEncodingFromByteOrderMarks, bufferSize) { }
		public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, FileStreamOptions options)
			: base(path, encoding, detectEncodingFromByteOrderMarks, options) { }
		public CsvReader(Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false)
			: base(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen) { }

		public static IEnumerable<string> ParseCsvRecord(string record)
		{
			var fields = new List<string>();
			var token = new StringBuilder();
			var insideQuotes = false;
			var escaped = false;

			foreach (var c in record.Trim())
			{
				if (insideQuotes)
				{
					if (c == DoubleQuoteChar)
					{
						if (escaped)
						{
							token.Append(c);
							escaped = false;
						}
						else
						{
							escaped = true;
						}
					}
					else
					{
						if (escaped)
						{
							insideQuotes = false;
							escaped = false;

							if (c == SeparatorChar)
							{
								fields.Add(token.ToString());
								token.Clear();
								continue;
							}
						}
						token.Append(c);
					}
				}
				else
				{

					if (token.Length == 0 && char.IsWhiteSpace(c))
					{
						continue;
					}

					if (c == DoubleQuoteChar)
					{
						token.Clear();
						insideQuotes = true;
					}
					else if (c == SeparatorChar)
					{
						fields.Add(token.ToString());
						token.Clear();
					}
					else
					{
						token.Append(c);
					}
				}
			}

			if (token.Length > 0)
			{
				fields.Add(token.ToString().TrimEnd());
			}

			return fields;
		}
		public string ReadCsvLine()
		{
			var record = new StringBuilder();
			var count = 0;
			while (!EndOfStream)
			{
				// 改行コードまでの1行分読み込み
				var line = ReadLine() ?? string.Empty;

				// レコード開始前の場合
				if (record.Length == 0)
				{
					// 始端の空白を除去する
					line = line.TrimStart();

					// 空行かコメント記号で始まっている場合
					if (string.IsNullOrWhiteSpace(line) || CommentMarks.Any(x => line.StartsWith(x)))
					{
						// 次の行まで読み飛ばす
						continue;
					}
				}

				// ここまでのダブルクォーテーションの数を計上する
				count += line.Count(x => x == DoubleQuoteChar);

				// 偶数であれば行内で閉じていると判断する
				if (count % 2 == 0)
				{
					// 終端の空白を除去してレコードに追加する
					record.Append(line.TrimEnd());

					// １件分のレコードが完成
					break;
				}

				// 奇数の場合次の行に続くのでそのままレコードに改行付きで追加する
				record.AppendLine(line);
			}

			// 現時点のレコード情報の両端空白を除去して返却する
			return record.ToString().Trim();
		}

		public IEnumerable<IEnumerable<string>> ReadCsvRecords()
		{	
			while (!EndOfStream)
			{
				yield return ParseCsvRecord(ReadCsvLine());
			}
		}

	}
}
