using System;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000054 RID: 84
	public class JSONBool : JSONNode
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000B851 File Offset: 0x00009A51
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Boolean;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override bool IsBoolean
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B854 File Offset: 0x00009A54
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000B86A File Offset: 0x00009A6A
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000B878 File Offset: 0x00009A78
		public override string Value
		{
			get
			{
				return this.m_Data.ToString();
			}
			set
			{
				bool data;
				if (bool.TryParse(value, out data))
				{
					this.m_Data = data;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000B896 File Offset: 0x00009A96
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000B89E File Offset: 0x00009A9E
		public override bool AsBool
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		public JSONBool(bool aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B75B File Offset: 0x0000995B
		public JSONBool(string aData)
		{
			this.Value = aData;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		public override JSONNode Clone()
		{
			return new JSONBool(this.m_Data);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append(this.m_Data ? "true" : "false");
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public override bool Equals(object obj)
		{
			return obj != null && obj is bool && this.m_Data == (bool)obj;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B8FF File Offset: 0x00009AFF
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x040000D0 RID: 208
		private bool m_Data;
	}
}
