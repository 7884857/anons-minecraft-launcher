namespace OpenLauncher
{
    /// <summary>
    /// 换名字、换仓库、换维护者，只改这一个文件。
    /// 任何人 fork 之后改掉这里 + 换成自己的签名公钥，就是一个独立发行版。
    ///
    /// 本发行版基于 OpenLauncher（原作者：有问题的红河）。
    /// 原作者主页：https://space.bilibili.com/362162451
    /// 短链：https://b23.tv/fOf5z56
    /// </summary>
    public static class Branding
    {
        /// <summary>内部标识 / 更新清单里的 app 字段 / 默认 exe 名</summary>
        public const string AppName = "asml";
        /// <summary>窗口标题、关于页显示名</summary>
        public const string DisplayName = "asml";
        public const string Tagline = "去中心化 Minecraft 启动器";

        /// <summary>主页 / 源码（关于页与 User-Agent）</summary>
        public const string RepoUrl = "https://github.com/7884857/anons-minecraft-launcher";

        /// <summary>
        /// 内置更新清单：仅本项目 GitHub（及同源 CDN 镜像）。
        /// 发布时在仓库 release/update.json 放置签名清单即可。
        /// 用户仍可在 设置 → 更新 里自行添加其它源。
        /// </summary>
        public static readonly string[] DefaultUpdateSources =
        {
            "https://raw.githubusercontent.com/7884857/anons-minecraft-launcher/main/release/update.json",
            "https://cdn.jsdelivr.net/gh/7884857/anons-minecraft-launcher@main/release/update.json",
            "https://fastly.jsdelivr.net/gh/7884857/anons-minecraft-launcher@main/release/update.json",
            "https://github.com/7884857/anons-minecraft-launcher/raw/main/release/update.json",
        };

        /// <summary>
        /// 内置信任的发布公钥（RSA，.NET XML 格式）。用 tools/Release 的 keygen 生成。
        /// 更新清单必须由其中任意一把私钥签名才会被接受。
        /// 当前保留 OpenLauncher 原作者公钥；你自己发版时应换成自己的钥匙并更新此列表。
        /// </summary>
        public static readonly string[] TrustedKeys =
        {
            // 指纹 12c461c38f08483a（OpenLauncher 初始发布钥匙，原作者：有问题的红河）
            // 若改用自有签名发版，请替换为你的公钥，否则无法通过校验安装更新。
            "<RSAKeyValue><Modulus>ugyFBDPVmXZcK68n80vPAgS5wrhhCwOgmlZuvUZULR1tGN67q7fShd4BV/iyjC0t4Qn9moE0NGY9O1KRMnlWPd2wnGzinX+536lrkmba43dbBL733tUhRbLjRa0WWGjV1CoABlljVU4XoUYgyuAOWnTWYwBq+dNeCr1nq5CUH8F+3KNUMrA5HGM50OiIRkM7j3IDpwxB8wQMngP5ObPLDMBpz94Zqn/+ZFFpXhlw81vZACe/7KrnEtIXHkVusDtZFuVAN7M6XLwaTyDHkEYshudCnj80sXBi1dbC3beKmcBSSd3oH1lDRmdEen+vU5aDOi9mY4V2RstgCdZKPt3xJ/G98Nm6t8khnrlpitqyAD60DHWhtDCLDUlAaaWsfda+PeGt1s8novdpnaiSJO9Fcaywiz+WdMHjvBN+Z1u18Adiu9MdYACY/Rg70pnk37drYp4GD8o+tZEfRxW32RfFV5jDI4INrFWdlT33VLmTpPtWQItW6ibXDgOIP23cnGAh</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
        };

        public const string DefaultMicrosoftClientId = "";
        public const string DefaultCurseForgeApiKey = "";
    }
}
