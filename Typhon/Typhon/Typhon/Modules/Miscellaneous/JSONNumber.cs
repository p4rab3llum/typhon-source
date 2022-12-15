using System;
using System.Globalization;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000053 RID: 83
	public class JSONNumber : JSONNode
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000B6D3 File Offset: 0x000098D3
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.Number;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public override bool IsNumber
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public override JSONNode.Enumerator GetEnumerator()
		{
			return default(JSONNode.Enumerator);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000B6EE File Offset: 0x000098EE
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000B700 File Offset: 0x00009900
		public override string Value
		{
			get
			{
				return this.m_Data.ToString(CultureInfo.InvariantCulture);
			}
			set
			{
				double data;
				if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out data))
				{
					this.m_Data = data;
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000B728 File Offset: 0x00009928
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000B730 File Offset: 0x00009930
		public override double AsDouble
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000B739 File Offset: 0x00009939
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000B742 File Offset: 0x00009942
		public override long AsLong
		{
			get
			{
				return (long)this.m_Data;
			}
			set
			{
				this.m_Data = (double)value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B74C File Offset: 0x0000994C
		public JSONNumber(double aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B75B File Offset: 0x0000995B
		public JSONNumber(string aData)
		{
			this.Value = aData;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B76A File Offset: 0x0000996A
		public override JSONNode Clone()
		{
			return new JSONNumber(this.m_Data);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B777 File Offset: 0x00009977
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append(this.Value);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B788 File Offset: 0x00009988
		private static bool IsNumeric(object value)
		{
			return value is int || value is uint || value is float || value is double || value is decimal || value is long || value is ulong || value is short || value is ushort || value is sbyte || value is byte;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B7F0 File Offset: 0x000099F0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.Equals(obj))
			{
				return true;
			}
			JSONNumber jsonnumber = obj as JSONNumber;
			if (jsonnumber != null)
			{
				return this.m_Data == jsonnumber.m_Data;
			}
			return JSONNumber.IsNumeric(obj) && Convert.ToDouble(obj) == this.m_Data;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B844 File Offset: 0x00009A44
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x040000CF RID: 207
		private double m_Data;
	}
}
