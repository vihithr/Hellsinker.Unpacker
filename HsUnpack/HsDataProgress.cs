using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HsUnpack
{
    internal class HsDataProgress
    {
        static public List<HsData> hsData;        //获取解析后的HS文件内容
        static public int dataNum = 0;            //解析的文件数量
        static public int indexDataNumOffset = 0x1F400;    //文件数量偏移
        static public int lengthIndexOffset = 0x1F404;     //文件长度索引位置
        static public int offsetIndexOffset = 0x1FBD4;     //文件索引偏移位置
        static public int dataOffset = 0x203A4;        //数据文件开头位置

        public static void UnpackDoProgress(string filepath)
        {
            dataNum = BitConverter.ToInt32(ReadBytesRange(filepath, indexDataNumOffset, 4));
            byte[] indexData = { (byte)dataNum };
            indexData = indexData.Combine(ReadBytesRange(filepath, 0, dataNum * 0x100));   //读取保存配置数据
            string indexDataPath = Path.Combine(Path.GetDirectoryName(filepath), Path.GetFileNameWithoutExtension(filepath), Path.GetFileNameWithoutExtension(filepath) + ".cfg");
            for (int i = 0; i < dataNum; i++) {
                HsData tempData = new HsData();
                tempData.NameData = ReadBytesRange(filepath, 1+i*0x100, 0x100-1);
                tempData.dataPath = Encoding.ASCII.GetString(tempData.NameData).Replace("\0", "");
                tempData.DataLength = (int)ReadInteger(filepath, lengthIndexOffset + i * 4, 4);
                tempData.DataOffset = (int)ReadInteger(filepath, offsetIndexOffset + i * 4, 4);
                tempData.data = ReadBytesRange(filepath,dataOffset+tempData.DataOffset,tempData.DataLength);
                WriteBinaryFile(Path.Combine(Path.GetDirectoryName(filepath),Path.GetFileNameWithoutExtension(filepath), tempData.dataPath), tempData.data);
                Console.WriteLine("success");
            }
            WriteBinaryFile(indexDataPath, indexData);
            MessageBox.Show("宛成");
        }

        public static void PackDoProgress(string filepath)
        {
            dataNum = ReadBytesRange(filepath, 0, 1)[0];     //读取文件数量
            byte[] indexData = ReadBytesRange(filepath, 1, dataNum * 0x100);   //读取保存配置数据
            List<HsData> combineData = new List<HsData>();
            for (int i = 0; i < dataNum; i++)
            {
                HsData tempData = new HsData();
                tempData.NameData = ReadBytesRange(filepath, 2 + i * 0x100, 0x100 - 1);
                tempData.dataPath = Encoding.ASCII.GetString(tempData.NameData).Replace("\0", "");
                tempData.data = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(filepath), tempData.dataPath));
                tempData.DataLength = tempData.data.Length;
                combineData.Add(tempData);
            }
            string dataPath = Path.Combine(Path.GetDirectoryName(filepath), Path.GetFileNameWithoutExtension(filepath) + ".pak");
            WriteBinaryFile(dataPath, indexData);
            byte[] lengthIndexOffsetData = BitConverter.GetBytes(dataNum);
            byte[] offsetIndexOffsetData = Array.Empty<byte>();
            int offsetPtr=0;
            for (int i = 0; i < dataNum; i++)
            {
                lengthIndexOffsetData = lengthIndexOffsetData.Combine(BitConverter.GetBytes(combineData[i].DataLength));
                offsetIndexOffsetData = offsetIndexOffsetData.Combine(BitConverter.GetBytes(offsetPtr));
                WriteBytes(dataPath, dataOffset + offsetPtr, combineData[i].data);
                offsetPtr += combineData[i].DataLength;
            }
            WriteBytes(dataPath, indexDataNumOffset, lengthIndexOffsetData);
            WriteBytes(dataPath, offsetIndexOffset, offsetIndexOffsetData);
            MessageBox.Show("宛成");
        }
        public static void WriteBinaryFile(string tempFilePath, byte[] data)
        {
            string directoryPath = Path.GetDirectoryName(tempFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllBytes(tempFilePath, data);
        }
        /// <summary>
        /// 读取文件的全部内容作为字节数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件的字节数组</returns>
        public static byte[] ReadAllBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 读取文件的指定范围的字节
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="length">要读取的长度</param>
        /// <returns>指定范围的字节数组</returns>
        public static byte[] ReadBytesRange(string filePath, long offset, int length)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (offset < 0 || offset >= fs.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset), "偏移量超出文件范围");
                }

                if (length <= 0 || offset + length > fs.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(length), "长度参数无效");
                }

                byte[] buffer = new byte[length];
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buffer, 0, length);
                return buffer;
            }
        }

        /// <summary>
        /// 从文件中读取指定位置和大小的整数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="size">整数大小(1, 2, 4, 8字节)</param>
        /// <param name="isLittleEndian">是否小端序</param>
        /// <returns>读取的整数值</returns>
        public static long ReadInteger(string filePath, long offset, int size, bool isLittleEndian = true)
        {
            byte[] bytes = ReadBytesRange(filePath, offset, size);

            if (!isLittleEndian)
            {
                Array.Reverse(bytes);
            }

            switch (size)
            {
                case 1:
                    return bytes[0];
                case 2:
                    return BitConverter.ToInt16(bytes, 0);
                case 4:
                    return BitConverter.ToInt32(bytes, 0);
                case 8:
                    return BitConverter.ToInt64(bytes, 0);
                default:
                    throw new ArgumentException("不支持的整数大小，必须是1, 2, 4或8", nameof(size));
            }
        }

        /// <summary>
        /// 从文件中读取指定位置和大小的浮点数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="size">浮点数大小(4或8字节)</param>
        /// <param name="isLittleEndian">是否小端序</param>
        /// <returns>读取的浮点数值</returns>
        public static double ReadFloatingPoint(string filePath, long offset, int size, bool isLittleEndian = true)
        {
            byte[] bytes = ReadBytesRange(filePath, offset, size);

            if (!isLittleEndian)
            {
                Array.Reverse(bytes);
            }

            switch (size)
            {
                case 4:
                    return BitConverter.ToSingle(bytes, 0);
                case 8:
                    return BitConverter.ToDouble(bytes, 0);
                default:
                    throw new ArgumentException("不支持的浮点数大小，必须是4或8", nameof(size));
            }
        }

        /// <summary>
        /// 从文件中读取指定位置和长度的字符串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="length">字符串长度</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>读取的字符串</returns>
        public static string ReadString(string filePath, long offset, int length, System.Text.Encoding encoding)
        {
            byte[] bytes = ReadBytesRange(filePath, offset, length);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 从文件中读取以null结尾的字符串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="maxLength">最大读取长度(防止无限读取)</param>
        /// <returns>读取的字符串</returns>
        public static string ReadNullTerminatedString(string filePath, long offset, System.Text.Encoding encoding, int maxLength = 1024)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"文件未找到: {filePath}");
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (offset < 0 || offset >= fs.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset), "偏移量超出文件范围");
                }

                fs.Seek(offset, SeekOrigin.Begin);

                var byteList = new System.Collections.Generic.List<byte>();
                int bytesRead;
                int totalRead = 0;
                byte[] singleByte = new byte[1];

                while (totalRead < maxLength)
                {
                    bytesRead = fs.Read(singleByte, 0, 1);
                    if (bytesRead == 0 || singleByte[0] == 0) // 文件结束或遇到null终止符
                    {
                        break;
                    }
                    byteList.Add(singleByte[0]);
                    totalRead++;
                }

                return encoding.GetString(byteList.ToArray());
            }
        }
        /// <summary>
        /// 写入模式
        /// </summary>
        public enum WriteMode
        {
            Overwrite,  // 覆盖模式 - 直接覆盖原有数据
            Insert      // 插入模式 - 插入数据，原有数据后移
        }

        /// <summary>
        /// 在文件中指定位置写入字节数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">写入位置偏移量</param>
        /// <param name="data">要写入的字节数组</param>
        /// <param name="mode">写入模式(覆盖/插入)</param>
        public static void WriteBytes(string filePath, long offset, byte[] data, WriteMode mode = WriteMode.Overwrite)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException(nameof(data), "写入的数据不能为空");
            }

            if (mode == WriteMode.Overwrite)
            {
                // 覆盖模式
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    if (offset > fs.Length)
                    {
                        // 如果偏移量超过文件长度，扩展文件并用0填充间隙
                        fs.SetLength(offset + data.Length);
                    }

                    fs.Seek(offset, SeekOrigin.Begin);
                    fs.Write(data, 0, data.Length);
                }
            }
            else
            {
                // 插入模式
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    // 读取插入点之后的数据
                    byte[] remainingData = null;
                    if (offset < fs.Length)
                    {
                        long remainingLength = fs.Length - offset;
                        remainingData = new byte[remainingLength];
                        fs.Seek(offset, SeekOrigin.Begin);
                        fs.Read(remainingData, 0, (int)remainingLength);
                    }

                    // 设置新文件长度
                    fs.SetLength(offset + data.Length + (remainingData?.Length ?? 0));

                    // 写入新数据
                    fs.Seek(offset, SeekOrigin.Begin);
                    fs.Write(data, 0, data.Length);

                    // 写入原有数据
                    if (remainingData != null)
                    {
                        fs.Write(remainingData, 0, remainingData.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 在文件中指定位置写入整数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">写入位置偏移量</param>
        /// <param name="value">要写入的整数值</param>
        /// <param name="size">整数大小(1, 2, 4, 8字节)</param>
        /// <param name="mode">写入模式(覆盖/插入)</param>
        /// <param name="isLittleEndian">是否小端序</param>
        public static void WriteInteger(string filePath, long offset, long value, int size,
            WriteMode mode = WriteMode.Overwrite, bool isLittleEndian = true)
        {
            byte[] bytes;

            switch (size)
            {
                case 1:
                    bytes = new byte[] { (byte)value };
                    break;
                case 2:
                    bytes = BitConverter.GetBytes((short)value);
                    break;
                case 4:
                    bytes = BitConverter.GetBytes((int)value);
                    break;
                case 8:
                    bytes = BitConverter.GetBytes(value);
                    break;
                default:
                    throw new ArgumentException("不支持的整数大小，必须是1, 2, 4或8", nameof(size));
            }

            if (!isLittleEndian)
            {
                Array.Reverse(bytes);
            }

            WriteBytes(filePath, offset, bytes, mode);
        }

        /// <summary>
        /// 在文件中指定位置写入浮点数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="offset">写入位置偏移量</param>
        /// <param name="value">要写入的浮点数值</param>
        /// <param name="size">浮点数大小(4或8字节)</param>
        /// <param name="mode">写入模式(覆盖/插入)</param>
        /// <param name="isLittleEndian">是否小端序</param>
        public static void WriteFloatingPoint(string filePath, long offset, double value, int size,
            WriteMode mode = WriteMode.Overwrite, bool isLittleEndian = true)
        {
            byte[] bytes;

            switch (size)
            {
                case 4:
                    bytes = BitConverter.GetBytes((float)value);
                    break;
                case 8:
                    bytes = BitConverter.GetBytes(value);
                    break;
                default:
                    throw new ArgumentException("不支持的浮点数大小，必须是4或8", nameof(size));
            }

            if (!isLittleEndian)
            {
                Array.Reverse(bytes);
            }

            WriteBytes(filePath, offset, bytes, mode);
        }

    }
    public static class ByteArrayExtensions
    {
        public static byte[] Combine(this byte[] first, params byte[][] arrays)
        {
            int totalLength = first.Length + arrays.Sum(a => a.Length);
            byte[] merged = new byte[totalLength];
            Array.Copy(first, 0, merged, 0, first.Length);
            int offset = first.Length;
            foreach (byte[] array in arrays)
            {
                Array.Copy(array, 0, merged, offset, array.Length);
                offset += array.Length;
            }
            return merged;
        }
    }

}