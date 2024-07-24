using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Nkk.IT.Trial.Programing.Login.Models
{

    public enum SecurityLevel
	{
		[Description("なし")]
		None,
		[Description("シーザー暗号")]
		CaesarCrypt,
		[Description("AES暗号")]
		AesCrypt,
		[Description("MD5ハッシュ")]
		MD5Hash,
		[Description("MD5ハッシュ(ソルトあり)")]
		MD5HashWithSalt,
		[Description("Sha256ハッシュ")]
		Sha256Hash,
		[Description("Sha256ハッシュ(ソルトあり)")]
		Sha256HashWithSalt,
	}

	public enum CryptAlgorithm
	{
		[Description("平文")]
		Plain,
		[Description("シーザー暗号")]
		Caesar,
		[Description("AES暗号")]
		Aes,
	}

	public static class Security
	{
		public static SecurityLevel FromInt(int level)
		{
			var value = (SecurityLevel)level;
			if (Enum.IsDefined(value))
			{
				return value;
			}

			if (value > SecurityLevel.Sha256HashWithSalt)
			{
				return SecurityLevel.Sha256HashWithSalt;
			}

			return SecurityLevel.None;
		}

		public static string GetDescription(this SecurityLevel thisLevel, int key = 0)
		{
			var field = thisLevel.GetType().GetField(thisLevel.ToString());
			if (field is null) return thisLevel.ToString();
			var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
			var ret = attr?.Description ?? thisLevel.ToString();

			if (thisLevel.GetCryptAlgorithm() != CryptAlgorithm.Plain && key != 0)
			{
				ret += $"({key})";
			}
			return ret;
		}

		public static HashAlgorithm? GetHashAlgorithm(this SecurityLevel thisLevel)
		{
			switch (thisLevel)
			{
				case SecurityLevel.MD5Hash:
				case SecurityLevel.MD5HashWithSalt:
					return HashAlgorithm.Create(HashAlgorithmName.MD5.Name ?? string.Empty);
				case SecurityLevel.Sha256Hash:
				case SecurityLevel.Sha256HashWithSalt:
					return HashAlgorithm.Create(HashAlgorithmName.SHA256.Name ?? string.Empty);
			}
			return null;
		}

		public static CryptAlgorithm GetCryptAlgorithm(this SecurityLevel thisLevel)
		{
			switch (thisLevel)
			{
				case SecurityLevel.CaesarCrypt:
					return CryptAlgorithm.Caesar;
				case SecurityLevel.AesCrypt:
					return CryptAlgorithm.Aes;
			}
			return CryptAlgorithm.Plain;
		}

		public static bool NeedsSalt(this SecurityLevel thisLevel)
		{
			switch (thisLevel)
			{
				case SecurityLevel.MD5HashWithSalt:
				case SecurityLevel.Sha256HashWithSalt:
					return true;
				default:
					return false;
			}
		}

		public static bool ShouldEncrypt(this SecurityLevel thisLevel)
		{
			switch (thisLevel)
			{
				case SecurityLevel.CaesarCrypt:
				case SecurityLevel.AesCrypt:
					return true;
			}
			return false;
		}

		public static bool ShouldHash(this SecurityLevel thisLevel)
		{
			switch (thisLevel)
			{
				case SecurityLevel.MD5Hash:
				case SecurityLevel.MD5HashWithSalt:
				case SecurityLevel.Sha256Hash:
				case SecurityLevel.Sha256HashWithSalt:
					return true;
			}
			return false;
		}

		public static string ComputeHash(HashAlgorithm? algorithm, string text, string? salt = null)
		{
			if (algorithm is null) return text;

			var srcText = string.Concat(salt ?? string.Empty, text);
			var srcBytes = Encoding.UTF8.GetBytes(srcText);

			var bytes = algorithm.ComputeHash(srcBytes);

			return Convert.ToBase64String(bytes);
		}
		public static string ComputeHash(SecurityLevel level, string text, string? salt = null)
		{
			return ComputeHash(level.GetHashAlgorithm(), text, salt);
		}

		public static string Encrypt(CryptAlgorithm algorithm, string text, int key)
		{
			switch (algorithm)
			{
				case CryptAlgorithm.Caesar:
					return EncryptCaesar(text, key);
				case CryptAlgorithm.Aes:
					return EncryptAes(text, key);
			}
			return text;
		}
		public static string Encrypt(SecurityLevel level, string text, int key)
		{
			return Encrypt(level.GetCryptAlgorithm(), text, key);
		}

		public static string Decrypt(CryptAlgorithm algorithm, string text, int key)
		{
			switch (algorithm)
			{
				case CryptAlgorithm.Caesar:
					return DecryptCaesar(text, key);
				case CryptAlgorithm.Aes:
					return DecryptAes(text, key);
			}
			return text;
		}

		public static string Decrypt(SecurityLevel level, string text, int key)
		{
			return Decrypt(level.GetCryptAlgorithm(), text, key);
		}

		private const int AesKeyLength = 32;
		private const int AesIVLength = 16;

		private static string EncryptAes(string text, int key)
		{
			var keyBytes = BitConverter.GetBytes(key);
			Array.Resize(ref keyBytes, AesKeyLength);
			var IVBytes = new byte[AesIVLength];

			using var aes = Aes.Create();
			aes.KeySize = keyBytes.Length * 8;
			aes.Mode = CipherMode.ECB;
			aes.Key = keyBytes;
			aes.IV = IVBytes;

			using var ms = new MemoryStream();
			using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
			using (var sw = new StreamWriter(cs))
			{
				sw.Write(text);
			}
			var encrypted = ms.ToArray();

			var ret = BitConverter.ToString(encrypted);

			return Convert.ToBase64String(encrypted);
		}

		private static string DecryptAes(string text, int key)
		{
			var keyBytes = BitConverter.GetBytes(key);
			Array.Resize(ref keyBytes, AesKeyLength);
			var IVBytes = new byte[AesIVLength];

			var cipherBytes = Convert.FromBase64String(text);

			using var aes = Aes.Create();
			aes.KeySize = keyBytes.Length * 8;
			aes.Mode = CipherMode.ECB;
			aes.Key = keyBytes;
			aes.IV = IVBytes;

			using var ms = new MemoryStream(cipherBytes);
			using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
			using var sr = new StreamReader(cs);
			return sr.ReadToEnd();
		}


		private static string EncryptCaesar(string text, int key)
		{
			const int Start = 32;
			const int End = 126;
			const int Range = End - Start + 1;
			return new string(text.ToCharArray().Select(x => (x >= Start && x <= End) ? (char)(((x - Start + key) % Range + Range) % Range + Start) : x).ToArray());
		}
		private static string DecryptCaesar(string text, int key)
		{
			return EncryptCaesar(text, -key);
		}
	}


}
