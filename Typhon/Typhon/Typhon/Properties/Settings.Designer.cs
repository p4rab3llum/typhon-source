using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Typhon.Properties
{
	// Token: 0x0200002A RID: 42
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.2.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00007A1B File Offset: 0x00005C1B
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000049 RID: 73
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
