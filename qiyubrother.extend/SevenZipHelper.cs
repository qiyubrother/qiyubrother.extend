/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华2023-07-05
 * 电子邮箱：13240137763@163.com2023-07-05
 */
/* 7z 压缩工具
 * install-package sevenzip
 * 
 * 
 * 
 */
using System;
using System.IO;

namespace qiyubrother.extend
{
    public class SevenZipHelper
    {
        //static void Main(string[] args)
        //{
        //    var inputFileName = $@"d:\Trace-20200318.log";
        //    var outputFileName = $"{inputFileName}.7z";

        //    CompressionFile(inputFileName, outputFileName);
        //    DecompressionFile(outputFileName, $"{outputFileName}.log");
        //}

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="inputFileName">输入文件名</param>
        /// <param name="outputFileName">输出文件名</param>
        public static void CompressionFile(string inputFileName, string outputFileName)
        {
            using (var si = new FileStream(inputFileName, FileMode.Open))
            {
                using (var so = new FileStream(outputFileName, FileMode.Create))
                {
                    SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
                    // Write the encoder properties
                    coder.WriteCoderProperties(so);
                    // Write the decompressed file size.
                    so.Write(BitConverter.GetBytes(si.Length), 0, 8);
                    // Encode the file.
                    coder.Code(si, so, si.Length, -1, null);
                    so.Flush();
                }
            }
        }
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="inputFileName">输入文件</param>
        /// <param name="outputFileName">输出文件</param>
        public static void DecompressionFile(string inputFileName, string outputFileName)
        {
            using (var si = new FileStream(inputFileName, FileMode.Open))
            {
                using (var so = new FileStream(outputFileName, FileMode.Create))
                {
                    // Read the decoder properties
                    byte[] properties = new byte[5];
                    si.Read(properties, 0, 5);

                    // Read in the decompress file size.
                    byte[] fileLengthBytes = new byte[8];
                    si.Read(fileLengthBytes, 0, 8);
                    long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

                    SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
                    // Decompress the file.
                    coder.SetDecoderProperties(properties);
                    coder.Code(si, so, si.Length, fileLength, null);
                }
            }
        }
    }
}
