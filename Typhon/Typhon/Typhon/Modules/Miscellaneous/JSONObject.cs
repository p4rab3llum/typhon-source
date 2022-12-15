using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x0200004F RID: 79
	public class JSONObject : JSONNode
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000B05B File Offset: 0x0000925B
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000B063 File Offset: 0x00009263
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000B06C File Offset: 0x0000926C
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Object;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override bool IsObject
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B06F File Offset: 0x0000926F
		public override JSONNode.Enumerator GetEnumerator()
		{
			return new JSONNode.Enumerator(this.m_Dict.GetEnumerator());
		}

		// Token: 0x17000043 RID: 67
		public override JSONNode this[string aKey]
		{
			get
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					return this.m_Dict[aKey];
				}
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				if (this.m_Dict.ContainsKey(aKey))
				{
					this.m_Dict[aKey] = value;
					return;
				}
				this.m_Dict.Add(aKey, value);
			}
		}

		// Token: 0x17000044 RID: 68
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return null;
				}
				return this.m_Dict.ElementAt(aIndex).Value;
			}
			set
			{
				if (value == null)
				{
					value = JSONNull.CreateOrGet();
				}
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return;
				}
				string key = this.m_Dict.ElementAt(aIndex).Key;
				this.m_Dict[key] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000B16A File Offset: 0x0000936A
		public override int Count
		{
			get
			{
				return this.m_Dict.Count;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B178 File Offset: 0x00009378
		public override void Add(string aKey, JSONNode aItem)
		{
			if (aItem == null)
			{
				aItem = JSONNull.CreateOrGet();
			}
			if (aKey == null)
			{
				this.m_Dict.Add(Guid.NewGuid().ToString(), aItem);
				return;
			}
			if (this.m_Dict.ContainsKey(aKey))
			{
				this.m_Dict[aKey] = aItem;
				return;
			}
			this.m_Dict.Add(aKey, aItem);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B1E1 File Offset: 0x000093E1
		public override JSONNode Remove(string aKey)
		{
			if (!this.m_Dict.ContainsKey(aKey))
			{
				return null;
			}
			JSONNode result = this.m_Dict[aKey];
			this.m_Dict.Remove(aKey);
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B20C File Offset: 0x0000940C
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_Dict.Count)
			{
				return null;
			}
			KeyValuePair<string, JSONNode> keyValuePair = this.m_Dict.ElementAt(aIndex);
			this.m_Dict.Remove(keyValuePair.Key);
			return keyValuePair.Value;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B254 File Offset: 0x00009454
		public override JSONNode Remove(JSONNode aNode)
		{
			JSONNode result;
			try
			{
				KeyValuePair<string, JSONNode> keyValuePair = (from k in this.m_Dict
				where k.Value == aNode
				select k).First<KeyValuePair<string, JSONNode>>();
				this.m_Dict.Remove(keyValuePair.Key);
				result = aNode;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000B2C0 File Offset: 0x000094C0
		public override JSONNode Clone()
		{
			JSONObject jsonobject = new JSONObject();
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				jsonobject.Add(keyValuePair.Key, keyValuePair.Value.Clone());
			}
			return jsonobject;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B32C File Offset: 0x0000952C
		public override bool HasKey(string aKey)
		{
			return this.m_Dict.ContainsKey(aKey);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B33C File Offset: 0x0000953C
		public override JSONNode GetValueOrDefault(string aKey, JSONNode aDefault)
		{
			JSONNode result;
			if (this.m_Dict.TryGetValue(aKey, out result))
			{
				return result;
			}
			return aDefault;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000B35C File Offset: 0x0000955C
		public override IEnumerable<JSONNode> Children
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
				{
					yield return keyValuePair.Value;
				}
				Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000B36C File Offset: 0x0000956C
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append('{');
			bool flag = true;
			if (this.inline)
			{
				aMode = JSONTextMode.Compact;
			}
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (!flag)
				{
					aSB.Append(',');
				}
				flag = false;
				if (aMode == JSONTextMode.Indent)
				{
					aSB.AppendLine();
				}
				if (aMode == JSONTextMode.Indent)
				{
					aSB.Append(' ', aIndent + aIndentInc);
				}
				aSB.Append('"').Append(JSONNode.Escape(keyValuePair.Key)).Append('"');
				if (aMode == JSONTextMode.Compact)
				{
					aSB.Append(':');
				}
				else
				{
					aSB.Append(" : ");
				}
				keyValuePair.Value.WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
			}
			if (aMode == JSONTextMode.Indent)
			{
				aSB.AppendLine().Append(' ', aIndent);
			}
			aSB.Append('}');
		}

		// Token: 0x040000C6 RID: 198
		private Dictionary<string, JSONNode> m_Dict = new Dictionary<string, JSONNode>();

		// Token: 0x040000C7 RID: 199
		private bool inline;
	}
}
