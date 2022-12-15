using System;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000039 RID: 57
	internal class DataModels
	{
		// Token: 0x0200003A RID: 58
		public struct Password
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x060000EC RID: 236 RVA: 0x00008FAC File Offset: 0x000071AC
			// (set) Token: 0x060000ED RID: 237 RVA: 0x00008FB4 File Offset: 0x000071B4
			public string sUrl { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x060000EE RID: 238 RVA: 0x00008FBD File Offset: 0x000071BD
			// (set) Token: 0x060000EF RID: 239 RVA: 0x00008FC5 File Offset: 0x000071C5
			public string sUsername { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x060000F0 RID: 240 RVA: 0x00008FCE File Offset: 0x000071CE
			// (set) Token: 0x060000F1 RID: 241 RVA: 0x00008FD6 File Offset: 0x000071D6
			public string sPassword { get; set; }
		}

		// Token: 0x0200003B RID: 59
		internal struct Cookie
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x00008FDF File Offset: 0x000071DF
			// (set) Token: 0x060000F3 RID: 243 RVA: 0x00008FE7 File Offset: 0x000071E7
			public string sHostKey { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00008FF0 File Offset: 0x000071F0
			// (set) Token: 0x060000F5 RID: 245 RVA: 0x00008FF8 File Offset: 0x000071F8
			public string sName { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00009001 File Offset: 0x00007201
			// (set) Token: 0x060000F7 RID: 247 RVA: 0x00009009 File Offset: 0x00007209
			public string sPath { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00009012 File Offset: 0x00007212
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000901A File Offset: 0x0000721A
			public string sExpiresUtc { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000FA RID: 250 RVA: 0x00009023 File Offset: 0x00007223
			// (set) Token: 0x060000FB RID: 251 RVA: 0x0000902B File Offset: 0x0000722B
			public string sKey { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x060000FC RID: 252 RVA: 0x00009034 File Offset: 0x00007234
			// (set) Token: 0x060000FD RID: 253 RVA: 0x0000903C File Offset: 0x0000723C
			public string sValue { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000FE RID: 254 RVA: 0x00009045 File Offset: 0x00007245
			// (set) Token: 0x060000FF RID: 255 RVA: 0x0000904D File Offset: 0x0000724D
			public string sIsSecure { get; set; }
		}

		// Token: 0x0200003C RID: 60
		internal struct CreditCard
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000100 RID: 256 RVA: 0x00009056 File Offset: 0x00007256
			// (set) Token: 0x06000101 RID: 257 RVA: 0x0000905E File Offset: 0x0000725E
			public string sNumber { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00009067 File Offset: 0x00007267
			// (set) Token: 0x06000103 RID: 259 RVA: 0x0000906F File Offset: 0x0000726F
			public string sExpYear { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000104 RID: 260 RVA: 0x00009078 File Offset: 0x00007278
			// (set) Token: 0x06000105 RID: 261 RVA: 0x00009080 File Offset: 0x00007280
			public string sExpMonth { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000106 RID: 262 RVA: 0x00009089 File Offset: 0x00007289
			// (set) Token: 0x06000107 RID: 263 RVA: 0x00009091 File Offset: 0x00007291
			public string sName { get; set; }
		}

		// Token: 0x0200003D RID: 61
		internal struct AutoFill
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000108 RID: 264 RVA: 0x0000909A File Offset: 0x0000729A
			// (set) Token: 0x06000109 RID: 265 RVA: 0x000090A2 File Offset: 0x000072A2
			public string sName { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600010A RID: 266 RVA: 0x000090AB File Offset: 0x000072AB
			// (set) Token: 0x0600010B RID: 267 RVA: 0x000090B3 File Offset: 0x000072B3
			public string sValue { get; set; }
		}
	}
}
