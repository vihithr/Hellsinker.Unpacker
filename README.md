# HsUnpack

**简体中文 | [English](#english)**

## 项目简介
HsUnpack 是一个基于 .NET 8 的 Windows 图形工具，专门面向游戏 **Hellsinker.** 的封包格式，用于拆包和重新打包对应的 `.pak` 资源。程序读取/生成 `.cfg` 索引文件，便于快速提取与回填资源。

## 功能
- 解析 `.pak` 文件并按原始路径批量导出。
- 基于 `.cfg` 配置将目录内容重新写入 `.pak`。
- 内置简单文件偏移/长度处理，无需命令行操作。

## 环境要求
- Windows 10/11
- .NET 8 桌面运行时（构建需 .NET 8 SDK）

## 快速开始
1) 直接使用  
   - 下载 `bin/Release/net8.0-windows/HsUnpack.exe`。  
   - 运行后点击 `选择 .pak`，完成后点 “解包” 输出到同名目录；  
   - 选择对应生成的 `.cfg` 后点 “打包” 生成新的 `.pak`。

2) 本地构建  
   ```powershell
   dotnet build HsUnpack.sln -c Release
   ```
   生成文件位于 `HsUnpack/bin/Release/net8.0-windows/`。

## 目录结构
- `HsUnpack/` WinForms 源码
- `HsUnpack/bin/` 编译产物
- `HsUnpack/HsDataProgress.cs` 核心打包/解包逻辑

## 贡献
欢迎提交 Issue/PR，提供新功能或格式支持。

## 许可证
MIT License

---

# English

## Overview
HsUnpack is a .NET 8 Windows GUI utility tailored for the game **Hellsinker.**. It unpacks and repacks the game’s `.pak` resources and reads/writes companion `.cfg` index files so assets can be exported and restored with their original paths.

## Features
- Unpack `.pak` archives to their original folder structure.
- Repack folders back into `.pak` using the generated `.cfg` index.
- Handles offsets and lengths internally; no command line required.

## Requirements
- Windows 10/11
- .NET 8 Desktop Runtime (and .NET 8 SDK if you build from source)

## Quick Start
1) Ready-to-run  
   - Use `bin/Release/net8.0-windows/HsUnpack.exe`.  
   - Click “Select .pak”, then “Unpack” to extract into a same-named folder.  
   - Select the generated `.cfg`, click “Pack” to produce a new `.pak`.

2) Build from source  
   ```powershell
   dotnet build HsUnpack.sln -c Release
   ```
   Outputs to `HsUnpack/bin/Release/net8.0-windows/`.

## Project Layout
- `HsUnpack/` WinForms source
- `HsUnpack/bin/` build artifacts
- `HsUnpack/HsDataProgress.cs` core packing/unpacking logic

## Contributing
Issues/PRs are welcome for new features or format support.

## License
MIT License

