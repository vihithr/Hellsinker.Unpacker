# 更新日志 / Changelog

## [未发布] - 2025-02-10

### 新增功能 / Added
- ✨ **CTX 文件加密/解密功能**
  - 新增 `CtxCrypto.cs` 类，实现 51BCD4 加密算法
  - 支持使用 LOSTPROPERTY 密钥进行 XOR 加密/解密
  - 在 UI 中添加 CTX 文件解密功能
  - 在 UI 中添加 CTX 文件加密功能
  - 支持 `messages.ctx` 等游戏文本文件的处理

- 🌍 **多语言资源扩展**
  - 为 CTX 功能添加中文、英文、日文资源字符串
  - 新增错误提示和成功消息的多语言支持

### 技术细节 / Technical Details
- 实现了完整的 51BCD4 算法（密钥起始索引 0，数据偏移 0）
- 支持自定义密钥索引和数据偏移参数
- 自动检测解密结果的有效性
- 提供文件级别的加密/解密 API

### 文件变更 / Files Changed
- 新增: `HsUnpack/CtxCrypto.cs` - CTX 加密/解密核心类
- 修改: `HsUnpack/Form1.cs` - 添加 CTX 处理事件
- 修改: `HsUnpack/Form1.Designer.cs` - 添加 CTX UI 控件
- 修改: `HsUnpack/Properties/Resources.resx` - 中文资源
- 修改: `HsUnpack/Properties/Resources.en.resx` - 英文资源
- 修改: `HsUnpack/Properties/Resources.ja.resx` - 日文资源
- 修改: `HsUnpack/Properties/Resources.Designer.cs` - 资源访问器
- 更新: `README.md` - 添加 CTX 功能文档

---

## [Previous Versions]

### 初始版本 / Initial Release
- PAK 文件解包功能
- PAK 文件封包功能
- 多语言支持（中文、英文、日文）
- GitHub Actions 自动构建

