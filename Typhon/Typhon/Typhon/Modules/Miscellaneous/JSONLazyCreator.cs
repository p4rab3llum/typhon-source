using System;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000056 RID: 86
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000B981 File Offset: 0x00009B81
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.None;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B984 File Offset: 0x00009B84
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B99A File Offset: 0x00009B9A
		public JSONLazyCreator(JSONNode aNode)
		{
			this.m_Node = aNode;
			this.m_Key = null;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
			this.m_Node = aNode;
			this.m_Key = aKey;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B9C6 File Offset: 0x00009BC6
		private T Set<T>(T aVal) where T : JSONNode
		{
			if (this.m_Key == null)
			{
				this.m_Node.Add(aVal);
			}
			else
			{
				this.m_Node.Add(this.m_Key, aVal);
			}
			this.m_Node = null;
			return aVal;
		}

		// Token: 0x1700005A RID: 90
		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.Set<JSONArray>(new JSONArray()).Add(value);
			}
		}

		// Token: 0x1700005B RID: 91
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				this.Set<JSONObject>(new JSONObject()).Add(aKey, value);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000BA32 File Offset: 0x00009C32
		public override void Add(JSONNode aItem)
		{
			this.Set<JSONArray>(new JSONArray()).Add(aItem);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000BA1E File Offset: 0x00009C1E
		public override void Add(string aKey, JSONNode aItem)
		{
			this.Set<JSONObject>(new JSONObject()).Add(aKey, aItem);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000BA45 File Offset: 0x00009C45
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return b == null || a == b;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000BA50 File Offset: 0x00009C50
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000BA45 File Offset: 0x00009C45
		public override bool Equals(object obj)
		{
			return obj == null || this == obj;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009F50 File Offset: 0x00008150
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000BA5C File Offset: 0x00009C5C
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000BA74 File Offset: 0x00009C74
		public override int AsInt
		{
			get
			{
				this.Set<JSONNumber>(new JSONNumber(0.0));
				return 0;
			}
			set
			{
				this.Set<JSONNumber>(new JSONNumber((double)value));
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000BA84 File Offset: 0x00009C84
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000BA74 File Offset: 0x00009C74
		public override float AsFloat
		{
			get
			{
				this.Set<JSONNumber>(new JSONNumber(0.0));
				return 0f;
			}
			set
			{
				this.Set<JSONNumber>(new JSONNumber((double)value));
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		public override double AsDouble
		{
			get
			{
				this.Set<JSONNumber>(new JSONNumber(0.0));
				return 0.0;
			}
			set
			{
				this.Set<JSONNumber>(new JSONNumber(value));
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000BACF File Offset: 0x00009CCF
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000BB02 File Offset: 0x00009D02
		public override long AsLong
		{
			get
			{
				if (JSONNode.longAsString)
				{
					this.Set<JSONString>(new JSONString("0"));
				}
				else
				{
					this.Set<JSONNumber>(new JSONNumber(0.0));
				}
				return 0L;
			}
			set
			{
				if (JSONNode.longAsString)
				{
					this.Set<JSONString>(new JSONString(value.ToString()));
					return;
				}
				this.Set<JSONNumber>(new JSONNumber((double)value));
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000BB2D File Offset: 0x00009D2D
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000BB3D File Offset: 0x00009D3D
		public override bool AsBool
		{
			get
			{
				this.Set<JSONBool>(new JSONBool(false));
				return false;
			}
			set
			{
				this.Set<JSONBool>(new JSONBool(value));
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public override JSONArray AsArray
		{
			get
			{
				return this.Set<JSONArray>(new JSONArray());
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000BB59 File Offset: 0x00009D59
		public override JSONObject AsObject
		{
			get
			{
				return this.Set<JSONObject>(new JSONObject());
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B961 File Offset: 0x00009B61
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append("null");
		}

		// Token: 0x040000D3 RID: 211
		private JSONNode m_Node;

		// Token: 0x040000D4 RID: 212
		private string m_Key;
	}
}
