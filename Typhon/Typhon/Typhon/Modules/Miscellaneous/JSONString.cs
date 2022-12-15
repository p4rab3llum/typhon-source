using System;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000052 RID: 82
	public class JSONString : JSONNode
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000B607 File Offset: 0x00009807
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.String;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override bool IsString
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B60C File Offset: 0x0000980C
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000B622 File Offset: 0x00009822
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000B62A File Offset: 0x0000982A
		public override string Value
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

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B633 File Offset: 0x00009833
		public JSONString(string aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B642 File Offset: 0x00009842
		public override JSONNode Clone()
		{
			return new JSONString(this.m_Data);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B64F File Offset: 0x0000984F
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('"').Append(JSONNode.Escape(this.m_Data)).Append('"');
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B674 File Offset: 0x00009874
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				return true;
			}
			string text = obj as string;
			if (text != null)
			{
				return this.m_Data == text;
			}
			JSONString jsonstring = obj as JSONString;
			return jsonstring != null && this.m_Data == jsonstring.m_Data;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B6C6 File Offset: 0x000098C6
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x040000CE RID: 206
		private string m_Data;
	}
}
