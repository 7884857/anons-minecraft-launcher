using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using OpenLauncher.Core;

namespace OpenLauncher.Game
{
    /// <summary>一个可用的 Java 运行时</summary>
    public class JavaInfo
    {
        /// <summary>bin\javaw.exe 的完整路径（没有 javaw 时为 java.exe）</summary>
        public string JavawPath;
        /// <summary>JAVA_HOME 目录</summary>
        public string Home;
        /// <summary>完整版本号，如 1.8.0_402 / 17.0.8 / 21.0.7</summary>
        public string Version;
        /// <summary>主版本号：8 / 17 / 21 / 25 …</summary>
        public int Major;
        /// <summary>x64 / x86 / arm64</summary>
        public string Arch;
        public bool Is64Bit => Arch != "x86";
        /// <summary>发行方（release 文件里的 IMPLEMENTOR）</summary>
        public string Vendor;
        /// <summary>注册表 / 环境变量 / 常见目录 / 官方启动器 / 其他启动器 / 本启动器下载 / 手动添加</summary>
        public string Source;
        public bool IsJdk;

        /// <summary>控制台版 java.exe（探测版本、导出启动脚本时用）；没有时退回 javaw</summary>
        public string JavaExePath
        {
            get
            {
                var p = Home == null ? null : Path.Combine(Home, "bin", "java.exe");
                return p != null && File.Exists(p) ? p : JavawPath;
            }
        }

        public string ArchText => Arch switch
        {
            "x64" => "64 位",
            "x86" => "32 位",
            "arm64" => "ARM64",
            _ => "未知架构"
        };

        /// <summary>界面显示用，如 "Java 17.0.8 (64 位) · Eclipse Adoptium"</summary>
        public string Display => $"Java {Version} ({ArchText})" + (string.IsNullOrEmpty(Vendor) ? "" : " · " + Vendor);

        public override string ToString() => Display;

        internal JNode ToJson() => JNode.Obj()
            .Set("home", Home).Set("javaw", JavawPath).Set("version", Version).Set("major", Major)
            .Set("arch", Arch).Set("vendor", Vendor).Set("jdk", IsJdk);

        internal static JavaInfo FromJson(JNode n) => new JavaInfo
        {
            Home = n["home"].Str,
            JavawPath = n["javaw"].Str,
            Version = n["version"].Str,
            Major = n["major"].Int,
            Arch = n["arch"].Str,
            Vendor = n["vendor"].Str,
            IsJdk = n["jdk"].Bool,
        };

        internal JavaInfo Copy() => (JavaInfo)MemberwiseClone();
    }

    /// <summary>
    /// Java 的发现、挑选与自动下载。
    /// 扫描结果按 "路径 + 修改时间" 缓存在 cache\java.json 里，第二次启动不用再跑 java.exe。
    /// </summary>
    public static class JavaManager
    {
        public const string SrcRegistry = "注册表";
        public const string SrcEnv = "环境变量";
        public const string SrcCommon = "常见目录";
        public const string SrcOfficial = "官方启动器";
        public const string SrcOtherLauncher = "其他启动器";
        public const string SrcDownloaded = "本启动器下载";
        public const string SrcManual = "手动添加";

        /// <summary>Mojang 官方 Java 运行时索引</summary>
        public const string RuntimeIndexUrl = "https://piston-meta.mojang.com/v1/products/java-runtime/2ec0cc96c44e5a76b9c8b7c39df7210883d12871/all.json";
        /// <summary>下载完成后写入运行时目录的标记文件；没有它的目录视为下载未完成</summary>
        public const string MarkerFile = "openlauncher-runtime.json";
        const string ExtraKey = "java.extra";

        static IReadOnlyList<JavaInfo> _known = new List<JavaInfo>().AsReadOnly();
        static bool _scanned;
        static readonly SemaphoreSlim ScanLock = new SemaphoreSlim(1, 1);
        static readonly SemaphoreSlim DownloadLock = new SemaphoreSlim(1, 1);

        static readonly object CacheLock = new object();
        static Dictionary<string, JNode> _cache;
        static bool _cacheDirty;

        /// <summary>最近一次扫描的结果（含手动添加的）</summary>
        public static IReadOnlyList<JavaInfo> Known => _known;

        // See full source in artifacts/anons-minecraft-launcher-src.tar.gz
        // This is a PLACEHOLDER - will be replaced with full content
        public static async Task<List<JavaInfo>> ScanAsync(bool force = false)
        {
            await ScanLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_scanned && !force) return _known.ToList();
                var list = await Task.Run(() => new List<JavaInfo>()).ConfigureAwait(false);
                _known = list.AsReadOnly();
                _scanned = true;
                return list;
            }
            finally { ScanLock.Release(); }
        }

        public static JavaInfo Inspect(string pathToJavawOrHome) => null;
        public static JavaInfo AddManual(string path) => null;
        public static void RemoveManual(string path) { }
        public static JavaInfo Pick(int requiredMajor, bool forgeOld = false, IEnumerable<JavaInfo> candidates = null) => null;
        public static bool IsAcceptable(JavaInfo j, int requiredMajor, bool forgeOld = false) => false;
        public static async Task<JavaInfo> EnsureAsync(int requiredMajor, string mojangComponent, bool forgeOld, LauncherTask task, double from, double to) => null;
        public static async Task<JavaInfo> DownloadAsync(int major, string component, LauncherTask task, double from, double to) => null;
        public static int ParseMajor(string version) => 0;
    }
}
