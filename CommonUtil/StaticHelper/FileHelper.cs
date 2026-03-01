using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonUtil
{
    /// <summary>
    /// 文件操作工具类，提供常用的文件和目录操作方法
    /// </summary>
    public static class FileHelper
    {
        #region 文件读取与写入

        /// <summary>
        /// 读取文本文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码，默认为UTF-8</param>
        /// <returns>文件内容</returns>
        public static string ReadText(string filePath, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            return File.ReadAllText(filePath, encoding);
        }

        /// <summary>
        /// 写入文本到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">要写入的内容</param>
        /// <param name="encoding">编码，默认为UTF-8</param>
        public static void WriteText(string filePath, string content, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            File.WriteAllText(filePath, content, encoding);
        }

        /// <summary>
        /// 追加文本到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">要追加的内容</param>
        /// <param name="encoding">编码，默认为UTF-8</param>
        public static void AppendText(string filePath, string content, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            File.AppendAllText(filePath, content, encoding);
        }

        /// <summary>
        /// 读取文件所有行
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码，默认为UTF-8</param>
        /// <returns>文件行列表</returns>
        public static List<string> ReadAllLines(string filePath, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            return File.ReadAllLines(filePath, encoding).ToList();
        }

        /// <summary>
        /// 写入多行到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="lines">要写入的行列表</param>
        /// <param name="encoding">编码，默认为UTF-8</param>
        public static void WriteAllLines(string filePath, List<string> lines, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            File.WriteAllLines(filePath, lines.ToArray(), encoding);
        }

        #endregion

        #region 文件复制与移动

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destFilePath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
        public static void CopyFile(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            File.Copy(sourceFilePath, destFilePath, overwrite);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destFilePath">目标文件路径</param>
        public static void MoveFile(string sourceFilePath, string destFilePath)
        {
            File.Move(sourceFilePath, destFilePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        #endregion

        #region 文件信息

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>如果文件存在则返回true，否则返回false</returns>
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 获取文件大小（字节）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public static long GetFileSize(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在", filePath);
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        /// <summary>
        /// 获取文件创建时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>创建时间</returns>
        public static DateTime GetCreationTime(string filePath)
        {
            return File.GetCreationTime(filePath);
        }

        /// <summary>
        /// 获取文件修改时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>修改时间</returns>
        public static DateTime GetLastWriteTime(string filePath)
        {
            return File.GetLastWriteTime(filePath);
        }

        /// <summary>
        /// 获取文件扩展名（不含点）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件扩展名</returns>
        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath).TrimStart('.').ToLower();
        }

        /// <summary>
        /// 获取文件名（不含扩展名）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件名（不含扩展名）</returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// 获取文件名（含扩展名）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件名（含扩展名）</returns>
        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        #endregion

        #region 目录操作

        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>如果目录存在则返回true，否则返回false</returns>
        public static bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归删除子目录和文件，默认为true</param>
        public static void DeleteDirectory(string directoryPath, bool recursive = true)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, recursive);
            }
        }

        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式，默认为"*.*"</param>
        /// <param name="searchOption">搜索选项，默认为TopDirectoryOnly</param>
        /// <returns>文件路径列表</returns>
        public static List<string> GetFiles(string directoryPath, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"目录不存在: {directoryPath}");
            return Directory.GetFiles(directoryPath, searchPattern, searchOption).ToList();
        }

        /// <summary>
        /// 获取目录下的所有子目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="searchPattern">搜索模式，默认为"*"</param>
        /// <param name="searchOption">搜索选项，默认为TopDirectoryOnly</param>
        /// <returns>目录路径列表</returns>
        public static List<string> GetDirectories(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"目录不存在: {directoryPath}");
            return Directory.GetDirectories(directoryPath, searchPattern, searchOption).ToList();
        }

        #endregion

        #region 路径操作

        /// <summary>
        /// 合并路径
        /// </summary>
        /// <param name="paths">要合并的路径</param>
        /// <returns>合并后的路径</returns>
        public static string CombinePaths(params string[] paths)
        {
            return Path.Combine(paths);
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetAbsolutePath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }

        /// <summary>
        /// 获取当前应用程序目录
        /// </summary>
        /// <returns>当前应用程序目录</returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns>临时目录路径</returns>
        public static string GetTempDirectory()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// 获取临时文件路径
        /// </summary>
        /// <param name="extension">文件扩展名，默认为".tmp"</param>
        /// <returns>临时文件路径</returns>
        public static string GetTempFilePath(string extension = ".tmp")
        {
            string tempFileName = Path.GetTempFileName();
            string newTempFileName = tempFileName + extension;
            File.Move(tempFileName, newTempFileName);
            return newTempFileName;
        }

        #endregion
    }
}