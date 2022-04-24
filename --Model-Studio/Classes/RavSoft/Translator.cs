using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Net.Http;

namespace RavSoft.GoogleTranslator
{
	public class Translator
	{
		public static IEnumerable<string> Languages
		{
			get
			{
				smethod_1();
				IEnumerable<string> keys = dictionary_0.Keys;
				if (func_0 == null)
				{
					func_0 = new Func<string, string>(smethod_2);
				}
				return keys.OrderBy(func_0);
			}
		}

		private static Func<string, string> func_0;

		public TimeSpan TranslationTime;

		public string TranslationSpeechUrl = "";

		public Exception Error;

		public string Translate(string sourceText, string sourceLanguage, string targetLanguage)
		{
			this.Error = null;
			this.TranslationSpeechUrl = null;
			this.TranslationTime = TimeSpan.Zero;
			DateTime now = DateTime.Now;
			string text = string.Empty;
			try
			{
				string address = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", smethod_0(sourceLanguage), smethod_0(targetLanguage), WebUtility.UrlEncode(sourceText));
				string tempFileName = Path.GetTempFileName();
				using (WebClient webClient = new WebClient())
				{
					webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
					webClient.DownloadFile(address, tempFileName);
				}
				if (File.Exists(tempFileName))
				{
					string text2 = File.ReadAllText(tempFileName);
					int num = text2.IndexOf(string.Format(",,\"{0}\"", smethod_0(sourceLanguage)));
					if (num == -1)
					{
						int num2 = text2.IndexOf('"');
						if (num2 != -1)
						{
							int num3 = text2.IndexOf('"', num2 + 1);
							if (num3 != -1)
							{
								text = text2.Substring(num2 + 1, num3 - num2 - 1);
							}
						}
					}
					else
					{
						text2 = text2.Substring(0, num);
						text2 = text2.Replace("],[", ",");
						text2 = text2.Replace("]", string.Empty);
						text2 = text2.Replace("[", string.Empty);
						text2 = text2.Replace("\",\"", "\"");
						string[] array = text2.Split(new char[]
						{
							'"'
						}, StringSplitOptions.RemoveEmptyEntries);
						for (int i = 0; i < array.Count<string>(); i += 2)
						{
							string text3 = array[i];
							if (text3.StartsWith(",,"))
							{
								i--;
							}
							else
							{
								text = text + text3 + "  ";
							}
						}
					}
					text = text.Trim();
					text = text.Replace(" ?", "?");
					text = text.Replace(" !", "!");
					text = text.Replace(" ,", ",");
					text = text.Replace(" .", ".");
					text = text.Replace(" ;", ";");
					this.TranslationSpeechUrl = string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx", WebUtility.UrlEncode(text), smethod_0(targetLanguage), text.Length);
				}
			}
			catch (Exception error)
			{
				this.Error = error;
			}
			this.TranslationTime = DateTime.Now - now;
			return text;
		}

		private static string smethod_0(string string_1)
		{
			string empty = string.Empty;
			smethod_1();
			dictionary_0.TryGetValue(string_1, out empty);
			return empty;
		}

		private static void smethod_1()
		{
			if (dictionary_0 == null)
			{
				dictionary_0 = new Dictionary<string, string>();
				dictionary_0.Add("cs-CZ", "cs");
				dictionary_0.Add("da-DK", "da");
				dictionary_0.Add("de-DE", "de");
				dictionary_0.Add("el-GR", "el");
				dictionary_0.Add("en-EN", "en");
				dictionary_0.Add("en-GB", "en");
				dictionary_0.Add("es-ES", "es");
				dictionary_0.Add("es-MX", "es");
				dictionary_0.Add("fi-FI", "fi");
				dictionary_0.Add("fr-FR", "fr");
				dictionary_0.Add("it-IT", "it");
				dictionary_0.Add("ja-JP", "ja");
				dictionary_0.Add("ko-KR", "ko");
				dictionary_0.Add("nb-NO", "no");
				dictionary_0.Add("nl-NL", "nl");
				dictionary_0.Add("pl-PL", "pl");
				dictionary_0.Add("pt-BR", "pt");
				dictionary_0.Add("pt-PT", "pt");
				dictionary_0.Add("ru-RU", "ru");
				dictionary_0.Add("sk-SK", "sk");
				dictionary_0.Add("sv-SE", "sv");
				dictionary_0.Add("tr-TR", "tr");
				dictionary_0.Add("zh-CN", "zh-CN");
				dictionary_0.Add("zh-HANS", "zh-HANS");
				dictionary_0.Add("zh-HANT", "zh-HANT");
			}
		}

		public static bool HasInternetConnection()
		{
			bool result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					using (webClient.OpenRead("https://www.google.com"))
					{
						result = true;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public Translator()
		{
		}

		[CompilerGenerated]
		private static string smethod_2(string string_1)
		{
			return string_1;
		}

		private static Dictionary<string, string> dictionary_0;

	}
}
