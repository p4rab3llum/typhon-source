using System;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000055 RID: 85
	public class JSONNull : JSONNode
	{
		// Token: 0x060001FA RID: 506 RVA: 0x0000B90C File Offset: 0x00009B0C
		public static JSONNull CreateOrGet()
		{
			if (JSONNull.reuseSameInstance)
			{
				return JSONNull.m_StaticInstance;
			}
			return new JSONNull();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B920 File Offset: 0x00009B20
		private JSONNull()
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000B928 File Offset: 0x00009B28
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.NullValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override bool IsNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B92C File Offset: 0x00009B2C
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000B942 File Offset: 0x00009B42
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00009F47 File Offset: 0x00008147
		public override string Value
		{
			get
			{
				return "null";
			}
			set
			{
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009F50 File Offset: 0x00008150
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00009F47 File Offset: 0x00008147
		public override bool AsBool
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B949 File Offset: 0x00009B49
		public override JSONNode Clone()
		{
			return JSONNull.CreateOrGet();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B950 File Offset: 0x00009B50
		public override bool Equals(object obj)
		{
			return this == obj || obj is JSONNull;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009F50 File Offset: 0x00008150
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B961 File Offset: 0x00009B61
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append("null");
		}

		// Token: 0x040000D1 RID: 209
		private static JSONNull m_StaticInstance = new JSONNull();

		// Token: 0x040000D2 RID: 210
		public static bool reuseSameInstance = true;
	}
}
