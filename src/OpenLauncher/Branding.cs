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
        public const string AppName = "AnonsMinecraftLauncher";
        public const string DisplayName = "anon's Minecraft launcher";
        public const string Tagline = "去中心化 Minecraft 启动器";

        /// <summary>主页 / 源码下载（仅用于"关于"页面和 User-Agent，不是更新的唯一来源）</summary>
        public const string RepoUrl = "https://github.com/7884857/anons-minecraft-launcher";

        /// <summary>
        /// 内置的更新清单地址。任意一个能访问到、且签名验证通过就行，
        /// 用户也可以在 设置 → 更新 里自己加镜像。顺序不代表优先级：会全部尝试并取版本最高的有效清单。
        /// </summary>
        public static readonly string[] DefaultUpdateSources =
        {
            "https://raw.githubusercontent.com/7884857/anons-minecraft-launcher/main/release/update.json",
            "https://cdn.jsdelivr.net/gh/7884857/anons-minecraft-launcher@main/release/update.json",
            "https://fastly.jsdelivr.net/gh/7884857/anons-minecraft-launcher@main/release/update.json",
            // 保留原 OpenLauncher 更新源，便于在换钥匙前继续收到上游更新
            "https://honghemc.cn/openlauncher/update.json",
            "https://raw.githubusercontent.com/OpenLauncher-MC/OpenLauncher/main/release/update.json",
            "https://cdn.jsdelivr.net/gh/OpenLauncher-MC/OpenLauncher@main/release/update.json",
            "https://gitee.com/OpenLauncher-MC/OpenLauncher/raw/main/release/update.json",
        };

        /// <summary>
        /// 内置信任的发布公钥（RSA，.NET XML 格式）。用 tools/Release 的 keygen 生成。
        /// 更新清单必须由其中任意一把私钥签名才会被接受。
        /// 当前保留 OpenLauncher 原作者公钥；社区接手时可在此追加新公钥。
        /// </summary>
        public static readonly string[] TrustedKeys =
        {
            // 指纹 12c461c38f08483a（OpenLauncher 初始发布钥匙，原作者：有问题的红河）
            "<RSAKeyValue><Modulus>ugyFBDPVmXZcK68n80vPAgS5wrhhCwOgmlZuvUZULR1tGN67q7fShd4BV/iyjC0t4Qn9moE0NGY9O1KRMnlWPd2wnGzinX+536lrkmba43dbBL733tUhRbLjRa0WWGjV1CoABlljVU4XoUYgyuAOWnTWYwBq+dNeCr1nq5CUH8F+3KNUMrA5HGM50OiIRkM7j3IDpwxB8wQMngP5ObPLDMBpz94Zqn/+ZFFpXhlw81vZACe/7KrnEtIXHkVusDtZFuVAN7M6XLwaTyDHkEYshudCnj80sXBi1dbC3beKmcBSSd3oH1lDRmdEen+vU5aDOi9mY4V2RstgCdZKPt3xJ/G98Nm6t8khnrlpitqyAD60DHWhtDCLDUlAaaWsfda+PeGt1s8novdpnaiSJO9Fcaywiz+WdMHjvBN+Z1u18Adiu9MdYACY/Rg70pnk37drYp4GD8o+tZEfRxW32RfFV5jDI4INrFWdlT33VLmTpPtWQItW6ibXDgOIP23cnGAh</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
        };

        /// <summary>
        /// 微软正版登录用的 Azure 应用 Client ID。
        /// 需要自己在 Azure 注册应用并向 Mojang 申请 Minecraft API 权限后填入；
        /// 留空时用户可在 设置 → 账户 里填自己的。
        /// </summary>
        public const string DefaultMicrosoftClientId = "";

        /// <summary>CurseForge API Key（可选）。留空时自动走无需 Key 的镜像。</summary>
        public const string DefaultCurseForgeApiKey = "";
    }
}
