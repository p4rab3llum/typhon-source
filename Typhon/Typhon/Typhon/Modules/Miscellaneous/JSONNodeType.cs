using System;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000043 RID: 67
	public enum JSONNodeType
	{
		// Token: 0x0400009C RID: 156
		Array = 1,
		// Token: 0x0400009D RID: 157
		Object,
		// Token: 0x0400009E RID: 158
		String,
		// Token: 0x0400009F RID: 159
		Number,
		// Token: 0x040000A0 RID: 160
		NullValue,
		// Token: 0x040000A1 RID: 161
		Boolean,
		// Token: 0x040000A2 RID: 162
		None,
		// Token: 0x040000A3 RID: 163
		Custom = 255
	}
}
