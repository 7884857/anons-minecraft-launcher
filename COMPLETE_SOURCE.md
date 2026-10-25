# 完整源码说明

本仓库通过 GitHub API 分批上传了大部分源码（约 80+ 文件）。

因 API 单次传参限制，下列较大文件可能尚未全部到位：

- `Core/Json.cs`
- `Accounts/MicrosoftAuth.cs` / `YggdrasilAuth.cs`
- `Content/*`（ContentService、Modrinth、CurseForge、LocalMods、ModpackImporter）
- `Game/LaunchService.cs` / `JavaManager.cs` / `CrashAnalyzer.cs`
- `UI/Pages/InstancePage.*` / `SettingsPage.*` / `Styles.xaml`
- `LICENSE`（GPL-3.0）

## 一次补全（推荐）

完整可编译源码已打包在项目附件 `anons-minecraft-launcher-src.tar.gz`。本地执行：

```bash
git clone https://github.com/7884857/anons-minecraft-launcher.git
cd anons-minecraft-launcher
tar xzf /path/to/anons-minecraft-launcher-src.tar.gz
# 覆盖/补齐所有文件
git add -A
git status
git commit -m "feat: import complete OpenLauncher source"
git push
```

原作者：有问题的红河（Bilibili：https://b23.tv/fOf5z56）  
许可证：GPL-3.0
