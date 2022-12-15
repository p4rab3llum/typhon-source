using System;
using System.Threading;

namespace Typhon.Modules.Implants
{
	// Token: 0x0200005D RID: 93
	internal class Delay
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000C89B File Offset: 0x0000AA9B
		public static void StartDelay()
		{
			Thread.Sleep(Convert.ToInt32(Config.StartDelay) * 1000);
		}
	}
}
