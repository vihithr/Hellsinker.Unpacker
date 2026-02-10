# HsUnpack

**简体中文 | [English](#english)**

## 项目简介
HsUnpack 是一个基于 .NET 8 的 Windows 图形工具，专门面向游戏 **Hellsinker.** 的资源文件处理，支持 `.pak` 封包格式的拆包/打包，以及 `.ctx` 文本文件的加密/解密。

## 功能
- **PAK 封包处理**
  - 解析 `.pak` 文件并按原始路径批量导出
  - 基于 `.cfg` 配置将目录内容重新写入 `.pak`
  - 内置简单文件偏移/长度处理，无需命令行操作
  
- **CTX 文件加密/解密** ✨ 新功能
  - 使用 51BCD4 算法（基于 LOSTPROPERTY 密钥的 XOR 加密）
  - 支持 `messages.ctx` 等文本文件的解密和加密
  - 一键解密 CTX 文件为可读文本
  - 修改后可重新加密回 CTX 格式
  
- 🌍 **多语言支持**：自动检测系统语言，支持中文、英文、日文，可在界面手动切换

## 环境要求
- Windows 10/11
- .NET 8 桌面运行时（构建需 .NET 8 SDK）

## 快速开始

### 使用 PAK 封包功能
1. 运行 `HsUnpack.exe`
2. **解包**：
   - 点击 "选择文件" 选择 `.pak` 文件
   - 点击 "解包" 按钮，文件将被提取到同名目录
   - 同时生成 `.cfg` 配置文件
3. **封包**：
   - 点击 "选择文件" 选择之前生成的 `.cfg` 文件
   - 点击 "封包" 按钮，生成新的 `.pak` 文件

### 使用 CTX 加密/解密功能
1. 运行 `HsUnpack.exe`
2. **解密 CTX 文件**：
   - 点击 "选择文件" 选择加密的 `.ctx` 文件（如 `messages.ctx`）
   - 点击 "解密CTX" 按钮
   - 解密后的文件将保存为 `原文件名_decrypted.ctx`
3. **加密为 CTX 文件**：
   - 点击 "选择文件" 选择要加密的文本文件
   - 点击 "加密CTX" 按钮
   - 加密后的文件将保存为 `原文件名.ctx`

### 本地构建
```powershell
dotnet build HsUnpack.sln -c Release
```
生成文件位于 `HsUnpack/bin/Release/net8.0-windows/`。

## 技术细节

### CTX 加密算法（51BCD4）
- 使用密钥字符串 "LOSTPROPERTY" 进行 XOR 加密/解密
- 密钥循环使用，从指定索引开始（标准 messages.ctx 使用索引 0）
- 支持自定义密钥起始索引和数据偏移量
- 算法实现位于 `HsUnpack/CtxCrypto.cs`

### Python 脚本支持
项目包含独立的 Python 脚本 `decrypt_packet.py`，提供命令行方式的 CTX 文件处理：
```bash
# 解密（自动检测模式）
python decrypt_packet.py messages.ctx

# 使用指定模式解密
python decrypt_packet.py messages.ctx output.txt 51BCD4

# 加密
python decrypt_packet.py --encrypt messages.txt messages.ctx 51BCD4
```

## 目录结构
```
HsUnpack/
├── HsUnpack/              # WinForms 源码
│   ├── CtxCrypto.cs       # CTX 加密/解密核心类
│   ├── HsDataProgress.cs  # PAK 打包/解包逻辑
│   ├── Form1.cs           # 主窗体逻辑
│   └── Properties/        # 多语言资源文件
├── decrypt_packet.py      # Python 命令行工具
└── README.md
```

## GitHub Actions
本项目配置了自动编译流程：
- 推送到 main/master 分支时自动编译
- 创建 Tag 时自动发布 Release
- 可在 Actions 标签页下载编译产物

## 贡献
欢迎提交 Issue/PR，提供新功能或格式支持。

## 许可证
MIT License

---

# English

## Overview
HsUnpack is a .NET 8 Windows GUI utility tailored for the game **Hellsinker.**. It handles `.pak` resource archives (unpack/repack) and `.ctx` text file encryption/decryption.

## Features
- **PAK Archive Handling**
  - Unpack `.pak` archives to their original folder structure
  - Repack folders back into `.pak` using the generated `.cfg` index
  - Handles offsets and lengths internally; no command line required
  
- **CTX File Encryption/Decryption** ✨ New Feature
  - Uses 51BCD4 algorithm (XOR encryption based on LOSTPROPERTY key)
  - Supports decryption and encryption of text files like `messages.ctx`
  - One-click decrypt CTX files to readable text
  - Re-encrypt modified files back to CTX format
  
- 🌍 **Multi-language Support**: Auto-detects system language, supports Chinese, English, and Japanese with manual UI switcher

## Requirements
- Windows 10/11
- .NET 8 Desktop Runtime (and .NET 8 SDK if you build from source)

## Quick Start

### Using PAK Archive Features
1. Run `HsUnpack.exe`
2. **Unpack**:
   - Click "Select File" to choose a `.pak` file
   - Click "Unpack" button to extract files to a same-named folder
   - A `.cfg` configuration file will be generated
3. **Pack**:
   - Click "Select File" to choose the previously generated `.cfg` file
   - Click "Pack" button to generate a new `.pak` file

### Using CTX Encryption/Decryption Features
1. Run `HsUnpack.exe`
2. **Decrypt CTX File**:
   - Click "Select File" to choose an encrypted `.ctx` file (e.g., `messages.ctx`)
   - Click "Decrypt CTX" button
   - Decrypted file will be saved as `originalname_decrypted.ctx`
3. **Encrypt to CTX File**:
   - Click "Select File" to choose a text file to encrypt
   - Click "Encrypt CTX" button
   - Encrypted file will be saved as `originalname.ctx`

### Build from Source
```powershell
dotnet build HsUnpack.sln -c Release
```
Outputs to `HsUnpack/bin/Release/net8.0-windows/`.

## Technical Details

### CTX Encryption Algorithm (51BCD4)
- Uses key string "LOSTPROPERTY" for XOR encryption/decryption
- Key cycles from specified index (standard messages.ctx uses index 0)
- Supports custom key start index and data offset
- Implementation located in `HsUnpack/CtxCrypto.cs`

### Python Script Support
The project includes a standalone Python script `decrypt_packet.py` for command-line CTX file processing:
```bash
# Decrypt (auto-detect mode)
python decrypt_packet.py messages.ctx

# Decrypt with specific mode
python decrypt_packet.py messages.ctx output.txt 51BCD4

# Encrypt
python decrypt_packet.py --encrypt messages.txt messages.ctx 51BCD4
```

## Project Layout
```
HsUnpack/
├── HsUnpack/              # WinForms source
│   ├── CtxCrypto.cs       # CTX encryption/decryption core class
│   ├── HsDataProgress.cs  # PAK packing/unpacking logic
│   ├── Form1.cs           # Main form logic
│   └── Properties/        # Multi-language resource files
├── decrypt_packet.py      # Python command-line tool
└── README.md
```

## GitHub Actions
Automated build workflow configured:
- Auto-builds on push to main/master branch
- Auto-releases on Tag creation
- Download build artifacts from Actions tab

## Contributing
Issues/PRs are welcome for new features or format support.

## License
MIT License
