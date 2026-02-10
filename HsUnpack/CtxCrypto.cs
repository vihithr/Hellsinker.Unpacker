using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsUnpack
{
    /// <summary>
    /// CTX文件加解密类
    /// 使用51BCD4算法（基于LOSTPROPERTY密钥的XOR加密）
    /// </summary>
    internal class CtxCrypto
    {
        // "LOSTPROPERTY"字符串作为密钥源
        private const string LOSTPROPERTY_KEY = "LOSTPROPERTY";

        /// <summary>
        /// 使用51BCD4算法解密CTX文件
        /// </summary>
        /// <param name="encryptedData">加密的数据</param>
        /// <param name="keyStartIdx">密钥起始索引（默认0，标准messages.ctx使用0）</param>
        /// <param name="dataStartOffset">数据起始偏移（默认0，从文件开头开始解密）</param>
        /// <returns>解密后的数据</returns>
        public static byte[] Decrypt51BCD4(byte[] encryptedData, int keyStartIdx = 0, int dataStartOffset = 0)
        {
            if (encryptedData == null || encryptedData.Length == 0)
            {
                throw new ArgumentException("加密数据不能为空", nameof(encryptedData));
            }

            byte[] decryptedData = new byte[encryptedData.Length];
            int keyCounter = keyStartIdx;

            for (int i = 0; i < encryptedData.Length; i++)
            {
                // 如果当前字节在数据起始偏移之前，直接复制（不解密）
                if (i < dataStartOffset)
                {
                    decryptedData[i] = encryptedData[i];
                    continue;
                }

                // 读取密钥字符
                char keyChar = LOSTPROPERTY_KEY[keyCounter];
                byte keyByte = (byte)keyChar;

                // XOR解密
                decryptedData[i] = (byte)(encryptedData[i] ^ keyByte);

                // 密钥计数器递增并循环
                keyCounter = (keyCounter + 1) % LOSTPROPERTY_KEY.Length;
            }

            return decryptedData;
        }

        /// <summary>
        /// 使用51BCD4算法加密CTX文件
        /// </summary>
        /// <param name="plaintextData">明文数据</param>
        /// <param name="keyStartIdx">密钥起始索引（默认0，标准messages.ctx使用0）</param>
        /// <param name="dataStartOffset">数据起始偏移（默认0，从文件开头开始加密）</param>
        /// <returns>加密后的数据</returns>
        public static byte[] Encrypt51BCD4(byte[] plaintextData, int keyStartIdx = 0, int dataStartOffset = 0)
        {
            if (plaintextData == null || plaintextData.Length == 0)
            {
                throw new ArgumentException("明文数据不能为空", nameof(plaintextData));
            }

            byte[] encryptedData = new byte[plaintextData.Length];
            int keyCounter = keyStartIdx;

            for (int i = 0; i < plaintextData.Length; i++)
            {
                // 如果当前字节在数据起始偏移之前，直接复制（不加密）
                if (i < dataStartOffset)
                {
                    encryptedData[i] = plaintextData[i];
                    continue;
                }

                // 读取密钥字符
                char keyChar = LOSTPROPERTY_KEY[keyCounter];
                byte keyByte = (byte)keyChar;

                // XOR加密
                encryptedData[i] = (byte)(plaintextData[i] ^ keyByte);

                // 密钥计数器递增并循环
                keyCounter = (keyCounter + 1) % LOSTPROPERTY_KEY.Length;
            }

            return encryptedData;
        }

        /// <summary>
        /// 解密CTX文件并保存
        /// </summary>
        /// <param name="inputPath">输入的加密CTX文件路径</param>
        /// <param name="outputPath">输出的解密文件路径（可选，默认为原文件名+.decrypted）</param>
        /// <param name="keyStartIdx">密钥起始索引</param>
        /// <param name="dataStartOffset">数据起始偏移</param>
        /// <returns>输出文件路径</returns>
        public static string DecryptFile(string inputPath, string? outputPath = null, int keyStartIdx = 0, int dataStartOffset = 0)
        {
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"文件未找到: {inputPath}");
            }

            // 读取加密文件
            byte[] encryptedData = File.ReadAllBytes(inputPath);

            // 解密
            byte[] decryptedData = Decrypt51BCD4(encryptedData, keyStartIdx, dataStartOffset);

            // 确定输出路径
            if (string.IsNullOrEmpty(outputPath))
            {
                string directory = Path.GetDirectoryName(inputPath) ?? "";
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string extension = Path.GetExtension(inputPath);
                outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_decrypted{extension}");
            }

            // 确保输出目录存在
            string? outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // 写入解密文件
            File.WriteAllBytes(outputPath, decryptedData);

            return outputPath;
        }

        /// <summary>
        /// 加密文件并保存为CTX格式
        /// </summary>
        /// <param name="inputPath">输入的明文文件路径</param>
        /// <param name="outputPath">输出的加密CTX文件路径（可选，默认为原文件名+.ctx）</param>
        /// <param name="keyStartIdx">密钥起始索引</param>
        /// <param name="dataStartOffset">数据起始偏移</param>
        /// <returns>输出文件路径</returns>
        public static string EncryptFile(string inputPath, string? outputPath = null, int keyStartIdx = 0, int dataStartOffset = 0)
        {
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"文件未找到: {inputPath}");
            }

            // 读取明文文件
            byte[] plaintextData = File.ReadAllBytes(inputPath);

            // 加密
            byte[] encryptedData = Encrypt51BCD4(plaintextData, keyStartIdx, dataStartOffset);

            // 确定输出路径
            if (string.IsNullOrEmpty(outputPath))
            {
                string directory = Path.GetDirectoryName(inputPath) ?? "";
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                outputPath = Path.Combine(directory, $"{fileNameWithoutExt}.ctx");
            }

            // 确保输出目录存在
            string? outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // 写入加密文件
            File.WriteAllBytes(outputPath, encryptedData);

            return outputPath;
        }

        /// <summary>
        /// 检查数据是否为有效文本
        /// </summary>
        /// <param name="data">要检查的数据</param>
        /// <param name="minPrintableRatio">最小可打印字符比例</param>
        /// <returns>是否为有效文本</returns>
        public static bool IsValidText(byte[] data, double minPrintableRatio = 0.7)
        {
            if (data == null || data.Length < 10)
            {
                return false;
            }

            try
            {
                // 尝试UTF-8解码
                int checkLength = Math.Min(1000, data.Length);
                string text = Encoding.UTF8.GetString(data, 0, checkLength);

                // 计算可打印字符比例
                int printableCount = text.Count(c => char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsWhiteSpace(c));
                double ratio = (double)printableCount / text.Length;

                return ratio >= minPrintableRatio;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 自动检测最佳解密模式
        /// </summary>
        /// <param name="encryptedData">加密数据</param>
        /// <returns>(解密数据, 密钥索引, 数据偏移)</returns>
        public static (byte[] decryptedData, int keyIdx, int offset)? AutoDetectMode(byte[] encryptedData)
        {
            // 定义常用的解密模式
            var modes = new[]
            {
                (keyIdx: 0, offset: 0, name: "51BCD4 (标准messages.ctx)"),
                (keyIdx: 3, offset: 3, name: "80224C (其他CTX文件)"),
                (keyIdx: 2, offset: 1, name: "key2_off1"),
                (keyIdx: 3, offset: 2, name: "key3_off2"),
                (keyIdx: 5, offset: 4, name: "key5_off4"),
            };

            foreach (var mode in modes)
            {
                try
                {
                    byte[] decrypted = Decrypt51BCD4(encryptedData, mode.keyIdx, mode.offset);
                    
                    // 检查是否为有效文本
                    if (IsValidText(decrypted))
                    {
                        return (decrypted, mode.keyIdx, mode.offset);
                    }
                }
                catch
                {
                    // 忽略错误，继续尝试下一个模式
                }
            }

            return null;
        }
    }
}

