# 完整源码说明

本仓库已通过 GitHub API 分批上传 **约 96 个源码文件**。

因单次推送体积限制（约 35KB），以下 **2 个大文件** 仍需本地补全：

| 文件 | 大小 |
|------|------|
| `src/OpenLauncher/Game/JavaManager.cs` | ~51 KB |
| `src/OpenLauncher/Game/CrashAnalyzer.cs` | ~74 KB |

其余源码（Core / Accounts / Content / UI / LICENSE / Branding 等）均已在仓库中。

## 一次补全（推荐）

完整可编译源码在附件 `anons-minecraft-launcher-src.tar.gz`。本地执行：

```bash
git clone https://github.com/7884857/anons-minecraft-launcher.git
cd anons-minecraft-launcher

# 解压完整源码（覆盖/补齐）
tar xzf /path/to/anons-minecraft-launcher-src.tar.gz

git add -A
git status
git commit -m "feat: add JavaManager.cs and CrashAnalyzer.cs"
git push
```

或只补这两个文件：

```bash
# 从 tar 中只抽出这两个文件
tar xzf anons-minecraft-launcher-src.tar.gz \
  src/OpenLauncher/Game/JavaManager.cs \
  src/OpenLauncher/Game/CrashAnalyzer.cs
git add src/OpenLauncher/Game/JavaManager.cs src/OpenLauncher/Game/CrashAnalyzer.cs
git commit -m "feat: add JavaManager.cs and CrashAnalyzer.cs"
git push
```

## 原作者与许可证

- **原作者**：有问题的红河（Bilibili：https://b23.tv/fOf5z56）
- **本仓库**：anon's Minecraft launcher（基于 OpenLauncher 重命名）
- **许可证**：GPL-3.0
