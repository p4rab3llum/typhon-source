using System;
using System.Collections.Generic;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x0200004D RID: 77
	public class JSONArray : JSONNode
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000ACAB File Offset: 0x00008EAB
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000ACB3 File Offset: 0x00008EB3
		public override bool Inline
		{
			get
			{
				return this.inline;
			}
			set
			{
				this.inline = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Array;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override bool IsArray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000ACBF File Offset: 0x00008EBF
		public override JSONNode.Enumerator GetEnumerator()
		{
			return new JSONNode.Enumerator(this.m_List.GetEnumerator());
		}

		// Token: 0x1700003A RID: 58
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					return new JSONLazyCreator(this);
				}
				return this.m_List[aIndex];
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					this.m_List.Add(value);
					return;
				}
				this.m_List[aIndex] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				this.m_List.Add(value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000AD3E File Offset: 0x00008F3E
		public override void Add(string aKey, JSONNode aItem)
		{
			if (aItem == null)
			{
				aItem = JSONNull.CreateOrGet();
			}
			this.m_List.Add(aItem);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000AD69 File Offset: 0x00008F69
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_List.Count)
			{
				return null;
			}
			JSONNode result = this.m_List[aIndex];
			this.m_List.RemoveAt(aIndex);
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000AD97 File Offset: 0x00008F97
		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public override JSONNode Clone()
		{
			JSONArray jsonarray = new JSONArray();
			jsonarray.m_List.Capacity = this.m_List.Capacity;
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (jsonnode != null)
				{
					jsonarray.Add(jsonnode.Clone());
				}
				else
				{
					jsonarray.Add(null);
				}
			}
			return jsonarray;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000AE30 File Offset: 0x00009030
		public override IEnumerable<JSONNode> Children
		{
			get
			{
				foreach (JSONNode jsonnode in this.m_List)
				{
					yield return jsonnode;
				}
				List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000AE40 File Offset: 0x00009040
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('[');
			int count = this.m_List.Count;
			if (this.inline)
			{
				aMode = JSONTextMode.Compact;
			}
			for (int i = 0; i < count; i++)
			{
				if (i > 0)
				{
					aSB.Append(',');
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.AppendLine();
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.Append(' ', aIndent + aIndentInc);
				}
				this.m_List[i].WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
			}
			if (aMode == JSONTextMode.Indent)
			{
				aSB.AppendLine().Append(' ', aIndent);
			}
			aSB.Append(']');
		}

		// Token: 0x040000BF RID: 191
		private List<JSONNode> m_List = new List<JSONNode>();

		// Token: 0x040000C0 RID: 192
		private bool inline;
	}
}
