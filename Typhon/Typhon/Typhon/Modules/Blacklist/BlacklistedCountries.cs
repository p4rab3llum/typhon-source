using System;
using System.Net;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Modules.Blacklist
{
	// Token: 0x02000061 RID: 97
	internal class BlacklistedCountries
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public static bool IsBlacklisted()
		{
			JSONNode jsonnode = JSON.Parse(new WebClient().DownloadString("http://ipinfo.io/json"));
			string[] array = Config.BlacklistedCountries.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == jsonnode["country"])
				{
					return true;
				}
			}
			return false;
		}
	}
}
