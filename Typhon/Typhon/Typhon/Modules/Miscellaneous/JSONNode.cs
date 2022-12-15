using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Typhon.Modules.Miscellaneous
{
	// Token: 0x02000045 RID: 69
	public abstract class JSONNode
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600012B RID: 299
		public abstract JSONNodeType Tag { get; }

		// Token: 0x17000015 RID: 21
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000016 RID: 22
		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009F49 File Offset: 0x00008149
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00009F47 File Offset: 0x00008147
		public virtual string Value
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool IsNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool IsString
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool IsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool IsNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool IsObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00009F50 File Offset: 0x00008150
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00009F47 File Offset: 0x00008147
		public virtual bool Inline
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009F47 File Offset: 0x00008147
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009F53 File Offset: 0x00008153
		public virtual void Add(JSONNode aItem)
		{
			this.Add("", aItem);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009F44 File Offset: 0x00008144
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009F44 File Offset: 0x00008144
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009F61 File Offset: 0x00008161
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009F44 File Offset: 0x00008144
		public virtual JSONNode Clone()
		{
			return null;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00009F64 File Offset: 0x00008164
		public virtual IEnumerable<JSONNode> Children
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00009F6D File Offset: 0x0000816D
		public IEnumerable<JSONNode> DeepChildren
		{
			get
			{
				foreach (JSONNode jsonnode in this.Children)
				{
					foreach (JSONNode jsonnode2 in jsonnode.DeepChildren)
					{
						yield return jsonnode2;
					}
					IEnumerator<JSONNode> enumerator2 = null;
				}
				IEnumerator<JSONNode> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009F50 File Offset: 0x00008150
		public virtual bool HasKey(string aKey)
		{
			return false;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009F7D File Offset: 0x0000817D
		public virtual JSONNode GetValueOrDefault(string aKey, JSONNode aDefault)
		{
			return aDefault;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009F80 File Offset: 0x00008180
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteToStringBuilder(stringBuilder, 0, 0, JSONTextMode.Compact);
			return stringBuilder.ToString();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009FA4 File Offset: 0x000081A4
		public virtual string ToString(int aIndent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteToStringBuilder(stringBuilder, 0, aIndent, JSONTextMode.Indent);
			return stringBuilder.ToString();
		}

		// Token: 0x06000147 RID: 327
		internal abstract void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode);

		// Token: 0x06000148 RID: 328
		public abstract JSONNode.Enumerator GetEnumerator();

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009FC7 File Offset: 0x000081C7
		public IEnumerable<KeyValuePair<string, JSONNode>> Linq
		{
			get
			{
				return new JSONNode.LinqEnumerator(this);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00009FCF File Offset: 0x000081CF
		public JSONNode.KeyEnumerator Keys
		{
			get
			{
				return new JSONNode.KeyEnumerator(this.GetEnumerator());
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00009FDC File Offset: 0x000081DC
		public JSONNode.ValueEnumerator Values
		{
			get
			{
				return new JSONNode.ValueEnumerator(this.GetEnumerator());
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00009FEC File Offset: 0x000081EC
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000A027 File Offset: 0x00008227
		public virtual double AsDouble
		{
			get
			{
				double result = 0.0;
				if (double.TryParse(this.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
				{
					return result;
				}
				return 0.0;
			}
			set
			{
				this.Value = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000A03B File Offset: 0x0000823B
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000A044 File Offset: 0x00008244
		public virtual int AsInt
		{
			get
			{
				return (int)this.AsDouble;
			}
			set
			{
				this.AsDouble = (double)value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000A04E File Offset: 0x0000824E
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000A044 File Offset: 0x00008244
		public virtual float AsFloat
		{
			get
			{
				return (float)this.AsDouble;
			}
			set
			{
				this.AsDouble = (double)value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000A058 File Offset: 0x00008258
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000A086 File Offset: 0x00008286
		public virtual bool AsBool
		{
			get
			{
				bool result = false;
				if (bool.TryParse(this.Value, out result))
				{
					return result;
				}
				return !string.IsNullOrEmpty(this.Value);
			}
			set
			{
				this.Value = (value ? "true" : "false");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000A0A0 File Offset: 0x000082A0
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000A0C3 File Offset: 0x000082C3
		public virtual long AsLong
		{
			get
			{
				long result = 0L;
				if (long.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0L;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000A0D2 File Offset: 0x000082D2
		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000A0DA File Offset: 0x000082DA
		public virtual JSONObject AsObject
		{
			get
			{
				return this as JSONObject;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A0E2 File Offset: 0x000082E2
		public static implicit operator JSONNode(string s)
		{
			return new JSONString(s);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A0EA File Offset: 0x000082EA
		public static implicit operator string(JSONNode d)
		{
			if (!(d == null))
			{
				return d.Value;
			}
			return null;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A0FD File Offset: 0x000082FD
		public static implicit operator JSONNode(double n)
		{
			return new JSONNumber(n);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A105 File Offset: 0x00008305
		public static implicit operator double(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsDouble;
			}
			return 0.0;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A120 File Offset: 0x00008320
		public static implicit operator JSONNode(float n)
		{
			return new JSONNumber((double)n);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A129 File Offset: 0x00008329
		public static implicit operator float(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsFloat;
			}
			return 0f;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A120 File Offset: 0x00008320
		public static implicit operator JSONNode(int n)
		{
			return new JSONNumber((double)n);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A140 File Offset: 0x00008340
		public static implicit operator int(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsInt;
			}
			return 0;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A153 File Offset: 0x00008353
		public static implicit operator JSONNode(long n)
		{
			if (JSONNode.longAsString)
			{
				return new JSONString(n.ToString());
			}
			return new JSONNumber((double)n);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A170 File Offset: 0x00008370
		public static implicit operator long(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsLong;
			}
			return 0L;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A184 File Offset: 0x00008384
		public static implicit operator JSONNode(bool b)
		{
			return new JSONBool(b);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A18C File Offset: 0x0000838C
		public static implicit operator bool(JSONNode d)
		{
			return !(d == null) && d.AsBool;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A19F File Offset: 0x0000839F
		public static implicit operator JSONNode(KeyValuePair<string, JSONNode> aKeyValue)
		{
			return aKeyValue.Value;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public static bool operator ==(JSONNode a, object b)
		{
			if (a == b)
			{
				return true;
			}
			bool flag = a is JSONNull || a == null || a is JSONLazyCreator;
			bool flag2 = b is JSONNull || b == null || b is JSONLazyCreator;
			return (flag && flag2) || (!flag && a.Equals(b));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A1FE File Offset: 0x000083FE
		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A20A File Offset: 0x0000840A
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A210 File Offset: 0x00008410
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000A218 File Offset: 0x00008418
		internal static StringBuilder EscapeBuilder
		{
			get
			{
				if (JSONNode.m_EscapeBuilder == null)
				{
					JSONNode.m_EscapeBuilder = new StringBuilder();
				}
				return JSONNode.m_EscapeBuilder;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A230 File Offset: 0x00008430
		internal static string Escape(string aText)
		{
			StringBuilder escapeBuilder = JSONNode.EscapeBuilder;
			escapeBuilder.Length = 0;
			if (escapeBuilder.Capacity < aText.Length + aText.Length / 10)
			{
				escapeBuilder.Capacity = aText.Length + aText.Length / 10;
			}
			int i = 0;
			while (i < aText.Length)
			{
				char c = aText[i];
				switch (c)
				{
				case '\b':
					escapeBuilder.Append("\\b");
					break;
				case '\t':
					escapeBuilder.Append("\\t");
					break;
				case '\n':
					escapeBuilder.Append("\\n");
					break;
				case '\v':
					goto IL_E2;
				case '\f':
					escapeBuilder.Append("\\f");
					break;
				case '\r':
					escapeBuilder.Append("\\r");
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							goto IL_E2;
						}
						escapeBuilder.Append("\\\\");
					}
					else
					{
						escapeBuilder.Append("\\\"");
					}
					break;
				}
				IL_121:
				i++;
				continue;
				IL_E2:
				if (c < ' ' || (JSONNode.forceASCII && c > '\u007f'))
				{
					ushort num = (ushort)c;
					escapeBuilder.Append("\\u").Append(num.ToString("X4"));
					goto IL_121;
				}
				escapeBuilder.Append(c);
				goto IL_121;
			}
			string result = escapeBuilder.ToString();
			escapeBuilder.Length = 0;
			return result;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A380 File Offset: 0x00008580
		private static JSONNode ParseElement(string token, bool quoted)
		{
			if (quoted)
			{
				return token;
			}
			string a = token.ToLower();
			if (a == "false" || a == "true")
			{
				return a == "true";
			}
			if (a == "null")
			{
				return JSONNull.CreateOrGet();
			}
			double n;
			if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out n))
			{
				return n;
			}
			return token;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000A400 File Offset: 0x00008600
		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jsonnode = null;
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			string aKey = "";
			bool flag = false;
			bool flag2 = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				if (c <= '/')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
						case '\r':
							goto IL_3C7;
						case '\v':
						case '\f':
							goto IL_3B9;
						default:
							if (c != ' ')
							{
								goto IL_3B9;
							}
							break;
						}
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
						}
					}
					else if (c != '"')
					{
						if (c != ',')
						{
							if (c != '/')
							{
								goto IL_3B9;
							}
							if (JSONNode.allowLineComments && !flag && i + 1 < aJSON.Length && aJSON[i + 1] == '/')
							{
								while (++i < aJSON.Length && aJSON[i] != '\n')
								{
									if (aJSON[i] == '\r')
									{
										break;
									}
								}
							}
							else
							{
								stringBuilder.Append(aJSON[i]);
							}
						}
						else if (flag)
						{
							stringBuilder.Append(aJSON[i]);
						}
						else
						{
							if (stringBuilder.Length > 0 || flag2)
							{
								jsonnode.Add(aKey, JSONNode.ParseElement(stringBuilder.ToString(), flag2));
							}
							aKey = "";
							stringBuilder.Length = 0;
							flag2 = false;
						}
					}
					else
					{
						flag = !flag;
						flag2 = (flag2 || flag);
					}
				}
				else
				{
					if (c <= ']')
					{
						if (c != ':')
						{
							switch (c)
							{
							case '[':
								if (flag)
								{
									stringBuilder.Append(aJSON[i]);
									goto IL_3C7;
								}
								stack.Push(new JSONArray());
								if (jsonnode != null)
								{
									jsonnode.Add(aKey, stack.Peek());
								}
								aKey = "";
								stringBuilder.Length = 0;
								jsonnode = stack.Peek();
								goto IL_3C7;
							case '\\':
								i++;
								if (flag)
								{
									char c2 = aJSON[i];
									if (c2 <= 'f')
									{
										if (c2 == 'b')
										{
											stringBuilder.Append('\b');
											goto IL_3C7;
										}
										if (c2 == 'f')
										{
											stringBuilder.Append('\f');
											goto IL_3C7;
										}
									}
									else
									{
										if (c2 == 'n')
										{
											stringBuilder.Append('\n');
											goto IL_3C7;
										}
										switch (c2)
										{
										case 'r':
											stringBuilder.Append('\r');
											goto IL_3C7;
										case 't':
											stringBuilder.Append('\t');
											goto IL_3C7;
										case 'u':
										{
											string s = aJSON.Substring(i + 1, 4);
											stringBuilder.Append((char)int.Parse(s, NumberStyles.AllowHexSpecifier));
											i += 4;
											goto IL_3C7;
										}
										}
									}
									stringBuilder.Append(c2);
									goto IL_3C7;
								}
								goto IL_3C7;
							case ']':
								break;
							default:
								goto IL_3B9;
							}
						}
						else
						{
							if (flag)
							{
								stringBuilder.Append(aJSON[i]);
								goto IL_3C7;
							}
							aKey = stringBuilder.ToString();
							stringBuilder.Length = 0;
							flag2 = false;
							goto IL_3C7;
						}
					}
					else if (c != '{')
					{
						if (c != '}')
						{
							if (c != '﻿')
							{
								goto IL_3B9;
							}
							goto IL_3C7;
						}
					}
					else
					{
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
							goto IL_3C7;
						}
						stack.Push(new JSONObject());
						if (jsonnode != null)
						{
							jsonnode.Add(aKey, stack.Peek());
						}
						aKey = "";
						stringBuilder.Length = 0;
						jsonnode = stack.Peek();
						goto IL_3C7;
					}
					if (flag)
					{
						stringBuilder.Append(aJSON[i]);
					}
					else
					{
						if (stack.Count == 0)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						if (stringBuilder.Length > 0 || flag2)
						{
							jsonnode.Add(aKey, JSONNode.ParseElement(stringBuilder.ToString(), flag2));
						}
						flag2 = false;
						aKey = "";
						stringBuilder.Length = 0;
						if (stack.Count > 0)
						{
							jsonnode = stack.Peek();
						}
					}
				}
				IL_3C7:
				i++;
				continue;
				IL_3B9:
				stringBuilder.Append(aJSON[i]);
				goto IL_3C7;
			}
			if (flag)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			if (jsonnode == null)
			{
				return JSONNode.ParseElement(stringBuilder.ToString(), flag2);
			}
			return jsonnode;
		}

		// Token: 0x040000A7 RID: 167
		public static bool forceASCII = false;

		// Token: 0x040000A8 RID: 168
		public static bool longAsString = false;

		// Token: 0x040000A9 RID: 169
		public static bool allowLineComments = true;

		// Token: 0x040000AA RID: 170
		[ThreadStatic]
		private static StringBuilder m_EscapeBuilder;

		// Token: 0x02000046 RID: 70
		public struct Enumerator
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600016F RID: 367 RVA: 0x0000A81F File Offset: 0x00008A1F
			public bool IsValid
			{
				get
				{
					return this.type > JSONNode.Enumerator.Type.None;
				}
			}

			// Token: 0x06000170 RID: 368 RVA: 0x0000A82A File Offset: 0x00008A2A
			public Enumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				this.type = JSONNode.Enumerator.Type.Array;
				this.m_Object = default(Dictionary<string, JSONNode>.Enumerator);
				this.m_Array = aArrayEnum;
			}

			// Token: 0x06000171 RID: 369 RVA: 0x0000A846 File Offset: 0x00008A46
			public Enumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				this.type = JSONNode.Enumerator.Type.Object;
				this.m_Object = aDictEnum;
				this.m_Array = default(List<JSONNode>.Enumerator);
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000172 RID: 370 RVA: 0x0000A864 File Offset: 0x00008A64
			public KeyValuePair<string, JSONNode> Current
			{
				get
				{
					if (this.type == JSONNode.Enumerator.Type.Array)
					{
						return new KeyValuePair<string, JSONNode>(string.Empty, this.m_Array.Current);
					}
					if (this.type == JSONNode.Enumerator.Type.Object)
					{
						return this.m_Object.Current;
					}
					return new KeyValuePair<string, JSONNode>(string.Empty, null);
				}
			}

			// Token: 0x06000173 RID: 371 RVA: 0x0000A8B0 File Offset: 0x00008AB0
			public bool MoveNext()
			{
				if (this.type == JSONNode.Enumerator.Type.Array)
				{
					return this.m_Array.MoveNext();
				}
				return this.type == JSONNode.Enumerator.Type.Object && this.m_Object.MoveNext();
			}

			// Token: 0x040000AB RID: 171
			private JSONNode.Enumerator.Type type;

			// Token: 0x040000AC RID: 172
			private Dictionary<string, JSONNode>.Enumerator m_Object;

			// Token: 0x040000AD RID: 173
			private List<JSONNode>.Enumerator m_Array;

			// Token: 0x02000047 RID: 71
			private enum Type
			{
				// Token: 0x040000AF RID: 175
				None,
				// Token: 0x040000B0 RID: 176
				Array,
				// Token: 0x040000B1 RID: 177
				Object
			}
		}

		// Token: 0x02000048 RID: 72
		public struct ValueEnumerator
		{
			// Token: 0x06000174 RID: 372 RVA: 0x0000A8DD File Offset: 0x00008ADD
			public ValueEnumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				this = new JSONNode.ValueEnumerator(new JSONNode.Enumerator(aArrayEnum));
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000A8EB File Offset: 0x00008AEB
			public ValueEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				this = new JSONNode.ValueEnumerator(new JSONNode.Enumerator(aDictEnum));
			}

			// Token: 0x06000176 RID: 374 RVA: 0x0000A8F9 File Offset: 0x00008AF9
			public ValueEnumerator(JSONNode.Enumerator aEnumerator)
			{
				this.m_Enumerator = aEnumerator;
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000177 RID: 375 RVA: 0x0000A904 File Offset: 0x00008B04
			public JSONNode Current
			{
				get
				{
					KeyValuePair<string, JSONNode> keyValuePair = this.m_Enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x06000178 RID: 376 RVA: 0x0000A924 File Offset: 0x00008B24
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000179 RID: 377 RVA: 0x0000A931 File Offset: 0x00008B31
			public JSONNode.ValueEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040000B2 RID: 178
			private JSONNode.Enumerator m_Enumerator;
		}

		// Token: 0x02000049 RID: 73
		public struct KeyEnumerator
		{
			// Token: 0x0600017A RID: 378 RVA: 0x0000A939 File Offset: 0x00008B39
			public KeyEnumerator(List<JSONNode>.Enumerator aArrayEnum)
			{
				this = new JSONNode.KeyEnumerator(new JSONNode.Enumerator(aArrayEnum));
			}

			// Token: 0x0600017B RID: 379 RVA: 0x0000A947 File Offset: 0x00008B47
			public KeyEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
			{
				this = new JSONNode.KeyEnumerator(new JSONNode.Enumerator(aDictEnum));
			}

			// Token: 0x0600017C RID: 380 RVA: 0x0000A955 File Offset: 0x00008B55
			public KeyEnumerator(JSONNode.Enumerator aEnumerator)
			{
				this.m_Enumerator = aEnumerator;
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600017D RID: 381 RVA: 0x0000A960 File Offset: 0x00008B60
			public string Current
			{
				get
				{
					KeyValuePair<string, JSONNode> keyValuePair = this.m_Enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x0600017E RID: 382 RVA: 0x0000A980 File Offset: 0x00008B80
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600017F RID: 383 RVA: 0x0000A98D File Offset: 0x00008B8D
			public JSONNode.KeyEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040000B3 RID: 179
			private JSONNode.Enumerator m_Enumerator;
		}

		// Token: 0x0200004A RID: 74
		public class LinqEnumerator : IEnumerator<KeyValuePair<string, JSONNode>>, IDisposable, IEnumerator, IEnumerable<KeyValuePair<string, JSONNode>>, IEnumerable
		{
			// Token: 0x06000180 RID: 384 RVA: 0x0000A995 File Offset: 0x00008B95
			internal LinqEnumerator(JSONNode aNode)
			{
				this.m_Node = aNode;
				if (this.m_Node != null)
				{
					this.m_Enumerator = this.m_Node.GetEnumerator();
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000181 RID: 385 RVA: 0x0000A9C3 File Offset: 0x00008BC3
			public KeyValuePair<string, JSONNode> Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A9D0 File Offset: 0x00008BD0
			object IEnumerator.Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x06000183 RID: 387 RVA: 0x0000A9E2 File Offset: 0x00008BE2
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000184 RID: 388 RVA: 0x0000A9EF File Offset: 0x00008BEF
			public void Dispose()
			{
				this.m_Node = null;
				this.m_Enumerator = default(JSONNode.Enumerator);
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000AA04 File Offset: 0x00008C04
			public IEnumerator<KeyValuePair<string, JSONNode>> GetEnumerator()
			{
				return new JSONNode.LinqEnumerator(this.m_Node);
			}

			// Token: 0x06000186 RID: 390 RVA: 0x0000AA11 File Offset: 0x00008C11
			public void Reset()
			{
				if (this.m_Node != null)
				{
					this.m_Enumerator = this.m_Node.GetEnumerator();
				}
			}

			// Token: 0x06000187 RID: 391 RVA: 0x0000AA04 File Offset: 0x00008C04
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new JSONNode.LinqEnumerator(this.m_Node);
			}

			// Token: 0x040000B4 RID: 180
			private JSONNode m_Node;

			// Token: 0x040000B5 RID: 181
			private JSONNode.Enumerator m_Enumerator;
		}
	}
}
