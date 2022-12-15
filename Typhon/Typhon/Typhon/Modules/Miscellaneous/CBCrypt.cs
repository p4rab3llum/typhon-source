using System;
using System.Runtime.InteropServices;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x0200002E RID: 46
	internal class CBCrypt
	{
		// Token: 0x060000C4 RID: 196
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] string pszImplementation, uint dwFlags);

		// Token: 0x060000C5 RID: 197
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, uint flags);

		// Token: 0x060000C6 RID: 198
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptGetProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, uint flags);

		// Token: 0x060000C7 RID: 199
		[DllImport("bcrypt.dll", EntryPoint = "BCryptSetProperty")]
		internal static extern uint BCryptSetAlgorithmProperty(IntPtr hObject, [MarshalAs(UnmanagedType.LPWStr)] string pszProperty, byte[] pbInput, int cbInput, int dwFlags);

		// Token: 0x060000C8 RID: 200
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptImportKey(IntPtr hAlgorithm, IntPtr hImportKey, [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType, out IntPtr phKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, uint dwFlags);

		// Token: 0x060000C9 RID: 201
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptDestroyKey(IntPtr hKey);

		// Token: 0x060000CA RID: 202
		[DllImport("bcrypt.dll")]
		public static extern uint BCryptEncrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref CBCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, uint dwFlags);

		// Token: 0x060000CB RID: 203
		[DllImport("bcrypt.dll")]
		internal static extern uint BCryptDecrypt(IntPtr hKey, byte[] pbInput, int cbInput, ref CBCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, int cbIV, byte[] pbOutput, int cbOutput, ref int pcbResult, int dwFlags);

		// Token: 0x0400004D RID: 77
		public const uint ERROR_SUCCESS = 0U;

		// Token: 0x0400004E RID: 78
		public const uint BCRYPT_PAD_PSS = 8U;

		// Token: 0x0400004F RID: 79
		public const uint BCRYPT_PAD_OAEP = 4U;

		// Token: 0x04000050 RID: 80
		public static readonly byte[] BCRYPT_KEY_DATA_BLOB_MAGIC = BitConverter.GetBytes(1296188491);

		// Token: 0x04000051 RID: 81
		public static readonly string BCRYPT_OBJECT_LENGTH = "ObjectLength";

		// Token: 0x04000052 RID: 82
		public static readonly string BCRYPT_CHAIN_MODE_GCM = "ChainingModeGCM";

		// Token: 0x04000053 RID: 83
		public static readonly string BCRYPT_AUTH_TAG_LENGTH = "AuthTagLength";

		// Token: 0x04000054 RID: 84
		public static readonly string BCRYPT_CHAINING_MODE = "ChainingMode";

		// Token: 0x04000055 RID: 85
		public static readonly string BCRYPT_KEY_DATA_BLOB = "KeyDataBlob";

		// Token: 0x04000056 RID: 86
		public static readonly string BCRYPT_AES_ALGORITHM = "AES";

		// Token: 0x04000057 RID: 87
		public static readonly string MS_PRIMITIVE_PROVIDER = "Microsoft Primitive Provider";

		// Token: 0x04000058 RID: 88
		public static readonly int BCRYPT_AUTH_MODE_CHAIN_CALLS_FLAG = 1;

		// Token: 0x04000059 RID: 89
		public static readonly int BCRYPT_INIT_AUTH_MODE_INFO_VERSION = 1;

		// Token: 0x0400005A RID: 90
		public static readonly uint STATUS_AUTH_TAG_MISMATCH = 3221266434U;

		// Token: 0x0200002F RID: 47
		public struct BCRYPT_PSS_PADDING_INFO
		{
			// Token: 0x060000CE RID: 206 RVA: 0x00008184 File Offset: 0x00006384
			public BCRYPT_PSS_PADDING_INFO(string pszAlgId, int cbSalt)
			{
				this.pszAlgId = pszAlgId;
				this.cbSalt = cbSalt;
			}

			// Token: 0x0400005B RID: 91
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x0400005C RID: 92
			public int cbSalt;
		}

		// Token: 0x02000030 RID: 48
		public struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO : IDisposable
		{
			// Token: 0x060000CF RID: 207 RVA: 0x00008194 File Offset: 0x00006394
			public BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO(byte[] iv, byte[] aad, byte[] tag)
			{
				this = default(CBCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO);
				this.dwInfoVersion = CBCrypt.BCRYPT_INIT_AUTH_MODE_INFO_VERSION;
				this.cbSize = Marshal.SizeOf(typeof(CBCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));
				if (iv != null)
				{
					this.cbNonce = iv.Length;
					this.pbNonce = Marshal.AllocHGlobal(this.cbNonce);
					Marshal.Copy(iv, 0, this.pbNonce, this.cbNonce);
				}
				if (aad != null)
				{
					this.cbAuthData = aad.Length;
					this.pbAuthData = Marshal.AllocHGlobal(this.cbAuthData);
					Marshal.Copy(aad, 0, this.pbAuthData, this.cbAuthData);
				}
				if (tag != null)
				{
					this.cbTag = tag.Length;
					this.pbTag = Marshal.AllocHGlobal(this.cbTag);
					Marshal.Copy(tag, 0, this.pbTag, this.cbTag);
					this.cbMacContext = tag.Length;
					this.pbMacContext = Marshal.AllocHGlobal(this.cbMacContext);
				}
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00008274 File Offset: 0x00006474
			public void Dispose()
			{
				if (this.pbNonce != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbNonce);
				}
				if (this.pbTag != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbTag);
				}
				if (this.pbAuthData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbAuthData);
				}
				if (this.pbMacContext != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pbMacContext);
				}
			}

			// Token: 0x0400005D RID: 93
			public int cbSize;

			// Token: 0x0400005E RID: 94
			public int dwInfoVersion;

			// Token: 0x0400005F RID: 95
			public IntPtr pbNonce;

			// Token: 0x04000060 RID: 96
			public int cbNonce;

			// Token: 0x04000061 RID: 97
			public IntPtr pbAuthData;

			// Token: 0x04000062 RID: 98
			public int cbAuthData;

			// Token: 0x04000063 RID: 99
			public IntPtr pbTag;

			// Token: 0x04000064 RID: 100
			public int cbTag;

			// Token: 0x04000065 RID: 101
			public IntPtr pbMacContext;

			// Token: 0x04000066 RID: 102
			public int cbMacContext;

			// Token: 0x04000067 RID: 103
			public int cbAAD;

			// Token: 0x04000068 RID: 104
			public long cbData;

			// Token: 0x04000069 RID: 105
			public int dwFlags;
		}

		// Token: 0x02000031 RID: 49
		public struct BCRYPT_KEY_LENGTHS_STRUCT
		{
			// Token: 0x0400006A RID: 106
			public int dwMinLength;

			// Token: 0x0400006B RID: 107
			public int dwMaxLength;

			// Token: 0x0400006C RID: 108
			public int dwIncrement;
		}

		// Token: 0x02000032 RID: 50
		public struct BCRYPT_OAEP_PADDING_INFO
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x000082F5 File Offset: 0x000064F5
			public BCRYPT_OAEP_PADDING_INFO(string alg)
			{
				this.pszAlgId = alg;
				this.pbLabel = IntPtr.Zero;
				this.cbLabel = 0;
			}

			// Token: 0x0400006D RID: 109
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszAlgId;

			// Token: 0x0400006E RID: 110
			public IntPtr pbLabel;

			// Token: 0x0400006F RID: 111
			public int cbLabel;
		}
	}
}
