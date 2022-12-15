using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Typhon.Properties
{
	// Token: 0x02000029 RID: 41
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00002057 File Offset: 0x00000257
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000079E0 File Offset: 0x00005BE0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Typhon.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00007A0C File Offset: 0x00005C0C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00007A13 File Offset: 0x00005C13
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000047 RID: 71
		private static ResourceManager resourceMan;

		// Token: 0x04000048 RID: 72
		private static CultureInfo resourceCulture;
	}
}
