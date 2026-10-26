using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenLauncher.Core
{
    /// <summary>
    /// 界面多语言。字符串用 Lang.T("key") 取；切换语言后触发 Changed，主窗口刷新可见文本。
    /// 支持：zh-CN 简体、zh-HK 繁體（香港）、en、de、fr、ja。
    /// </summary>
    public static class Lang
    {
        public class LocaleInfo
        {
            public string Code { get; set; }
            public string NativeName { get; set; }
            public string EnglishName { get; set; }
            public override string ToString() => NativeName;
        }

        public static readonly LocaleInfo[] Locales =
        {
            new LocaleInfo { Code = "zh-CN", NativeName = "简体中文", EnglishName = "Simplified Chinese" },
            new LocaleInfo { Code = "zh-HK", NativeName = "繁體中文（香港）", EnglishName = "Traditional Chinese (Hong Kong)" },
            new LocaleInfo { Code = "en", NativeName = "English", EnglishName = "English" },
            new LocaleInfo { Code = "de", NativeName = "Deutsch", EnglishName = "German" },
            new LocaleInfo { Code = "fr", NativeName = "Français", EnglishName = "French" },
            new LocaleInfo { Code = "ja", NativeName = "日本語", EnglishName = "Japanese" },
        };

        static Dictionary<string, string> _map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        static Dictionary<string, string> _fallback = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        static string _code = "zh-CN";

        public static string Code => _code;
        public static event Action Changed;

        public static void Init()
        {
            _fallback = LoadDict("zh-CN");
            var saved = Settings.UiLanguage;
            if (string.IsNullOrWhiteSpace(saved) || !Locales.Any(l => l.Code.Equals(saved, StringComparison.OrdinalIgnoreCase)))
                saved = DetectSystem();
            Apply(saved, raise: false);
        }

        public static void Apply(string code, bool raise = true)
        {
            if (string.IsNullOrWhiteSpace(code)) code = "zh-CN";
            code = Locales.FirstOrDefault(l => l.Code.Equals(code, StringComparison.OrdinalIgnoreCase))?.Code ?? "zh-CN";
            _code = code;
            _map = LoadDict(code);
            if (_map.Count == 0 && code != "zh-CN") _map = new Dictionary<string, string>(_fallback, StringComparer.OrdinalIgnoreCase);
            Settings.UiLanguage = code;
            if (raise) Changed?.Invoke();
        }

        /// <summary>取翻译；缺省回退简体，再回退 key 本身。</summary>
        public static string T(string key)
        {
            if (string.IsNullOrEmpty(key)) return key;
            if (_map != null && _map.TryGetValue(key, out var s) && s != null) return s;
            if (_fallback != null && _fallback.TryGetValue(key, out s) && s != null) return s;
            return key;
        }

        public static string T(string key, params object[] args)
        {
            var fmt = T(key);
            try { return args == null || args.Length == 0 ? fmt : string.Format(fmt, args); }
            catch { return fmt; }
        }

        static string DetectSystem()
        {
            try
            {
                var c = CultureInfo.CurrentUICulture;
                var name = (c.Name ?? "").Replace('_', '-');
                if (name.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
                {
                    if (name.IndexOf("HK", StringComparison.OrdinalIgnoreCase) >= 0
                        || name.IndexOf("TW", StringComparison.OrdinalIgnoreCase) >= 0
                        || name.IndexOf("MO", StringComparison.OrdinalIgnoreCase) >= 0
                        || name.IndexOf("Hant", StringComparison.OrdinalIgnoreCase) >= 0)
                        return "zh-HK";
                    return "zh-CN";
                }
                var two = c.TwoLetterISOLanguageName;
                if (Locales.Any(l => l.Code.Equals(two, StringComparison.OrdinalIgnoreCase))) return two;
            }
            catch { }
            return "zh-CN";
        }

        static Dictionary<string, string> LoadDict(string code)
        {
            var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                var name = "lang." + code + ".json";
                Stream stream = null;
                foreach (var n in asm.GetManifestResourceNames())
                    if (n.EndsWith(name, StringComparison.OrdinalIgnoreCase) || n.Equals(name, StringComparison.OrdinalIgnoreCase))
                    { stream = asm.GetManifestResourceStream(n); break; }
                if (stream == null)
                    stream = asm.GetManifestResourceStream(name);
                if (stream == null) return BuiltIn(code);
                using (stream)
                using (var r = new StreamReader(stream, Encoding.UTF8))
                {
                    var text = r.ReadToEnd();
                    if (JNode.TryParse(text, out var j) && j.IsObj)
                    {
                        foreach (var kv in j.Props)
                            if (kv.Value.Str != null) d[kv.Key] = kv.Value.Str;
                    }
                }
            }
            catch (Exception ex) { Log.Warn("加载语言包 " + code + " 失败：" + ex.Message); }
            if (d.Count == 0) return BuiltIn(code);
            return d;
        }

        static Dictionary<string, string> BuiltIn(string code)
        {
            if (code == "zh-CN") return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["nav.launch"] = "启动", ["nav.download"] = "下载", ["nav.settings"] = "设置", ["nav.more"] = "更多",
                ["ui.language"] = "界面语言", ["common.ok"] = "确定", ["common.cancel"] = "取消",
            };
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
